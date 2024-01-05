using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WildOasis.Infrastructure.Persistence.Migrations
{
    public partial class Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                schema: "oasis",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    full_name = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "character varying(75)", unicode: false, maxLength: 75, nullable: false),
                    nationality = table.Column<string>(type: "character varying(35)", unicode: false, maxLength: 35, nullable: false),
                    national_id = table.Column<string>(type: "character varying(75)", unicode: false, maxLength: 75, nullable: false),
                    country_flag = table.Column<string>(type: "character varying(75)", unicode: false, maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer",
                schema: "oasis");
        }
    }
}
