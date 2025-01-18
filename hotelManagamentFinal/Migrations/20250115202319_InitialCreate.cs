//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace hotelManagamentFinal.Migrations
//{
//    /// <inheritdoc />
//    public partial class InitialCreate : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "Action",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
//                    pershkrim = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Action__3213E83FDBDBB588", x => x.id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Akomodim",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    emer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
//                    adults = table.Column<int>(type: "int", nullable: false),
//                    femije = table.Column<int>(type: "int", nullable: true),
//                    cmim = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
//                    krevat_extra = table.Column<bool>(type: "bit", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Akomodim__3213E83F0F7E9F72", x => x.id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Extra_Service",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    emer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
//                    pershkrim = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Extra_Se__3213E83F74321873", x => x.id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Role",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    emer_roli = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Role__3213E83FCD91C39A", x => x.id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Room_Rate",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    emer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
//                    cmim_baze = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Room_Rat__3213E83FA8C6F001", x => x.id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Tip_dhome",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    emer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
//                    cmim = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
//                    siperfaqe = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
//                    pershkrim = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
//                    kapacitet = table.Column<int>(type: "int", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Tip_dhom__3213E83F8FAA4439", x => x.id);
//                });

//            migrationBuilder.CreateTable(
//                name: "User",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    emer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
//                    mbiemer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
//                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
//                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
//                    role = table.Column<int>(type: "int", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__User__3213E83FD058E8B0", x => x.id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Privilegj",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    action = table.Column<int>(type: "int", nullable: false),
//                    rol = table.Column<int>(type: "int", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Privileg__3213E83FA168C80C", x => x.id);
//                    table.ForeignKey(
//                        name: "FK__Privilegj__actio__3E52440B",
//                        column: x => x.action,
//                        principalTable: "Action",
//                        principalColumn: "id");
//                    table.ForeignKey(
//                        name: "FK__Privilegj__rol__3F466844",
//                        column: x => x.rol,
//                        principalTable: "Role",
//                        principalColumn: "id");
//                });

//            migrationBuilder.CreateTable(
//                name: "Room_Rate_Ranges",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    room_rate_id = table.Column<int>(type: "int", nullable: false),
//                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
//                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
//                    weekend_pricing = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
//                    holiday_pricing = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
//                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Room_Rat__3213E83FF0D65227", x => x.id);
//                    table.ForeignKey(
//                        name: "FK__Room_Rate__room___4E88ABD4",
//                        column: x => x.room_rate_id,
//                        principalTable: "Room_Rate",
//                        principalColumn: "id");
//                });

//            migrationBuilder.CreateTable(
//                name: "Dhome",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    tip_dhome = table.Column<int>(type: "int", nullable: false),
//                    kat = table.Column<int>(type: "int", nullable: true),
//                    numer_dhome = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Dhome__3213E83FADA7053C", x => x.id);
//                    table.ForeignKey(
//                        name: "FK__Dhome__tip_dhome__47DBAE45",
//                        column: x => x.tip_dhome,
//                        principalTable: "Tip_dhome",
//                        principalColumn: "id");
//                });

//            migrationBuilder.CreateTable(
//                name: "Review",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    user = table.Column<int>(type: "int", nullable: false),
//                    rating = table.Column<int>(type: "int", nullable: true),
//                    pershkrim = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
//                    date = table.Column<DateOnly>(type: "date", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Review__3213E83F340D5072", x => x.id);
//                    table.ForeignKey(
//                        name: "FK__Review__user__4222D4EF",
//                        column: x => x.user,
//                        principalTable: "User",
//                        principalColumn: "id");
//                });

//            migrationBuilder.CreateTable(
//                name: "Rezervim",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    user = table.Column<int>(type: "int", nullable: false),
//                    dhome = table.Column<int>(type: "int", nullable: false),
//                    room_rate = table.Column<int>(type: "int", nullable: true),
//                    akomodim = table.Column<int>(type: "int", nullable: true),
//                    check_in = table.Column<DateOnly>(type: "date", nullable: false),
//                    check_out = table.Column<DateOnly>(type: "date", nullable: false),
//                    cmim = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Rezervim__3213E83F284C1B56", x => x.id);
//                    table.ForeignKey(
//                        name: "FK__Rezervim__akomod__5441852A",
//                        column: x => x.akomodim,
//                        principalTable: "Akomodim",
//                        principalColumn: "id");
//                    table.ForeignKey(
//                        name: "FK__Rezervim__dhome__52593CB8",
//                        column: x => x.dhome,
//                        principalTable: "Dhome",
//                        principalColumn: "id");
//                    table.ForeignKey(
//                        name: "FK__Rezervim__room_r__534D60F1",
//                        column: x => x.room_rate,
//                        principalTable: "Room_Rate",
//                        principalColumn: "id");
//                    table.ForeignKey(
//                        name: "FK__Rezervim__user__5165187F",
//                        column: x => x.user,
//                        principalTable: "User",
//                        principalColumn: "id");
//                });

