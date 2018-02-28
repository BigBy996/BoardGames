using System.Collections.Generic;

namespace BoardGames.Models
{
    public class IndexListView
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public List<BoardGame> BoardGames { get; set; }
    }
}