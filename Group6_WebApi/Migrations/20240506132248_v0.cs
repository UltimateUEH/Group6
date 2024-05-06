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
                name: "Tenant",
                columns: table => new
                {
                    tenant_id = table.Column<int>(type: "int", nullable: false),
                    tenant_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tenant__D6F29F3E743A1727", x => x.tenant_id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "int", nullable: false),
                    tenant_id = table.Column<int>(type: "int", nullable: true),
                    company_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    company_email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    company_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Company__3E2672358DB7E6E0", x => x.company_id);
                    table.ForeignKey(
                        name: "FK__Company__tenant___4BAC3F29",
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
                    product_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    product_description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__47027DF58055AEFD", x => x.product_id);
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
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__46A222CD3F76C11B", x => x.account_id);
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
                name: "Customer",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    customer_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    invoice_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    tenant_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__CD65CB85FBCB887D", x => x.customer_id);
                    table.ForeignKey(
                        name: "FK__Customer__compan__5535A963",
                        column: x => x.company_id,
                        principalTable: "Company",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK__Customer__tenant__5629CD9C",
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
                    product_id = table.Column<int>(type: "int", nullable: true),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    invoice_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    product_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    customer_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    discount = table.Column<int>(type: "int", nullable: true),
                    tax_rate = table.Column<int>(type: "int", nullable: true),
                    detail_id = table.Column<int>(type: "int", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invoice__F58DFD490AFB7D4B", x => x.invoice_id);
                    table.ForeignKey(
                        name: "FK__Invoice__custome__5AEE82B9",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "FK__Invoice__product__59FA5E80",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK__Invoice__tenant___59063A47",
                        column: x => x.tenant_id,
                        principalTable: "Tenant",
                        principalColumn: "tenant_id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetail",
                columns: table => new
                {
                    detail_id = table.Column<int>(type: "int", nullable: false),
                    invoice_id = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__InvoiceD__38E9A224305D2688", x => x.detail_id);
                    table.ForeignKey(
                        name: "FK__InvoiceDe__invoi__60A75C0F",
                        column: x => x.invoice_id,
                        principalTable: "Invoice",
                        principalColumn: "invoice_id");
                    table.ForeignKey(
                        name: "FK__InvoiceDe__produ__619B8048",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "product_id");
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
                name: "IX_Customer_company_id",
                table: "Customer",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_tenant_id",
                table: "Customer",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_customer_id",
                table: "Invoice",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_product_id",
                table: "Invoice",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_tenant_id",
                table: "Invoice",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_invoice_id",
                table: "InvoiceDetail",
                column: "invoice_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_product_id",
                table: "InvoiceDetail",
                column: "product_id");

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
                name: "InvoiceDetail");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
