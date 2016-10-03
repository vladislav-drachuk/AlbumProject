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
       
       
        ILikeServise ls;
        IMapper mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();


        public LikeController()
        {
            ServiceCreator serviceCreator = new ServiceCreator();
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
                var operation = await ls.LikePhoto(User.Identity.Name, imageId);
                if (operation.Succedeed == true)
                {
                    var count = ls.GetCountOfIsLikes(imageId);
                    return Json(new { result = true, countOfLikes = count }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = false, message = operation.Message}, JsonRequestBehavior.AllowGet);
                }
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
                var operation = await ls.UnLikePhoto(User.Identity.Name, imageId);
                if (operation.Succedeed == true)
                {
                    var count = ls.GetCountOfIsLikes(imageId);
                    return Json(new { result = true, countOfLikes = count }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = false, message = operation.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        


    }
}