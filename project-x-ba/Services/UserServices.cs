using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using project_x_da.Data;
using project_x_da.Entity;
using project_x_da.PostModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace project_x_ba.Services
{
    public class UserServices
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public UserServices(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public string Login(User user, string ip, int expiredDate = 1)
        {

            try
            {

                var encryptKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Jwt:EncryptKey"]));
                string validIssusers = _configuration["Jwt:ValidIssuer"];
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = validIssusers,
                    Audience = null,
                    IssuedAt = DateTime.Now,
                    NotBefore = DateTime.Now,
                    Expires = DateTime.Now.AddDays(expiredDate),
                    Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim("role", "User"),
                    }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(System.Convert.FromBase64String(_configuration["Jwt:EncryptKey"])), SecurityAlgorithms.HmacSha256Signature)
                };
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                var token = jwtTokenHandler.WriteToken(jwtToken);
                _context.UserLogins.Add(new UserLogin
                {
                    UserId = user.Id,
                    Ip = ip,
                    LoginDate = DateTime.Now,
                });
                _context.SaveChanges();
                return token;

            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source: {0}", e.Message);
            }
            return "Error generate token";
        }
        public string Register(RegisterPostModel model, string ip)
        {
            try
            {
                var user = new User
                {
                    Email = model.Email,
                    Password = CryptoService.AESHash(model.Password, _configuration["SecretKey"]),
                    CreatedDate = DateTime.Now,
                    IsDisabled = false,
                };
                _context.UserDetails.Add(new UserDetail
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    UserId = user.Id,
                    CreatedDate = DateTime.Now,
                });
                _context.UserLogins.Add(new UserLogin
                {
                    UserId = user.Id,
                    Ip = ip,
                    LoginDate = DateTime.Now,
                });
                _context.Users.Add(user);
                var token = Login(user, ip);
                _context.SaveChanges();
                return token;
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source: {0}", e.Message);
                return "Error register";
            }
        }
    }
}