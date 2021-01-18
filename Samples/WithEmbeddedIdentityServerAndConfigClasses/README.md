# Sample - With Embedded Identity Server - Loaded From Static Classes

## Description

This is the bare-bones setup for AuthorizationServer running on top of the Default 
Identity Server provided by ASP.NET Core. Only the following modifications have 
been made to the default template.

1. A new middleware called `SubjectClaimInjectorMiddleware` has been added to the project.
   This middleware transforms the `NameIdentifier` claim into a `sub` claim that can
   be read by AuthorizationServer.
   ```csharp
    public class SubjectClaimInjectorMiddleware
    {
        private readonly RequestDelegate _next;

        public SubjectClaimInjectorMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task Invoke(
            HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var user = context.User;
                if (string.IsNullOrWhiteSpace(user.FindFirstValue("sub")))
                {
                    var claim = new Claim("sub", user.FindFirstValue(ClaimTypes.NameIdentifier));
                    var id = new ClaimsIdentity(
                        "SubInjectorMiddleware",
                        "name",
                        "role");
                    id.AddClaim(claim);
                    
                    context.User.AddIdentity(id);
                }
            }

            await _next(context);
        }
    }
   ```
2. The `AuthorizationServer` is referenced by the project.
3. A static `Config` class is created containing our configuration settings:
   ```csharp
    public static class Config
    {
        public static IEnumerable<Role> GetRoles()
        {
            return new List<Role>
            {
                new()
                {
                    Name = "Admin",
                    PermissionsAllowed = new []{ "AccessAdmin" }
                } 
            };
        }

        public static IEnumerable<User> GetUsers()
        {
            return new List<User>
            {
                new()
                {
                    SubjectId = "0c303a4e-5c25-4d10-8396-c74c39c8610a",
                    Roles = new[] {"Admin"}
                }
            };
        }
    }
   ```
4. The In-Memory `AuthorizationServer` is added to the `ConfigureServices` call 
   in `startup.cs` via the following lines: 
   ```csharp
   services
    .AddAuthorizationServer()
    .AddInMemoryRoles(Config.GetRoles())
    .AddInMemoryRoleStore()
    .AddInMemoryUsers(Config.GetUsers())
    .AddInMemoryUserStore()
    .AddInMemoryUserEvaluator();
   ```
5. The `SubjectClaimInjectorMiddleware` and `AuthorizationServer` claim middleware are 
   added to the `Configure` method in `startup.cs` in between the `UseAuthentication` and
   `UseAuthorization` calls, like the following:
   ```csharp
   app.UseAuthentication();
   
   // Default identity doesn't populate the sub claim. 
   app.UseMiddleware<SubjectClaimInjectorMiddleware>();
   // Add authorization server claims
   app.UseAuthorizationServerClaims();
   
   app.UseAuthorization();
   ```
6. A secure `Admin Area` was created by adding an `AdminController` with an associated `Index` page. 
   The controller is decorated with an `[Authorizate("AdminArea")]` attribute, denying
   access to any users not belonging to a role with the `AdminArea` permission.
   
All of the roles, permissions, and associated users are defined in `appsettings.json`.
   
# Demo Account

Use the following credentials to access the demo admin account

```
Username: demo@authorizationserver.io
Password: P@ssw0rd
```