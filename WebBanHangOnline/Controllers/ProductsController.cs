using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using PagedList;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        //public ActionResult Index()
        //{
        //    var items = db.Products.ToList();
            
        //    return View(items);
        //}
        public ActionResult Index(int? page)
        {
            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);
            var pageSize = 8;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        //public ActionResult Search(string query)
        //{
        //    var products = db.Products
        //        .Where(p => p.Title.Contains(query))
        //        .ToList();

        //    return View("Index", products);
        //}

        public ActionResult Search(string query, int? page)
        {
            var products = db.Products
                .Where(p => p.Title.Contains(query))
                .OrderByDescending(x => x.Id); // Sắp xếp theo Id giảm dần

            var pageSize = 8; // Số lượng item trên mỗi trang
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var pagedList = products.ToPagedList(pageIndex, pageSize); // Tạo PagedList<Product>

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.CurrentQuery = query; // Để giữ giá trị tìm kiếm khi phân trang

            return View("Index", pagedList); // Truyền PagedList vào View
        }

        public ActionResult Detail(string alias,int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
            var countReview = db.Reviews.Where(x => x.ProductId == id).Count();
            ViewBag.CountReview = countReview;
            return View(item);
        }
        public ActionResult ProductCategory(string alias,int id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }

            ViewBag.CateId = id;
            return View(items);
        }

        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }

        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }

      
    }
}