using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Data;
using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Repositories
{
    public interface IProprietatiRepository 
    {
        Task<List<Proprietate>> GetProprietati();
        Task<Proprietate> AddProprietate(Proprietate prop);
        Task<Proprietate> DeleteProprietate(int id);
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
        public async Task<Proprietate> AddProprietate(Proprietate prop)
        {
            _context.Proprietate.Add(prop);
            await _context.SaveChangesAsync();
            return prop;
        }
        public async Task<Proprietate> DeleteProprietate(int id)
        {
            var prop = await _context.Proprietate.FindAsync(id);
            _context.Proprietate.Remove(prop);
            await _context.SaveChangesAsync();
            return prop;
        }

    }
}
