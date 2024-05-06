using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Group6_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class v0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    tax_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    rate = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tax__129B8670C0BA286F", x => x.tax_id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    tenant_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    contact_info = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tenant__D6F29F3E1CBCA3CD", x => x.tenant_id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "int", nullable: false),
                    tenant_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    contact_info = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Company__3E26723594932133", x => x.company_id);
                    table.ForeignKey(
                        name: "FK__Company__tenant___4BAC3F29",
                        column: x => x.tenant_id,
                        principalTable: "Tenant",
                        principalColumn: "tenant_id");
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    invoice_id = table.Column<int>(type: "int", nullable: false),
                    tenant_id = table.Column<int>(type: "int", nullable: true),
                    invoice_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invoice__F58DFD49129FC0A3", x => x.invoice_id);
                    table.ForeignKey(
                        name: "FK__Invoice__tenant___5535A963",
                        column: x => x.tenant_id,
                        principalTable: "Tenant",
                        principalColumn: "tenant_id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false),
                    tenant_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__47027DF5F3E7F9EC", x => x.product_id);
                    table.ForeignKey(
                        name: "FK__Product__tenant___52593CB8",
                        column: x => x.tenant_id,
                        principalTable: "Tenant",
                        principalColumn: "tenant_id");
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "int", nullable: false),
                    tenant_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__46A222CD442DB334", x => x.account_id);
                    table.ForeignKey(
                        name: "FK__Account__company__4F7CD00D",
                        column: x => x.company_id,
                        principalTable: "Company",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK__Account__tenant___4E88ABD4",
                        column: x => x.tenant_id,
                        principalTable: "Tenant",
                        principalColumn: "tenant_id");
                });

            migrationBuilder.CreateTable(
                name: "Detail_Invoice",
                columns: table => new
                {
                    detail_id = table.Column<int>(type: "int", nullable: false),
                    invoice_id = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    tax_id = table.Column<int>(type: "int", nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detail_I__38E9A22406349550", x => x.detail_id);
                    table.ForeignKey(
                        name: "FK__Detail_In__invoi__59FA5E80",
                        column: x => x.invoice_id,
                        principalTable: "Invoice",
                        principalColumn: "invoice_id");
                    table.ForeignKey(
                        name: "FK__Detail_In__produ__5AEE82B9",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK__Detail_In__tax_i__5BE2A6F2",
                        column: x => x.tax_id,
                        principalTable: "Tax",
                        principalColumn: "tax_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_company_id",
                table: "Account",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Account_tenant_id",
                table: "Account",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Company_tenant_id",
                table: "Company",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_Invoice_invoice_id",
                table: "Detail_Invoice",
                column: "invoice_id");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_Invoice_product_id",
                table: "Detail_Invoice",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_Invoice_tax_id",
                table: "Detail_Invoice",
                column: "tax_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_tenant_id",
                table: "Invoice",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_tenant_id",
                table: "Product",
                column: "tenant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Detail_Invoice");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
