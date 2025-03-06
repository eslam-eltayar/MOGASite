using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Specifications.Blogs
{
    public class BlogWithContentsSpecification : BaseSpecification<Blog>
    {
        public BlogWithContentsSpecification(PaginationDto paginationDto)
        {
            AddInclude();

            ApplyOrderByDescending(b => b.Id);

            var pageIndexHelper = 0;

            if ((paginationDto.PageIndex - 1) < 0)
            {
                pageIndexHelper = 0;
            }
            else
            {
                pageIndexHelper = paginationDto.PageIndex - 1;
            }

            ApplyPagination(pageIndexHelper * paginationDto.PageSize, paginationDto.PageSize);

        }

        public BlogWithContentsSpecification()
        {
            
        }

        public BlogWithContentsSpecification(int id)
            : base(b => b.Id == id)
        {
            AddInclude();
        }

        public BlogWithContentsSpecification(string slug)
            : base(b => b.Slug == slug)
        {
            AddInclude();
        }

        private void AddInclude()
        {
            //Includes.Add(b=>b.BlogContents);
        }
    }
}
