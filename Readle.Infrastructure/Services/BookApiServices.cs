using Readle.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Readle.Infrastructure.Services
{
    public class BookApiServices
    {
        private readonly HttpClient _http;
        public BookApiServices(HttpClient http)
        {
            _http = http;
        }
        
        public async Task<BookGutendex?> GetBookAsync(int Id)
        {
            return await _http.GetFromJsonAsync<BookGutendex>($"https://gutendex.com/books/{Id}");
        }
        public async Task <List<BookGutendex>> MostReadBookAsync()
        {
            var response =  await _http.GetFromJsonAsync<GutendexResponse>("https://gutendex.com/books/?sort=download_count&page_size=10");
            return response?.Results ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> AdventureAsync()
        {
            var response = await _http.GetFromJsonAsync<GutendexResponse>("https://gutendex.com/books?topic=adventure");
            return response?.Results ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> RomanceAsync()
        {
            var response = await _http.GetFromJsonAsync<GutendexResponse>("https://gutendex.com/books?topic=romance");
            return response?.Results ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> ScienceAsync()
        {
            var response = await _http.GetFromJsonAsync<GutendexResponse>("https://gutendex.com/books?topic=science");
            return response?.Results ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> MysteryAsync()
        {
            var response = await _http.GetFromJsonAsync<GutendexResponse>("https://gutendex.com/books?topic=mystery");
            return response?.Results ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> ChildrenAsync()
        {
            var response = await _http.GetFromJsonAsync<GutendexResponse>("https://gutendex.com/books?topic=Children's");
            return response?.Results ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> PoetryAsync()
        {
            var response = await _http.GetFromJsonAsync<GutendexResponse>("https://gutendex.com/books?topic=Poetry");
            return response?.Results ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> HistoryAsync()
        {
            var response = await _http.GetFromJsonAsync<GutendexResponse>("https://gutendex.com/books?topic=History");
            return response?.Results ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> ShortStoriesAsync()
        {
            var response = await _http.GetFromJsonAsync<GutendexResponse>("https://gutendex.com/books?topic=Short%20Stories");
            return response?.Results ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> ClassicsAsync()
        {
            var response = await _http.GetFromJsonAsync<GutendexResponse>("https://gutendex.com/books?topic=Classics");
            return response?.Results ?? new List<BookGutendex>();
        }
    }
}
