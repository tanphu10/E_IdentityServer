using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMicroservice.IDP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Create_PermissionDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5cb4df13-b3c3-434c-9ce0-450e84b4fcf5");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d6982370-32ad-4e75-b73a-937bb3a81feb");

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Function = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Command = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    RoleId = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a89ebcb-3f5c-4871-892a-f9c5d79fc5ad", null, "Administrator", "ADMINISTRATOR" },
                    { "596de8bb-2e54-4de2-9628-e174d68dc7b7", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId_Function_Command",
                schema: "Identity",
                table: "Permissions",
                columns: new[] { "RoleId", "Function", "Command" },
                unique: true,
                filter: "[Function] IS NOT NULL AND [Command] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "Identity");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0a89ebcb-3f5c-4871-892a-f9c5d79fc5ad");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "596de8bb-2e54-4de2-9628-e174d68dc7b7");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5cb4df13-b3c3-434c-9ce0-450e84b4fcf5", null, "Administrator", "ADMINISTRATOR" },
                    { "d6982370-32ad-4e75-b73a-937bb3a81feb", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
