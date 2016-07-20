using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace _Proje_Blog_.EF
{
    public class BlogContext : DbContext
    {

        public BlogContext()
        {
            
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogContext, BlogConfiguration>());
        }

       
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<ClientIPs> ClientIPs { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}