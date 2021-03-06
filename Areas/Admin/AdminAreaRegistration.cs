﻿using System.Web.Mvc;

namespace Bazaar.Areas.Admin
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

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller ="Home", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_WebApiRoute",
                "Admin/Api/{controller}/{id}",
                new { id = UrlParameter.Optional });
        }
    }
}