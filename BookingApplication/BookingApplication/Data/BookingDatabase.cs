using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookingApplication.Models;
using BookingApplication.DTOs;
using BookingAPI.DTOs;

namespace BookingApplication.Data
{
    public class BookingDatabase
    {
        
        IuserRestService _userRestService;
        public BookingDatabase( IuserRestService iuserRestService)
        {
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

        public Task<List<Proprietate>> GetProprietatiFiltered(string filter,int lowerValue,int upperValue)
        {
            return _userRestService.GetProprietatiFiltered(filter,lowerValue,upperValue);
        }
        public Task<string> AddRezervare(int proprietateId,DateTime dataRezervare)
        {
            return _userRestService.AddRezervare(proprietateId, dataRezervare);
        }
       
        public Task<List<GetRezervareUserDTO>> GetIstoricRezervariBasic()
        {
            return _userRestService.GetIstoricRezervariBasic();
        }

        public Task<List<Proprietate>> GetProprietatiAdmin()
        {
            return _userRestService.GetProprietatiAdmin();
        }


        /*
         * 
         * 
         * Image handlers 
         */
        public Task<string> GetOneImagePath(int proprietateId)
        {
            return _userRestService.GetOneImage(proprietateId);
        }

        public Task<List<string>> GetImages(int proprietateId)
        {
            return _userRestService.GetImages(proprietateId);
        }

        public Task<Proprietate> GetProprietate(int proprietateId)
        {
            return _userRestService.GetProprietate(proprietateId);
        }

        public Task<string> ChangePassword(string password)
        {
            return _userRestService.ChangePassword(password);
        }

        public Task<UpdateUserDTO> GetConnectedUser()
        {
            return _userRestService.GetConnectedUser();
        }

        public Task<string> UpdateUserDetails(UpdateUserDTO user)
        {
            return _userRestService.UpdateUserDetails(user);
        }

        public Task<string> AddProprietate(AddProprietateDTO proprietate)
        {
            return _userRestService.AddProprietate(proprietate);
        }

        public Task<List<RezervariProprietateDTO>> GetRezervariProprietate(int proprietateId)
        {
            return _userRestService.GetRezervariProprietate(proprietateId);
        }
        public Task<List<GetReviewsProprietateDTO>> GetReviewsProprietate(int proprietateId)
        {
            return _userRestService.GetReviewsProprietate(proprietateId);
        }
        public Task<bool> HasReviewRights(int proprietateId)
        {
            return _userRestService.HasReviewRights(proprietateId);
        }

        public Task<string> AddReview(int proprietateId, string textReview, int notaReview)
        {
            return _userRestService.AddReview(proprietateId, textReview, notaReview);
        }
    }
}
