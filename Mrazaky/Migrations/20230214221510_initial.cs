using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mrazaky.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "User's first name"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "User's last name"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodCategory",
                columns: table => new
                {
                    FoodCategoryId = table.Column<int>(type: "int", nullable: false, comment: "FoodCategoryId")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Category")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCategory", x => x.FoodCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Freezer",
                columns: table => new
                {
                    FreezerId = table.Column<int>(type: "int", nullable: false, comment: "FreezerId")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreezerName = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "FreezerName"),
                    NumberOfShelves = table.Column<int>(type: "int", nullable: false, comment: "NumberOfShelves"),
                    LastDefrosted = table.Column<DateTime>(type: "date", nullable: false, comment: "LastDefrosted"),
                    DefrostInterval = table.Column<int>(type: "int", nullable: false, comment: "DefrostInterval"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Note"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Freezer", x => x.FreezerId);
                    table.ForeignKey(
                        name: "FK_Freezer_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserFreezer",
                columns: table => new
                {
                    ApplicationUserFreezerID = table.Column<int>(type: "int", nullable: false, comment: "Gets or sets the user freezer identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Gets or sets the user identifier."),
                    FreezerID = table.Column<int>(type: "int", nullable: false, comment: "Gets or sets the freezed identifier."),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Gets or sets the create at."),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: " Gets or sets a value indicating whether this instance is active.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserFreezer", x => x.ApplicationUserFreezerID);
                    table.ForeignKey(
                        name: "FK_UserFreezer_ApplicationUser",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFreezer_Freezer",
                        column: x => x.FreezerID,
                        principalTable: "Freezer",
                        principalColumn: "FreezerId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Holds information about user's freezers");

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false, comment: "FoodId")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Category"),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "FoodName"),
                    FreezerName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "FreezerName"),
                    NumberOfPackages = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "NumberOfPackages"),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Weight"),
                    DatePurchase = table.Column<DateTime>(type: "date", nullable: false, comment: "DatePurchase"),
                    BestBefore = table.Column<DateTime>(type: "date", nullable: false, comment: "BestBefore"),
                    PackageLabel = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "PackageLabel"),
                    FreezerPosition = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "FreezerPosition"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Note"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FreezerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.FoodId);
                    table.ForeignKey(
                        name: "FK_Food_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Food_Freezer_FreezerId",
                        column: x => x.FreezerId,
                        principalTable: "Freezer",
                        principalColumn: "FreezerId");
                });

            migrationBuilder.CreateTable(
                name: "FreezerFood",
                columns: table => new
                {
                    FreezerFoodID = table.Column<int>(type: "int", nullable: false, comment: "FreezerFoodID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodID = table.Column<int>(type: "int", nullable: false, comment: "FoodID"),
                    FreezerID = table.Column<int>(type: "int", nullable: false, comment: "FreezerID"),
                    FreezerName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "FreezerLocation")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreezerFood", x => x.FreezerFoodID);
                    table.ForeignKey(
                        name: "FK_FreezerFood_Food",
                        column: x => x.FoodID,
                        principalTable: "Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreezerFood_Freezer",
                        column: x => x.FreezerID,
                        principalTable: "Freezer",
                        principalColumn: "FreezerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserFreezer_ApplicationUserID",
                table: "ApplicationUserFreezer",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserFreezer_FreezerID",
                table: "ApplicationUserFreezer",
                column: "FreezerID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Food_ApplicationUserId",
                table: "Food",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_FreezerId",
                table: "Food",
                column: "FreezerId");

            migrationBuilder.CreateIndex(
                name: "IX_Freezer_ApplicationUserId",
                table: "Freezer",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Freezer_FreezerName",
                table: "Freezer",
                column: "FreezerName",
                unique: true,
                filter: "[FreezerName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FreezerFood_FoodID",
                table: "FreezerFood",
                column: "FoodID");

            migrationBuilder.CreateIndex(
                name: "IX_FreezerFood_FreezerID",
                table: "FreezerFood",
                column: "FreezerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserFreezer");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FoodCategory");

            migrationBuilder.DropTable(
                name: "FreezerFood");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "Freezer");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
