using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTaiSan_UserManagement.Attribute;
using QuanLyTaiSan_UserManagement.Models;
namespace QuanLyTaiSan_UserManagement.Controllers
{
  //  [Authorize]
    //[AuthorizationHandler]
    public class DeviceTypeController : Controller
    {
        QuanLyTaiSanCtyEntities data = new QuanLyTaiSanCtyEntities();
      //  [AuthorizationViewHandler]
        public ActionResult DeviceType()
        {
            return View(data.DeviceTypes.ToList());
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddDeviceType(FormCollection collection)
        {
            string TypeName = collection["TypeName"];
            string Notes = collection["Notes"];
            string TypeSymbol = collection["TypeSymbol"];
            data.AddDeviceType(TypeName,TypeSymbol, Notes);
            return RedirectToAction("DeviceType", "DeviceType");
        }
        [HttpGet]
        public JsonResult GetDetail(int id)
        {
            data.Configuration.ProxyCreationEnabled = false;
            var DeviceType = data.DeviceTypes.Find(id);
            return Json(new
            {
                data = DeviceType,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditDeviceType(int Id, string TypeName,string TypeSymbol, string Notes)
        {
            bool result = true;
            data.UpdateDeviceType(Id, TypeName,TypeSymbol, Notes);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteDeviceType(int Id)
        {
            bool result = false;
            var charts = data.SearchDevice(null, Id, null, null,null).ToList().Count();
            if (charts== 0)
            {
                int checkdele = data.DeleteDeviceType(Id);
                result = true;
            }
            else result = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
      //  [AuthorizationViewHandler]
        public ActionResult StatisticalDeviceType()
        {
            return View(data.DeviceTypes.ToList());
        }
    }
}