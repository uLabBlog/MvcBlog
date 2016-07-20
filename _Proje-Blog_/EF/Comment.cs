using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _Proje_Blog_.EF
{
    public class Comment
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public DateTime InsertDate { get; set; }
        public bool Approve { get; set; }

        //NP
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}