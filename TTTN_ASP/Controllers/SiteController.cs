using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using PagedList;

namespace TTTN_ASP.Controllers
{
    public class SiteController : Controller
    {
        LinkDAO linkDAO = new LinkDAO();
        ProductDAO productDAO = new ProductDAO();
        PostDAO postDAO = new PostDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        TopicDAO topicDAO = new TopicDAO();
        // GET: Site
        public ActionResult Index(string slug = null, int? page = null)
        {
            if (slug == null)
            {
                return this.Home();
            }
            else
            {
                Link link = linkDAO.getRow(slug);
                if (link != null)
                {
                    string typelink = link.TypeLink;
                    switch (typelink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug, page);
                            }
                        case "topic":
                            {
                                return this.PostTopic(slug, page);
                            }
                        case "page":
                            {
                                return this.PostPage(slug);
                            }
                        default:
                            {
                                return this.Error404(slug);

                            }
                    }
                }
                else
                {
                    Product product = productDAO.getRow(slug);
                    if (product != null)
                    {
                        return this.ProductDetail(product);
                    }
                    else
                    {
                        Post post = postDAO.getRow(slug);
                        if (post != null)
                        {
                            return this.PostDetail(post);
                        }
                        else
                        {
                            return this.Error404(slug);
                        }
                    }
                }
            }
        }
        //trang chu
        public ActionResult Home()
        {
            List<ProductInfo> productNew = productDAO.getList(8);
            ViewBag.ProductNew = productNew;
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("Home", list);
        }
        public ActionResult HomeProduct(int id)
        {
            Category category = categoryDAO.getRow(id);
            ViewBag.Category = category;
            //Danh mục loại theo 3 cấp
            List<int> listcatid = new List<int>();
            listcatid.Add(id);//cấp 1
            List<Category> listcategory2 = categoryDAO.getListByParentId(id);
            if (listcategory2.Count() != 0)
            {
                foreach (var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);//cấp 2
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var category3 in listcategory3)
                        {
                            listcatid.Add(category3.Id);//cấp 3
                        }

                    }
                }
            }
            //
            List<ProductInfo> list = productDAO.getListByListCatId(listcatid, 4); //lấy ra 4 sản phẩm ở trang homeproduct
            return View("HomeProduct", list);
        }
        //Nhóm action Product
        public ActionResult Product(int? page) //Trang tat ca sp
        {
            
            int pageNumber = page ?? 1;//Trang hiện tại
            int pageSize = 6; //Số sp hiển thị trên 1 trang
            IPagedList<ProductInfo> list = productDAO.getList(pageSize, pageNumber);
            return View("Product", list);
        }
        public ActionResult ProductCategory(string slug, int? page)
        {
            int pageNumber = page ?? 1;//Trang hiện tại
            int pageSize = 6; //Số mẫu tin hiển thị trên 1 trang

            Category category = categoryDAO.getRow(slug);
            ViewBag.Category = category;
            //Danh mục loại theo 3 cấp
            List<int> listcatid = new List<int>();
            listcatid.Add(category.Id);//cấp 1
            List<Category> listcategory2 = categoryDAO.getListByParentId(category.Id);
            if (listcategory2.Count() != 0)
            {
                foreach (var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);//cấp 2
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var category3 in listcategory3)
                        {
                            listcatid.Add(category3.Id);//cấp 3
                        }

                    }
                }
            }
            IPagedList<ProductInfo> list = productDAO.getListByListCatId(listcatid, pageSize, pageNumber);
            return View("ProductCategory", list);
        }
        public ActionResult ProductDetail(Product product)
        {
            ViewBag.product = productDAO.getListByListCatId(product.CatId, product.Id, 4); //lay ra 4 sp o phan san pham cung loai
            return View("ProductDetail", product);
        }

        //Nhóm Post
       
        public ActionResult Post(int? page) //trang tat ca bài viết
        {
            int pageNumber = page ?? 1;//Trang hiện tại
            int pageSize = 4; //Số mẫu tin hiển thị trên 1 trang
            IPagedList<PostInfo> list = postDAO.getList("Post",pageSize, pageNumber);
            return View("Post", list);
        }

        public ActionResult PostTopic(string slug, int? page) // chu de 
        {
            int pageNumber = page ?? 1;//Trang hiện tại
            int pageSize = 4; //Số mẫu tin hiển thị trên 1 trang
            Topic topic = topicDAO.getRow(slug);
            ViewBag.Topic = topic;
            IPagedList<PostInfo> list = postDAO.getListByTopicId(topic.Id, "Post", null, pageSize, pageNumber);
            return View("PostTopic", list);
        }
        public ActionResult PostPage(string slug) // trang don
        {
            Post post = postDAO.getRow(slug, "page");
            return View("PostPage", post);
        }
        public ActionResult PostDetail(Post post)
        {
            ViewBag.ListOther = postDAO.getListByTopicId(post.TopicId, "Post", post.Id);
            return View("PostDetail", post);
        }

        //Lỗi
        public ActionResult Error404(string slug)
        {

            return View("Error404");
        }

    }
}