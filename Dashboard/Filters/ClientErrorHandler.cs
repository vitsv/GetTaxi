using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Dashboard.Common
{
    public class ClientErrorHandler : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //    if (filterContext.HttpContext.IsCustomErrorEnabled)
            //    {
            //        var responce = filterContext.RequestContext.HttpContext.Response;

            //        string controllerName = "";
            //        string actionName = "";

            //        //if (filterContext.Exception.GetType() == typeof(SqlException))
            //        //{

            //        //}


            //        if (filterContext.RouteData.Values["controller"] != null)
            //            controllerName = filterContext.RouteData.Values["controller"].ToString();

            //        if (filterContext.RouteData.Values["action"] != null)
            //            actionName = filterContext.RouteData.Values["action"].ToString();

            //        var model = new ErrorModel();

            //        //log error
            //        //ApplicationErrorManager.LogApplicationError(filterContext.Exception);
            //        model.ActionName = actionName;
            //        model.ControllerName = controllerName;
            //        model.ErrorInfo = filterContext.Exception;
            //        model.IsErrorFromAjax = filterContext.HttpContext.Request.IsAjaxRequest();


            //        if (filterContext.HttpContext.Request.UrlReferrer != null)
            //            model.ErrorUrl = filterContext.HttpContext.Request.UrlReferrer.AbsoluteUri;
            //        else
            //            model.ErrorUrl = "#";

            //        responce.Clear();

            //        if (model.IsErrorFromAjax)
            //        {
            //            responce.Write(new JavaScriptSerializer().Serialize(new { action = model.ActionName, controller = model.ControllerName, msg = model.ErrorMessage }));
            //            responce.ContentType = "application/json";
            //        }
            //        else
            //        {
            //            string displayHtml = CRAWeb.Controllers.GlobalController.RenderPartialViewToString(filterContext.Controller.ControllerContext, "Error", model);

            //            responce.Write(displayHtml);
            //            responce.ContentType = MediaTypeNames.Text.Html;
            //        }

            //        filterContext.ExceptionHandled = true;

            //        filterContext.HttpContext.Response.StatusCode = 500;
            //    }
        }
    }
}