using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLayer.Models;
using AlbumProject.BusinessLogicLayer.Interfaces;

namespace WebLayer.Infrastructure
{
    public static class StatusSetter
    {
        public static void SetLikeStatus(this ICollection<ImageModel> images, ILikeServise likeServise ,string userName)
        {
            images.All(i =>
            {
                i.IsLiked = likeServise.IsLiked(userName, i.Id);
                return true;
            });
        }

        public static void SetLikeStatus(this ImageModel image, ILikeServise likeServise, string userName)
        {
            image.IsLiked = likeServise.IsLiked(userName, image.Id);
        }

        public static void SetLikeCount(this ICollection<ImageModel> images, ILikeServise likeServise)
        {
            images.All(image =>
                      {
                          image.CountOfLikes = likeServise.GetCountOfIsLikes(image.Id);
                          return true;
                      });
        }

        public static void SetLikeCount(this ImageModel image, ILikeServise likeServise)
        {
            image.CountOfLikes = likeServise.GetCountOfIsLikes(image.Id);
        }

        public static void SetFollowingStatus(this ICollection<UserModel> users, IFollowingServise followingServise, string currUserName)
        {
            users.All(user =>
            {
                user.IsFollowing = followingServise.IsFollowing(currUserName, user.UserName);
                return true;
            });
        }

        public static void SetFollowingStatus(UserModel user, IFollowingServise followingServise, string currUserName)
        {
           
                user.IsFollowing = followingServise.IsFollowing(currUserName, user.UserName);
                
        }

        public static void SetRoleStatus(UserModel user, IRoleServise roleServise, string currUserName)
        {

        

        }

    }
}