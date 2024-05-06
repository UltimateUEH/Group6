﻿using Group6_WebApi.Models;
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

        //[HttpPost]
        //public IActionResult Create(Invoice invoice)
        //{
        //    try
        //    {
        //        _context.Invoices.Add(invoice);
        //        _context.SaveChanges();

        //        return Ok(invoice);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpPost]
        public IActionResult Create(Invoice invoice)
        {
            try
            {
                // Lấy danh sách sản phẩm trong hóa đơn
                var invoiceDetails = _context.InvoiceDetails
                    .Where(d => d.InvoiceId == invoice.InvoiceId)
                    .ToList();

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

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, Invoice invoice)
        //{
        //    var existingInvoice = _context.Invoices.SingleOrDefault(i =>
        //                   i.InvoiceId == id);

        //    if (existingInvoice != null)
        //    {
        //        existingInvoice.InvoiceDate = invoice.InvoiceDate;
        //        existingInvoice.TotalAmount = invoice.TotalAmount;

        //        _context.SaveChanges();

        //        return Ok(existingInvoice);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpPut("{id}")]
        public IActionResult Update(int id, Invoice invoice)
        {
            var existingInvoice = _context.Invoices.SingleOrDefault(i =>
                           i.InvoiceId == id);

            if (existingInvoice != null)
            {
                try
                {
                    // Lấy danh sách sản phẩm trong hóa đơn
                    var invoiceDetails = _context.InvoiceDetails
                        .Where(d => d.InvoiceId == invoice.InvoiceId)
                        .ToList();

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
                    existingInvoice.Status = invoice.Status;

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
