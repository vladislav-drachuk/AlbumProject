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

        public async Task Follow(string currUserName, string followUserName)
        {
            ApplicationUser currUser = await Database.UserManager.FindByNameAsync(currUserName);
            ApplicationUser followUser = await Database.UserManager.FindByNameAsync(followUserName);
            if (currUserName == followUserName)
            {
                throw new InvalidOperationException("Cant follow yourself");
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
            }
        }

        public async Task Unfollow(string currUserName, string followUserName)
        {
            ApplicationUser currUser = await Database.UserManager.FindByNameAsync(currUserName);
            ApplicationUser followUser = await Database.UserManager.FindByNameAsync(followUserName);
            var rel =Database.RelationshipManager.FindBy(r => r.FollowerUserId == currUser.Id && r.FollowingUserId == followUser.Id).FirstOrDefault();
            Database.RelationshipManager.Delete(rel);
            await Database.SaveAsync();

        }

        public int GetCountOfFollowers(string userName)
        {
            return Database.RelationshipManager.FindBy(r => r.FollowingUser.UserName == userName).Count();
        }


        public int GetCountOfFollowings(string userName)
        {
            return Database.RelationshipManager.FindBy(r => r.FollowerUser.UserName == userName).Count();
        }


        public bool IsFollowing(string curUserName, string userName)
        {
            return Database.RelationshipManager.FindBy(r => r.FollowerUser.UserName == curUserName && r.FollowingUser.UserName == userName).Count() == 1;
        }

        public ICollection<UserProfileDTO> GetFollowers(string userName)
        {
            var followers = Database.RelationshipManager.FindBy(r => r.FollowingUser.UserName == userName).ToList()
                                                        .Select(r => r.FollowerUser).ToList();
            return mapper.Map<ICollection<ApplicationUser>, ICollection<UserProfileDTO>>(followers);
        }

        public ICollection<UserProfileDTO> GetFollowings(string userName)
        {
            var followings = Database.RelationshipManager.FindBy(r => r.FollowerUser.UserName == userName).ToList()
                                                         .Select(r => r.FollowingUser).ToList();
            return mapper.Map<ICollection<ApplicationUser>, ICollection<UserProfileDTO>>(followings);
        }





    }
}


      /*  public async Task UnFollow(string curUserId, string id)
        {
            if (curUserId == id)
            {
                throw new InvalidOperationException("Cant unfollow yourself");
            }
            else
            {
                if (IsFollowing(curUserId,id))
                {
                    var rel = db.RelationshipManager.FindBy(r => r.FollowerUserId == curUserId && r.FollowingUserId == id).FirstOrDefault();
                    db.RelationshipManager.Delete(rel);
                    await db.SaveAsync();
                }
            }
        }
      
        public async Task<IEnumerable<UserProfileDTO>> GerFollowers(string id)
        {
            var rel = db.RelationshipManager.FindBy(r => r.FollowingUserId == id).ToList();

            List <UserProfileDTO> followers = new List<UserProfileDTO>();
            foreach(var r in rel)
                {

                     var follower = await  db.UserManager.FindByIdAsync(r.FollowerUserId);
                    
                //followers.Add(Mapper.Map<UserProfileDTO>(follower));
                followers.Add(new UserProfileDTO { Id = follower.Id, UserName = follower.UserName });
                }
            return followers;
        }


        public async Task<IEnumerable<UserProfileDTO>> GerFollowings(string id)
        {
            var rel = db.RelationshipManager.FindBy(r => r.FollowerUserId == id).ToList();
            List<UserProfileDTO> followings = new List<UserProfileDTO>();
            foreach (var r in rel)
            {
                var follower = await db.UserManager.FindByIdAsync(r.FollowingUserId);
                followings.Add(new UserProfileDTO { Id = follower.Id, UserName = follower.UserName });
            }
            return followings;
        }


        public bool IsFollowing(string curUserId, string id)
        {
            return db.RelationshipManager.FindBy(r => r.FollowerUserId == curUserId && r.FollowingUserId == id).Count()==1;
        }
        public bool IsFollowingByName(string curUserName, string userName)
        {
            return db.RelationshipManager.FindBy(r => r.FollowerUser.UserName == curUserName && r.FollowingUser.UserName == userName).Count()==1;
        }


        public int GetCountOfFollowers(string id)
        {
            return db.RelationshipManager.FindBy(r=>r.FollowingUserId == id).Count();
        }


        public int GetCountOfFollowings(string id)
        {
            return db.RelationshipManager.FindBy(r => r.FollowerUserId == id).Count();
        }

    }
}
*/