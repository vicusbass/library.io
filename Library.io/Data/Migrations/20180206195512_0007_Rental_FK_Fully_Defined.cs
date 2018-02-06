using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Library.io.Data.Migrations
{
    public partial class _0007_Rental_FK_Fully_Defined : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Book_BookIdID",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_AspNetUsers_UserIdId",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Rental_BookIdID",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "BookIdID",
                table: "Rental");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "Rental",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_UserIdId",
                table: "Rental",
                newName: "IX_Rental_ApplicationUserId");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Rental",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rental_BookId",
                table: "Rental",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_AspNetUsers_ApplicationUserId",
                table: "Rental",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Book_BookId",
                table: "Rental",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_AspNetUsers_ApplicationUserId",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Book_BookId",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Rental_BookId",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Rental");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Rental",
                newName: "UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_ApplicationUserId",
                table: "Rental",
                newName: "IX_Rental_UserIdId");

            migrationBuilder.AddColumn<int>(
                name: "BookIdID",
                table: "Rental",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rental_BookIdID",
                table: "Rental",
                column: "BookIdID");

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
    }
}
