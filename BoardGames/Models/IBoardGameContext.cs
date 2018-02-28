using System.Collections.Generic;

namespace BoardGames.Models
{
    public interface IBoardGameContext
    {
        void AddBoardGame(BoardGame boardGame);
        void AddReview(Review review);
        List<BoardGame> GetAll();
        int Count();
        List<BoardGame> GetRange(int from, int to);
        BoardGame GetBoardGame(int boardGameId);
        BoardGame Update(BoardGame boardGameData);
        List<BoardGame> GetTop20();
        void AddMark(int boardGameId, int newValue);
    }
}
