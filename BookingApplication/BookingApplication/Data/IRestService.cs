using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookingApplication.Models;

namespace BookingApplication.Data
{
    public interface IRestService
    {
        Task<List<Users>> RefreshDataAsync();
        Task SaveUserAsync(Users item, bool isNewItem);
        Task DeleteUserAsync(int id);
        Task<List<Proprietate>> GetProprietati();


    }
}
