using Group6_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
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
                int a;
                if (listSearchProducts.Count() < 1)
                {
                    return NotFound("Not Found ProductName");
                }
                return Ok(listSearchProducts);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

        }
        // check if user logined then allow else back
        [HttpPost("{productId}")]
        public async Task<IActionResult> AddToInvoice(int productId, int AccountId)
        {
            try
            {
                var userId = GetUserIdFromContext(HttpContext);
                // chưa có phần đăng nhập chưa lấy được userID
                if (!AccountId.ToString().Equals(userId) || String.IsNullOrEmpty(AccountId.ToString()))
                {
                    return StatusCode(500, "Please Login To Add To Invoice  ");
                }

                string customerName = _context.Accounts
                  .Where(account => account.AccountId == AccountId).Select(account => account.Username).FirstOrDefault();

                Random random = new Random();
                return Ok(new Invoice
                {
                    // tạm sửa sau
                    InvoiceId = random.Next(1, 9999),
                    CustomerId = AccountId,
                    CustomerName = customerName,
                    Discount = 10,  //  10$
                    InvoiceDate = DateTime.Now,
                    InvoiceStatus = "processing", // set lai cho phu h
                    Note = "None",
                    TotalAmount = null,
                    TaxRate = null,
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




        private string GetUserIdFromContext(HttpContext context)
        {
            return context.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        }

    }
}