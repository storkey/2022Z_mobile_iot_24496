using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.Win32;
using Newtonsoft.Json;
using RealEstateApp.Models;



namespace RealEstateApp.Services
{
    public static class ApiService
    {
        public static async Task<bool> RegisterUser(string name, string email, string password, string phone)
        {
            Register register = new()
            {
                Name = name,
                Email = email,
                Password = password,
                Phone = phone
            };

            var httpClient = new HttpClient();
            var registerSerialized = JsonConvert.SerializeObject(register);
            var content = new StringContent(registerSerialized, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/users/register", content);

            if (!response.IsSuccessStatusCode)
                return false;
            return true;
        }

        public static async Task<bool> Login(string email, string password)
        {
            Login login = new()
            {
                Email = email,
                Password = password
            };

            var httpClient = new HttpClient();
            var registerSerialized = JsonConvert.SerializeObject(login);
            var content = new StringContent(registerSerialized, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/users/login", content);

            if (!response.IsSuccessStatusCode)
                return false;

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);
            Preferences.Set("accessToken", result.AccessToken);
            Preferences.Set("userId", result.UserId);
            Preferences.Set("userName", result.UserName);

            return true;
        }

        public static async Task<List<Category>> GetCategories()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/categories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public static async Task<List<TrendingProperty>> GetTrendingProperties()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/TrendingProperties");
            return JsonConvert.DeserializeObject<List<TrendingProperty>>(response);
        }

        public static async Task<List<PropertyByCategory>> GetPropertyByCategory(int categoryId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/PropertyList?categoryId=" + categoryId);
            return JsonConvert.DeserializeObject<List<PropertyByCategory>>(response);
        }

        public static async Task<PropertyDetail> GetPropertyDetail(int propertyId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/PropertyDetail?id=" + propertyId);
            return JsonConvert.DeserializeObject<PropertyDetail>(response);
        }

        public static async Task<List<BookmarkList>> GetBookmarkList()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/bookmarks");
            return JsonConvert.DeserializeObject<List<BookmarkList>>(response);
        }

        public static async Task<bool> AddBookmark(AddBookmark addBookmark)
        {
            var httpClient = new HttpClient();
            var registerSerialized = JsonConvert.SerializeObject(addBookmark);
            var content = new StringContent(registerSerialized, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/bookmarks", content);

            if (!response.IsSuccessStatusCode)
                return false;
            return true;
        }

        public static async Task<bool> DeleteBookmark(int bookmarkId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + "/api/bookmarks/" + bookmarkId);

            if (!response.IsSuccessStatusCode)
                return false;
            return true;
        }
    }
}
