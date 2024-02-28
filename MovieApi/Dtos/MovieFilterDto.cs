using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Dtos
{
    public class MovieFilterDto
    {
        public int maxPageSize = 20;
        public string? NameQuery { get; set; } = "";

        public string GenreQuery {get; set;} = "";

        public bool? IsDescending {get; set;} = false;
        public string? OrderBy { get; set; } = "Id";
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}