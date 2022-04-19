using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Repositories;
using BookingAPI.Models;
namespace BookingAPI.Managers
{
    public interface IProprietatiManager
    {
        Task<Proprietate> AddProprietate(Proprietate prop);
        Task<List<Proprietate>> GetProprietati();
        Task<Proprietate> DeleteProprietate(int id);
    }

    public class ProprietatiManager : IProprietatiManager
    {
        private readonly IProprietatiRepository _proprietatiRepository;
        public ProprietatiManager(IProprietatiRepository prop)
        {
            _proprietatiRepository = prop;
        }

        public async Task<Proprietate> AddProprietate(Proprietate prop)
        {
            return await _proprietatiRepository.AddProprietate(prop);
        }
        public async Task<List<Proprietate>> GetProprietati()
        {
            return await _proprietatiRepository.GetProprietati();
        }
        public async Task<Proprietate> DeleteProprietate(int id)
        {
            return await _proprietatiRepository.DeleteProprietate(id);
        }
    }
}
