using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Blazor.Client.Utilities
{
	public class AuthStateProvider : AuthenticationStateProvider
	{
		private readonly HttpClient _client;
		private readonly ILocalStorageService _localStorageService;
		private readonly AuthenticationState _authState;

		public AuthStateProvider(HttpClient client, ILocalStorageService localStorageService)
		{
			_client = client;
			_localStorageService = localStorageService;
			_authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		}
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			string token = await _localStorageService.GetItemAsStringAsync("token");
			var trimmedToken = token?.Trim('"');

			if (String.IsNullOrEmpty(trimmedToken))
			{
				return _authState;
			}

			var email = await _localStorageService.GetItemAsStringAsync("email");

			var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
				new List<Claim>()
				{
					new Claim(ClaimTypes.Email, email)
				}, "jwt"));

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", trimmedToken);

			return new AuthenticationState(claimsPrincipal);
		}

		public void NotifyLogin(string email)
		{
			var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
				new List<Claim>()
				{
					new Claim(ClaimTypes.Email, email)
				}, "jwt"));

			var authState = Task.FromResult(new AuthenticationState(claimsPrincipal));

			NotifyAuthenticationStateChanged(authState);
		}

		public void NotifyLogout()
		{
			var authState = Task.FromResult(_authState);
			NotifyAuthenticationStateChanged(authState);
		}
	}
}
