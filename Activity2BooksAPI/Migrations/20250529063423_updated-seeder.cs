using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Activity2BooksAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedseeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "Id",
                keyValue: "25ab6d7e-e25d-446f-86af-df82d884e7b7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d8808e-51be-4060-a50a-7c79c4e02586", "AQAAAAIAAYagAAAAELJ7ZPea3kZcbr8uKykT7uqICP3KVwUsn8AErvMGc5nr75Dw7p2IhPLXMl6sjgslOQ==", "372f5d7e-8eec-46ec-9670-32bc12f61de6" });

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "Id",
                keyValue: "9911b550-3b0e-4889-902b-483d56fd7210",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d8808e-51be-4060-a50a-7c79c4e02586", "AQAAAAIAAYagAAAAECRSL36zEJUqgeYOMa2wiN1NIePclV18upLXF4sbSo+lqFFKhd+Wfb/bnwJLqbSUZg==", "372f5d7e-8eec-46ec-9670-32bc12f61de6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "Id",
                keyValue: "25ab6d7e-e25d-446f-86af-df82d884e7b7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "CONCURRENCYSTAMP1", "AQAAAAIAAYagAAAAEHlfkKuuOOcp8fkbhTaWy3l8Z9+Vd7QJpRgdiQ7KjP0i2QRGjbqEoGKzU+5u5L5y5Q==", "SECURITYSTAMP1" });

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "Id",
                keyValue: "9911b550-3b0e-4889-902b-483d56fd7210",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "CONCURRENCYSTAMP2", "AQAAAAIAAYagAAAAEBfnE2kNv7I8pEEh8+sHKz4Rb8qJp1QRa3cW5d6f7g8h9i0j1k2l3m4n5o6p7q8r9s0t==", "SECURITYSTAMP2" });
        }
    }
}
