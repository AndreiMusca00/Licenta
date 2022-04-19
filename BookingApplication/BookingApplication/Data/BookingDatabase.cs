using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookingApplication.Models;
namespace BookingApplication.Data
{
    public class BookingDatabase
    {
        IRestService restService;
        public BookingDatabase(IRestService service)
        {
            restService = service;
        }


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
            return restService.GetProprietati();
        }
       
    }
}
