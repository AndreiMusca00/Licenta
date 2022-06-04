using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Repositories;
using BookingAPI.Models;
using BookingAPI.DTOs;

namespace BookingAPI.Managers
{
    public interface IProprietatiManager
    {
        Task<Proprietate> AddProprietate(AddProprietateDTO prop,int userId);
        Task<List<Proprietate>> GetProprietati(int userId, string filtru,int? filtruPretMax,int? filtruPretMin);
        Task<Proprietate> DeleteProprietate(int id);
        Task<bool> FindById(int idProprietate);
        Task<List<Proprietate>> GetPropByUserId(int userId);
        Task<Proprietate> GetProprietate(int proprietateId);
    }

    public class ProprietatiManager : IProprietatiManager
    {
        private readonly IProprietatiRepository _proprietatiRepository;
        public ProprietatiManager(IProprietatiRepository prop)
        {
            _proprietatiRepository = prop;
        }

        public async Task<Proprietate> AddProprietate(AddProprietateDTO prop,int userId)
        {         
            return await _proprietatiRepository.AddProprietate(prop, userId);
        }
        public async Task<List<Proprietate>> GetProprietati(int userId, string filtru,int? filtruPretMax, int? filtruPretMin)
        {

            List<Proprietate> proprietati = await _proprietatiRepository.GetProprietati();
            proprietati.RemoveAll(x => x.userId == userId);
            if(filtru != null)
                proprietati.RemoveAll(x => !x.Nume.ToLower().Contains(filtru.ToLower()));
            if (filtruPretMax.HasValue)
                proprietati.RemoveAll(x => x.Pret > filtruPretMax);
            if (filtruPretMin.HasValue)
                proprietati.RemoveAll(x => x.Pret < filtruPretMin);
            return proprietati;
        }
        public async Task<Proprietate> GetProprietate(int proprietateId)
        {
            List<Proprietate> proprietati = await _proprietatiRepository.GetProprietati();
            foreach(Proprietate prop in proprietati)
            {
                if(prop.Id == proprietateId)
                {
                    return prop;
                }
            }
            return null;
        }
        public async Task<Proprietate> DeleteProprietate(int id)
        {
            return await _proprietatiRepository.DeleteProprietate(id);
        }
        public async Task<List<Proprietate>> GetPropByUserId(int userId)
        {
            return await _proprietatiRepository.GetPropByUserId(userId);
        }
        public async Task<bool> FindById(int idProprietate)
        {
            List<Proprietate> proprietati = await _proprietatiRepository.GetProprietati();
            foreach (Proprietate prop in proprietati)
            {
                if (prop.Id == idProprietate)
                    return true;
            }
            return false;
        }
    }
}
