using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Data;
using BookingAPI.Models;
namespace BookingAPI.Repositories
{
    public interface IRezervariRepository {
    Object UserRezervariHistory(int id);
        Task<Rezervare> UserAddRezervare(int userId, int proprietateId,string data);
        Object AdminRezervariHistory(int id);
    }

    public class RezervariRepository : IRezervariRepository
    {
        private readonly BookingAPIContext _context;
        public RezervariRepository(BookingAPIContext context)
        {
            _context = context;
        }
        public  Object  UserRezervariHistory(int id)
        {

            var rez = _context.User.Where(x => x.Id ==id).Join
                (
                    _context.Rezervare,
                    user => user.Id,
                    rezervare => rezervare.userId, (user, rezervare) => new
                    {
                        dataRezervare=rezervare.Data,
                        numeUser = user.Nume,
                        idUser = user.Id,
                        idRezervare = rezervare.Id,
                        idProprietate = rezervare.proprietateId
                    }
                )
                .Join
                (
                    _context.Proprietate,
                    rezervare => rezervare.idProprietate,
                    proprietate => proprietate.Id,
                    (rezervare, proprietate) => new
                    {
                        User=rezervare.numeUser,
                        idUser=rezervare.idUser,
                        idRezervare=rezervare.idRezervare,
                        idProprietate = proprietate.Id,
                        numeProprietate = proprietate.Nume,
                        judetProprietate=proprietate.Judet,
                        orasProprietate= proprietate.Oras,
                        stradaProprietate= proprietate.Strada,
                        numarProprietate= proprietate.Numar,
                        dataRezervare=rezervare.dataRezervare
                    }
                ).ToList();
                
            return rez;
        }
        public async Task<Rezervare> UserAddRezervare(int userId,int proprietateId,string data)
        {
            Rezervare rez = new Rezervare();
            rez.proprietateId = proprietateId;
            rez.userId = userId;
            rez.Data = Convert.ToDateTime(data);
            _context.Rezervare.Add(rez);
            await _context.SaveChangesAsync();
            return rez;
        }

        public Object AdminRezervariHistory(int id)
        {

            var rez = _context.Proprietate.Where(x => x.userId == id).Join(
                _context.Rezervare,
                proprietate => proprietate.Id,
                rezervare => rezervare.proprietateId,
                (proprietate, rezervare) => new
                {
                    adminId = proprietate.userId,
                    rezervareId = rezervare.Id,
                    rezervatorId = rezervare.userId,
                    dataRezervare=rezervare.Data
                }).Join(
                _context.User,
                rezervare => rezervare.rezervatorId,
                user => user.Id,
                (rezervare,user) => new
                {
                    nume=user.Nume,
                    prenume =user.Prenume,
                    mail = user.Mail,
                    dataRezervare=rezervare.dataRezervare
                }
                ).
                ToList();

            return rez;
        }

    }
}
