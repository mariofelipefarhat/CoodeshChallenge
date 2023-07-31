using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coodesh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TransactionKindAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Kind",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
                UPDATE Transactions
                SET Kind = CASE
                    WHEN Type = 3 THEN 2
                    ELSE 1
                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kind",
                table: "Transactions");
        }
    }
}
