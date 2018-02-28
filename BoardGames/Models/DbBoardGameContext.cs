using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.SqlQuery;

namespace BoardGames.Models
{
    public class DbBoardGameContext : IBoardGameContext
    {
        public void AddBoardGame(BoardGame boardGame)
        {
            using (var db = new Database())
            {
                db.Insert(boardGame);
            }
        }

        public void AddReview(Review review)
        {
            using (var db = new Database())
            {
                db.BoardGame
                    .Where(p => p.Id == review.BoardGameId)
                    .Set(p => p.CountReview, (from f in db.BoardGame
                                                 where f.Id == review.BoardGameId
                                                 select f.CountReview).First() + 1)
                    .Update();
                db.InsertWithIdentity(review);
            }
        }


        public void AddMark(int boardGameId, int newValue)
        {
            using (var db = new Database())
            {
                db.BoardGame
                    .Where(p => p.Id == boardGameId)
                    .Set(p => p.AverageMark, ((from f in db.BoardGame
                                                   where f.Id == boardGameId
                                                   select f.AverageMark).First() + newValue) / 2)
                    .Update();
            }
        }

        public List<BoardGame> GetTop20()
        {
            using (var db = new Database())
            {
                var query = from f in db.BoardGame
                    orderby f.CountReview descending
                    select f;
                return query.Take(20).ToList();
            }
        }

        public List<BoardGame> GetAll()
        {
            using (var db = new Database())
            {
                var query = (from b in db.BoardGame
                    select b);
                return query.ToList();
            }
        }

        public int Count()
        {
            using (var db = new Database())
            {
                var query = (from b in db.BoardGame
                    select b);
                return query.Count();
            }
        }

        public List<BoardGame> GetRange(int from, int count)
        {
            using (var db = new Database())
            {
                return (from b in db.BoardGame select b).Skip(from).Take(count).ToList();
            }
        }

        public BoardGame GetBoardGame(int id)
        {
            using (var db = new Database())
            {
                var boardGame = db.BoardGame.SingleOrDefault(x => x.Id == id);
                boardGame.Reviews = (from r in db.Review where r.BoardGameId == boardGame.Id select r).Take(10).ToList();
                return boardGame;
            }
        }

        public BoardGame Update(BoardGame boardGame)
        {
            if (boardGame == null)
                throw new ArgumentNullException(nameof(boardGame));

            using (var db = new Database())
            {
                var newBoardGame =
                    db.BoardGame.SingleOrDefault(x => x.Id == boardGame.Id);

                if (boardGame == null)
                    return null;

                db.Update(boardGame);

                newBoardGame.Update(boardGame);
                return newBoardGame;
            }
        }

        private class Database : DataConnection
        {
            public Database() : base("Main")
            {
            }

            public ITable<BoardGame> BoardGame => GetTable<BoardGame>();
            public ITable<Review> Review => GetTable<Review>();
        }
    }
}