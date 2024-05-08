using Group6_WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Group6_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Group06Context _context;

        public LoginController(Group06Context context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Account login)
        {
            // Hash the provided password
            string hashedPassword = HashPassword(login.Password);

            // Find user by username and hashed password
            var user = _context.Accounts.FirstOrDefault(u => u.Email == login.Email && u.Password == hashedPassword);

            if (user != null)
            {
                return Ok("Login successful!");
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Compute hash of the password
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string representation
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly Group06Context _context;

        public RegisterController(Group06Context context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Register(Account registration)
        {
            // Kiểm tra xem tên người dùng đã tồn tại chưa
            if (_context.Accounts.Any(u => u.Email == registration.Email))
            {
                return BadRequest("Email already exists");
            }

            // Hash the password
            registration.Password = HashPassword(registration.Password);

            // Tạo một bản ghi mới và thêm vào cơ sở dữ liệu
            _context.Accounts.Add(registration);
            _context.SaveChanges();

            return Ok("");
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Compute hash of the password
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string representation
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}