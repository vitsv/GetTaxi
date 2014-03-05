using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Infrastructure
{
    public static class NotificationExtention
    {
        public static void ShowSuccessMsg(this Controller controller, string text)
        {
            controller.TempData["MsgText"] = text;
            controller.TempData["MsgType"] = "alert-success";
            controller.TempData["MsgIsRender"] = false;
        }

        public static void ShowWarningMsg(this Controller controller, string text)
        {
            controller.TempData["MsgText"] = text;
            controller.TempData["MsgType"] = "alert-warning";
            controller.TempData["MsgIsRender"] = false;
        }

        public static void ShowErrorMsg(this Controller controller, string text)
        {
            controller.TempData["MsgText"] = text;
            controller.TempData["MsgType"] = "alert-danger";
            controller.TempData["MsgIsRender"] = false;
        }

        public static void RenderSuccessMsg(this Controller controller, string partial)
        {
            controller.TempData["MsgPartialName"] = partial;
            controller.TempData["MsgType"] = "alert-success";
            controller.TempData["MsgIsRender"] = true;
        }

        public static void RenderWarningMsg(this Controller controller, string partial)
        {
            controller.TempData["MsgPartialName"] = partial;
            controller.TempData["MsgType"] = "alert-warning";
            controller.TempData["MsgIsRender"] = true;
        }

        public static void RenderErrorMsg(this Controller controller, string partial)
        {
            controller.TempData["MsgPartialName"] = partial;
            controller.TempData["MsgType"] = "alert-danger";
            controller.TempData["MsgIsRender"] = true;
        }
    }
}