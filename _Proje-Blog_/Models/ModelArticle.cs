using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _Proje_Blog_.Models
{
    public class ModelArticle
    {

        public ModelArticle()
        {
            Tags = new List<ModelTag>();
            comments = new List<ModelComment>();
        }

        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public bool isActive { get; set; }
        public DateTime InsertDate { get; set; }
        public string InsertDateStr { get { return InsertDate.ToString("g", CultureInfo.CreateSpecificCulture("de-DE")); } }
        public int ReadCount { get; set; }


        //////
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        ////// 

        public string CategoryName { get; set; }

        //Navigation Property
        public int CategoryId { get; set; }
        public virtual ModelCategory Category { get; set; }

        public virtual List<ModelTag> Tags { get; set; }
        public List<ModelComment> comments { get; set; }

        public List<SelectListItem> Categories { get; set; }
    }
}