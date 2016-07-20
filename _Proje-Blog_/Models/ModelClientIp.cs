using _Proje_Blog_.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _Proje_Blog_.Models
{
    public class ModelClientIp
    {
        public int ID { get; set; }
        public string ClientIpNo { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}