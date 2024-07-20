using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_Some_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Users",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Roles",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Roles",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "PublicKey",
                table: "Users",
                type: "varchar(64)",
                unicode: false,
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "PrivateKey",
                table: "Users",
                type: "varchar(64)",
                unicode: false,
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DeathDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    PrivateKey = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    PublicKey = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FamilyRelationships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Relationship = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyRelationships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.0m),
                    Reduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.0m),
                    Tax = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 0.0m),
                    VAT = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 0.0m),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.0m),
                    Discount = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 0.0m),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.0m),
                    VAT = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 0.0m),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: false, defaultValue: 0.0m),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(170)", unicode: false, maxLength: 170, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SendSms = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.000m),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(90)", unicode: false, maxLength: 90, nullable: false),
                    ProvinceId = table.Column<long>(type: "bigint", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WalletTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Transaction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    ProvinceId = table.Column<long>(type: "bigint", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "Created By System"),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_CustomerId",
                table: "BankAccounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CountryId",
                table: "Banks",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId",
                table: "Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CityId",
                table: "CustomerAddresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CountryId",
                table: "CustomerAddresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CustomerId",
                table: "CustomerAddresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_ProvinceId",
                table: "CustomerAddresses",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_CustomerId",
                table: "PhoneNumbers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryId",
                table: "Provinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_CustomerId",
                table: "Wallets",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_WalletId",
                table: "WalletTransactions",
                column: "WalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "CustomerAddresses");

            migrationBuilder.DropTable(
                name: "FamilyRelationships");

            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "InvoicePayments");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "WalletTransactions");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Users",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Roles",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Roles",
                newName: "Created");

            migrationBuilder.AlterColumn<string>(
                name: "PublicKey",
                table: "Users",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldUnicode: false,
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "PrivateKey",
                table: "Users",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldUnicode: false,
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);
        }
    }
}
