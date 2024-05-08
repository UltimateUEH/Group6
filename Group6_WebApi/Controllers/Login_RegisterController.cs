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
    public class Login_RegisterController : ControllerBase
    {
        private readonly Group06Context _context;

        public Login_RegisterController(Group06Context context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Account login)
        {
            string hashedPassword = HashPassword(login.Password);
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

        [HttpPost("register")]
        public IActionResult Register(Account registration)
        {
            if (_context.Accounts.Any(u => u.Email == registration.Email))
            {
                return BadRequest("Email already exists");
            }

            registration.Password = HashPassword(registration.Password);
            _context.Accounts.Add(registration);
            _context.SaveChanges();

            return Ok("Registration successful!");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
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

