﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TTTN_ASP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start()
        {
            //Lưu thông tin đăng nhập quản lý
            Session["UserAdmin"] = "";
            //Lưu mã người đăng nhập quản lý
            Session["UserId"] = "";
            //Lưu họ tên người đăng nhập quản lý
            Session["Fullname"] = "";
            //Lưu hình ảnh người đăng nhập quản lý
            Session["Img"] = "";
            //Giỏ hàng
            Session["MyCart"] = "";
            //Lưu thông tin đăng nhập người dùng
            Session["UserCustomer"] = "";
            Session["CustomerId"] = "";
        }
    }
}
