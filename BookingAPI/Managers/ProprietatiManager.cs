﻿using System;
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
        Task<List<Proprietate>> GetProprietati();
        Task<Proprietate> DeleteProprietate(int id);
        Task<bool> FindById(int idProprietate);
        Task<List<Proprietate>> GetPropByUserId(int userId);
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
        public async Task<List<Proprietate>> GetProprietati()
        {
            return await _proprietatiRepository.GetProprietati();
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
