using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Readle.Domain.Dtos;
using Readle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Readle.Infrastructure.Services
{
    public class AuthApiServices
    {
        private readonly HttpClient _http;
        private readonly ProtectedLocalStorage _localstorage;
        public AuthApiServices(HttpClient http, ProtectedLocalStorage localstorage)
        {
            _http = http;
            _localstorage = localstorage;
        }

        public async Task<User?> RegisterAsync( UserDto user)
        {
            var request = await _http.PostAsJsonAsync("api/Auth/Register", user);
            if (!request.IsSuccessStatusCode)
            {
                return null;
            }
            return await request.Content.ReadFromJsonAsync<User>();
        }
        public async Task<TokenResponseDto?> LoginAsync(UserDto user)
        {
            var request = await _http.PostAsJsonAsync("api/Auth/Login", user);
            if (!request.IsSuccessStatusCode)
            {
                return null;
            }
            return await request.Content.ReadFromJsonAsync<TokenResponseDto>();
        }



    }
}
