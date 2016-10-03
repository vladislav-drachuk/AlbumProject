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
    public class ImageController : Controller
    {
        
        IImageServise ims;
        ILikeServise ls;
       
        IMapper mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();




        public ImageController()
        {
            ServiceCreator serviceCreator = new ServiceCreator();
            ims = serviceCreator.CreateImageServise("DefaultConnection");
            ls = serviceCreator.CreateLikeServise("DefaultConnection");
           


        }



        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Upload()
        {
            if (User.IsInRole("banned"))
            {
                return Json(new { result = false, message = "Access denied. Profile has been banned" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (Request.Files.Count == 1)
                {
                    string description = Request.Form[0];
                    string type = Request.Form[1];
                    HttpPostedFileBase img = Request.Files[0];
                    if (img.IsImage())
                    {
                        ImageDTO image = new ImageDTO() { UserId = User.Identity.GetUserId() };
                        if(type=="galery")
                        {
                            image.Description = description;
                            image.IsMain = false;
                        }
                        else
                        {
                            image.Description = "";
                            image.IsMain = true;
                        }
                        string extension = Path.GetExtension(img.FileName);
                        string url = Guid.NewGuid().ToString();
                        image.Url = @"~/Files/" + url + extension;
                        img.SaveAs(Server.MapPath(image.Url));
                        await ims.CreateImage(image);
                        return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return Json(new { result = false, message = "File not valid" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { result = false, message = "File not valid" }, JsonRequestBehavior.AllowGet);
                }
            }

        }

    

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Delete(string imageId)
        {
            var image = ims.GetImage(imageId);
            await ims.Delete(image.Id);
            if (System.IO.File.Exists(Server.MapPath(image.Url)))
            {
                System.IO.File.Delete(Server.MapPath(image.Url));
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }
    }
}