using BookingAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Models;
using BookingAPI.DTOs;

namespace BookingAPI.Repositories
{
    public interface IReviewRepository {
        Task<string> AddReview(int userId, int notaReview, string textReview,int proprietateId);
        List<GetReviewsProprietateDTO> GetReviewsProprietate(int proprietateId, int userId);
    }
    public class ReviewRepository : IReviewRepository
    {
        private readonly BookingAPIContext _context;
        public ReviewRepository(BookingAPIContext context)
        {
            _context = context;
        }

        public async Task<string> AddReview(int userId, int notaReview, string textReview,int proprietateId)
        {
            Review review = new Review()
            {
                proprietateId = proprietateId,
                Text=textReview,
                Nota=notaReview,
                userId=userId
            };
            _context.Review.Add(review);
            await _context.SaveChangesAsync();
            return "ok";
        }

        public List<GetReviewsProprietateDTO> GetReviewsProprietate(int proprietateId,int userId)
        {
            List<GetReviewsProprietateDTO> reviews = new List<GetReviewsProprietateDTO>();
            var rez = _context.Review.Where(review => review.proprietateId == proprietateId & review.userId == userId).Join(
                _context.User,
                review => review.userId,
                user => user.Id,
                (review, user) => new
                {
                    reviewText = review.Text,
                    reviewNota = review.Nota,
                    userPrenume = user.Prenume
                }).ToList();
            foreach (var rev in rez)
            {
                GetReviewsProprietateDTO review= new GetReviewsProprietateDTO();
                review.Nume = rev.userPrenume;
                review.Nota = rev.reviewNota;
                review.Text = rev.reviewText;
                reviews.Add(review);
            }
            return reviews;
        }

    }
}
