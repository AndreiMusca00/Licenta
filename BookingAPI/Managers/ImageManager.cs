using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Repositories;
using BookingAPI.Models;
namespace BookingAPI.Managers
{
    public interface IImageManager
    {
        Task<Image> AddImage(int idProprietate, string pathImagine);
        bool CheckPhotoUpdated(int idProprietate, string pathImagine);
        Task<string> OneImagePath(int proprietateId);
        

        //try 
        string ImagineTry(int proprietateId);

        //images ID 
        List<string> GetImagesList(int proprietateId);
    }
    public class ImageManager : IImageManager
    {
        private readonly IImageRespository _imageRepository;
        public ImageManager (IImageRespository imageRespository)
        {
            _imageRepository = imageRespository;
        }

        public async Task<Image> AddImage (int idProprietate, string pathImagine)
        {
            return await _imageRepository.AddImage(idProprietate, pathImagine);
        }
        public bool CheckPhotoUpdated(int idProprietate, string pathImagine)
        {
            return _imageRepository.CheckPhotoUpdated(idProprietate, pathImagine);
        }

        public async Task<string> OneImagePath (int proprietateId)
        {
            return await _imageRepository.OneImagePath(proprietateId);
        }

        //Incercare eficienta 
        public string ImagineTry(int proprietateId)
        {
            List<string> list = _imageRepository.GetImagesList(proprietateId);
            if (list.Count !=0 )
                return list[0];
            else return "empty";
        }

        //Get images list of paths 
        public List<string> GetImagesList(int proprietateId)
        {
            List<string> list = _imageRepository.GetImagesList(proprietateId);
            if (list.Count != 0)
                return list;
            else
            {
                list.Add("def.jpg");
                return list;
            }
        }
    }
}
