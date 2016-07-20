using _Proje_Blog_.EF;
using _Proje_Blog_.EF.CRUD;
using _Proje_Blog_.Filters;
using _Proje_Blog_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _Proje_Blog_.Controllers
{
    //[LoginGerektirir]
    public class AdminController : Controller
    {
        ArticleRepository _artRepo;
        CategoryRepository _catRepo;
        CommentRepository _comRepo;
        TagRepository _tagRepo;
        BlogContext db;

        public AdminController()
        {
            _artRepo = new ArticleRepository();
            _catRepo = new CategoryRepository();
            _comRepo = new CommentRepository();
            _tagRepo = new TagRepository();
            db = new BlogContext();
        }

        public ActionResult Index()
        {
            var model = new List<ModelArticle>();

            model = _artRepo.GetAll().Select(x => new ModelArticle()
            {
                Id = x.Id,
                CategoryName = x.Category.Name,
                Subject = x.Subject,
                Summary = x.Summary,
                InsertDate = x.InsertDate,
                ReadCount = x.ReadCount,
                LikeCount = x.LikeCount,
                DislikeCount = x.DislikeCount,
                isActive = x.isActive
            }).ToList();

            return View(model);
        }
        public ActionResult ActiveControl(int id)
        {
            var article = _artRepo.GetById(id);
            if (!article.isActive)
            {
                article.isActive = true;
                _artRepo.Update(article);
                return Json("TRUE");
            }
            else
            {
                article.isActive = false;
                _artRepo.Update(article);
                return Json("FALSE");
            }
        }
        public ActionResult CommentApproveControl(int id)
        {
            var comment = _comRepo.GetById(id);
            if (!comment.Approve)
            {
                comment.Approve = true;
                _comRepo.Update(comment);
                return Json("TRUE");
            }
            else
            {
                comment.Approve = false;
                _comRepo.Update(comment);
                return Json("FALSE");
            }
        }
        public ActionResult ListCategory()
        {
            var cat = _catRepo.GetAll().Select(x => new ModelCategory()
            {
                Id = x.Id,
                Name = x.Name,
                ArticleId = _artRepo.GetAll().Where(y => y.CategoryId == x.Id).Count()
            }).ToList();

            return View(cat);
        }
        public ActionResult ListComment()
        {
            var com = _comRepo.GetAll().Select(x => new ModelComment()
            {
                Id = x.Id,
                FullName = x.FullName,
                ArticleName = _artRepo.GetById(x.ArticleId).Subject,
                Content = x.Content,
                Email = x.Email,
                InsertDate = x.InsertDate,
                Approve = x.Approve
            }).ToList();

            return View(com);
        }
        public ActionResult ListTag()
        {
            var tag = _tagRepo.GetAll().Select(x => new ModelTag()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return View(tag);
        }
        /// <summary>
        /// Alttaki delete işlmeri aja ile yapılmıştır o yüzden result tpleri JsonResult tır GoToView diyerek ten viewlardki <script></script> içerinde bulunan functionları inceleya bilirsiniz
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeleteTAg(int id)
        {
            try
            {
                Tag tg = _tagRepo.GetById(id);
                _tagRepo.Remove(tg);
                return Json("OK");

            }
            catch
            {
                return Json("FAIL");
            }
        }
        public JsonResult DeleteComment(int id)
        {
            try
            {
                Comment com = _comRepo.GetById(id);
                _comRepo.Remove(com);
                return Json("OK");
            }
            catch
            {
                return Json("FAIL");
            }
        }
        public JsonResult DeleteCategory(int id)
        {
            try
            {
                Category cat = _catRepo.GetById(id);
                _catRepo.Remove(cat);
                return Json("OK");
            }
            catch
            {
                return Json("FAIL");
            }
        }
        public JsonResult DeleteArticle(int id)
        {
            try
            {
                Article art = _artRepo.GetById(id);
                _artRepo.Remove(art);
                return Json("OK");
            }
            catch
            {
                return Json("FAIL");
            }
        }
        public JsonResult MakaleGoster(int id)
        {
            try
            {
                return Json(new { redirectTo = Url.Action("Goster", "Home", new { id = id }) });
            }
            catch
            {
                return Json("FAIL");
            }
        }

        /// <summary>
        /// Taglar için Düzenleme işlemlerini AJAX ile yaptım tamamen ekstra br çalışma hocanın gösterdiğinin dışında bir çalışmadır arkadaşlar aldırış etmenize gerek yoktur alttaki sırasıyle 3 action tag duzenlemeye aittir
        /// </summary>
        /// <param name="id">Tag ID</param>
        /// <returns></returns>
        public JsonResult TagDuzenle(int id)
        {
            try
            {
                return Json(new { redirectTo = Url.Action("TagDuzenleme", "Admin", new { id = id }) });
            }
            catch
            {
                return Json("FAIL");
            }
        }
        public ActionResult TagDuzenleme(int id)
        {
            Tag tag = (db.Tags.Where(x => x.Id == id)).FirstOrDefault();
            return View(tag);
        }

        [HttpPost]
        public ActionResult TagDuzenleme(Tag tag)
        {
            Tag tg = _tagRepo.GetById(tag.Id);
            tg.Name = tag.Name;

            _tagRepo.Update(tg);
            return RedirectToAction("ListTag");
        }
        public ActionResult GosterMakale(int id)
        {
            var gelenArticle = _artRepo.GetById(id);

            ModelArticle mdl = new ModelArticle();
            mdl.Id = gelenArticle.Id;
            mdl.Subject = gelenArticle.Subject;
            mdl.Summary = gelenArticle.Summary;
            mdl.InsertDate = gelenArticle.InsertDate;
            mdl.DislikeCount = gelenArticle.DislikeCount;
            mdl.LikeCount = gelenArticle.LikeCount;
            mdl.ReadCount = gelenArticle.ReadCount;

            mdl.Content = gelenArticle.Content;
            mdl.CategoryName = gelenArticle.Category.Name;

            return View(mdl);
        }

        public ActionResult EditArticle(int id)
        {
            var gelenArtcile = _artRepo.GetById(id);

            ModelArticle mdlA = new ModelArticle();
            mdlA.Id = gelenArtcile.Id;
            mdlA.Subject = gelenArtcile.Subject;
            mdlA.Summary = gelenArtcile.Summary;
            mdlA.Content = gelenArtcile.Content;
            mdlA.CategoryId = gelenArtcile.CategoryId;
            mdlA.Tags = gelenArtcile.Tags.Select(x => new ModelTag()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            mdlA.isActive = gelenArtcile.isActive;
            mdlA.InsertDate = gelenArtcile.InsertDate;
            mdlA.LikeCount = gelenArtcile.LikeCount;
            mdlA.DislikeCount = gelenArtcile.DislikeCount;
            mdlA.DislikeCount = gelenArtcile.DislikeCount;

            mdlA.Categories = _catRepo.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = x.Id == mdlA.CategoryId

            }).ToList();


            return View(mdlA);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditArticle(ModelArticle mdl)
        {
            Article DuzenlenenArt = _artRepo.GetById(mdl.Id);
            DuzenlenenArt.Subject = mdl.Subject;
            DuzenlenenArt.Summary = mdl.Summary;
            DuzenlenenArt.CategoryId = mdl.CategoryId;
            DuzenlenenArt.Content = mdl.Content;

            _artRepo.Update(DuzenlenenArt);

            return RedirectToAction("GosterMakale", "Admin", new { id = mdl.Id });
        }
        //Yeni makale ekleme Actionları
        public ActionResult CreateArticle()
        {
            ModelArticle mdlA = new ModelArticle();
            mdlA.Categories = _catRepo.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()

            }).ToList();
            return View(mdlA);
        }
        [HttpPost]
        public ActionResult CreateArticle(ModelArticle mdl)
        {
            Article EklenecekArt = new Article();
            EklenecekArt.Subject = mdl.Subject;
            EklenecekArt.Summary = mdl.Summary;
            EklenecekArt.Content = mdl.Content;
            EklenecekArt.CategoryId = mdl.CategoryId;
            EklenecekArt.InsertDate = DateTime.Now;

            _artRepo.Create(EklenecekArt);

            return RedirectToAction("Index", "Admin");
        }

        public JsonResult CategoriEkle(string kategoriName)
        {
            try
            {
                Category ctg = new Category();
                ctg.Name = kategoriName;
                _catRepo.Create(ctg);

                return Json(ctg.Id);
            }
            catch
            {
                return Json("FAIL");

            }
        }
        public JsonResult CategoryDuzenleme(string yeniAd,int id)
        {
            try
            {
                Category ctg = _catRepo.GetById(id);
                ctg.Name = yeniAd;
                _catRepo.Update(ctg);

                return Json("OK");
            }
            catch
            {
                return Json("FAIL");

            }
        }
        //public JsonResult CategoryDuzenleme(int id)
        //{
        //    try
        //    {
        //        return Json(new { redirectTo = Url.Action("CatDuzenleme", "Admin") });
        //    }
        //    catch
        //    {
        //        return Json("FAIL");
        //    }

        //}
        //public ActionResult CatDuzenleme(ModelCategory cat)
        //{
        //    Category ct = _catRepo.GetById(cat.Id);
        //    ct.Name = cat.Name;

        //    _catRepo.Update(ct);
        //    return RedirectToAction("ListCategory");
        //}
    }
}