//            migrationBuilder.CreateTable(
//                name: "Fature",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    rezervim = table.Column<int>(type: "int", nullable: false),
//                    room_charge = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
//                    service_charge = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
//                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
//                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
//                    date_fature = table.Column<DateOnly>(type: "date", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Fature__3213E83F24F7FE2A", x => x.id);
//                    table.ForeignKey(
//                        name: "FK__Fature__rezervim__5BE2A6F2",
//                        column: x => x.rezervim,
//                        principalTable: "Rezervim",
//                        principalColumn: "id");
//                });

//            migrationBuilder.CreateTable(
//                name: "Rezervim_Service",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    rezervim = table.Column<int>(type: "int", nullable: false),
//                    sherbim = table.Column<int>(type: "int", nullable: false),
//                    sasi = table.Column<int>(type: "int", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Rezervim__3213E83FD24DA3E8", x => x.id);
//                    table.ForeignKey(
//                        name: "FK__Rezervim___rezer__571DF1D5",
//                        column: x => x.rezervim,
//                        principalTable: "Rezervim",
//                        principalColumn: "id");
//                });

//            migrationBuilder.CreateTable(
//                name: "Pagese",
//                columns: table => new
//                {
//                    id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    fature = table.Column<int>(type: "int", nullable: false),
//                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
//                    menyre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
//                    date_pagese = table.Column<DateOnly>(type: "date", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Pagese__3213E83FDF1DDDFC", x => x.id);
//                    table.ForeignKey(
//                        name: "FK__Pagese__fature__5EBF139D",
//                        column: x => x.fature,
//                        principalTable: "Fature",
//                        principalColumn: "id");
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_Dhome_tip_dhome",
//                table: "Dhome",
//                column: "tip_dhome");

//            migrationBuilder.CreateIndex(
//                name: "IX_Fature_rezervim",
//                table: "Fature",
//                column: "rezervim");

//            migrationBuilder.CreateIndex(
//                name: "IX_Pagese_fature",
//                table: "Pagese",
//                column: "fature");

//            migrationBuilder.CreateIndex(
//                name: "IX_Privilegj_action",
//                table: "Privilegj",
//                column: "action");

//            migrationBuilder.CreateIndex(
//                name: "IX_Privilegj_rol",
//                table: "Privilegj",
//                column: "rol");

//            migrationBuilder.CreateIndex(
//                name: "IX_Review_user",
//                table: "Review",
//                column: "user");

//            migrationBuilder.CreateIndex(
//                name: "IX_Rezervim_akomodim",
//                table: "Rezervim",
//                column: "akomodim");

//            migrationBuilder.CreateIndex(
//                name: "IX_Rezervim_dhome",
//                table: "Rezervim",
//                column: "dhome");

//            migrationBuilder.CreateIndex(
//                name: "IX_Rezervim_room_rate",
//                table: "Rezervim",
//                column: "room_rate");

//            migrationBuilder.CreateIndex(
//                name: "IX_Rezervim_user",
//                table: "Rezervim",
//                column: "user");

//            migrationBuilder.CreateIndex(
//                name: "IX_Rezervim_Service_rezervim",
//                table: "Rezervim_Service",
//                column: "rezervim");

//            migrationBuilder.CreateIndex(
//                name: "IX_Room_Rate_Ranges_room_rate_id",
//                table: "Room_Rate_Ranges",
//                column: "room_rate_id");

//            migrationBuilder.CreateIndex(
//                name: "UQ__User__AB6E61640BA26EED",
//                table: "User",
//                column: "email",
//                unique: true);
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "Extra_Service");

//            migrationBuilder.DropTable(
//                name: "Pagese");

//            migrationBuilder.DropTable(
//                name: "Privilegj");

//            migrationBuilder.DropTable(
//                name: "Review");

//            migrationBuilder.DropTable(
//                name: "Rezervim_Service");

//            migrationBuilder.DropTable(
//                name: "Room_Rate_Ranges");

//            migrationBuilder.DropTable(
//                name: "Fature");

//            migrationBuilder.DropTable(
//                name: "Action");

//            migrationBuilder.DropTable(
//                name: "Role");

//            migrationBuilder.DropTable(
//                name: "Rezervim");

//            migrationBuilder.DropTable(
//                name: "Akomodim");

//            migrationBuilder.DropTable(
//                name: "Dhome");

//            migrationBuilder.DropTable(
//                name: "Room_Rate");

//            migrationBuilder.DropTable(
//                name: "User");

//            migrationBuilder.DropTable(
//                name: "Tip_dhome");
//        }
//    }
//}
