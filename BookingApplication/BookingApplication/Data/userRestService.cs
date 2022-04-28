using System;
using System.Collections.Generic;
using System.Text;
using BookingApplication.Models;
using BookingApplication.DTOs;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

namespace BookingApplication.Data
{
    public interface IuserRestService 
    {
        Task<string> RegisterUserAsync(UserRegisterDTO user);
        Task<LoginToken> LoginUserAsync(UserLoginDTO user);
        Task<List<Proprietate>> GetProprietati();
        Task<string> AddRezervare(int proprietateId, DateTime data);
    }

    public class userRestService : IuserRestService
    {
        HttpClient client;
        //se va modifica ulterior cu ip-ul si portul corespunzator

        string RegisterUrl = "https://192.168.0.128:45455/api/User/Register/{0}";
        string LoginUrl = "https://192.168.0.128:45455/api/User/Login/{0}";
        string ProprietatiUrl = "https://192.168.0.128:45455/api/Proprietati/all/{0}";
        string RezervariUrl = "https://192.168.0.128:45455/api/Rezervari/{0}";

        public List<Proprietate> Proprietati;
        public userRestService()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
        }
        public async Task<string> RegisterUserAsync(UserRegisterDTO user)
        {
            Uri uri = new Uri(string.Format(RegisterUrl, string.Empty));
            try
            {
                string json = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    return "User creat cu succes";
                }
                else
                {
                    return "Eroare: User existent";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return "";
        }
        public async Task<LoginToken> LoginUserAsync(UserLoginDTO user)
        {
            Uri uri = new Uri(string.Format(LoginUrl, string.Empty));
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            string x = await response.Content.ReadAsStringAsync();
            LoginToken token = JsonConvert.DeserializeObject<LoginToken>(x);
            //adaugare token in header 
            //var handler = new JwtSecurityTokenHandler();
           // var tokenn = handler.ReadJwtToken(token.Token);
            
            var authHeader = new AuthenticationHeaderValue("bearer", token.Token);
            client.DefaultRequestHeaders.Authorization = authHeader;
            return token;
        }

        public async Task<List<Proprietate>> GetProprietati()
        {
            Proprietati = new List<Proprietate>();
            Uri uri = new Uri(string.Format(ProprietatiUrl, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Proprietati = JsonConvert.DeserializeObject<List<Proprietate>>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return Proprietati;

        }

        public async Task<string> AddRezervare(int proprietateId, DateTime data)
        {
            //de completat pentru add rezervare cu interpolation 
            AddRezervareDTO rezervare = new AddRezervareDTO();
            rezervare.dataRezervare = data;
            rezervare.proprietateId = proprietateId;
            Uri uri = new Uri(string.Format(RezervariUrl,string.Empty));
            string json = JsonConvert.SerializeObject(rezervare);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
          
            if(response.IsSuccessStatusCode)
            {
                return "ok";
            }else
            {
                return "fail";
            }
        }
    }
}
