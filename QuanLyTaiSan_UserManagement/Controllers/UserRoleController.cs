using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using QuanLyTaiSan_UserManagement.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using QuanLyTaiSan_UserManagement.Attribute;
using QuanLyTaiSan_UserManagement.Common;

namespace QuanLyTaiSan_UserManagement.Controllers
{
  //  [Authorize]
  //  [AuthorizationHandler]
    public class UserRoleController : Controller
    {
      
        QuanLyTaiSanCtyEntities data = new QuanLyTaiSanCtyEntities();
        //    [AuthorizationViewHandler]
        public ActionResult UserIndex()
        {
            var dao = new UserDao(); 
          //  var db = new ApplicationDbContext();
         //   var dbContext = new UserAuthorizationDatabseAction();
            var users = data.UserLogins.Where(x=> x.IsDeleted == false).ToList();
            var userRole = new List<InformationUser>();
            foreach (var item in users)
            {
               
             //  var firstRoleId = data.UserLogins.Where(x => x.);
              //  var firstRoleId = data.UserLogins.FirstOrDefault()?.GroupID;
                if (!string.IsNullOrEmpty(item.GroupID))
                {
                    var NameGrRole = data.UserGroups.Where(x=>x.ID == item.GroupID).Select(x => x.Name).First();
                    userRole.Add(new InformationUser()
                    {    Id = item.Id ,
                        Username = item.UserName,
                        RoleGroupID = item.GroupID,
                        RoleGroupName = NameGrRole,
                        FullName = item.FullName,
                       
                    });
                }
                else
                {
                    userRole.Add(new InformationUser()
                    {
                        Id = item.Id,
                        Username = item.UserName,
                        RoleGroupID = item.GroupID,
                        RoleGroupName = "Chưa được phân quyền",
                        FullName = item.FullName,

                    });
                }
            }
            ViewData["Users"] = userRole;
            ViewData["Roles"] = data.UserGroups.ToList();
            return View();
        }




        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult RegisterUser(string FullName,  string Role, string Username, string Password)
        {
                var dao = new UserDao();
                var result = dao.UpdateRoleUser(FullName,Username, Role, Password);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetInfoAccount(string userId)
        { bool result = true; 
            var lstInfo = data.UserLogins.Where(x => x.UserName == userId).FirstOrDefault();
            var Result = new { result, lstInfo }; 
            return Json(Result);
        }

        [HttpPost]
        public JsonResult ChangeRoleByUserId(int ID, string FullName, string Username, string Role,string Password)
        {
           // bool result = true;
            var dao = new UserDao();
            var result = dao.UpdateRole(ID, FullName, Username, Role, Password);
            return Json(result);
        }


        [HttpPost]
        public JsonResult DeleteUser(int userId)
        {
            var dao = new UserDao();
            var result = dao.DeleteRoleUser(userId);
            return Json(result);
        }
    }
}