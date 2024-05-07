using Group6_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group6_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly Group06Context _context;

        public InvoiceDetailController(Group06Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var invoiceDetailList = _context.InvoiceDetails.ToList();

            return Ok(invoiceDetailList);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var invoiceDetail = _context.InvoiceDetails.FirstOrDefault(i =>
                i.InvoiceId == id);

            if (invoiceDetail != null)
            {
                return Ok(invoiceDetail);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create(InvoiceDetail invoiceDetail)
        {
            try
            {
                // Lấy danh sách sản phẩm trong hóa đơn
                var invoiceDetails = _context.InvoiceDetails
                    .Where(d => d.InvoiceId == invoiceDetail.InvoiceId)
                    .ToList();

                if (invoiceDetail.Product != null)
                {
                    // Lưu sản phẩm vào cơ sở dữ liệu và lấy product_id (nếu sản phẩm chưa tồn tại)
                    int productId = SaveProductAndGetId(invoiceDetail.Product);

                    // Gán product_id cho invoiceDetail
                    invoiceDetail.ProductId = productId;
                    invoiceDetail.ProductName = invoiceDetail.Product.ProductName;
                    invoiceDetail.Price = invoiceDetail.Product.Price;
                }

                // Lưu invoiceDetail vào cơ sở dữ liệu
                _context.InvoiceDetails.Add(invoiceDetail);
                _context.SaveChanges();

                return Ok(invoiceDetail);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private int SaveProductAndGetId(Product product)
        {
            // Kiểm tra xem khách hàng đã tồn tại trong cơ sở dữ liệu chưa
            var existingProduct = _context.Products.FirstOrDefault(c => c.ProductName == product.ProductName);

            if (existingProduct != null)
            {
                // Trả về customer_id của khách hàng đã tồn tại
                return existingProduct.ProductId;
            }
            else
            {
                // Lưu khách hàng mới vào cơ sở dữ liệu và trả về customer_id
                _context.Products.Add(product);
                _context.SaveChanges();
                return product.ProductId;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, InvoiceDetail invoiceDetail)
        {
            var existingInvoiceDetail = _context.InvoiceDetails.SingleOrDefault(i =>
                           i.InvoiceId == id);

            if (existingInvoiceDetail != null)
            {
                try
                {
                    // Lấy danh sách sản phẩm trong hóa đơn
                    var invoiceDetails = _context.InvoiceDetails
                        .Where(d => d.InvoiceId == invoiceDetail.InvoiceId)
                        .ToList();

                    if (invoiceDetail.Product != null)
                    {
                        // Lưu sản phẩm vào cơ sở dữ liệu và lấy product_id (nếu sản phẩm chưa tồn tại)
                        int productId = SaveProductAndGetId(invoiceDetail.Product);

                        // Gán product_id cho invoiceDetail
                        invoiceDetail.ProductId = productId;
                        invoiceDetail.ProductName = invoiceDetail.Product.ProductName;
                        invoiceDetail.Price = invoiceDetail.Product.Price;
                    }

                    // Cập nhật invoiceDetail vào cơ sở dữ liệu
                    existingInvoiceDetail.ProductId = invoiceDetail.ProductId;
                    existingInvoiceDetail.ProductName = invoiceDetail.ProductName;
                    existingInvoiceDetail.Quantity = invoiceDetail.Quantity;
                    existingInvoiceDetail.Price = invoiceDetail.Price;

                    _context.SaveChanges();

                    return Ok(existingInvoiceDetail);
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var query = $"DELETE FROM InvoiceDetail WHERE invoice_id = {id}";
            _context.Database.ExecuteSqlRaw(query);

            return Ok();
        }
    }
}
