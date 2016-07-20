using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _Proje_Blog_.Models
{
    public class ModelCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ArticleId { get; set; }
        public virtual ModelArticle Article { get; set; }
    }
}