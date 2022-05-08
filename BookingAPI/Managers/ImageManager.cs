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
    }
}
