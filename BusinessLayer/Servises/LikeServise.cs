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
    public class LikeServise : ILikeServise
    {
        private IUnitOfWork Database;
        private MapperConfiguration conf = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile<OrganizationDTOProfile>();
            });
        private IMapper mapper;


        public LikeServise(IUnitOfWork uow)
        {
            Database = uow;
            mapper = conf.CreateMapper();
        }

        /// <summary>
        /// Method check if user like image
        /// </summary>
        /// <param name="userName">User</param>
        /// <param name="imageId">Image id</param>
        public bool IsLiked(string userName, string imageId)
        {
            if (Database.LikeManager.FindBy(like => like.ApplicationUser.UserName == userName && like.ImageId == imageId).Count() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method get the number of image likes 
        /// </summary>
        /// <param name="imageId">Image id</param>
        /// /// <returns>Count of like</returns>
        public int GetCountOfIsLikes(string imageId)
        {
            return Database.LikeManager.FindBy(like => like.ImageId == imageId).Count();

        }

        public ICollection<LikeDTO> GetLikes(string imageId)
        {

            var likes = Database.LikeManager.FindBy(l => l.ImageId == imageId).ToList();

            return mapper.Map<ICollection<Like>, ICollection<LikeDTO>>(likes);
        }

        /// <summary>
        /// Method make user "like" photo
        /// </summary>
        /// <param name="userName">User username</param>
        ///<param name="imageId">Image id</param>
        /// <returns>Return "OperationDetails" object, that gives informanion if method been executed successful </returns>
        public async Task<OperationDetails> LikePhoto(string userName, string imageId)
        {

            ApplicationUser currUser = await Database.UserManager.FindByNameAsync(userName);
            Image image = Database.ImageManager.GetSingle(imageId);
            if (currUser == null)
            {
                return new OperationDetails(false, "User do not exist", "");
            }
            
            else if (image == null)
            {
                return new OperationDetails(false, "Image do not exist", "");
            }
            else 
            {
                if (Database.LikeManager.FindBy(like => like.ApplicationUser.UserName == userName && like.ImageId == imageId).Count() == 0)
                {
                    Database.LikeManager.Create(new Like
                    {

                        ApplicationUser = currUser,
                        ApplicationUserId = currUser.Id,
                        ImageId = imageId
                    });

                    await Database.SaveAsync();
                }
                return new OperationDetails(true, "", "");

            }




        }


        /// <summary>
        /// Method make user "unlike" photo
        /// </summary>
        /// <param name="userName">User username</param>
        ///<param name="imageId">Image id</param>
        /// <returns>Return "OperationDetails" object, that gives informanion if method been executed successful </returns>
        public async Task<OperationDetails> UnLikePhoto(string userName, string imageId)
        {
            ApplicationUser currUser = await Database.UserManager.FindByNameAsync(userName);
            Image image = Database.ImageManager.GetSingle(imageId);
            if (currUser == null)
            {
                return new OperationDetails(false, "User do not exist", userName);
            }

            else if (image == null)
            {
                return new OperationDetails(false, "Image do not exist", imageId);
            }
            else
            {
                var likeDb = Database.LikeManager.FindBy(l =>
                          l.ImageId == imageId &&
                          l.ApplicationUser.UserName == userName
                          ).FirstOrDefault();
                if (likeDb != null)
                {
                    Database.LikeManager.Delete(likeDb);
                    await Database.SaveAsync();
                    
                }

                return new OperationDetails(true, "", "");

            }
        }

        
        /*  public ICollection<ImageDTO>  GetLikedPhotoesByUser(string userId)
          {

              var likes = Database.U.FindBy(like => like.ApplicationUserId == userId);

              return photoes;
          } 

      } */

    }
}
