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
    public class BookPurchaseRepository : IBookPurchaseRepository
    {
        private readonly IConfiguration _config;
        private string baseUrl;         
        public BookPurchaseRepository(IConfiguration config)
        {
            _config = config;
            baseUrl = _config.GetValue<string>("baseUrl")+"/bookpurchase";
        }
        public async Task<bool> AddUpdate(BookPurchase model)
        {
            try
            {
                string path = baseUrl + "/addupdate";
                string jsonContent = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(path, content);
                string jsonData = await response.Content.ReadAsStringAsync();
                Status status = JsonConvert.DeserializeObject<Status>(jsonData);
                return Convert.ToBoolean(status.StatusCode);
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
                string path = baseUrl + $"/delete/{id}";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(path);
                string jsonData = await response.Content.ReadAsStringAsync();
                Status status = JsonConvert.DeserializeObject<Status>(jsonData);
                return Convert.ToBoolean(status.StatusCode);
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<BookPurchase> FindById(int id)
        {
            try
            {
                string path = baseUrl + $"/getbyid/{id}";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(path);
                string jsonData = await response.Content.ReadAsStringAsync();
                BookPurchase bookPurchase = JsonConvert.DeserializeObject<BookPurchase>(jsonData);
                return bookPurchase;
            }
            catch (Exception ex)
            {
                return new BookPurchase();
            }
        }

        public async Task<IEnumerable<BookPurchase>> GetAll()
        {
            try
            {
                string path = baseUrl + "/getall";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(path);
                string jsonData = await response.Content.ReadAsStringAsync();
                IEnumerable<BookPurchase> bookPurchaseData = JsonConvert.DeserializeObject<IEnumerable<BookPurchase>>(jsonData);
                return bookPurchaseData;          
            }
            catch(Exception ex)
            {
                return new List<BookPurchase>();
            }
        }
    }
}
