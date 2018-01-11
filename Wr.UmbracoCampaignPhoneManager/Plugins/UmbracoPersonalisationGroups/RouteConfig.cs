﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Wr.UmbracoCampaignPhoneManager.Plugins.UmbracoPersonalisationGroups
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Criteria resources",
                url: "App_Plugins/UmbracoPersonalisationGroups/GetResourceForCriteria/campaignphonemanagercriteria/{fileName}",
                defaults: new { controller = "Resource", action = "GetResourceForCriteria" });

            /*routes.MapRoute(
                name: "Core esources",
                url: "App_Plugins/UmbracoPersonalisationGroups/GetResource/{fileName}",
                defaults: new { controller = "Resource", action = "GetResource" });

            routes.MapRoute(
                name: "Criteria methods",
                url: "App_Plugins/UmbracoPersonalisationGroups/Criteria/{action}",
                defaults: new { controller = "Criteria", action = "Index" });
                */
        }
    }
}