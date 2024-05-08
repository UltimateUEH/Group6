using Group6_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Group6_WebApi.Controllers
{
    [Route("api/shopping")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly Group06Context _context;

        public ShoppingController(Group06Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SearchProduct(string productName)
        {
            try
            {
                IEnumerable<Product> list = _context.Products.ToList();
                IEnumerable<Product> listSearchProducts = list.Where(pn => pn.ProductName.Contains(productName));

                if (listSearchProducts.Count() < 1)
                {
                    return NotFound("Không tìm thấy tên sản phẩm");
                }
                return Ok(listSearchProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{productId}")]
        public IActionResult AddToInvoice(int productId, int accountId)
        {
            try
            {
                var userId = GetUserIdFromContext(HttpContext);

                if (!accountId.ToString().Equals(userId) || String.IsNullOrEmpty(accountId.ToString()))
                {
                    return StatusCode(500, "Vui lòng đăng nhập");
                }

                string? customerName = _context.Accounts
                  .Where(account => account.AccountId == accountId)
                  .Select(account => account.Username)
                  .FirstOrDefault();

                Random random = new Random();
                var invoice = new Invoice
                {
                    InvoiceId = random.Next(1, 99999),
                    CustomerId = accountId,
                    CustomerName = customerName,
                    Discount = null,
                    InvoiceDate = DateTime.Now,
                    InvoiceStatus = "Chưa thanh toán",
                    Note = "",
                    TotalAmount = null,
                    TaxRate = 10,
                };

                _context.Invoices.Add(invoice);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GetUserIdFromContext(HttpContext context)
        {
            return context.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        }
    }
}