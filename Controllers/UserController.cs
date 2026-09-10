using AutoMapper;
using EComerceAPI.DTOs;
using EComerceAPI.Models;
using EComerceAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EComerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(
    IUserRepository userRepository,
    IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _userRepository.GetByEmail(dto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                dto.Password
                );
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid email or password");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role, user.Role)

            };

            var key = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes("MY_SUPER_SECRET_KEY_123456789012")
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                 signingCredentials: credentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new
            {
                token = tokenString
            });
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userRepository.GetAll();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [HttpPost]
        public IActionResult Add(CreateUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                Role = dto.Role
            };

            var passwordHasher = new PasswordHasher<User>();

            user.PasswordHash = passwordHasher.HashPassword(
                user,
                dto.Password
            );

            _userRepository.Add(user);

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }
        [HttpPut]
        public IActionResult Update(User user)
        {
            _userRepository.Update(user);
            return Ok(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
                return NotFound();

            _userRepository.Delete(user);

            return Ok();
        }
    }
}
