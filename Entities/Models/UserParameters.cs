using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class UserParameters
    {
        public UserParameters()
        {
            OrderBy = "name";
        }

        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool ValidAgeRange => MaxAge > MinAge;
        public string Name { get; set; }
        public string OrderBy { get; set; }

        const int maxPageSize = 50;
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
