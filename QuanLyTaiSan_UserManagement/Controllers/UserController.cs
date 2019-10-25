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

namespace QuanLyTaiSan_UserManagement.Controllers
{
  //  [Authorize]
  //  [AuthorizationHandler]
    public class UserController : Controller
    {
        // GET: User
    //    [AuthorizationViewHandler]
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            var dbContext = new UserAuthorizationDatabseAction();
            var users = db.Users.ToList();
            var userRole = new List<ShortInformationUser>();
            foreach (var item in users)
            {
                var firstRoleId = item.Roles.FirstOrDefault()?.RoleId;
                if (!string.IsNullOrEmpty(firstRoleId))
                {
                    var role = dbContext.GetRoleById(firstRoleId).Name;
                    userRole.Add(new ShortInformationUser()
                    {
                        Id = item.Id,
                        Username = item.UserName,
                        RoleId = firstRoleId,
                        RoleName = role,
                        LastLoginTime = item.LastLoginTime
                    });
                }
                else
                {
                    userRole.Add(new ShortInformationUser()
                    {
                        Id = item.Id,
                        Username = item.UserName,
                        RoleId = string.Empty,
                        RoleName = "Chưa được phân quyền"
                    });
                }
            }
            ViewData["Users"] = userRole;
            ViewData["Roles"] = dbContext.GetAllRole();
            return View();
        }

        [HttpPost]
        public JsonResult ChangeRoleByUserId(string userId, string roleId)
        {
            var db = new ApplicationDbContext();
            var sqlDelete = @"DELETE FROM [AspNetUserRoles] WHERE UserId = @UserId";
            db.Database.ExecuteSqlCommand(sqlDelete, new SqlParameter("@UserId", userId));
            var sqlInsert = @"INSERT INTO [AspNetUserRoles] (UserId, RoleId) VALUES (@UserId, @RoleId)";
            db.Database.ExecuteSqlCommand(sqlInsert, new SqlParameter("@UserId", userId)
                , new SqlParameter("@RoleId", roleId));
            return Json(new {status = true});
        }

        [HttpPost]
        public JsonResult DeleteUser(string userId)
        {
            var db = new ApplicationDbContext();
            var sqlDeleteRole = @"DELETE FROM [AspNetUserRoles] WHERE UserId = @UserId";
            db.Database.ExecuteSqlCommand(sqlDeleteRole, new SqlParameter("@UserId", userId));
            var sqlDeleteUser = @"DELETE FROM [AspNetUsers] WHERE Id = @UserId";
            db.Database.ExecuteSqlCommand(sqlDeleteUser, new SqlParameter("@UserId", userId));
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }
    }
}