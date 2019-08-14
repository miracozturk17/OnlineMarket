using System;

namespace OM.WebUI.Models
{
    public class BaseListingViewModel
    {

    }

    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            if (pageSize == 0) pageSize = 10;

            var totalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
            var currentPage = page ?? 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        private int TotalItems { get; }
        public int CurrentPage { get; }
        private int PageSize { get; }
        public int TotalPages { get; }
        public int StartPage { get; }
        public int EndPage { get; }
    }
}