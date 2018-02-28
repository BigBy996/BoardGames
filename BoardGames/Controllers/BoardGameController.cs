using System;
using System.Web.Mvc;
using BoardGames.Models;

namespace BoardGames.Controllers
{
    public class BoardGameController : Controller
    {

        private const int _PAGE_SIZE = 10;
        private IBoardGameContext _boardGameContext;

        public BoardGameController() : this(new DbBoardGameContext())
        {
        }

        public BoardGameController(IBoardGameContext boardGameContext)
        {
            _boardGameContext = boardGameContext ?? throw new ArgumentNullException();
        }

        public ActionResult List(int currentPage = 1)
        {
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            var paginatorNum = currentPage - 2;
            if (paginatorNum < 1)
            {
                paginatorNum = 1;
            }
            var indexListView = new IndexListView()
            {
                BoardGames = _boardGameContext.GetRange((currentPage - 1) * _PAGE_SIZE, _PAGE_SIZE),
                CurrentPage = paginatorNum,
                TotalPage = (_boardGameContext.Count() + _PAGE_SIZE  - 1) / _PAGE_SIZE
            };
            return View(indexListView);
        }

        public ActionResult Top20()
        {
            var boardGames = _boardGameContext.GetTop20();
            return View(boardGames);
        }

        public ActionResult Details(int id = 0)
        {
            ViewBag.Title = "Подробнее о настольной игре";
            var boardGame = _boardGameContext.GetBoardGame(id);

            if (boardGame == null)
                return HttpNotFound();

            return View(boardGame);
        }
        
        [HttpPost]
        public void AddMark(int boardGameId, int mark)
        {
            _boardGameContext.AddMark(boardGameId, mark);
        } 

        public ActionResult Edit(int id = 0)
        {
            var boardGame = _boardGameContext.GetBoardGame(id);

            if (boardGame == null)
                return HttpNotFound();

            return View(boardGame);
        }
        
        [HttpPost]
        public ActionResult Edit(BoardGame editBoardGame)
        {
            var first = _boardGameContext.GetBoardGame(editBoardGame.Id);

            if (first == null)
                return HttpNotFound();

            _boardGameContext.Update(editBoardGame);

            return RedirectToAction("Details", "BoardGame", new {id = editBoardGame.Id});
        }
        
        public ActionResult Create()
        {
            return View(new BoardGame());
        }
        
        [HttpPost]
        public ActionResult Create(BoardGame boardGame)
        {
            if (ModelState.IsValid)
            {
                _boardGameContext.AddBoardGame(boardGame);
            }
            else
            {
                ModelState.AddModelError("Create", "Something went wrong!");
            }
            return RedirectToAction("List", "BoardGame");
        }

    }
}