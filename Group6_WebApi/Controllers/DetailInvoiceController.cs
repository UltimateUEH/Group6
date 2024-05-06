using Group6_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group6_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailInvoiceController : ControllerBase
    {
        private readonly Group06Context _context;

        public DetailInvoiceController(Group06Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetailInvoice>>> GetDetailInvoices()
        {
            // Lấy danh sách tất cả các mục chi tiết hóa đơn từ cơ sở dữ liệu
            var detailInvoices = await _context.DetailInvoices.ToListAsync();

            if (detailInvoices == null || detailInvoices.Count == 0)
            {
                return NotFound("No detail invoices found");
            }

            // Duyệt qua từng mục chi tiết hóa đơn để tính tổng số tiền của mỗi mục
            foreach (var detailInvoice in detailInvoices)
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == detailInvoice.ProductId);
                if (product == null)
                {
                    return NotFound($"Product not found for detail invoice with ID {detailInvoice.DetailId}");
                }

                decimal taxRate = detailInvoice.Tax != null ? detailInvoice.Tax.Rate.GetValueOrDefault() : 0;

                detailInvoice.TotalAmount = CalculateProductTotal(product.Price, detailInvoice.Quantity.GetValueOrDefault(), taxRate, detailInvoice.Discount != null ? Convert.ToDecimal(detailInvoice.Discount) : 0);

            }

            return detailInvoices;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DetailInvoice>> GetDetailInvoice(int id)
        {
            // Lấy chi tiết hóa đơn từ cơ sở dữ liệu
            var detailInvoice = await _context.DetailInvoices.FindAsync(id);

            if (detailInvoice == null)
            {
                return NotFound();
            }

            // Lấy danh sách các mục chi tiết hóa đơn của hóa đơn hiện tại
            var detailInvoices = _context.DetailInvoices.Where(di => di.InvoiceId == detailInvoice.InvoiceId).ToList();

            // Tính tổng số tiền cho toàn bộ hóa đơn
            decimal invoiceTotalAmount = CalculateInvoiceTotal(detailInvoices);

            // Gán tổng số tiền của hóa đơn vào chi tiết hóa đơn
            detailInvoice.TotalAmount = invoiceTotalAmount;

            return detailInvoice;
        }

        private decimal CalculateProductTotal(decimal? price, int quantity, decimal? taxRate, decimal discount)
        {
            if (!price.HasValue || !taxRate.HasValue)
            {
                // Xử lý trường hợp giá hoặc tỷ lệ thuế không có giá trị
                return 0; // Hoặc giá trị mặc định tùy thuộc vào yêu cầu của bạn
            }

            // Tính tổng tiền cho mỗi sản phẩm
            decimal total = price.Value * quantity;

            // Áp dụng thuế VAT
            total *= (1 + taxRate.Value / 100);

            // Áp dụng giảm giá (nếu có)
            total -= discount;

            return total;
        }



        private decimal CalculateInvoiceTotal(List<DetailInvoice> detailInvoices)
        {
            decimal totalAmount = 0;

            foreach (var detailInvoice in detailInvoices)
            {
                // Lấy thông tin sản phẩm từ cơ sở dữ liệu
                var product = _context.Products.FirstOrDefault(p => p.ProductId == detailInvoice.ProductId);
                if (product == null)
                {
                    continue; // Bỏ qua nếu không tìm thấy sản phẩm
                }

                // Tính tổng cho từng sản phẩm
                decimal taxRate = 0;
                if (detailInvoice.Tax != null)
                {
                    taxRate = detailInvoice.Tax.Rate.GetValueOrDefault();
                }

                decimal productTotal = CalculateProductTotal(product.Price, detailInvoice.Quantity.GetValueOrDefault(), taxRate, detailInvoice.Discount != null ? Convert.ToDecimal(detailInvoice.Discount) : 0);

                // Cộng vào tổng số tiền của hóa đơn
                totalAmount += productTotal;
            }

            return totalAmount;
        }


        [HttpPost]
        public async Task<ActionResult<DetailInvoice>> PostDetailInvoice(List<DetailInvoice> detailInvoices)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Tính tổng của tất cả sản phẩm trong hóa đơn
            decimal totalAmount = CalculateInvoiceTotal(detailInvoices);

            // Gán tổng vào mỗi mục chi tiết hóa đơn
            foreach (var detailInvoice in detailInvoices)
            {
                detailInvoice.TotalAmount = totalAmount;
            }

            // Thêm các mục chi tiết hóa đơn vào cơ sở dữ liệu
            _context.DetailInvoices.AddRange(detailInvoices);
            await _context.SaveChangesAsync();

            return Ok("Detail invoices added successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetailInvoice(int id, DetailInvoice detailInvoice)
        {
            if (id != detailInvoice.DetailId)
            {
                return BadRequest("Id mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lấy thông tin sản phẩm từ cơ sở dữ liệu
            var product = await _context.Products.FindAsync(detailInvoice.ProductId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            // Tính tổng amount sử dụng phương thức CalculateTotalAmount
            decimal totalAmount = CalculateProductTotal(product.Price, detailInvoice.Quantity.GetValueOrDefault(), detailInvoice.TaxId.GetValueOrDefault(), detailInvoice.Discount != null ? Convert.ToDecimal(detailInvoice.Discount) : 0);

            // Gán tổng amount cho đơn hàng
            detailInvoice.TotalAmount = totalAmount;

            _context.Entry(detailInvoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailInvoiceExists(id))
                {
                    return NotFound("DetailInvoice not found");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool DetailInvoiceExists(int id)
        {
            return _context.DetailInvoices.Any(e => e.DetailId == id);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetailInvoice(int id)
        {
            // Tìm mục chi tiết hóa đơn cần xóa từ cơ sở dữ liệu
            var detailInvoice = await _context.DetailInvoices.FindAsync(id);
            if (detailInvoice == null)
            {
                return NotFound("DetailInvoice not found");
            }

            // Xóa mục chi tiết hóa đơn khỏi cơ sở dữ liệu
            _context.DetailInvoices.Remove(detailInvoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
