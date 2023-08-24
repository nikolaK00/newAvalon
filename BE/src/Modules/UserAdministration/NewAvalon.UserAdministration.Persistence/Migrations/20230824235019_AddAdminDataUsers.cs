using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewAvalon.UserAdministration.Persistence.Migrations
{
    public partial class AddAdminDataUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedOnUtc", "DateOfBirth", "Email", "FirstName", "IdentityProviderId", "LastName", "ModifiedOnUtc", "Password", "UserName" },
                values: new object[] { new Guid("ba3704c6-82a9-49e9-9a16-73b7358249b4"), "Admin address", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", "Admin", null, "Admin", null, "��������l�V[���!�t���8�(", "Admin" });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[] { 2, new Guid("ba3704c6-82a9-49e9-9a16-73b7358249b4") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 2, new Guid("ba3704c6-82a9-49e9-9a16-73b7358249b4") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ba3704c6-82a9-49e9-9a16-73b7358249b4"));
        }
    }
}
