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

        
        IImageServise ims;
        ILikeServise likeServise;
      
        int itemsPerPage = 3;
        IMapper mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();


        public NavigationController()
        {
            ServiceCreator serviceCreator = new ServiceCreator();
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
            else
            {
                ViewBag.Message = "Favorites";
            }
            var imagesDTO = await ims.GetFavouriteImages(User.Identity.Name, pg);
            var images = mapper.Map<ICollection<ImageDTO>, ICollection<ImageModel>>(imagesDTO);
            images.SetLikeStatus(likeServise, User.Identity.Name);
            images.SetLikeCount(likeServise);
            images.All(i =>
            {
                i.Description = MvcExtensions.FormatDescriptionText(i.Description);
                return true;
            });
            Session["pageInfo"] = pg;
            ViewBag.IsEnd = pg.IsEnd;
            ViewBag.From = "favorites";
            if (!Request.IsAjaxRequest())
            {
                return View("NavigationView", images);
            }
         
            
                return View("NavigationImageView", images);
            
        }

        [Authorize]
        public async Task<ActionResult> Feed(bool loadMore = false)
        {
            PagingInfo pg = new PagingInfo(itemsPerPage);
            if (loadMore)
            {
                pg = (PagingInfo)Session["pageInfo"];
            }
            else
            {
                ViewBag.Message = "Feed";
            }
            var imagesDTO = await ims.GetImageNewsFeed(User.Identity.Name, pg);
            var images = mapper.Map<ICollection<ImageDTO>, ICollection<ImageModel>>(imagesDTO);
            images.SetLikeStatus(likeServise, User.Identity.Name);
            images.SetLikeCount(likeServise);
            images.All(i =>
            {
                i.Description = MvcExtensions.FormatDescriptionText(i.Description);
                return true;
            });
            Session["pageInfo"] = pg;
            ViewBag.IsEnd = pg.IsEnd;
            ViewBag.From = "newsfeed";
            if (!Request.IsAjaxRequest())
            {
                return View("NavigationView", images);
            }


            return View("NavigationImageView", images);
        }

        
        public ActionResult Search(string searchText = "", bool loadMore = false)
        {
            
            PagingInfo pg = new PagingInfo(itemsPerPage);
            if (loadMore)
            {
                pg = (PagingInfo)Session["pageInfo"];
            }
            else
            {
                ViewBag.Message = "Search: " + searchText;
            }
            var images = mapper.Map<ICollection<ImageDTO>, ICollection<ImageModel>>(ims.SearchImage(searchText, pg));
            images.All(i =>
            {
                i.Description = MvcExtensions.FormatDescriptionText(i.Description);
                return true;
            });
            images.SetLikeStatus(likeServise, User.Identity.Name);
            images.SetLikeCount(likeServise);
            Session["pageInfo"] = pg;
            ViewBag.IsEnd = pg.IsEnd;
            ViewBag.From = "search";
            ViewBag.Key = searchText;
            return View("NavigationImageView", images);
        }

    }
       
}