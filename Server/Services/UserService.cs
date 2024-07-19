using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blazor.Server.Data;
using Blazor.Server.Models;
using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Services
{
	public class UserService : IUserService
	{
		private readonly IMapper _mapper;
		private readonly AppDbContext _dbContext;
        private readonly int _size;
        public UserService(IMapper mapper, AppDbContext dbContext)
        {
			_mapper = mapper;
			_dbContext = dbContext;
			_size = ItemsPerPage.User;
		}
		public async Task<int> Count()
		{
			return await _dbContext.Users.CountAsync();
		}
		public async Task<IEnumerable<UserDTO>> GetAll()
		{
            return await _dbContext.Users.OrderByDescending(u => u.RegisteredAt).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
        public async Task<UserDTO> Create(UserDTO userDTO)
		{
			var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
			if (user != null)
			{
				throw new AppException("This user is already exist");
			}

			userDTO.FirstName = char.ToUpper(userDTO.FirstName[0]) + userDTO.FirstName.Substring(1);
			userDTO.LastName = char.ToUpper(userDTO.LastName[0]) + userDTO.LastName.Substring(1);

			var userModel = _mapper.Map<User>(userDTO);
			userModel.Password = PasswordCrypter.Encrypt(userDTO.Password);

			await _dbContext.Users.AddAsync(userModel);
			var cart = await _dbContext.Carts.AddAsync(new Cart()
			{
				UserId = userModel.Id,
			});

			userModel.Cart = cart.Entity;

			await _dbContext.SaveChangesAsync();

			return _mapper.Map<UserDTO>(userModel);
		}

		public async Task<bool> Delete(Guid userId)
		{
			var user = await _dbContext.Users.FindAsync(userId);
			if (user == null)
			{
				throw new AppException("User not found");
			}

			_dbContext.Users.Remove(user);
			var result = await _dbContext.SaveChangesAsync();

			return result > 0;
		}

		public async Task<UserDTO> GetByID(Guid userId)
		{
			var user = await _dbContext.Users.FindAsync(userId);

			return _mapper.Map<UserDTO>(user);
		}

		public async Task<UserDTO> Update(UserDTO userDTO)
		{
			var userInDb = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userDTO.Id);
			if (userInDb == null)
			{
				throw new AppException("User not found");
			}

			var user = _mapper.Map(userInDb, userDTO);

			await _dbContext.SaveChangesAsync();

			return user;
		}

        public async Task<IEnumerable<UserDTO>> GetByPage(int page)
        {
            int skip = (page - 1) * _size;
            return await _dbContext.Users.OrderByDescending(u => u.RegisteredAt).Skip(skip).Take(_size).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}