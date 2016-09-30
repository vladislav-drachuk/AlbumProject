using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlbumProject.BusinessLogicLayer.Servises;
using AlbumProject.BusinessLogicLayer.Interfaces;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using AutoMapper;
using WebLayer.Models;

namespace WebLayer.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        ServiceCreator serviceCreator = new ServiceCreator();
        IFollowingServise fs;
        IMapper mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();


        public FollowController()
        {
            fs = serviceCreator.CreateFollowingServise("DefaultConnection");
        }

        [HttpPost]
        public async Task<ActionResult> Follow(string userName)
        {
            if (User.IsInRole("banned"))
            {
                return Json(new { result = false, message = "Access denied. Profile has been banned" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                await fs.Follow(User.Identity.Name, userName);
                int followersNumber = fs.GetCountOfFollowers(userName);
                return Json(new { result = true,  number = followersNumber }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Unfollow(string userName)
        {
            if (User.IsInRole("banned"))
            {
                return Json(new { result = false, message = "Access denied. Profile has been banned" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                await fs.Unfollow(User.Identity.Name, userName);
                int followersNumber = fs.GetCountOfFollowers(userName);
                return Json(new { result = true, number = followersNumber }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}