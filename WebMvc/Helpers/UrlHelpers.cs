﻿using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace WebMvc.Helpers
{
    public static class UrlHelpers
    {
        public static string IsSelected(this HtmlHelper html, string controllers = "", string actions = "", string cssClass = "active")
        {
            ViewContext viewContext = html.ViewContext;
            bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

            if (isChildAction)
                viewContext = html.ViewContext.ParentActionViewContext;

            RouteValueDictionary routeValues = viewContext.RouteData.Values;
            string currentAction = routeValues["action"].ToString();
            string currentController = routeValues["controller"].ToString();

            if (String.IsNullOrEmpty(actions))
                actions = currentAction;

            if (String.IsNullOrEmpty(controllers))
                controllers = currentController;

            string[] acceptedActions = actions.Trim().Split(',').Distinct().ToArray();
            string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }

        public static string IsSelected(this HtmlHelper html, string controllers = "", string cssClass = "active")
        {
            ViewContext viewContext = html.ViewContext;
            bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

            if (isChildAction)
                viewContext = html.ViewContext.ParentActionViewContext;

            RouteValueDictionary routeValues = viewContext.RouteData.Values;
            string currentAction = routeValues["action"].ToString();
            string currentController = routeValues["controller"].ToString();

            if (String.IsNullOrEmpty(controllers))
                controllers = currentController;

            string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

            return acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }

        public static bool IsCheckSelected(this HtmlHelper html, string controllers = "", string actions = "", string cssClass = "active")
        {
            if (html.IsSelected(controllers: controllers, actions: actions) == cssClass)
                return true;
            else
                return false;
        }

        public static bool IsCheckSelected(this HtmlHelper html, string controllers = "", string cssClass = "active")
        {
            if (html.IsSelected(controllers, cssClass) == cssClass)
                return true;
            else
                return false;
        }
    }
}