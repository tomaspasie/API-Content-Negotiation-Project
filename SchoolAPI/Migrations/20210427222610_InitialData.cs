using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolAPI.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.AssignmentId);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    OrgName = table.Column<string>(maxLength: 60, nullable: false),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    SectionId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.SectionId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    AssignmentId = table.Column<Guid>(nullable: true),
                    SectionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "AssignmentId", "Name" },
                values: new object[,]
                {
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991873"), "Assignment 1" },
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Assignment 2" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "AssignmentId", "Name", "SectionId" },
                values: new object[,]
                {
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"), null, "Advanced Web Applications", null },
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce1"), null, "Web Mining", null }
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "OrganizationId", "City", "Country", "OrgName" },
                values: new object[,]
                {
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Bloomfield", "USA", "xyz org" },
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Lusaka", "ZM", "lmnop org" }
                });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "SectionId", "Name" },
                values: new object[,]
                {
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991872"), "Section 1" },
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce2"), "Section 2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Age", "CourseId", "Name", "OrganizationId" },
                values: new object[,]
                {
                    { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 0, null, "kwilliams", new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), 0, null, "ka393939", new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), 0, null, "kaw3939", new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") },
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479812"), 21, null, "Tomas", new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") },
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479813"), 25, null, "Natalie", new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") },
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479814"), 31, null, "Ethan", new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") },
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479815"), 28, null, "Dan", new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AssignmentId",
                table: "Courses",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SectionId",
                table: "Courses",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CourseId",
                table: "Users",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Sections");
        }
    }
}
