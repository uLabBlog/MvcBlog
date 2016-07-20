using _Proje_Blog_.EF;
using _Proje_Blog_.EF.CRUD;
using _Proje_Blog_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _Proje_Blog_.Controllers
{
    public class HomeController : Controller
    {
        ArticleRepository _artRepo;
        CommentRepository _comRepo;
        CategoryRepository _catRepo;
        ClientIpRepository _cIpRepo;
        public HomeController()
        {
            _artRepo = new ArticleRepository();
            _comRepo = new CommentRepository();
            _catRepo = new CategoryRepository();
            _cIpRepo = new ClientIpRepository();
        }

        public ActionResult Index(int? catId)
        {
            var model = new List<ModelArticle>();
            if (!catId.HasValue)
            {
                model = _artRepo.GetAll().Where(x => x.isActive == true).Select(x => new ModelArticle()
                {
                    Id = x.Id,
                    CategoryName = x.Category.Name,
                    Subject = x.Subject,
                    Summary = x.Summary,
                    InsertDate = x.InsertDate,
                    ReadCount = x.ReadCount,
                    LikeCount = x.LikeCount,
                    DislikeCount = x.DislikeCount
                }).ToList();
            }
            else
            {
                model = _artRepo.GetAll().Where(x => x.isActive == true && x.CategoryId == catId).Select(x => new ModelArticle()
                {
                    Id = x.Id,
                    CategoryName = x.Category.Name,
                    Subject = x.Subject,
                    Summary = x.Summary,
                    InsertDate = x.InsertDate,
                    ReadCount = x.ReadCount,
                    LikeCount = x.LikeCount,
                    DislikeCount = x.DislikeCount
                }).ToList();
            }
            return View(model);
        }

        public ActionResult Goster(int id)
        {
            var gelenArticle = _artRepo.GetById(id);
            if (!gelenArticle.isActive)
            {
                //şimdilik makale listesi sayfasına yönlendiriyoruz. olması gereken 404sayfasıret
                return RedirectToAction("Index");
            }

            //makalenin görüntülenme sayısı arttırma
            gelenArticle.ReadCount++;
            _artRepo.Update(gelenArticle);
            ModelArticle mdl = new ModelArticle();
            mdl.Id = gelenArticle.Id;
            mdl.Subject = gelenArticle.Subject;
            mdl.Summary = gelenArticle.Summary;
            mdl.InsertDate = gelenArticle.InsertDate;
            mdl.DislikeCount = gelenArticle.DislikeCount;
            mdl.LikeCount = gelenArticle.LikeCount;
            mdl.ReadCount = gelenArticle.ReadCount;
            mdl.comments = _comRepo.GetAll().Where(x => x.ArticleId == mdl.Id && x.Approve == true).Select(x => new ModelComment()
            {
                Content = x.Content,
                InsertDate = x.InsertDate,
                Email = x.Email,
                FullName = x.FullName
            }).ToList();
            mdl.Content = gelenArticle.Content;
            mdl.CategoryName = gelenArticle.Category.Name;
            mdl.Tags = gelenArticle.Tags.Select(x => new ModelTag()
            {
                Name = x.Name
            }).ToList();


            return View(mdl);
        }
        public ActionResult Yorum(ModelComment mdl)
        {
            Comment cmt = new Comment();
            cmt.ArticleId = mdl.ArticleId;
            cmt.Content = mdl.Content;
            cmt.FullName = mdl.FullName;
            cmt.Email = mdl.Email;
            cmt.InsertDate = DateTime.Now;
            cmt.Approve = true;

            _comRepo.Create(cmt);

            return RedirectToAction("Goster", new { id = mdl.ArticleId });
        }
        public ActionResult Like(int id)
        {
            var KullaniciIP = GetIP();

            var article = _artRepo.GetById(id);
            var IpControl = _cIpRepo.GetAll().Where(x => x.ClientIpNo == KullaniciIP && x.ArticleId == id).FirstOrDefault();

            if (IpControl != null)
            {
                return Json("IPTAL");
            }

            if (!article.isActive || article == null)
            {
                return Json("ERROR");
            }


            article.LikeCount++;
            _artRepo.Update(article);

            IpAdd(KullaniciIP, id);

            return Json("OK");
        }
        public ActionResult LikeControl(string Ip)
        {
            var IpControl = _cIpRepo.GetAll().Where(x => x.ClientIpNo == Ip).FirstOrDefault();
            if (IpControl != null)
            {
                return Json("ERROR");
            }

            return Json("OK");
        }


        private void IpAdd(string kullaniciIp, int id)
        {
            ClientIPs cIp = new ClientIPs();
            cIp.ClientIpNo = kullaniciIp;
            cIp.ArticleId = id;


            _cIpRepo.Create(cIp);
        }

        private string GetIP()
        {
            return Request.ServerVariables["REMOTE_ADDR"].ToString();
        }

        public ActionResult CategoryList()
        {
            var mdl = _catRepo.GetAll().Select(x => new ModelCategory()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return PartialView("_CategoryList", mdl);
        }
    }
}