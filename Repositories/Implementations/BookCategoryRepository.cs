using CoreMvcTest.Models;
using CoreMvcTest.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreMvcTest.Repositories.Implementations
{
    public class BookCategoryRepository : IBookCategoryRepository
    {
        private readonly string baseUrl;
        private readonly IConfiguration _configuration;
        public BookCategoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            baseUrl= configuration.GetValue<string>("baseUrl") + "/bookcategory";
        }
        public async Task<bool> AddUpdate(BookCategory bookCategory)
        {
            try
            {
                HttpClient client = new HttpClient();
                string path = baseUrl + "/addupdate";
                var content = new StringContent(JsonConvert.SerializeObject(bookCategory), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(path, content);
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var status = JsonConvert.DeserializeObject<Status>(res);
                    return status.StatusCode == 1 ? true : false;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                string path = baseUrl + "/delete?id=" + id;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Status>(jsonContent);
                    return data.StatusCode == 1 ? true : false;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<BookCategory> FindById(int id)
        {
            BookCategory bookCategory = new BookCategory();
            try
            {
                HttpClient client = new HttpClient();
                string path = baseUrl + "/getbyid?id=" + id;
                HttpResponseMessage response = await client.GetAsync(path);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                bookCategory = JsonConvert.DeserializeObject<BookCategory>(jsonResponse);
            }
            catch(Exception ex)
            {

            }
            return bookCategory;
        }

        public async Task<IEnumerable<BookCategory>> GetAll()
        {
            try
            {
                string path = baseUrl + "/getall";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(path);
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<BookCategory>>(content);
                return data;
            }
            catch(Exception ex)
            {
                return new List<BookCategory>();
            }
        }
    }
}
