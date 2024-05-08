using Group6_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost("add-to-invoice")]
        public IActionResult AddToInvoice([FromBody] AddToInvoiceRequestModel requestModel)
        {
            try
            {
                //var userId = GetUserIdFromContext(HttpContext);

                //if (!requestModel.AccountId.ToString().Equals(userId) || String.IsNullOrEmpty(userId))
                //{
                //    return StatusCode(401, "Vui lòng đăng nhập hoặc không có quyền truy cập.");
                //}

                var customer = _context.Accounts.FirstOrDefault(a => a.AccountId == requestModel.AccountId);

                if (customer == null)
                {
                    return NotFound("Không tìm thấy thông tin khách hàng.");
                }

                var product = _context.Products.FirstOrDefault(p => p.ProductId == requestModel.ProductId);

                if (product == null)
                {
                    return NotFound("Không tìm thấy thông tin sản phẩm.");
                }

                var tenant = _context.Tenants.FirstOrDefault(t => t.TenantId == requestModel.TenantId);

                if (tenant == null)
                {
                    return NotFound("Không tìm thấy thông tin khách hàng.");
                }

                var invoice = new Invoice
                {
                    InvoiceId = new Random().Next(1, 99999),
                    CustomerId = requestModel.AccountId,
                    CustomerName = customer.Username,
                    TenantId = requestModel.TenantId, 
                    Discount = null,
                    InvoiceDate = DateTime.Now,
                    InvoiceStatus = "Chưa thanh toán",
                    Note = "",
                    TotalAmount = null,
                    TaxRate = 10,
                };
                _context.Invoices.Add(invoice);
                _context.SaveChanges();
                var invoiceDetail = new InvoiceDetail
                {
                    //InvoiceDetailId = 1,
                    InvoiceId = invoice.InvoiceId,
                    ProductId = requestModel.ProductId,
                    Quantity = requestModel.Quantity,
                    Price = product.Price,
                    TenantId = requestModel.TenantId 
                };

                
                //_context.InvoiceDetails.Add(invoiceDetail);
                //_context.SaveChanges(true);

                return Ok("Thêm sản phẩm vào hóa đơn thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi: {ex.ToString()}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Ngoại lệ bên trong: {ex.InnerException.ToString()}");
                }

                return StatusCode(500, "Đã xảy ra lỗi khi lưu các thay đổi vào cơ sở dữ liệu.");
            }

        }


        private string GetUserIdFromContext(HttpContext context)
        {
            return context.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        }
    }
}
