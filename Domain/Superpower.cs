using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Superpower
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название суперспособности обязательно должно быть указано")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "В названии суперспособности должно быть не менее 3 и не более 50 символов")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описание суперспособности обязательно должно быть указано")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "В названии суперспособности должно быть не менее 10 и не более 500 символов")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Уровень прокачки должен быть указан")]
        [Range(1, 100, ErrorMessage = "Введите число в диапазоне от 1 до 100")]
        [Display(Name = "Прокачка")]
        public int Rating { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        [Display(Name = "Супергерой")]
        public int? SuperheroId { get; set; }
        public virtual Superhero Superhero { get; set; }
    }
}
