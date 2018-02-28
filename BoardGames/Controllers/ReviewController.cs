using System;
using System.Web.Mvc;
using BoardGames.Models;

namespace BoardGames.Controllers
{
    public class ReviewController : Controller
    {
        private IBoardGameContext _boardGameContext;
        private IReviewContext _reviewContext;

        public ReviewController() : this(new DbBoardGameContext(), new DbReviewContext())
        {
        }

        public ReviewController(IBoardGameContext boardGameContext, IReviewContext reviewContext)
        {
            _boardGameContext = boardGameContext ?? throw new ArgumentNullException();
            _reviewContext = reviewContext ?? throw new ArgumentNullException();
        }

        public ActionResult Create(int boardGameId)
        {
            if (_boardGameContext.GetBoardGame(boardGameId) == null)
                return HttpNotFound();

            var review = new Review {BoardGameId = boardGameId };
            
            return View(review);
        }

        [HttpPost]
        public ActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                _boardGameContext.AddReview(review);
            }
            else
            {
                ModelState.AddModelError("Create", "Something wrong!");
            }
            return RedirectToAction("Details", "BoardGame", new { id = review.BoardGameId });
        }

        [HttpPost]
        public int IncrementAndGetLikes(int reviewId)
        {
            var likesCount = _reviewContext.IncrementAndGetLikes(reviewId);
            if (likesCount != -1) return likesCount;
            Response.StatusCode = 404;
            return -1;
        }

        [HttpPost]
        public void ReportOffensiveReview(int reviewId, string reason)
        {
            _reviewContext.Report(reviewId, reason);
        }
    }
}