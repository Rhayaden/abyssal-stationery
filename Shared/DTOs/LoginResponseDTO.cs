﻿namespace Blazor.Shared.DTOs
{
	public class LoginResponseDTO
	{
		public string Token { get; set; }
		public UserDTO? User { get; set; }
	}
}
