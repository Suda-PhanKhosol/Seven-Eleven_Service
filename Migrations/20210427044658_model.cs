using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SevenEleven.Migrations
{
      public partial class model : Migration
      {
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                  migrationBuilder.DeleteData(
                      schema: "auth",
                      table: "Role",
                      keyColumn: "Id",
                      keyValue: new Guid("531e713a-f058-4797-94d5-f16285f30502"));

                  migrationBuilder.DeleteData(
                      schema: "auth",
                      table: "Role",
                      keyColumn: "Id",
                      keyValue: new Guid("57e0bf9f-4625-4e39-a35a-a29d6bf7c910"));

                  migrationBuilder.DeleteData(
                      schema: "auth",
                      table: "Role",
                      keyColumn: "Id",
                      keyValue: new Guid("62bfb6f0-98b7-43ec-a1ec-0db49dbbad03"));

                  migrationBuilder.DeleteData(
                      schema: "auth",
                      table: "Role",
                      keyColumn: "Id",
                      keyValue: new Guid("f5d39aee-3f36-4436-9aba-ec42628928f1"));

                  migrationBuilder.CreateTable(
                      name: "Order",
                      columns: table => new
                      {
                            Id = table.Column<int>(nullable: false)
                              .Annotation("SqlServer:Identity", "1, 1"),
                            Total = table.Column<float>(nullable: false),
                            Discount = table.Column<float>(nullable: false),
                            Net = table.Column<float>(nullable: false),
                            IsActive = table.Column<bool>(nullable: false),
                            CreatedDate = table.Column<DateTime>(nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_Order", x => x.Id);
                      });

                  migrationBuilder.CreateTable(
                      name: "ProductGroup",
                      columns: table => new
                      {
                            Id = table.Column<int>(nullable: false)
                              .Annotation("SqlServer:Identity", "1, 1"),
                            Name = table.Column<string>(maxLength: 255, nullable: false),
                            IsActive = table.Column<bool>(nullable: false),
                            CreatedDate = table.Column<DateTime>(nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_ProductGroup", x => x.Id);
                      });

                  migrationBuilder.CreateTable(
                      name: "Product",
                      columns: table => new
                      {
                            Id = table.Column<int>(nullable: false)
                              .Annotation("SqlServer:Identity", "1, 1"),
                            Name = table.Column<string>(maxLength: 255, nullable: false),
                            Price = table.Column<float>(nullable: false),
                            IsActive = table.Column<bool>(nullable: false),
                            CreatedDate = table.Column<DateTime>(nullable: false),
                            ProductGroupId = table.Column<int>(nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_Product", x => x.Id);
                            table.ForeignKey(
                          name: "FK_Product_ProductGroup_ProductGroupId",
                          column: x => x.ProductGroupId,
                          principalTable: "ProductGroup",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.Cascade);
                      });

                  migrationBuilder.CreateTable(
                      name: "OrderItem",
                      columns: table => new
                      {
                            Id = table.Column<int>(nullable: false)
                              .Annotation("SqlServer:Identity", "1, 1"),
                            Price = table.Column<float>(nullable: false),
                            Quantity = table.Column<float>(nullable: false),
                            Total = table.Column<float>(nullable: false),
                            OrderId = table.Column<int>(nullable: false),
                            ProductId = table.Column<int>(nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_OrderItem", x => x.Id);
                            table.ForeignKey(
                          name: "FK_OrderItem_Order_OrderId",
                          column: x => x.OrderId,
                          principalTable: "Order",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.Cascade);
                            table.ForeignKey(
                          name: "FK_OrderItem_Product_ProductId",
                          column: x => x.ProductId,
                          principalTable: "Product",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.Cascade);
                      });

                  migrationBuilder.InsertData(
                      schema: "auth",
                      table: "Role",
                      columns: new[] { "Id", "Name" },
                      values: new object[,]
                      {
                    { new Guid("acc599f7-8397-4c27-b9f0-d9578d75a166"), "user" },
                    { new Guid("02686086-af64-4d92-99ea-7b72415c6e72"), "Manager" },
                    { new Guid("cfac6785-9e26-47dd-8e7a-4b3ce043e9f2"), "Admin" },
                    { new Guid("fcba9e82-03e7-4ddf-b215-de176d4a5bac"), "Developer" }
                      });

                  migrationBuilder.CreateIndex(
                      name: "IX_OrderItem_OrderId",
                      table: "OrderItem",
                      column: "OrderId");

                  migrationBuilder.CreateIndex(
                      name: "IX_OrderItem_ProductId",
                      table: "OrderItem",
                      column: "ProductId");

                  migrationBuilder.CreateIndex(
                      name: "IX_Product_ProductGroupId",
                      table: "Product",
                      column: "ProductGroupId");
            }

            protected override void Down(MigrationBuilder migrationBuilder)
            {
                  migrationBuilder.DropTable(
                      name: "OrderItem");

                  migrationBuilder.DropTable(
                      name: "Order");

                  migrationBuilder.DropTable(
                      name: "Product");

                  migrationBuilder.DropTable(
                      name: "ProductGroup");

                  migrationBuilder.DeleteData(
                      schema: "auth",
                      table: "Role",
                      keyColumn: "Id",
                      keyValue: new Guid("02686086-af64-4d92-99ea-7b72415c6e72"));

                  migrationBuilder.DeleteData(
                      schema: "auth",
                      table: "Role",
                      keyColumn: "Id",
                      keyValue: new Guid("acc599f7-8397-4c27-b9f0-d9578d75a166"));

                  migrationBuilder.DeleteData(
                      schema: "auth",
                      table: "Role",
                      keyColumn: "Id",
                      keyValue: new Guid("cfac6785-9e26-47dd-8e7a-4b3ce043e9f2"));

                  migrationBuilder.DeleteData(
                      schema: "auth",
                      table: "Role",
                      keyColumn: "Id",
                      keyValue: new Guid("fcba9e82-03e7-4ddf-b215-de176d4a5bac"));

                  migrationBuilder.InsertData(
                      schema: "auth",
                      table: "Role",
                      columns: new[] { "Id", "Name" },
                      values: new object[,]
                      {
                    { new Guid("57e0bf9f-4625-4e39-a35a-a29d6bf7c910"), "user" },
                    { new Guid("f5d39aee-3f36-4436-9aba-ec42628928f1"), "Manager" },
                    { new Guid("531e713a-f058-4797-94d5-f16285f30502"), "Admin" },
                    { new Guid("62bfb6f0-98b7-43ec-a1ec-0db49dbbad03"), "Developer" }
                      });
            }
      }
}
