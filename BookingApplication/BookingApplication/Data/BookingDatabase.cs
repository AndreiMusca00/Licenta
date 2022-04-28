﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookingApplication.Models;
using BookingApplication.DTOs;
namespace BookingApplication.Data
{
    public class BookingDatabase
    {
        IRestService restService;
        IuserRestService _userRestService;
        public BookingDatabase(IRestService service, IuserRestService iuserRestService)
        {
            restService = service;
            _userRestService = iuserRestService;
        }
        //user rest service 
        /*
         * 
         * 
         * 
         */
        public Task<string> RegisterUser(UserRegisterDTO user)
        {
            return _userRestService.RegisterUserAsync(user);
        }

        public Task<LoginToken> LoginUserAsync(UserLoginDTO user)
        {
            return _userRestService.LoginUserAsync(user);
        }
        /*
         * 
         * 
         * 
         */

        public Task<List<Users>> GetUsersAsync()
        {
            
            return restService.RefreshDataAsync();
        }
        public Task SaveUserAsync(Users item, bool isNewItem = true)
        {
            return restService.SaveUserAsync(item, isNewItem);
        }
        public Task DeleteUserAsync(Users item)
        {
            return restService.DeleteUserAsync(item.ID);
        }
        //vizualizare proprietate 
        public Task<List<Proprietate>> GetProprietati()
        {
            return _userRestService.GetProprietati();
        }
        public Task<string> AddRezervare(int proprietateId,DateTime dataRezervare)
        {
            return _userRestService.AddRezervare(proprietateId, dataRezervare);
        }
       
    }
}
