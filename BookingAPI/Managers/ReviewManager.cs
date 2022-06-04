using BookingAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Models;
using BookingAPI.DTOs;

namespace BookingAPI.Managers
{
    public interface IReviewManager {
        Task<string> AddReview(int userId, int notaReview, string textReview,int proprietateId);
        List<GetReviewsProprietateDTO> GetReviewsProprietate(int proprietateId, int userId);
    }
    public class ReviewManager : IReviewManager
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewManager(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<string> AddReview(int userId, int notaReview, string textReview, int proprietateId)
        {
            if (notaReview > 0 && notaReview <= 10) {
                await _reviewRepository.AddReview(userId, notaReview, textReview,proprietateId);
                return "Succes";
            }
            else
                return "Nota trebuie sa fie intre 1 si 10";
        }

        public List<GetReviewsProprietateDTO> GetReviewsProprietate(int proprietateId,int userId)
        {
            return _reviewRepository.GetReviewsProprietate(proprietateId, userId);
        }
    }
}
