using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GGWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdatatodifficultiesandmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5c0fa415-0c68-43f2-8ad6-32388a9ca610"), "Hard" },
                    { new Guid("9ae1daa7-4787-408e-9f80-eba016fced94"), "Medium" },
                    { new Guid("f11e1ef8-bf11-4b54-b083-66d9c7ed096a"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2539941c-7c1e-4110-93e3-486dac8394b7"), "UV", "Udyog Vihar", null },
                    { new Guid("38f20107-91b9-42ec-a49a-7d46b39f693c"), "CH", "Cyber Hub", "https://b.zmtcdn.com/data/pictures/2/18289242/2fd5c943d0a52b9ebcbb50c674b8ca8c.jpg?w=1600&h=-1&s=1" },
                    { new Guid("4e87bdce-b9a7-4506-aaf2-15dc1e892cfd"), "CC", "Cyber City", "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/0f/b8/e2/a4/photo5jpg.jpg?w=1600&h=-1&s=1" },
                    { new Guid("52f96bc5-87e0-44c2-bc09-fb704b97292b"), "PV", "Palam vihar", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5c0fa415-0c68-43f2-8ad6-32388a9ca610"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("9ae1daa7-4787-408e-9f80-eba016fced94"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f11e1ef8-bf11-4b54-b083-66d9c7ed096a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2539941c-7c1e-4110-93e3-486dac8394b7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("38f20107-91b9-42ec-a49a-7d46b39f693c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4e87bdce-b9a7-4506-aaf2-15dc1e892cfd"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("52f96bc5-87e0-44c2-bc09-fb704b97292b"));
        }
    }
}
