using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class CourseParameters
    {
        public CourseParameters()
        {
            OrderBy = "name";
        }

        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public string Name { get; set; }
        public string OrderBy { get; set; }
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