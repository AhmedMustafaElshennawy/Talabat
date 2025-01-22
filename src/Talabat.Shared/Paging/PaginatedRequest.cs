﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Shared.Paging.Paging
{
    public record PaginatedRequest
    {
        public int PageNumber { get; set; } = 0;
        private int pageSize = 5;
        public int PageSize 
        {
            get => pageSize;
            set => pageSize = value > 10 ? 10 : value;
        }
        public string? SortBy { get; set; } 
        public string SortOrder { get; set; }     
    }
}