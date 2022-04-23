using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Data;
using BookingAPI.DTOs;
using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Repositories
{
    public interface IProprietatiRepository 
    {
        Task<List<Proprietate>> GetProprietati();
        Task<Proprietate> AddProprietate(AddProprietateDTO prop,int userId);
        Task<Proprietate> DeleteProprietate(int id);


        ///Incercare get prop by usr id 
        Task<List<Proprietate>> GetPropByUserId(int userId);
    }

    public class ProprietatiRepository:IProprietatiRepository
    {
        private readonly BookingAPIContext _context;
        public ProprietatiRepository(BookingAPIContext context)
        {
            _context = context;
        }
        public async Task<List<Proprietate>> GetProprietati()
        {
            return await _context.Proprietate.ToListAsync();
        }
     
        public async Task<Proprietate> DeleteProprietate(int id)
        {
            var prop = await _context.Proprietate.FindAsync(id);
            _context.Proprietate.Remove(prop);
            await _context.SaveChangesAsync();
            return prop;
        }
        //try get prop by usr id 
        /*
         * 
         * User section for properties
         */
        public async Task<List<Proprietate>> GetPropByUserId(int userId)
        {
            return await _context.Proprietate.Where(x => x.userId == userId).ToListAsync();
        }
        public async Task<Proprietate> AddProprietate(AddProprietateDTO prop, int userId)
        {
            Proprietate proprietate = new Proprietate();
            proprietate.Nume = prop.Nume;
            proprietate.Judet = prop.Judet;
            proprietate.Oras = prop.Oras;
            proprietate.Strada = prop.Strada;
            proprietate.Numar = prop.Numar;
            proprietate.Pret = prop.Pret;
            proprietate.userId = userId;
            _context.Proprietate.Add(proprietate);
            await _context.SaveChangesAsync();
            return proprietate;
        }

    }
}
