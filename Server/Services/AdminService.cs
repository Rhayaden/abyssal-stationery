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
	public class AdminService : IAdminService
	{
		private readonly IMapper _mapper;
		private readonly AppDbContext _dbContext;
		private readonly IConfiguration _configuration;
		public AdminService(IMapper mapper, AppDbContext dbContext, IConfiguration configuration)
		{
			_mapper = mapper;
			_dbContext = dbContext;
			_configuration = configuration;
		}

		public async Task<bool> IsAdmin(string emailAsClaim)
		{
			return await _dbContext.Admins.FirstOrDefaultAsync(a => a.Email == emailAsClaim) != null ? true : false;
		}

		public async Task<AdminLoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
		{
			var hashedPw = PasswordCrypter.Encrypt(loginRequestDTO.Password);
			var admin = await _dbContext.Admins.FirstOrDefaultAsync(a => a.Email == loginRequestDTO.Email && a.Password == hashedPw);

			if (admin == null)
			{
				return null;
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")["Secret"]));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expiry = DateTime.Now.AddDays(int.Parse(_configuration.GetSection("Jwt")["Expiry"]));

			var claims = new List<Claim>();

			claims.Add(new Claim(ClaimTypes.UserData, admin.Id.ToString()));
			claims.Add(new Claim(ClaimTypes.Email, admin.Email));
			

			var token = new JwtSecurityToken(
				_configuration.GetSection("Jwt")["Issuer"],
				_configuration.GetSection("Jwt")["Audience"],
				claims,
				null,
				expiry,
				credentials);

			AdminLoginResponseDTO result = new ()
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				Admin = _mapper.Map<AdminDTO>(admin)
			};

			return result;
		}
	}
}
