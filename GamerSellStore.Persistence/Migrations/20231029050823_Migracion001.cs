using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GamerSellStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Migracion001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "GamerShop");

            migrationBuilder.CreateTable(
                name: "Clasificacion",
                schema: "GamerShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clasificacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                schema: "GamerShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Correo = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consola",
                schema: "GamerShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consola", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                schema: "GamerShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                schema: "GamerShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                });            

            migrationBuilder.CreateTable(
                name: "Rol",
                schema: "GamerShop",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "GamerShop",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DocumentType = table.Column<short>(type: "smallint", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
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
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Titulo",
                schema: "GamerShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    GeneroId = table.Column<int>(type: "int", nullable: false),
                    ConsolaId = table.Column<int>(type: "int", nullable: false),
                    ClasificacionId = table.Column<int>(type: "int", nullable: false),
                    Costo = table.Column<double>(type: "float", nullable: false),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Agotado = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    AnioPublicacion = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Titulo_Clasificacion_ClasificacionId",
                        column: x => x.ClasificacionId,
                        principalSchema: "GamerShop",
                        principalTable: "Clasificacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Titulo_Consola_ConsolaId",
                        column: x => x.ConsolaId,
                        principalSchema: "GamerShop",
                        principalTable: "Consola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Titulo_Genero_GeneroId",
                        column: x => x.GeneroId,
                        principalSchema: "GamerShop",
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Titulo_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalSchema: "GamerShop",
                        principalTable: "Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "GamerShop",
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
                        name: "FK_AspNetRoleClaims_Rol_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "GamerShop",
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "GamerShop",
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
                        name: "FK_AspNetUserClaims_Usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "GamerShop",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "GamerShop",
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
                        name: "FK_AspNetUserLogins_Usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "GamerShop",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "GamerShop",
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
                        name: "FK_AspNetUserTokens_Usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "GamerShop",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                schema: "GamerShop",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Rol_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "GamerShop",
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "GamerShop",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evaluacion",
                schema: "GamerShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    TituloId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Calificacion = table.Column<int>(type: "int", nullable: false),
                    Resenia = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluacion_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "GamerShop",
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluacion_Titulo_TituloId",
                        column: x => x.TituloId,
                        principalSchema: "GamerShop",
                        principalTable: "Titulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                schema: "GamerShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NroTxn = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FechaTxn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    TituloId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<double>(type: "float", nullable: false),
                    ImporteTotal = table.Column<double>(type: "float", nullable: false),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserva_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "GamerShop",
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Titulo_TituloId",
                        column: x => x.TituloId,
                        principalSchema: "GamerShop",
                        principalTable: "Titulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "GamerShop",
                table: "Clasificacion",
                columns: new[] { "Id", "Estado", "Nombre" },
                values: new object[,]
                {
                    { 1, true, "E" },
                    { 2, true, "E10+" },
                    { 3, true, "T" },
                    { 4, true, "M" },
                    { 5, true, "AO" },
                    { 6, true, "RP" }
                });

            migrationBuilder.InsertData(
                schema: "GamerShop",
                table: "Cliente",
                columns: new[] { "Id", "Correo", "Estado", "Nombre", "Pais", "Sexo", "Usuario" },
                values: new object[,]
                {
                    { 1, "jperez@gmail.com", true, "Juan Perez", "ECU", "M", "jperez" },
                    { 2, "mpalermo@gmail.com", true, "Martin Palermo", "PER", "M", "mpalermo" },
                    { 3, "emartinez@gmail.com", true, "Eduardo Martinez", "PER", "M", "emartinez" },
                    { 4, "lquevedo@gmail.com", true, "Lorena Quevedo", "BOL", "F", "lquevedo" },
                    { 5, "jiriarte@gmail.com", true, "Juan Iriarte", "ARG", "M", "jiriarte" },
                    { 6, "msolano@gmail.com", true, "Manuela Solano", "BRA", "F", "msolano" },
                    { 7, "ipedraza@gmail.com", true, "Iris Pedraza", "PER", "F", "ipedraza" },
                    { 8, "lbernaechea@gmail.com", true, "Luz Bernaechea", "ARG", "F", "lbernaechea" }
                });

            migrationBuilder.InsertData(
                schema: "GamerShop",
                table: "Consola",
                columns: new[] { "Id", "Estado", "Nombre" },
                values: new object[,]
                {
                    { 1, true, "ATARI" },
                    { 2, true, "NES" },
                    { 3, true, "MAME" },
                    { 4, true, "GENESIS" },
                    { 5, true, "SNES" },
                    { 6, true, "SEGA SATURN" },
                    { 7, true, "PS1" },
                    { 8, true, "PS2" },
                    { 9, true, "PS3" },
                    { 10, true, "PS4" },
                    { 11, true, "PS5" },
                    { 12, true, "PSP" },
                    { 13, true, "PSVita" },
                    { 14, true, "Xbox one" },
                    { 15, true, "Xbox 360" },
                    { 16, true, "Xbox Series X" },
                    { 17, true, "PC" },
                    { 18, true, "Arcade" },
                    { 19, true, "GBA" },
                    { 20, true, "GBA Advanced" },
                    { 21, true, "Nintendo Switch" },
                    { 22, true, "WII" },
                    { 23, true, "WIIU" }
                });

            migrationBuilder.InsertData(
                schema: "GamerShop",
                table: "Genero",
                columns: new[] { "Id", "Estado", "Nombre" },
                values: new object[,]
                {
                    { 1, true, "Survival Horror" },
                    { 2, true, "RPG" },
                    { 3, true, "SCI-FI" },
                    { 4, true, "Shooter" },
                    { 5, true, "Aventura" },
                    { 6, true, "Fantasia" },
                    { 7, true, "Deporte" },
                    { 8, true, "Musical" },
                    { 9, true, "Automoviles" },
                    { 10, true, "Medieval" }
                });

            migrationBuilder.InsertData(
                schema: "GamerShop",
                table: "Publisher",
                columns: new[] { "Id", "Estado", "Nombre", "Pais" },
                values: new object[,]
                {
                    { 1, true, "Microsoft Game Studios", "USA" },
                    { 2, true, "RockStar Games", "USA" },
                    { 3, true, "EA TM", "USA" },
                    { 4, true, "SEGA", "USA" },
                    { 5, true, "CAPCOM", "JPN" },
                    { 6, true, "UBISOFT", "USA" },
                    { 7, true, "Insomniac Games", "USA" },
                    { 8, true, "Warner Bros Games", "USA" },
                    { 9, true, "Sony Interactive Games", "JPN" },
                    { 10, true, "THQ", "AUT" },
                    { 11, true, "Blizzard Entertaiment", "USA" },
                    { 12, true, "Telltale Games", "USA" },
                    { 13, true, "SquareEnix", "JPN" },
                    { 14, true, "Nintendo", "JPN" },
                    { 15, true, "Machine Zone", "USA" },
                    { 16, true, "EIDOS Interactive Ltd.", "UK" },
                    { 17, true, "Bethesda Softwork", "CHN" },
                    { 18, true, "Konami", "JPN" },
                    { 19, true, "Santa Monica Studio", "USA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "GamerShop",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "GamerShop",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "GamerShop",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluacion_ClienteId",
                schema: "GamerShop",
                table: "Evaluacion",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluacion_TituloId",
                schema: "GamerShop",
                table: "Evaluacion",
                column: "TituloId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_ClienteId",
                schema: "GamerShop",
                table: "Reserva",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_NroTxn",
                schema: "GamerShop",
                table: "Reserva",
                column: "NroTxn");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_TituloId",
                schema: "GamerShop",
                table: "Reserva",
                column: "TituloId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "GamerShop",
                table: "Rol",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Titulo_ClasificacionId",
                schema: "GamerShop",
                table: "Titulo",
                column: "ClasificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Titulo_ConsolaId",
                schema: "GamerShop",
                table: "Titulo",
                column: "ConsolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Titulo_GeneroId",
                schema: "GamerShop",
                table: "Titulo",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Titulo_Nombre",
                schema: "GamerShop",
                table: "Titulo",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Titulo_PublisherId",
                schema: "GamerShop",
                table: "Titulo",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "GamerShop",
                table: "Usuario",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "GamerShop",
                table: "Usuario",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_RoleId",
                schema: "GamerShop",
                table: "UsuarioRol",
                column: "RoleId");

            migrationBuilder.Sql(@"CREATE OR ALTER   PROCEDURE [dbo].[uspReporteReserva]
            @Desde DATE,
            @Hasta DATE
            AS
            BEGIN
            SELECT T.Nombre AS TITULO, R.TotalVentas, E.PromedioClasificacion
                FROM GamerShop.Titulo T
                LEFT JOIN (
                    SELECT R.TituloId, SUM(R.ImporteTotal) AS TotalVentas
                    FROM GamerShop.Reserva R
                    WHERE @Hasta >= R.FechaTxn AND @Desde <= R.FechaTxn
                    GROUP BY R.TituloId
                ) R ON T.Id = R.TituloId
                LEFT JOIN (
                    SELECT E.TituloId, AVG(E.Calificacion) AS PromedioClasificacion
                    FROM GamerShop.Evaluacion E
                    WHERE @Hasta >= E.Fecha AND @Desde <= E.Fecha
                    GROUP BY E.TituloId
                ) E ON T.Id = E.TituloId;
            --SELECT T.Nombre, SUM(R.ImporteTotal) AS TotalVentas, AVG(E.Calificacion) AS PromedioClasificacion
            --FROM GamerShop.Reserva R
            --INNER JOIN GamerShop.Titulo T
            --ON T.Id = R.TituloId
            --INNER JOIN GamerShop.Evaluacion E
            --ON T.Id = E.TituloId
            --WHERE @Hasta >= R.FechaTxn AND @Desde <= R.FechaTxn
            --GROUP BY T.Nombre
            END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "Evaluacion",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "EvaluacionInfo",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "Reserva",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "ReservaInfo",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "UsuarioRol",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "Cliente",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "Titulo",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "Rol",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "Clasificacion",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "Consola",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "Genero",
                schema: "GamerShop");

            migrationBuilder.DropTable(
                name: "Publisher",
                schema: "GamerShop");

            migrationBuilder.Sql("DROP PROC uspReporteReserva");
        }
    }
}
