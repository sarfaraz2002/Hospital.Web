using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class modelupdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_HospitalInfos_HospitalInfoId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_HospitalInfos_HospitalInfoId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HospitalInfoId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_HospitalInfoId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "HospitalInfoId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "HospitalInfoId",
                table: "Contacts");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HospitalId",
                table: "Rooms",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_HospitalId",
                table: "Contacts",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_HospitalInfos_HospitalId",
                table: "Contacts",
                column: "HospitalId",
                principalTable: "HospitalInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_HospitalInfos_HospitalId",
                table: "Rooms",
                column: "HospitalId",
                principalTable: "HospitalInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_HospitalInfos_HospitalId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_HospitalInfos_HospitalId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HospitalId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_HospitalId",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "HospitalInfoId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HospitalInfoId",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HospitalInfoId",
                table: "Rooms",
                column: "HospitalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_HospitalInfoId",
                table: "Contacts",
                column: "HospitalInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_HospitalInfos_HospitalInfoId",
                table: "Contacts",
                column: "HospitalInfoId",
                principalTable: "HospitalInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_HospitalInfos_HospitalInfoId",
                table: "Rooms",
                column: "HospitalInfoId",
                principalTable: "HospitalInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
