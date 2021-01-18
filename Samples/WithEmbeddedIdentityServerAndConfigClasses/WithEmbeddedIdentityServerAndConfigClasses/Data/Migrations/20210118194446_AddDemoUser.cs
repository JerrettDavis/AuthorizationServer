using Microsoft.EntityFrameworkCore.Migrations;

namespace WithEmbeddedIdentityServerAndConfigClasses.Data.Migrations
{
    public partial class AddDemoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0c303a4e-5c25-4d10-8396-c74c39c8610a", 0, "d4aa1684-1502-47ea-bf07-832f19ae05bc", "demo@authorizationserver.io", true, true, null, "DEMO@AUTHORIZATIONSERVER.IO", "DEMO@AUTHORIZATIONSERVER.IO", "AQAAAAEAACcQAAAAEJ4+zzSIOTx/Od09oUVXBzIsbKg1KqMJwTRWezU3AugKDZSJ64CPo3i5aK+5VnCVxA==", null, false, "S2SWX2DQRBDLJCUQNC4HF7H7IMZAUEAS", false, "demo@authorizationserver.io" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0c303a4e-5c25-4d10-8396-c74c39c8610a");
        }
    }
}
