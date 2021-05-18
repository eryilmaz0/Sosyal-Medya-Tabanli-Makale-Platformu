using System;
using System.Collections.Generic;
using Project.Core.Entities;
using Project.Entities.Entities;

namespace Project.Entities.Dtos
{
    public class ArticleCategoryDto : DtoBase
    {
        public long Id { get; set; }
        public string Category { get; set; }
        public string CategoryDescription { get; set; }
        public DateTime? Created { get; set; }
        public bool? IsDeleted { get; set; }


        //NAV PROPS
        public List<Article> Articles { get; set; }
    }
}