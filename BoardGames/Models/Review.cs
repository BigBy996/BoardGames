using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace BoardGames.Models
{
    public class Review
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
		
        [Display(Name = "Автор"), Column]
        public string Author { get; set; }

        [Column]
        public int BoardGameId { get; set; }
		
        [Required (ErrorMessage = "Поле Отзыв обязательно для заполнения")]
        [Display(Name = "Отзыв")]
        [Column]
        public string Text { get; set; }

        [Column]
        public int LikeCount { get; set; }

        [Column]
        public string ReportReason { get; set; }
    }
}