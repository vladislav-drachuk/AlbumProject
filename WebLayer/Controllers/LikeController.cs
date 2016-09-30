using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLayer.Models;
using AlbumProject.BusinessLogicLayer.Servises;
using AlbumProject.BusinessLogicLayer.Interfaces;
using AlbumProject.BusinessLogicLayer.Infrastructure;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.IO;
using AutoMapper;
using WebLayer.Helpers;
using WebLayer.Infrastructure;

namespace WebLayer.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
       
        ServiceCreator serviceCreator = new ServiceCreator();
        ILikeServise ls;
        IMapper mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();


        public LikeController()
        {
            ls = serviceCreator.CreateLikeServise("DefaultConnection");
        }


        [HttpPost]
        public async Task<JsonResult> LikeImage(string imageId)
        {
            if (User.IsInRole("banned"))
            {
                return Json(new { result = false, message = "Access denied. Profile has been banned" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                await ls.LikePhoto(User.Identity.Name, imageId);
                var count = ls.GetCountOfIsLikes(imageId);
                return Json(new { result = true,countOfLikes = count }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<JsonResult> UnlikeImage(string imageId)
        {
            if (User.IsInRole("banned"))
            {
                return Json(new { result = false, message = "Access denied. Profile has been banned" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                await ls.UnLikePhoto(User.Identity.Name, imageId);
                var count = ls.GetCountOfIsLikes(imageId);
                return Json(new { result = true, countOfLikes = count }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}