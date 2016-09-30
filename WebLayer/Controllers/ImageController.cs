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
        ServiceCreator serviceCreator = new ServiceCreator();
        IImageServise ims;
        ILikeServise ls;
        int itemsPerPage = 3;
        IMapper mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();




        public ImageController()
        {
            ims = serviceCreator.CreateImageServise("DefaultConnection");
            ls = serviceCreator.CreateLikeServise("DefaultConnection");
           


        }

        public ActionResult Index(string imageId)
        {
            ImageModel image = mapper.Map<ImageModel>(ims.GetImage(imageId));
            image.SetLikeCount(ls);
            image.SetLikeCount(ls);
            return View(image);
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
                if (Request.Files.Count > 0)
                {
                    string description = Request.Form[0];
                    HttpPostedFileBase img = Request.Files[0];
                    ImageDTO image = new ImageDTO() { UserId = User.Identity.GetUserId(), Description = description, IsMain = false };
                    string extension = Path.GetExtension(img.FileName);
                    string id = Guid.NewGuid().ToString();
                    image.Id = id;
                    image.Url = @"~/Files/" + id + extension;
                    img.SaveAs(Server.MapPath(image.Url));
                    await ims.CreateImage(image);
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { result = false, message = "error" }, JsonRequestBehavior.AllowGet);
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