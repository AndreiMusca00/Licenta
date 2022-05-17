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
    }

    public class userRestService : IuserRestService
    {
        HttpClient client;
        //se va modifica ulterior cu ip-ul si portul corespunzator

        string RegisterUrl = "https://192.168.0.103:45455/api/User/Register/{0}";
        string LoginUrl = "https://192.168.0.103:45455/api/User/Login/{0}";
        string ProprietatiUrl = "https://192.168.0.103:45455/api/Proprietati/all/{0}";
        string ProprietatiAdminUrl = "https://192.168.0.103:45455/api/Proprietati/{0}";
        string RezervariUrl = "https://192.168.0.103:45455/api/Rezervari/{0}";
        string ImagineUrl = "https://192.168.0.103:45455/api/Image/img?idProprietate={0}";
        string ChangePasswordUrl = "https://192.168.0.103:45455/api/User/password{0}";
        string ConnectedUserUrl = "https://192.168.0.103:45455/api/User/ConnectedUser";
        string UpdateUserDetailsUrl = "https://192.168.0.103:45455/api/User/user";

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
        public async Task<List<GetRezervareUserDTO>> GetIstoricRezervariBasic()
        {
            Rezervari = new List<GetRezervareUserDTO>();
            Uri uri = new Uri(string.Format(RezervariUrl, string.Empty));
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
            Proprietati = new List<Proprietate>();
            Uri uri = new Uri(string.Format(ProprietatiAdminUrl, string.Empty));
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
            Uri uri = new Uri(String.Format(ImagineUrl, proprietateId));
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
            string ImagineUrl = "https://192.168.0.103:45455/api/Image/{1}?idProprietate={0}";
            Uri uri = new Uri(String.Format(ImagineUrl,proprietateId, "imagini"));
            var response = await client.GetAsync(uri);
            var content  = await response.Content.ReadAsStringAsync();
            List<string> paths = JsonConvert.DeserializeObject<List<string>>(content);
            return paths;
        }
        public async Task<Proprietate> GetProprietate(int proprietateId)
        {
            string GetProprietateUrl = "https://192.168.0.103:45455/api/Proprietati/proprietateId?proprietateId={0}";
            Uri uri = new Uri(String.Format(GetProprietateUrl, proprietateId));
            var response = await client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            Proprietate proprietate = JsonConvert.DeserializeObject<Proprietate>(content);
            return proprietate;
        }
        public async Task<string> ChangePassword(string password)
        {
            Uri uri = new Uri(string.Format(ChangePasswordUrl,string.Empty));
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
            Uri uri = new Uri(ConnectedUserUrl);
            var response = await client.GetAsync(uri);
            string content = await response.Content.ReadAsStringAsync();
            UpdateUserDTO connectedUser = JsonConvert.DeserializeObject<UpdateUserDTO>(content);
            return connectedUser;
        }
        public async Task<string> UpdateUserDetails(UpdateUserDTO userDetails)
        {
            Uri uri = new Uri(UpdateUserDetailsUrl);
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
    }
}
