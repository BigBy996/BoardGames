using System.Collections.Generic;

namespace BoardGames.Models
{
    public interface IReviewContext
    {
        int IncrementAndGetLikes(int reviewId);

        List<Review> GetAll();
        void Report(int reviewId, string reason);
    }
}