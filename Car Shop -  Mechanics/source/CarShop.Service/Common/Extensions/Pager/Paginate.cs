//Local
using CarShop.Models.Pagination;
//Nuget packets
using Microsoft.EntityFrameworkCore;
//Public
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Service.Common.Extensions.Pager
{
    public class Paginate<T>
    {
        public Paginate(IEnumerable<T> entities, int count, int pageNumber, int postsPerPage,int totalPages)
        {
            this.Metadata = new Metadata
            {
                PerPage = postsPerPage,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                TotalEntites = count
            };

            this.Entities = entities;
        }

        public Metadata Metadata { get; set; }

        public IEnumerable<T> Entities { get; set; }

        public static async Task<Paginate<T>> ToPaginatedCollection(IQueryable<T> entities, int pageNumber, int postsPerPage)
        {
            var count = await entities.CountAsync();

            int totalPages = (int)Math.Ceiling(count / (double)postsPerPage);

            pageNumber = ValidatePageNumber(pageNumber, totalPages);
            postsPerPage = ValidatePerPage(postsPerPage, count);

            var chunk = ValidateChunk(entities, pageNumber, postsPerPage);

            return new Paginate<T>(chunk, count, pageNumber, postsPerPage, totalPages);
        }
        private static IQueryable<T> ValidateChunk(IQueryable<T> entities, int pageNumber, int postsPerPage)
        {
            IQueryable<T> chunk;

            if (postsPerPage == 0 && pageNumber == 0)
            {
                chunk = entities;
            }
            else
            {               
                chunk = entities.Skip((pageNumber - 1) * postsPerPage).Take(postsPerPage);
            }

            return chunk;
        }

        private static int ValidatePerPage(int postsPerPage, int count)
        {
            if (postsPerPage <= 0)
            {
                postsPerPage = count;
            }
            else if (postsPerPage > count)
            {
                postsPerPage = count;
            }

            return postsPerPage;
        }

        private static int ValidatePageNumber(int pageNumber, int totalPages)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }
            else if (pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }

            return pageNumber;
        }
    }
}