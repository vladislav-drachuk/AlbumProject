using System;
using System.Collections.Generic;
using AlbumProject.DataLayer.Entities;
using AlbumProject.DataLayer.Interfaces;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using AlbumProject.BusinessLogicLayer.Interfaces;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using AlbumProject.BusinessLogicLayer.MappingConfiguration;
using AlbumProject.BusinessLogicLayer.Infrastructure;

namespace AlbumProject.BusinessLogicLayer.Servises
{
    public class FollowingServise : IFollowingServise
    {

        private IUnitOfWork Database;
        private MapperConfiguration conf = new MapperConfiguration(cfg =>
                            {
                                  cfg.AddProfile<OrganizationDTOProfile>();
                            });
        private IMapper mapper;

        public FollowingServise(IUnitOfWork iuow)
        {
            Database = iuow;
            mapper = conf.CreateMapper();
        }

        /// <summary>
        /// Method follow one user to another
        /// </summary>
        /// <param name="currUserName">Follower username</param>
        /// <param name="followUserName">Following username</param>
        /// <returns>Return "OperationDetails" object, that gives informanion if method been executed successful </returns>
        public async Task<OperationDetails> Follow(string currUserName, string followUserName)
        {
            ApplicationUser currUser = await Database.UserManager.FindByNameAsync(currUserName);
            if (currUser == null)
            {
                return new OperationDetails(false, "User do not exist", "");
            }
            ApplicationUser followUser = await Database.UserManager.FindByNameAsync(followUserName);
            if (currUser == null)
            {
                return new OperationDetails(false, "User do not exist", "");
            }
            if (currUserName == followUserName)
            {
                return new OperationDetails(false, "Cant follow yourself", "");
            }
            else
            {
                if (!IsFollowing(currUserName, followUserName))
                {
                    var rel = new Relationship
                    {
                        FollowerUser = currUser,
                        FollowerUserId = currUser.Id,
                        FollowingUser = followUser,
                        FollowingUserId = followUser.Id
                    };
                    Database.RelationshipManager.Create(rel);
                    await Database.SaveAsync();
                    
                }
                return new OperationDetails(true, "", "");
            }
        }

        /// <summary>
        /// Method unfollow one user from another
        /// </summary>
        /// <param name="currUserName">Follower username</param>
        /// <param name="followUserName">Following username</param>
        /// <returns>Return "OperationDetails" object, that gives informanion if method been executed successful </returns>
        public async Task<OperationDetails> Unfollow(string currUserName, string followUserName)
        {
           
            ApplicationUser currUser = await Database.UserManager.FindByNameAsync(currUserName);
            if (currUser == null)
            {
                return new OperationDetails(false, "User do not exist", "");
            }
            ApplicationUser followUser = await Database.UserManager.FindByNameAsync(followUserName);
            if (currUser == null)
            {
                return new OperationDetails(false, "User do not exist", "");
            }
            var rel =Database.RelationshipManager.FindBy(r => r.FollowerUserId == currUser.Id && r.FollowingUserId == followUser.Id).FirstOrDefault();
            if (rel != null)
            {
                Database.RelationshipManager.Delete(rel);
                await Database.SaveAsync();
            }
            return new OperationDetails(true, "", "");
        }

        /// <summary>
        /// Method get the number of user's followers 
        /// </summary>
        /// <param name="userName">User's username</param>
        ///  <returns>Count of followers</returns>
        public int GetCountOfFollowers(string userName)
        {
            return Database.RelationshipManager.FindBy(r => r.FollowingUser.UserName == userName).Count();
        }

        /// <summary>
        /// Method get the number of user's followings 
        /// </summary>
        /// <param name="userName">User's username</param>
        /// /// <returns>Count of followings</returns>
        public int GetCountOfFollowings(string userName)
        {
            return Database.RelationshipManager.FindBy(r => r.FollowerUser.UserName == userName).Count();
        }

        /// <summary>
        /// Method check if one user follow another
        /// </summary>
        /// <param name="currUserName">Follower username</param>
        /// <param name="followUserName">Following username</param>
        public bool IsFollowing(string curUserName, string userName)
        {
            return Database.RelationshipManager.FindBy(r => r.FollowerUser.UserName == curUserName && r.FollowingUser.UserName == userName).Count() == 1;
        }

        /// <summary>
        /// Method returns user's followers 
        /// </summary>
        /// <param name="userName">User's username</param>
        ///  <returns>Collection of followers</returns>
        public ICollection<UserProfileDTO> GetFollowers(string userName)
        {
            var followers = Database.RelationshipManager.FindBy(r => r.FollowingUser.UserName == userName).ToList()
                                                        .Select(r => r.FollowerUser).ToList();
            return mapper.Map<ICollection<ApplicationUser>, ICollection<UserProfileDTO>>(followers);
        }

        /// <summary>
        /// Method returns user's followings 
        /// </summary>
        /// <param name="userName">User's username</param>
        ///  <returns>Collection of followingss</returns>
        public ICollection<UserProfileDTO> GetFollowings(string userName)
        {
            var followings = Database.RelationshipManager.FindBy(r => r.FollowerUser.UserName == userName).ToList()
                                                         .Select(r => r.FollowingUser).ToList();
            return mapper.Map<ICollection<ApplicationUser>, ICollection<UserProfileDTO>>(followings);
        }





    }
}


    