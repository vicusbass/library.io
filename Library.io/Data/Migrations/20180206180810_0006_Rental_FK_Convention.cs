using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Library.io.Data.Migrations
{
    public partial class _0006_Rental_FK_Convention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Book_BookID",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_AspNetUsers_UserId",
                table: "Rental");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Rental",
                newName: "UserIdId");

            migrationBuilder.RenameColumn(
                name: "BookID",
                table: "Rental",
                newName: "BookIdID");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_UserId",
                table: "Rental",
                newName: "IX_Rental_UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_BookID",
                table: "Rental",
                newName: "IX_Rental_BookIdID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Book_BookIdID",
                table: "Rental",
                column: "BookIdID",
                principalTable: "Book",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_AspNetUsers_UserIdId",
                table: "Rental",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Book_BookIdID",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_AspNetUsers_UserIdId",
                table: "Rental");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "Rental",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "BookIdID",
                table: "Rental",
                newName: "BookID");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_UserIdId",
                table: "Rental",
                newName: "IX_Rental_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_BookIdID",
                table: "Rental",
                newName: "IX_Rental_BookID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Book_BookID",
                table: "Rental",
                column: "BookID",
                principalTable: "Book",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_AspNetUsers_UserId",
                table: "Rental",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
