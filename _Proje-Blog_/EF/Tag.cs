using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _Proje_Blog_.EF
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //NP olarak bu yazılabilir
        public virtual List<Article> Articles { get; set; }

    }
}