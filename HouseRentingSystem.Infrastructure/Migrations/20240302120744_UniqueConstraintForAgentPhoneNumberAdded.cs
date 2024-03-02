using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class UniqueConstraintForAgentPhoneNumberAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc1317db-32a3-45c3-ac22-a60eccf0f3d3", "AQAAAAEAACcQAAAAELVmNrigapSpiIcyxIXhk4HEzMxGKWxPWwVvqa4IkhEB3dNAR5Pq4Hgj90/z5Ki2yA==", "7a699bd4-ed6f-4e07-a6fb-513da7809ebc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7358c13-c9a2-4a98-8793-8671126bacda", "AQAAAAEAACcQAAAAEDjwkogV4WwivPcs/2NXihW3wYpRzqysi2hO1XF9d+NoQQv5Glbi2RY58FsHLJltKA==", "5fdde850-ba93-4058-99b7-745a234bde2f" });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e7a67dd-fe8e-4a95-b421-fab3e950424c", "AQAAAAEAACcQAAAAEMHkI44U7p+S1DolX/R+i5SFlq7I7yD+HEasOl2Fx6DX4hVIClLaRVQFZxdbYTZB5g==", "fa552b54-bfe6-480d-abdf-9f62bbdaf506" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6098385b-f500-474f-a635-9bb99cf80241", "AQAAAAEAACcQAAAAEAGt/E4QXABbO/bgPMus3LyIZe/tS+dpO4ZOt/7Wir/wD0dpgfQzpdSi0OXF3h4FtQ==", "9aafcbb4-fc1f-4c1e-95a3-534363300983" });
        }
    }
}
