using MyClass.DAO;
using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TTTN_ASP.Controllers
{
    public class TimkiemController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        // GET: Tìm kiếm
        public ActionResult Index(string keyword)
        {
            int totalRecord = 0;
            var model = productDAO.Search(keyword);
            ViewBag.Keyword = keyword;
            ViewBag.Total = totalRecord;
            return View(model);
        }
    }
}