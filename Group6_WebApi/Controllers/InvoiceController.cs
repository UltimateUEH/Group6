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

        [HttpGet]
        public IActionResult GetAll()
        {
            var invoiceList = _context.Invoices.ToList();

            return Ok(invoiceList);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var invoice = _context.Invoices.SingleOrDefault(i =>
                i.InvoiceId == id);

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
                _context.Invoices.Add(invoice);
                _context.SaveChanges();

                return Ok(invoice);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Invoice invoice)
        {
            var existingInvoice = _context.Invoices.SingleOrDefault(i =>
                           i.InvoiceId == id);

            if (existingInvoice != null)
            {
                existingInvoice.InvoiceDate = invoice.InvoiceDate;
                existingInvoice.TotalAmount = invoice.TotalAmount;

                _context.SaveChanges();

                return Ok(existingInvoice);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var invoice = _context.Invoices.SingleOrDefault(i =>
                           i.InvoiceId == id);

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
