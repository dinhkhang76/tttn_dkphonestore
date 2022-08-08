using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
namespace TTTN_ASP.Controllers
{
    public class ModuleController : Controller
    {
        private MenuDAO menuDAO = new MenuDAO();
        private SliderDAO sliderDAO = new SliderDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private TopicDAO topicDAO = new TopicDAO();
        private PostDAO postDAO = new PostDAO();

        // GET: Module
        public ActionResult MainMenu()
        {
            List<Menu> list = menuDAO.getListByParentId("mainmenu", 0);
            return View("MainMenu", list);
        }
        public ActionResult FooterMenu()
        {
            List<Menu> list = menuDAO.getListByParentId("footermenu", 0);
            return View("FooterMenu", list);
        }
        public ActionResult MainMenuSub(int id)
        {
            Menu menu = menuDAO.getRow(id);
            List<Menu> list = menuDAO.getListByParentId("mainmenu", id);
            if (list.Count == 0)
            {
                return View("MainMenuSub1", menu);

            }
            else
            {
                ViewBag.Menu = menu;
                return View("MainMenuSub2", list);
            }
        }
        //slideshow
        //slider
        public ActionResult Slideshow()
        {
            List<Slider> list = sliderDAO.getListByPosition("slideshow");
            return View("Slideshow", list);
        }
        //list category

        public ActionResult ListCategory()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("ListCategory", list);
        }
        public ActionResult PostLastNews()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("PostLastNews", list);
        }
        
    }
}