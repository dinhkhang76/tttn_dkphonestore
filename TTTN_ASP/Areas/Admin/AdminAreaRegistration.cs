using System.Web.Mvc;

namespace TTTN_ASP.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }
        //tùy biến url trang quản trị (url friendly)
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_login",
                "dang-nhap-trang-quan-tri",
                new { Controller = "Auth", action = "Login", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_logout",
                "dang-xuat-admin",
                new { Controller = "Auth", action = "Logout", id = UrlParameter.Optional }
            );
          
            context.MapRoute(
                name: "TrangQuanTri",
                url: "trang-quan-tri",
                defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
                );

            //quan ly san pham
            context.MapRoute(
                name: "QuanLySanPham",
                url: "quan-ly-san-pham",
                defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional }
                );
            context.MapRoute(
               name: "ThemSanPham",
               url: "them-san-pham",
               defaults: new { controller = "Product", action = "Create", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "ChiTietSanPham",
               url: "chi-tiet-san-pham/{id}", 
               defaults: new { controller = "Product", action = "Details", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "CapNhatSanPham",
               url: "cap-nhat-san-pham/{id}",
               defaults: new { controller = "Product", action = "Edit", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "ThungRacSanPham",
               url: "thung-rac-san-pham",
               defaults: new { controller = "Product", action = "Trash", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "XoaSanPham",
               url: "xoa-san-pham/{id}",
               defaults: new { controller = "Product", action = "Delete", id = UrlParameter.Optional }
               );


            //quan ly danh muc
            context.MapRoute(
                name: "QuanLyDanhMuc",
                url: "quan-ly-danh-muc",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional }
                );
            context.MapRoute(
               name: "ThemDanhMuc",
               url: "them-danh-muc",
               defaults: new { controller = "Category", action = "Create", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "ChiTietDanhMuc",
               url: "chi-tiet-danh-muc/{id}",
               defaults: new { controller = "Category", action = "Details", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "CapNhatDanhMuc",
               url: "cap-nhat-danh-muc/{id}",
               defaults: new { controller = "Category", action = "Edit", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "ThungRacDanhMuc",
               url: "thung-rac-danh-muc",
               defaults: new { controller = "Category", action = "Trash", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "XoaDanhMuc",
               url: "xoa-danh-muc/{id}",
               defaults: new { controller = "Category", action = "Delete", id = UrlParameter.Optional }
               );

            //quan ly bai viet
            context.MapRoute(
                name: "QuanLyBaiViet",
                url: "quan-ly-bai-viet",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional }
                );
            context.MapRoute(
              name: "ThemBaiViet",
              url: "them-bai-viet",
              defaults: new { controller = "Post", action = "Create", id = UrlParameter.Optional }
              );
            context.MapRoute(
               name: "ChiTietBaiViet",
               url: "chi-tiet-bai-viet/{id}",
               defaults: new { controller = "Post", action = "Details", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "CapNhatBaiViet",
               url: "cap-nhat-bai-viet/{id}",
               defaults: new { controller = "Post", action = "Edit", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "ThungRacBaiViet",
               url: "thung-rac-bai-viet",
               defaults: new { controller = "Post", action = "Trash", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "XoaBaiViet",
               url: "xoa-bai-viet/{id}",
               defaults: new { controller = "Post", action = "Delete", id = UrlParameter.Optional }
               );

            //Quan ly chu de
            context.MapRoute(
                name: "QuanLyChuDe",
                url: "quan-ly-chu-de",
                defaults: new { controller = "Topic", action = "Index", id = UrlParameter.Optional }
                );
            context.MapRoute(
              name: "ThemChuDe",
              url: "them-chu-de",
              defaults: new { controller = "Topic", action = "Create", id = UrlParameter.Optional }
              );
            context.MapRoute(
               name: "ChiTietChuDe",
               url: "chi-tiet-chu-de/{id}",
               defaults: new { controller = "Topic", action = "Details", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "CapNhatChuDe",
               url: "cap-nhat-chu-de/{id}",
               defaults: new { controller = "Topic", action = "Edit", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "ThungRacChuDe",
               url: "thung-rac-chu-de",
               defaults: new { controller = "Topic", action = "Trash", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "XoaChuDe",
               url: "xoa-chu-de/{id}",
               defaults: new { controller = "Topic", action = "Delete", id = UrlParameter.Optional }
               );

            //quan ly trang don
            context.MapRoute(
               name: "QuanLyTrangDon",
               url: "quan-ly-trang-don",
               defaults: new { controller = "Page", action = "Index", id = UrlParameter.Optional }
               );
            context.MapRoute(
              name: "ThemTrangDon",
              url: "them-trang-don",
              defaults: new { controller = "Page", action = "Create", id = UrlParameter.Optional }
              );
            context.MapRoute(
               name: "ChiTietTrangDon",
               url: "chi-tiet-trang-don/{id}",
               defaults: new { controller = "Page", action = "Details", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "CapNhatTrangDon",
               url: "cap-nhat-trang-don/{id}",
               defaults: new { controller = "Page", action = "Edit", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "ThungRacTrangDon",
               url: "thung-rac-trang-don",
               defaults: new { controller = "Page", action = "Trash", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "XoaTrangDon",
               url: "xoa-trang-don/{id}",
               defaults: new { controller = "Page", action = "Delete", id = UrlParameter.Optional }
               );

            //quan ly don hang
            context.MapRoute(
               name: "QuanLyDonHang",
               url: "quan-ly-don-hang",
               defaults: new { controller = "Order", action = "Index", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "ChiTietDonHang",
               url: "chi-tiet-don-hang/{id}",
               defaults: new { controller = "Order", action = "Details", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "ThungRacDonHang",
               url: "thung-rac-don-hang",
               defaults: new { controller = "Order", action = "Trash", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "XoaDonHang",
               url: "xoa-don-hang/{id}",
               defaults: new { controller = "Order", action = "Delete", id = UrlParameter.Optional }
               );

            //quan ly nguoi dung
            context.MapRoute(
               name: "QuanLyNguoiDung",
               url: "quan-ly-nguoi-dung",
               defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
               );
            context.MapRoute(
              name: "ThemNguoiDung",
              url: "them-nguoi-dung",
              defaults: new { controller = "User", action = "Create", id = UrlParameter.Optional }
              );
            context.MapRoute(
               name: "ChiTietNguoiDung",
               url: "chi-tiet-nguoi-dung/{id}",
               defaults: new { controller = "User", action = "Details", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "CapNhatNguoiDung",
               url: "cap-nhat-nguoi-dung/{id}",
               defaults: new { controller = "User", action = "Edit", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "ThungRacNguoiDung",
               url: "thung-rac-nguoi-dung",
               defaults: new { controller = "User", action = "Trash", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "XoaNguoiDung",
               url: "xoa-nguoi-dung/{id}",
               defaults: new { controller = "User", action = "Delete", id = UrlParameter.Optional }
               );

            //quan ly lien he
            context.MapRoute(
               name: "QuanLyLienHe",
               url: "quan-ly-lien-he",
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
               );
         
            context.MapRoute(
               name: "ChiTietLienHe",
               url: "chi-tiet-lien-he/{id}",
               defaults: new { controller = "Contact", action = "Details", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "ThungRacLienHe",
               url: "thung-rac-lien-he",
               defaults: new { controller = "Contact", action = "Trash", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "XoaLienHe",
               url: "xoa-lien-he/{id}",
               defaults: new { controller = "Contact", action = "Delete", id = UrlParameter.Optional }
               );

            //quan ly menu
            context.MapRoute(
               name: "QuanLyMenu",
               url: "quan-ly-menu",
               defaults: new { controller = "Menu", action = "Index", id = UrlParameter.Optional }
               );
             
            context.MapRoute(
               name: "ChiTietMenu",
               url: "chi-tiet-menu/{id}",
               defaults: new { controller = "Menu", action = "Details", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "CapNhatMenu",
               url: "cap-nhat-menu/{id}",
               defaults: new { controller = "Menu", action = "Edit", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "ThungRacMenu",
               url: "thung-rac-menu",
               defaults: new { controller = "Menu", action = "Trash", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "XoaMenu",
               url: "xoa-menu/{id}",
               defaults: new { controller = "Menu", action = "Delete", id = UrlParameter.Optional }
               );


            //quan ly slider
            context.MapRoute(
               name: "QuanLySlider",
               url: "quan-ly-slider",
               defaults: new { controller = "Slider", action = "Index", id = UrlParameter.Optional }
               );
            context.MapRoute(
             name: "ThemSlider",
             url: "them-slide",
             defaults: new { controller = "Slider", action = "Create", id = UrlParameter.Optional }
             );
            context.MapRoute(
               name: "ChiTietSlide",
               url: "chi-tiet-slide/{id}",
               defaults: new { controller = "Slider", action = "Details", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "CapNhatSlider",
               url: "cap-nhat-slide/{id}",
               defaults: new { controller = "Slider", action = "Edit", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "ThungRacSlider",
               url: "thung-rac-slide",
               defaults: new { controller = "Slider", action = "Trash", id = UrlParameter.Optional }
               );
            context.MapRoute(
               name: "XoaSlider",
               url: "xoa-slide/{id}",
               defaults: new { controller = "Slider", action = "Delete", id = UrlParameter.Optional }
               );


            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { Controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}