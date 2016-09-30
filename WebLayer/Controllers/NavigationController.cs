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
    public class NavigationController : Controller
    {

        ServiceCreator serviceCreator = new ServiceCreator();
        IImageServise ims;
        ILikeServise likeServise;
      
        int itemsPerPage = 3;
        IMapper mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();


        public NavigationController()
        {
            ims = serviceCreator.CreateImageServise("DefaultConnection");
            likeServise = serviceCreator.CreateLikeServise("DefaultConnection");
        }


        [Authorize]
        public async Task<ActionResult> Favorites(bool loadMore=false)
        {
            PagingInfo pg = new PagingInfo(itemsPerPage);
            if(loadMore)
            {
                pg = (PagingInfo)Session["pageInfo"];
            }
            var imagesDTO = await ims.GetFavouriteImages(User.Identity.Name, pg);
            var images = mapper.Map<ICollection<ImageDTO>, ICollection<ImageModel>>(imagesDTO);
            images.SetLikeStatus(likeServise, User.Identity.Name);
            images.SetLikeCount(likeServise);
            Session["pageInfo"] = pg;
            ViewBag.IsEnd = pg.IsEnd;
           /* if (Request.IsAjaxRequest())
            {
                bool isEnd = pg.IsEnd;
                string result = RazorViewToString.RenderRazorViewToString(this, "GetUserImages", images);
                return Json(new { isEnd = isEnd, partialView = result }, JsonRequestBehavior.AllowGet);
            }
            */
            
                return View("NavigationImageView", images);
            
        }

        public async Task<ActionResult> Feed(bool loadMore = false)
        {
            /* PagingInfo pg = new PagingInfo(itemsPerPage);
             if (loadMore)
             {
                 pg = (PagingInfo)Session["pageInfo"];
             }
             var imagesDTO = await ims.GetImageNewsFeed(User.Identity.Name, pg);
             var images = mapper.Map<ICollection<ImageDTO>, ICollection<ImageModel>>(imagesDTO);
             images.SetLikeStatus(likeServise, User.Identity.Name);
             images.SetLikeCount(likeServise);
             Session["pageInfo"] = pg;
             ViewBag.IsEnd = pg.IsEnd;
             if (Request.IsAjaxRequest())
             {
                 bool isEnd = pg.IsEnd;
                 string result = RazorViewToString.RenderRazorViewToString(this, "GetUserImages", images);
                 return Json(new { isEnd = isEnd, partialView = result }, JsonRequestBehavior.AllowGet);
             }  
             return View("NavigationImageView", images);
             */
            return HttpNotFound();
        }

        public ActionResult Search(string text="")
        {
            var images = mapper.Map<ICollection<ImageDTO>, ICollection<ImageModel>>(ims.SearchImage(text));
            images.All(i =>
            {
                i.Description = MvcExtensions.FormatDescriptionText(i.Description);
                return true;
            });
            return View("NavigationImageView", images);
        }

    }
       
}