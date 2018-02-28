using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace BoardGames.Models
{
    [Table]
    public class BoardGame
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Display(Name = "Название"), Column]
        public string Name { get; set; }

        [Display(Name = "Производитель"), Column]
        public string CreatedBy { get; set; }

        [Display(Name = "Минимальное число игроков"), Column]
        public int MinPlayers { get; set; }

        [Display(Name = "Максимальное число игроков"), Column]
        public int MaxPlayers { get; set; }

        [Display(Name = "Осталось в наличии"), Column]
        public int LeftAmount { get; set; }

        [Display(Name = "Короткое описание"), Column]
        public string ShortDescription { get; set; }

        [Display(Name = "Цена"), Column]
        public int Price { get; set; }
        
        [Display(Name = "Число отзывов"), Column]
        public int CountReview { get; set; }
        
        [Display(Name = "Оценка"), Column]
        public int AverageMark { get; set; }

        public void Update(BoardGame boardGame)
        {
            Name = boardGame.Name;
            CreatedBy = boardGame.CreatedBy;
            MinPlayers = boardGame.MinPlayers;
            MaxPlayers = boardGame.MaxPlayers;
            LeftAmount = boardGame.LeftAmount;
            ShortDescription = boardGame.ShortDescription;
            Price = boardGame.Price;
            CountReview = boardGame.CountReview;
            AverageMark = boardGame.AverageMark;
        }

        public List<Review> Reviews { get; set; }
    }
}