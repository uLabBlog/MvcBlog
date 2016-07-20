using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace _Proje_Blog_.Models
{
    public class ModelComment
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public DateTime InsertDate { get; set; }
        public bool Approve { get; set; }

        //NP
        public string ArticleName { get; set; }
        public int ArticleId { get; set; }
        public virtual ModelArticle Article { get; set; }

        public string InsertDateStr
        {
            get
            {
                return InsertDate.ToString("G", CultureInfo.CreateSpecificCulture("de-DE"));
            }
            set { }
        }
    }
}