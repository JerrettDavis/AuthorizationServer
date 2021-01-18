# Sample - With Embedded Identity Server

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
3. The In-Memory `AuthorizationServer` is added to the `ConfigureServices` call 
   in `startup.cs` via the following line: 
   ```csharp
   services.AddInMemoryAuthorizationServer(Configuration);
   ```
4. The `SubjectClaimInjectorMiddleware` and `AuthorizationServer` claim middleware are 
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
5. A secure `Admin Area` was created by adding an `AdminController` with an associated `Index` page. 
   The controller is decorated with an `[Authorizate("AdminArea")]` attribute, denying
   access to any users not belonging to a role with the `AdminArea` permission.
   
All of the roles, permissions, and associated users are defined in `appsettings.json`.
   
# Demo Account

Use the following credentials to access the demo admin account

```
Username: demo@authorizationserver.io
Password: P@ssw0rd
```