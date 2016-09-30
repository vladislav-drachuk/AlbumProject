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

        public int GetCountOfIsLikes(string imageId)
        {
            return Database.LikeManager.FindBy(like => like.ImageId == imageId).Count();

        }

        public ICollection<LikeDTO> GetLikes(string imageId)
        {

            var likes = Database.LikeManager.FindBy(l => l.ImageId == imageId).ToList();

            return mapper.Map<ICollection<Like>, ICollection<LikeDTO>>(likes);
        }

        public async Task LikePhoto(string userName, string imageId)
        {

            if (Database.LikeManager.FindBy(like => like.ApplicationUser.UserName == userName && like.ImageId == imageId).Count()==0)
            {
                ApplicationUser currUser = await Database.UserManager.FindByNameAsync(userName);
                Database.LikeManager.Create(new Like
                {

                    ApplicationUser = currUser,
                    ApplicationUserId = currUser.Id,
                    ImageId = imageId
                });
                
                    await Database.SaveAsync();
                
            }

        }

        public async Task UnLikePhoto(string userName, string imageId)
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
        }

        
        /*  public ICollection<ImageDTO>  GetLikedPhotoesByUser(string userId)
          {

              var likes = Database.U.FindBy(like => like.ApplicationUserId == userId);

              return photoes;
          } 

      } */

    }
}
