using Blazor.Shared.DTOs;

namespace Blazor.Server.Services.IServices
{
	public interface IUserService
	{
		public Task<int> Count();
        public Task<IEnumerable<UserDTO>> GetAll();
        public Task<IEnumerable<UserDTO>> GetByPage(int page);
        public Task<UserDTO> GetByID(Guid userId);
		public Task<IEnumerable<UserDTO>> SortBy(int page, string option, string sortingOrder);
		public Task<UserDTO> Create(UserDTO userDTO);
		public Task<UserDTO> Update(UserDTO userDTO);
		public Task<bool> Delete(Guid userId);
	}
}
