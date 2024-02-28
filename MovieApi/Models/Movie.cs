using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace MovieApi.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int Id {get; set;}

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Overview { get; set; }

        [Required]
        public decimal Popularity { get; set; }

        [Required]
        public int VoteCount { get; set; }

        [Required]
        public decimal VoteAverage { get; set; }

        [Required]
        public string OriginalLanguage { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string PosterUrl { get; set; }
    }
}