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
    public class BookRepository : IBookRepository
    {
        public BookRepository(IConfiguration config)
        {
            _config = config;
            baseUrl = _config.GetValue<string>("baseUrl") + "/book";
        }

        private readonly IConfiguration _config;
        private readonly string baseUrl;
        public void MethodNotInInterface()
        {

        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                string path = baseUrl + "/getall";
                HttpResponseMessage response = await client.GetAsync(path);
                string jsonContent = await response.Content.ReadAsStringAsync();
                var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(jsonContent);
                return books;
            }
            catch(Exception ex)
            {
                return new List<Book>();
            }
        }

        public async Task<bool> AddUpdate(Book book)
        {
            try
            {
                HttpClient client = new HttpClient();
                string path = baseUrl + "/addupdate";
                StringContent content = new StringContent(JsonConvert.SerializeObject(book),Encoding.UTF8,"application/json");
                HttpResponseMessage response = await client.PostAsync(path, content);
                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    var status = JsonConvert.DeserializeObject<Status>(jsonData);
                    return status.StatusCode == 1 ? true : false;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                string path = baseUrl + "/delete/" + id;
                HttpResponseMessage response = await client.DeleteAsync(path);
                string jsonData = await response.Content.ReadAsStringAsync();
                Status status = JsonConvert.DeserializeObject<Status>(jsonData);
                return Convert.ToBoolean(status.StatusCode);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Book> GetById(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                string path = baseUrl + "/getbyid/" + id;
                HttpResponseMessage response = await client.GetAsync(path);
                string jsonData =await response.Content.ReadAsStringAsync();
                Book book = JsonConvert.DeserializeObject<Book>(jsonData);
                return book;
            }
            catch (Exception ex)
            {
                return new Book();
            }
        }

        public async Task<Book> GetByName(string term)
        {
            try
            {
                string path = baseUrl + "/getbyname?term=" + term;
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(path);
                string jsonData = await response.Content.ReadAsStringAsync();
                Book book = JsonConvert.DeserializeObject<Book>(jsonData);
                return book;
            }
            catch (Exception ex)
            {
                return new Book();

            }
        }
    }
}
