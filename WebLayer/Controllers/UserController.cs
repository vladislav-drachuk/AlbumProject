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
using AlbumProject.BusinessLogicLayer.Infrastructure;
using WebLayer.Infrastructure;
using WebLayer.Helpers;

namespace WebLayer.Controllers
{
    
    public class UserController : Controller
    {
      
        IUserProfileServise us;
        IFollowingServise fs;
        IImageServise ims;
        ILikeServise ls;
        IRoleServise rs;
        int itemsPerPage = 6;
        IMapper mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();


        public UserController()
        {
            ServiceCreator serviceCreator = new ServiceCreator();
            us = serviceCreator.CreateUserProfileServise("DefaultConnection");
            fs = serviceCreator.CreateFollowingServise("DefaultConnection");
            ims = serviceCreator.CreateImageServise("DefaultConnection");
            ls = serviceCreator.CreateLikeServise("DefaultConnection");
            rs = serviceCreator.CreateRoleServise("DefaultConnection");
        }

        public  async Task<ActionResult> Index(string userName)
        {
            UserProfileDTO userDTO =await  us.FindProfileByName(userName);
            UserModel user = mapper.Map<UserModel>(userDTO);
            user.IsFollowing = fs.IsFollowing(User.Identity.Name,userName);
            user.CountOfFollowers = fs.GetCountOfFollowers(userName);
            user.CountOfFollowings = fs.GetCountOfFollowings(userName);
            user.ProfileImage = mapper.Map<ImageModel>(ims.GetMainUserImageById(userName));
            if (User.IsInRole("admin"))
            {
                user.IsBlocked = await rs.IsInRole(userName, "banned");
                user.IsAdmin = await rs.IsInRole(userName, "admin");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Images(string userName, bool loadMore =false)
        {
            if (!loadMore)
            {
                Session["pageInfo"] = null;
                PagingInfo pg = new PagingInfo(itemsPerPage);
                var imagesDTO = ims.GetUserGaleryImages(userName, pg);
                var images = mapper.Map<ICollection<ImageDTO>, ICollection<ImageModel>>(imagesDTO);
                images.All(i =>
                {
                    i.IsLiked = ls.IsLiked(User.Identity.Name, i.Id);
                    i.Description = MvcExtensions.FormatDescriptionText(i.Description);
                    return true;
                });

                ViewBag.IsEnd = pg.IsEnd;
                Session["pageInfo"] = pg;
                return View(images);
            }
            else
            {
                PagingInfo pg = (PagingInfo)Session["pageInfo"];
                var imagesDTO = ims.GetUserGaleryImages(userName, pg);
                var images = mapper.Map<ICollection<ImageDTO>, ICollection<ImageModel>>(imagesDTO);
                images.All(i =>
                {
                    i.IsLiked = ls.IsLiked(User.Identity.Name, i.Id);
                    i.Description = MvcExtensions.FormatDescriptionText(i.Description);
                    return true;
                });
                string result = RazorViewToString.RenderRazorViewToString(this, "Images", images);
                bool isEnd = pg.IsEnd;
                return Json(new { isEnd = isEnd, partialView = result }, JsonRequestBehavior.AllowGet);
            }
        }



        [AjaxOnly]
        public ActionResult Followers(string userName)
        {
            ViewBag.Message = "Followers";
            var followers= mapper
                .Map<ICollection<UserProfileDTO>, ICollection<UserModel>>(fs.GetFollowers(userName));
            followers.All(follower =>
            {
                follower.ProfileImage = mapper.Map<ImageModel>(ims.GetMainUserImageById(follower.UserName));

                     return true;
            });
            return PartialView(followers);
        }

        [AjaxOnly]
        public ActionResult Followings(string userName)
        {
            ViewBag.Message = "Followings";
            var followings = mapper
               .Map<ICollection<UserProfileDTO>, ICollection<UserModel>>(fs.GetFollowings(userName));
            followings.All(following =>
                {
                    following.ProfileImage = mapper.Map<ImageModel>(ims.GetMainUserImageById(following.UserName));
                     return true;
                });
            return PartialView("Followers",followings);

        }
        
    }
}