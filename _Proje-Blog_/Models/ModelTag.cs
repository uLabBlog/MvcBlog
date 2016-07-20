using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _Proje_Blog_.Models
{
    public class ModelTag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //NP olarak bu yazılabilir
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public virtual List<ModelArticle> Articles { get; set; }
    }
}