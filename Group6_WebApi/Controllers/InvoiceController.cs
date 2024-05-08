using Group6_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Group6_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly Group06Context _context;

        public InvoiceController(Group06Context context)
        {
            _context = context;
        }

        [HttpGet("{tenantId}")]
        public IActionResult GetAll(int tenantId)
        {
            var invoiceList = _context.Invoices.Where(t => t.TenantId == tenantId).ToList();

            return Ok(invoiceList);
        }

        [HttpGet("{invoiceId}/{tenantId}")]
        public IActionResult GetById(int invoiceId, int tenantId)
        {
            var invoice = _context.Invoices.Where(t => t.TenantId == tenantId).SingleOrDefault(i =>
                i.InvoiceId == invoiceId);

            if (invoice != null)
            {
                return Ok(invoice);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult Create(Invoice invoice)
        {
            try
            {
                // Lấy danh sách sản phẩm trong hóa đơn
                var invoiceDetails = _context.InvoiceDetails
                    .Where(d => d.InvoiceId == invoice.InvoiceId)
                    .ToList();

                if (invoice.Customer != null)
                {
                    // Lưu khách hàng vào cơ sở dữ liệu và lấy customer_id (nếu khách hàng chưa tồn tại)
                    int customerId = SaveCustomerAndGetId(invoice.Customer);

                    // Gán customer_id cho invoice
                    invoice.CustomerId = customerId;
                    invoice.CustomerName = invoice.Customer.CustomerName;
                }
                else
                {
                    // Tạo một đối tượng khách hàng mới nếu không có thông tin khách hàng được cung cấp
                    Customer newCustomer = new Customer();

                    int customerId = SaveCustomerAndGetId(newCustomer);
                    invoice.CustomerId = customerId;
                    invoice.CustomerName = newCustomer.CustomerName;
                }

                // Tính tổng tiền
                decimal? totalPrice = invoiceDetails.Sum(detail => detail.Quantity * detail.Price);

                // Tính thuế và giảm giá
                decimal? tax = totalPrice * invoice.TaxRate / 100;
                decimal? discountAmount = totalPrice * invoice.Discount / 100;

                // Tính tổng tiền cuối cùng
                decimal? totalAmount = (totalPrice + tax - discountAmount);

                // Lưu tổng tiền vào hóa đơn
                invoice.TotalAmount = totalAmount;

                _context.Invoices.Add(invoice);
                _context.SaveChanges();

                return Ok(invoice);
            }
            catch
            {
                return BadRequest();
            }
        }

        private int SaveCustomerAndGetId(Customer customer)
        {
            // Kiểm tra xem khách hàng đã tồn tại trong cơ sở dữ liệu chưa
            var existingCustomer = _context.Customers.FirstOrDefault(c => c.CustomerName == customer.CustomerName);

            if (existingCustomer != null)
            {
                // Trả về customer_id của khách hàng đã tồn tại
                return existingCustomer.CustomerId;
            }
            else
            {
                // Lưu khách hàng mới vào cơ sở dữ liệu và trả về customer_id
                _context.Customers.Add(customer);

                return customer.CustomerId;
            }
        }

        [HttpPut("{invoiceId}")]
        public IActionResult Update(int invoiceId, Invoice invoice)
        {
            var existingInvoice = _context.Invoices.SingleOrDefault(i =>
                           i.InvoiceId == invoiceId);

            if (existingInvoice != null)
            {
                try
                {
                    // Lấy danh sách sản phẩm trong hóa đơn
                    var invoiceDetails = _context.InvoiceDetails
                        .Where(d => d.InvoiceId == invoice.InvoiceId)
                        .ToList();

                    if (invoice.Customer != null)
                    {
                        // Lưu khách hàng vào cơ sở dữ liệu và lấy customer_id (nếu khách hàng chưa tồn tại)
                        int customerId = SaveCustomerAndGetId(invoice.Customer);

                        // Gán customer_id cho invoice
                        invoice.CustomerId = customerId;
                        invoice.CustomerName = invoice.Customer.CustomerName;
                    }
                    else
                    {
                        // Tạo một đối tượng khách hàng mới nếu không có thông tin khách hàng được cung cấp
                        Customer newCustomer = new Customer();

                        int customerId = SaveCustomerAndGetId(newCustomer);
                        invoice.CustomerId = customerId;
                        invoice.CustomerName = newCustomer.CustomerName;
                    }

                    // Tính tổng tiền
                    decimal? totalPrice = invoiceDetails.Sum(detail => detail.Quantity * detail.Price);

                    // Tính thuế và giảm giá
                    decimal? tax = totalPrice * invoice.TaxRate / 100;
                    decimal? discountAmount = totalPrice * invoice.Discount / 100;

                    // Tính tổng tiền cuối cùng
                    decimal? totalAmount = (totalPrice + tax - discountAmount);

                    // Cập nhật tổng tiền vào hóa đơn
                    existingInvoice.TotalAmount = totalAmount;

                    // Cập nhật các thông tin khác của hóa đơn
                    existingInvoice.InvoiceDate = invoice.InvoiceDate;
                    existingInvoice.InvoiceStatus = invoice.InvoiceStatus;
                    existingInvoice.CustomerId = invoice.CustomerId;
                    existingInvoice.CustomerName = invoice.CustomerName;

                    _context.SaveChanges();

                    return Ok(existingInvoice);
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }


        [HttpDelete("{invoiceId}")]
        public IActionResult Delete(int invoiceId)
        {
            var invoice = _context.Invoices.SingleOrDefault(i =>
                           i.InvoiceId == invoiceId);

            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                _context.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
