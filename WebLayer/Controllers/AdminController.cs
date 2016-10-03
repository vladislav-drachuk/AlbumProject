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
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        
        IRoleServise rs;
        
        IMapper mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();


        public AdminController()
        {
            ServiceCreator serviceCreator = new ServiceCreator();
            rs = serviceCreator.CreateRoleServise("DefaultConnection");
            
        }
        [HttpPost]
        public async Task<ActionResult> Block(string userName)
        {
          
            await rs.AddToRole(userName, null);
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
        public  async Task<ActionResult> AddAdminStatus(string userName)
        {
            await rs.DeleteFromRole(userName, "banned");
            await rs.AddToRole(userName, "admin");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveAdminStatus(string userName)
        {
            await rs.DeleteFromRole(userName, "admin");
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