using BookingAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Models;
namespace BookingAPI.Repositories
{
    public interface IImageRespository {
        Task<Image> AddImage(int idProprietate, string pathImagine);
        bool CheckPhotoUpdated(int idProprietate, string pathImagine);
        Task<string> OneImagePath(int proprietateId);

        //try 
        string ImagineTry(int idRezervare);
        List<string> GetImagesList(int proprietateId);
    }

    public class ImageRepository : IImageRespository
    {
        private readonly BookingAPIContext _context;
        public ImageRepository(BookingAPIContext context)
        {
            _context = context;
        }

        public async Task<Image> AddImage(int idProprietate, string pathImagine)
        {
            Image image = new Image();
            image.proprietate.Id= idProprietate;
            image.imagePath = pathImagine;

            _context.Imagine.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public bool CheckPhotoUpdated(int idProprietate, string pathImagine)
        {
            if (_context.Imagine.FirstOrDefault(x => x.proprietate.Id== idProprietate && x.imagePath == pathImagine) != null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> OneImagePath(int proprietateId)
        {
            Image img = _context.Imagine.FirstOrDefault(x => x.proprietate.Id == proprietateId);
            return img.imagePath.ToString();
        } 


        //try eficienta 
        public string ImagineTry(int idRezervare)
        {
            Image img = _context.Imagine.FirstOrDefault(x => x.proprietate.Id == idRezervare);
            if (img != null)
                return img.imagePath.ToString();
            else return ""; 
        }

        //Get image list of paths 
        public List<string> GetImagesList(int proprietateId)
        {
            return _context.Imagine.Where(im => im.proprietate.Id==proprietateId).Select(x => x.imagePath).ToList();
        }
    }
}
