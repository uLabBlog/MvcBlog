using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace _Proje_Blog_.EF
{
    public class BlogConfiguration : DbMigrationsConfiguration<BlogContext>
    {
        public BlogConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }


        protected override void Seed(BlogContext context)
        {
            if (context.Categories.ToList().Count < 1)
            {
                Category cat1 = new Category()
                {
                    Name = "OOP"
                };
                Category cat2 = new Category()
                {
                    Name = "C#"
                };
                Category cat3 = new Category()
                {
                    Name = "MVC"
                };
                Category cat4 = new Category()
                {
                    Name = "ORM"
                };
                Category cat5 = new Category()
                {
                    Name = "Lorem Ipsum"
                };
                context.Categories.Add(cat1);
                context.Categories.Add(cat2);
                context.Categories.Add(cat3);
                context.Categories.Add(cat4);
                context.Categories.Add(cat5);

                Tag t1 = new Tag()
                {
                    Name = "Uygulama"
                };
                Tag t2 = new Tag()
                {
                    Name = "Eğitim"
                };
                Tag t3 = new Tag()
                {
                    Name = "Video"
                };
                context.Tags.Add(t1);
                context.Tags.Add(t2);
                context.Tags.Add(t3);

                context.SaveChanges();

                Article art1 = new Article()
                {
                    CategoryId = cat2.Id,
                    Subject = "Lorem Ipsum Nedir?",
                    Summary = "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır.",
                    Content = @"Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia'daki Hampden-Sydney College'dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan 'consectetur' sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan 'de Finibus Bonorum et Malorum' (İyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan 'Lorem ipsum dolor sit amet' 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 

                      1500'lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir. Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H. Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.",
                    InsertDate = DateTime.Now,
                    isActive = true,
                };
                context.Articles.Add(art1);

                art1.Tags.Add(t1);
                art1.Tags.Add(t3);

                context.SaveChanges();

                //FirstData Part Of Comment

                context.Comment.Add(new Comment
                {
                    FullName = "Gökhan Bezirci",
                    Email = "gokhan-bezirci@hotmail.com",
                    Content = "Bu blog başarılı olmuş",
                    ArticleId = 1,
                    InsertDate = new DateTime(2016, 6, 10, 2, 21, 44)
                });

                context.Comment.Add(new Comment
                {
                    FullName = "Burak Kılıç",
                    Email = "burak.kilic@hotmail.com",
                    Content = "Bu blog başarılı olmamış değiştirilsin",
                    ArticleId = 1,
                    InsertDate = new DateTime(2016, 6, 6, 2, 02, 45)
                });

                context.Comment.Add(new Comment
                {
                    FullName = "Kerim Çelik",
                    Email = "kerim.celik@hotmail.com",
                    Content = "Bu blog çok guzel olmuş isim neydi acaba not olıcamda",
                    ArticleId = 1,
                    InsertDate = new DateTime(2016, 6, 11, 20, 02, 45)
                });

                context.SaveChanges();

            }

            base.Seed(context);
        }
    }
}