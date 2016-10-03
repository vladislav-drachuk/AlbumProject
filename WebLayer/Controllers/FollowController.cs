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
        
        IFollowingServise fs;
        IMapper mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();


        public FollowController()
        {
            ServiceCreator serviceCreator = new ServiceCreator();
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
                var operation = await fs.Follow(User.Identity.Name, userName);
                if (operation.Succedeed == true)
                {
                    int followersNumber = fs.GetCountOfFollowers(userName);
                    return Json(new { result = true, number = followersNumber }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = false, message = operation.Message }, JsonRequestBehavior.AllowGet);
                }
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
                var operation = await fs.Unfollow(User.Identity.Name, userName);
                if (operation.Succedeed == true)
                {
                    int followersNumber = fs.GetCountOfFollowers(userName);
                    return Json(new { result = true, number = followersNumber }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = false, message = operation.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }


    }
}