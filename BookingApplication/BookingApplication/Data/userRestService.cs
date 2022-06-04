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
using BookingAPI.DTOs;

namespace BookingApplication.Data
{
    public interface IuserRestService 
    {
        Task<string> RegisterUserAsync(UserRegisterDTO user);
        Task<LoginToken> LoginUserAsync(UserLoginDTO user);
        Task<List<Proprietate>> GetProprietati();
        Task<string> AddRezervare(int proprietateId, DateTime data);
        Task<List<GetRezervareUserDTO>> GetIstoricRezervariBasic();
        Task<List<Proprietate>> GetProprietatiAdmin();
        Task<string> GetOneImage(int proprietateId);
        Task<Proprietate> GetProprietate(int proprietateId);

        Task<List<string>> GetImages(int proprietateId);
        Task<string> ChangePassword(string password);
        Task<UpdateUserDTO> GetConnectedUser();
        Task<string> UpdateUserDetails(UpdateUserDTO userDetails);
        Task<string> AddProprietate(AddProprietateDTO proprietate);
        Task<List<RezervariProprietateDTO>> GetRezervariProprietate(int proprietateId);
        Task<List<Proprietate>> GetProprietatiFiltered(string filter);
    }

    public class userRestService : IuserRestService
    {
        HttpClient client;
        string generalUrl = "https://192.168.0.105:45455/api";
        public List<Proprietate> Proprietati;
        public List<GetRezervareUserDTO> Rezervari;

        public userRestService()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
        }

        public async Task<string> RegisterUserAsync(UserRegisterDTO user)
        {
            string addUrl = "/User/Register";
            Uri uri = new Uri(generalUrl+addUrl);
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
            string addUrl = "/User/Login";
            Uri uri = new Uri(generalUrl+addUrl);
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            string x = await response.Content.ReadAsStringAsync();
            LoginToken token = JsonConvert.DeserializeObject<LoginToken>(x);
            var authHeader = new AuthenticationHeaderValue("bearer", token.Token);
            client.DefaultRequestHeaders.Authorization = authHeader;
            return token;
        }
        public async Task<List<Proprietate>> GetProprietatiFiltered(string filter)
        {
            string addUrl = "/Proprietati/all?filtru={0}";
            Proprietati = new List<Proprietate>();
            Uri uri = new Uri(generalUrl + String.Format(addUrl,filter));
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
        public async Task<List<Proprietate>> GetProprietati()
        {
            string addUrl = "/Proprietati/all";
            Proprietati = new List<Proprietate>();
            Uri uri = new Uri(generalUrl+addUrl);
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
            string addUrl = "/Rezervari";
            AddRezervareDTO rezervare = new AddRezervareDTO();
            rezervare.dataRezervare = data;
            rezervare.proprietateId = proprietateId;
            Uri uri = new Uri(generalUrl + addUrl);
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
        public async Task<List<GetRezervareUserDTO>> GetIstoricRezervariBasic()
        {
            string addUrl = "/Rezervari";
            Rezervari = new List<GetRezervareUserDTO>();
            Uri uri = new Uri(generalUrl+addUrl);
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Rezervari = JsonConvert.DeserializeObject<List<GetRezervareUserDTO>>(content);
                    
                }
            }catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return Rezervari;
        }
        public async Task<List<Proprietate>> GetProprietatiAdmin()
        {
            string addUrl = "/Proprietati";
            Proprietati = new List<Proprietate>();
            Uri uri = new Uri(generalUrl+addUrl);
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
        public async Task<string> GetOneImage(int proprietateId)
        {
            string addUrl = "/Image/img?idProprietate={0}";
            Uri uri = new Uri(generalUrl+ String.Format(addUrl, proprietateId));
            var response  = await client.GetAsync(uri);
            string imaginePath = await response.Content.ReadAsStringAsync();
            if (imaginePath != "empty")
            {
                return imaginePath;
            }else
            { 
                return "def.jpg";
            }
        }
        public async Task<List<string>> GetImages(int proprietateId)
        {
            string addUrl = "/Image/{1}?idProprietate={0}";
            Uri uri = new Uri(generalUrl + String.Format(addUrl, proprietateId, "imagini"));
            var response = await client.GetAsync(uri);
            var content  = await response.Content.ReadAsStringAsync();
            List<string> paths = JsonConvert.DeserializeObject<List<string>>(content);
            return paths;
        }
        public async Task<Proprietate> GetProprietate(int proprietateId)
        {
            string addUrl = "/Proprietati/proprietateId?proprietateId={0}";
            Uri uri = new Uri(generalUrl+ String.Format(addUrl, proprietateId));
            var response = await client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            Proprietate proprietate = JsonConvert.DeserializeObject<Proprietate>(content);
            return proprietate;
        }
        public async Task<string> ChangePassword(string password)
        {
            string addUrl = "/User/password";
            Uri uri = new Uri(generalUrl+addUrl);
            UserLoginDTO user = new UserLoginDTO();
            user.Password = password;

            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync(uri,content);
            if (response.IsSuccessStatusCode)
            {
                return "Parola schimbata";
            }
            else { 
                return "Fail";
            }
        }
        public async Task<UpdateUserDTO> GetConnectedUser()
        {
            string addUrl = "/User/ConnectedUser";
            Uri uri = new Uri(generalUrl+addUrl);
            var response = await client.GetAsync(uri);
            string content = await response.Content.ReadAsStringAsync();
            UpdateUserDTO connectedUser = JsonConvert.DeserializeObject<UpdateUserDTO>(content);
            return connectedUser;
        }
        public async Task<string> UpdateUserDetails(UpdateUserDTO userDetails)
        {
            string addUrl = "/User/user";
            Uri uri = new Uri(generalUrl+addUrl);
            string json = JsonConvert.SerializeObject(userDetails);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var resposnse = await client.PostAsync(uri, content);
            if (resposnse.IsSuccessStatusCode)
            {
                return "ok";
            }else
            {
                return "fail";
            }
        }
        public async Task<string> AddProprietate(AddProprietateDTO proprietate)
        {
            string addUrl = "/Proprietati";
            Uri uri = new Uri(generalUrl+addUrl);
            string json = JsonConvert.SerializeObject(proprietate);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var resposnse = await client.PostAsync(uri, content);
            if (resposnse.IsSuccessStatusCode)
            {
                return "ok";
            }
            else
            {
                return "fail";
            }
        }
        public async Task<List<RezervariProprietateDTO>> GetRezervariProprietate(int proprietateId)
        {
            string addUrl = "/Rezervari/proprietate?proprietateId={0}";
            Uri uri = new Uri(generalUrl+ String.Format(addUrl, proprietateId));
            var response = await client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            List<RezervariProprietateDTO> rezervari = JsonConvert.DeserializeObject<List<RezervariProprietateDTO>>(content);
            return rezervari;
        }
    }
}
