using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLayer.Models;
using AlbumProject.BusinessLogicLayer.Servises;
using AlbumProject.BusinessLogicLayer.Interfaces;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.IO;
using AutoMapper;
using WebLayer.Helpers;


namespace WebLayer.Controllers
{
    public class AdminController : Controller
    {
        ServiceCreator serviceCreator = new ServiceCreator();
        IRoleServise rs;
        
        IMapper mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();


        public AdminController()
        {
            rs = serviceCreator.CreateRoleServise("DefaultConnection");
            
        }
        [HttpPost]
        public async Task<ActionResult> Block(string userName)
        {
            await rs.AddToRole(userName, "banned");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Unblock(string userName)
        {
            await rs.DeleteFromRole(userName, "banned");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlockedUsers()

        {
            ViewBag.Message = "Blocked";
            var users = mapper.Map<ICollection<UserProfileDTO>, ICollection<UserModel>>(rs.GetUsersByRole("banned"));
            return View("UserList", users);
        }

        [HttpPost]
        public ActionResult AddAdminStatus(string userName)
        {
            
            rs.AddToRole(userName, "admin");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveAdminStatus(string userName)
        {
            rs.DeleteFromRole(userName, "admin");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Admins()
        {
            ViewBag.Message = "Blocked";
            var users = mapper.Map<ICollection<UserProfileDTO>, ICollection<UserModel>>(rs.GetUsersByRole("admin"));
            return View("UserList", users);
        }

        public async Task<ActionResult> GetRoleStatus(string userName)
        {
            ViewBag.IsBlocked = await rs.IsInRole(userName, "banned");
            bool isAdmin = await rs.IsInRole(userName, "admin");
            ViewBag.IsAdmin = isAdmin;
            ViewBag.UserName = userName;
            return View();
        }
    }
}