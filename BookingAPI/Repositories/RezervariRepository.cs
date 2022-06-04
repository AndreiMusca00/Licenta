using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Data;
using BookingAPI.Models;
using BookingAPI.DTOs;
namespace BookingAPI.Repositories
{
    public interface IRezervariRepository {
    List<GetRezervareUserDTO> UserRezervariHistory(int id);
        Task<Rezervare> UserAddRezervare(int userId, int proprietateId,DateTime data);
        Object AdminRezervariHistory(int id);
        List<RezervariProprietateDTO> GetRezervariProprietate(int proprietateId);
        List<Rezervare> AllRezervationsOfAProperty(int proprietateId);
    }

    public class RezervariRepository : IRezervariRepository
    {
        private readonly BookingAPIContext _context;
        public RezervariRepository(BookingAPIContext context)
        {
            _context = context;
        }
        public List<RezervariProprietateDTO> GetRezervariProprietate(int proprietateId)
        {
            List<RezervariProprietateDTO> rezervari = new List<RezervariProprietateDTO>();
            var rez = _context.Rezervare.Where(x => x.proprietateId == proprietateId).Join
                (
                    _context.User,
                    rezervare => rezervare.userId,
                    user => user.Id, (rezervare,user) => new
                    { 
                        dataRezervare = rezervare.Data,
                        userNume = user.Nume,
                        userPrenume = user.Prenume,
                        userMail = user.Mail,
                        userTelefon = user.Telefon
                    }).ToList();
            foreach (var variabila in rez)
            {
                RezervariProprietateDTO rezervare = new RezervariProprietateDTO();
                rezervare.dataRezervare = variabila.dataRezervare;
                rezervare.nume = variabila.userNume;
                rezervare.prenume = variabila.userPrenume;
                rezervare.mail = variabila.userMail;
                rezervare.telefon = variabila.userTelefon;
                rezervari.Add(rezervare);
            }
            return rezervari;

        }
        public  List<GetRezervareUserDTO>  UserRezervariHistory(int id)
        {
            List<GetRezervareUserDTO> rezervari = new List<GetRezervareUserDTO>();
            
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
            foreach( var variabila in rez)
            {
                GetRezervareUserDTO rezervare = new GetRezervareUserDTO();
                rezervare.dataRezervare = variabila.dataRezervare;
                rezervare.idProprietate = variabila.idProprietate;
                rezervare.idUser = variabila.idUser;
                rezervare.judetProprietate = variabila.judetProprietate;
                rezervare.numarProprietate = variabila.numarProprietate;
                rezervare.numeProprietate = variabila.numeProprietate;
                rezervare.orasProprietate = variabila.orasProprietate;
                rezervare.stradaProprietate = variabila.stradaProprietate;
                rezervare.user = variabila.User;
                rezervari.Add(rezervare);
            }
            return rezervari;
        }
        public async Task<Rezervare> UserAddRezervare(int userId,int proprietateId,DateTime data)
        {
            Rezervare rez = new Rezervare();
            rez.proprietateId = proprietateId;
            rez.userId = userId;
            rez.Data = data;
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

        public List<Rezervare> AllRezervationsOfAProperty(int proprietateId)
        {
            return _context.Rezervare.Where(rez => rez.proprietateId == proprietateId).ToList();
        }

    }
}
