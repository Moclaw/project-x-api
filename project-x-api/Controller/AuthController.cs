using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_x_ba.Services;
using project_x_da.Data;
using project_x_da.Entity;
using project_x_da.Models.Response;
using project_x_da.PostModels;
namespace project_x_api.Controller
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthController> _logger;
        private readonly UserServices _userServices;
        private string _secretKey;
        public AuthController(IConfiguration configuration, ApplicationDbContext context, ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
            _userServices = new UserServices(_configuration, _context);
            _secretKey = _configuration["SecretKey"];
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Login(LoginPostModel model)
        {
            try
            {
                User user = _context?.Users?.Where(x => x.Email == model.Email).FirstOrDefault()!;
                if (user == null)
                    return Unauthorized(new DefaultResponseContext { Message = "User not found" });
                if (CryptoService.AESHash(model.Password, _secretKey) != user.Password)
                    return Unauthorized(new DefaultResponseContext { Message = "passwordincorrect" });
                string ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                string JwtToken = _userServices.Login(user, ip, model.IsRemember ? 7 : 1);

                return Ok(new DefaultResponseContext { Message = "Login success", Data = JwtToken });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return BadRequest(new DefaultResponseContext { Message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Register(RegisterPostModel model)
        {
            try
            {
                User user = _context?.Users?.Where(x => x.Email == model.Email).FirstOrDefault()!;
                if (user != null)
                    return Unauthorized(new DefaultResponseContext { Message = "Email already exists" });
                string ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                var token = _userServices.Register(model, ip);
                return Ok(new DefaultResponseContext { Message = "Register success", Data = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return BadRequest(new DefaultResponseContext { Message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
