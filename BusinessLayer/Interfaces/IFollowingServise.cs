using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumProject.BusinessLogicLayer.Interfaces
{
    public interface IFollowingServise
    {
        Task Follow(string curUserName, string followUserName);
        Task Unfollow(string currUserName, string followUserName);
        //Task UnFollow(string curUserId, string id);
        ICollection<UserProfileDTO> GetFollowers(string userName);
        ICollection<UserProfileDTO> GetFollowings(string userName);
        //bool IsFollowing(string curUserId, string id);
        bool IsFollowing(string currUserName, string UserName);
        int GetCountOfFollowers(string userName);
        int GetCountOfFollowings(string userName);
    }
}
