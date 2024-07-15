using AutoMapper;
using Blazor.Server.Data;
using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blazor.Server.Services
{
	public class AuthService : IAuthService
	{
		private readonly IMapper _mapper;
		private readonly AppDbContext _dbContext;
		private readonly IConfiguration _configuration;

		public AuthService(IMapper mapper, AppDbContext dbContext, IConfiguration configuration)
        {
			_mapper = mapper;
			_dbContext = dbContext;
			_configuration = configuration;
		}
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
		{
			var hashedPw = PasswordCrypter.Encrypt(loginRequestDTO.Password);
			var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDTO.Email && u.Password == hashedPw);

			if (user == null)
			{
				return null;
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")["Secret"]));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expiry = DateTime.Now.AddDays(int.Parse(_configuration.GetSection("Jwt")["Expiry"]));

			var claims = new List<Claim>();

			claims.Add(new Claim(ClaimTypes.UserData, user.Id.ToString()));
			claims.Add(new Claim(ClaimTypes.Email, user.Email));

			var token = new JwtSecurityToken(
				_configuration.GetSection("Jwt")["Issuer"],
				_configuration.GetSection("Jwt")["Audience"],
				claims,
				null,
				expiry,
				credentials);

			LoginResponseDTO result = new LoginResponseDTO()
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				User = user != null ? _mapper.Map<UserDTO>(user) : null,
			};

			return result;
		}
	}
}
