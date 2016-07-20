using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _Proje_Blog_.EF
{
    public class Article
    {

        public Article()
        {
            Tags = new List<Tag>();
        }


        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public bool isActive { get; set; }
        public DateTime InsertDate { get; set; }
        public int ReadCount { get; set; }


        //////
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        ////// 


        //Navigation Property
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Tag> Tags { get; set; }
    }
}