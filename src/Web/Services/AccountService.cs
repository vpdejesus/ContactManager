using System;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Web.Models;
using Web.Helpers;
using Microsoft.AspNetCore.Components.Authorization;

namespace Web.Services
{
    public class AccountService
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AccountService(AuthenticationStateProvider authStateProvider)
        {
            _client = new HttpClient();
            _authStateProvider = authStateProvider;
        }

        private StringContent GetStringContentFromObject(object obj)
        {
            var serialized = JsonSerializer.Serialize(obj);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            return content;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var uri = new Uri(string.Format(Constants.BaseAddress + "/api/register", string.Empty));
            var response = await _client.PostAsync(uri, GetStringContentFromObject(registerModel));
            var result = JsonSerializer.Deserialize<RegisterResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var uri = new Uri(string.Format(Constants.BaseAddress + "/api/login", string.Empty));
            var response = await _client.PostAsync(uri, GetStringContentFromObject(loginModel));
            var result = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return result;
            }

            Settings.AccessToken = result.Token;
            ((AuthStateProvider)_authStateProvider).MarkAsAuthenticated(loginModel.Email);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);

            return result;
        }

        public void Logout()
        {
            Settings.AccessToken = "";
            ((AuthStateProvider)_authStateProvider).MarkAsLoggedOut();
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
