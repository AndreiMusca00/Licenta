﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Models;
using BookingAPI.DTOs;
using BookingAPI.Repositories;
namespace BookingAPI.Managers
{
    public interface IRezervariManager 
    {
        Object UserRezervariHistory(int id);
        Task<string> UserAddRezervare(int userId, int proprietateId, DateTime data);
        Object AdminRezervariHistory(int id);
        List<RezervariProprietateDTO> GetRezervariProrietate(int proprietateId);
        bool HasReview(int userId,int proprietateId);
        Task<string> DeleteRezervare(int rezervareId);
    }
    public class RezervariManager : IRezervariManager
    {
        private readonly IRezervariRepository _rezervariRepository;
        public  RezervariManager(IRezervariRepository rezervariRepository)
        {
            _rezervariRepository = rezervariRepository;
        }

        public async Task<string> UserAddRezervare(int userId, int proprietateId, DateTime data)
        {
            var rezervari = _rezervariRepository.AllRezervationsOfAProperty(proprietateId);
            foreach(Rezervare rez in rezervari)
            {
                if (rez.Data == data)
                    return "Data introdusa e deja rezervata";
            }
            if (data > DateTime.Today)
            {
                await _rezervariRepository.UserAddRezervare(userId, proprietateId, data);
                return "Succes";
            }
            else
            {
                return "Data introdusa nu este corecta";
            }
            
        }
        public Object UserRezervariHistory(int id)
        {
            return _rezervariRepository.UserRezervariHistory(id);
        }
        public  Object AdminRezervariHistory(int id)
        {
            return _rezervariRepository.AdminRezervariHistory(id);
        }
        public List<RezervariProprietateDTO> GetRezervariProrietate(int proprietateId)
        {
            return _rezervariRepository.GetRezervariProprietate(proprietateId);
        }
        public bool HasReview(int userId,int proprietateId)
        {
            return _rezervariRepository.HasReview(userId,proprietateId);
        }

        public async Task<string> DeleteRezervare(int rezervareId)
        {
            return await _rezervariRepository.DeleteRezervare(rezervareId);
        }
    }
}
