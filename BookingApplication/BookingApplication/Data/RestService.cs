using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookingApplication.Models;
using Newtonsoft.Json;
namespace BookingApplication.Data
{
    public class RestService : IRestService
    {
        HttpClient client;
        //se va modifica ulterior cu ip-ul si portul corespunzator
        string RestUrl = "https://192.168.0.100:45455/api/Users/{0}";
        string LoginUrl = "https://192.168.0.100:45455/api/Users/login/";
        public List<Users> Items { get; private set; }
        public Users Item {get;set;}
        public RestService()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
        }
        //Incercare login 
        public async Task<Users> GetUserByUserName(string UsNa)
        {
            Item = new Users();
            Uri uri = new Uri(string.Format(LoginUrl, UsNa));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Item = JsonConvert.DeserializeObject<Users>(content);
                }
            }catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return Item;

        }
        //FInal incercari 
        public async Task<List<Users>> RefreshDataAsync()
        {
            Items = new List<Users>();
            Uri uri = new Uri(string.Format(RestUrl, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Users>>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return Items;
        }
       
        
        
        public async Task SaveUserAsync(Users item, bool isNewItem = true)
        {
            Uri uri = new Uri(string.Format(RestUrl, string.Empty));
            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(@"\tTodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task DeleteUserAsync(int id)
        {
            Uri uri = new Uri(string.Format(RestUrl, id));
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(@"\tTodoItem successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
