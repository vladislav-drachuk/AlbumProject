using System;
using System.Collections.Generic;
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
   public class ImageServise : IImageServise
    {
        private IUnitOfWork Database;
        private MapperConfiguration conf = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile<OrganizationDTOProfile>();
            });
        private IMapper mapper;
        
       

   public ImageServise(IUnitOfWork iuow)
        {
            Database = iuow;
            //    config = new Configuration();
            //config.Configure();

            mapper = conf.CreateMapper();
            

        }

        /// <summary>
        /// Method returns user's images 
        /// </summary>
        /// <param name="userName">User's username</param>
        ///  <param name="pagingInfo">Information about current page</param>
        ///  <returns>Collection of images</returns>
        public ICollection<ImageDTO> GetUserGaleryImages(string userName, PagingInfo pagingInfo)
       {
            if (pagingInfo.LastTime == null)
            {
                var images = Database.ImageManager.FindBy(i => i.ApplicationUser.UserName == userName 
                                                   && i.IsMain == false)
                                                   .OrderByDescending(i => i.Date)
                                                   .Take(pagingInfo.ItemsPerPage)
                                                   .ToList();
                bool isEnd = Database.ImageManager.FindBy(i => i.ApplicationUser.UserName == userName
                                                   && i.IsMain == false)
                                                   .OrderByDescending(i => i.Date)
                                                   .Count() <= pagingInfo.ItemsPerPage;
                var imagesDTO = mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
                pagingInfo.Update(imagesDTO, isEnd);
                
                return imagesDTO;
            }
            else
            {
                var images = Database.ImageManager.FindBy(i => i.ApplicationUser.UserName == userName
                              && i.IsMain == false
                             && i.Date<=pagingInfo.LastTime
                             && !pagingInfo.IdCollection.Contains(i.Id))
                             .OrderByDescending(i => i.Date)
                             .Take(pagingInfo.ItemsPerPage)
                             .ToList();
                bool isEnd = Database.ImageManager.FindBy(i => i.ApplicationUser.UserName == userName
                              && i.IsMain == false
                             && i.Date <= pagingInfo.LastTime
                             && !pagingInfo.IdCollection.Contains(i.Id))
                             .OrderByDescending(i => i.Date)
                             .Count() <= pagingInfo.ItemsPerPage;
                
                var imagesDTO = mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
                pagingInfo.Update(imagesDTO, isEnd);
                return imagesDTO;
            }
          
       }
     
   public ImageDTO GetMainUserImageById(string userName)
      {
          
            return mapper.Map<ImageDTO>(Database.ImageManager.FindBy
                 (i => i.ApplicationUser.UserName == userName && i.IsMain == true).FirstOrDefault());
    }

        /// <summary>
        /// Method returns followings images  of user
        /// </summary>
        /// <param name="userName">User's username</param>
        ///  <param name="pagingInfo">Information about current page</param>
        ///  <returns>Collection of images</returns>
        public async Task<ICollection<ImageDTO>> GetImageNewsFeed(string userName, PagingInfo pagingInfo)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
            ICollection<string> followingsId = user.Followers.Select(f => f.FollowingUser.Id).ToList();
            if (pagingInfo.LastTime == null)
            {
                var images = Database.ImageManager.FindBy(img => followingsId.Any(followedId => followedId==img.ApplicationUserId)
                                              && img.IsMain == false)
                                             .OrderByDescending(i => i.Date).Take(pagingInfo.ItemsPerPage).ToList();
                bool isEnd = Database.ImageManager.FindBy(img => followingsId.Any(followedId => followedId == img.ApplicationUserId)
                                              && img.IsMain == false)
                                             .OrderByDescending(i => i.Date).Count() <= pagingInfo.ItemsPerPage;
                var imagesDTO = mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
                pagingInfo.Update(imagesDTO, isEnd);
                return imagesDTO;
            }
            else
            {
                var images = Database.ImageManager.FindBy(img => followingsId.Any(followedId => followedId == img.ApplicationUserId)
                                && img.IsMain == false
                             && img.Date <= pagingInfo.LastTime
                             && !pagingInfo.IdCollection.Contains(img.Id))
                             .OrderByDescending(img => img.Date)
                             .Take(pagingInfo.ItemsPerPage)
                             .ToList();
                bool isEnd = Database.ImageManager.FindBy(img => followingsId.Any(followedId => followedId == img.ApplicationUserId)
                             && img.IsMain == false
                             && img.Date <= pagingInfo.LastTime
                             && !pagingInfo.IdCollection.Contains(img.Id))
                             .OrderByDescending(img => img.Date)
                             .Count() <= pagingInfo.ItemsPerPage;

                var imagesDTO = mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
                pagingInfo.Update(imagesDTO, isEnd);
                return imagesDTO;
            }
        }

        /// <summary>
        /// Method returns user's images that he liked
        /// </summary>
        /// <param name="userName">User's username</param>
        ///  <param name="pagingInfo">Information about current page</param>
        ///  <returns>Collection of images</returns>
        public async Task<ICollection<ImageDTO>> GetFavouriteImages(string userName, PagingInfo pagingInfo)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
            if (pagingInfo.LastTime == null)
            {
             
                var images = Database.ImageManager.FindBy(image => image.Likes.Any(l => l.ApplicationUser.UserName == userName)
                                              && image.IsMain == false)
                                             .OrderByDescending(i => i.Date)
                                             .Take(pagingInfo.ItemsPerPage).Where(e => e != null).ToList();
                /* .Select(like => like.Image)
                 .OrderByDescending(i => i.Date)
                 .Take(pagingInfo.ItemsPerPage).Where(e => e != null)
                 .ToList();*/
                bool isEnd = Database.ImageManager.FindBy(image => image.Likes.Any(l => l.ApplicationUser.UserName == userName)
                                    && image.IsMain == false)
                              .OrderByDescending(i => i.Date).Count() <= pagingInfo.ItemsPerPage;
                var imagesDTO = mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
                pagingInfo.Update(imagesDTO, isEnd);
                return imagesDTO;
            }
            else
            {
                var images = Database.ImageManager.FindBy(image => image.Likes.Any(l => l.ApplicationUser.UserName == userName)
                             && image.IsMain == false
                             && image.Date <= pagingInfo.LastTime
                             && !pagingInfo.IdCollection.Contains(image.Id))
                              .OrderByDescending(i => i.Date)
                              .Take(pagingInfo.ItemsPerPage).Where(e => e != null)
                             .ToList();
                bool isEnd = Database.ImageManager.FindBy(image => image.Likes.Any(l => l.ApplicationUser.UserName == userName)
                             && image.IsMain == false
                             && image.Date <= pagingInfo.LastTime
                             && !pagingInfo.IdCollection.Contains(image.Id))
                              .OrderByDescending(i => i.Date)
                             .Count() <= pagingInfo.ItemsPerPage;

                var imagesDTO = mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
                pagingInfo.Update(imagesDTO, isEnd);
                return imagesDTO;
            }
        }

   public ImageDTO GetImage(string imageId)
    {
            var img = Database.ImageManager.GetSingle(imageId);
            return mapper.Map<ImageDTO>(img);   
    }

        /// <summary>
        /// Method save image to database
        /// </summary>
        /// <param name="image">Image</param>
        public async Task CreateImage(ImageDTO image)
    {
        if(image.IsMain==true)
            {
                var images = Database.ImageManager.FindBy(img => img.IsMain == true);
                foreach(var img in images)
                {
                    Database.ImageManager.Delete(img);
                }
              
            }
        Database.ImageManager.Create(new Image
        {
            
            Url = image.Url,
            IsMain = image.IsMain,
            Description = image.Description,
            ApplicationUserId = image.UserId,
            ApplicationUser = await Database.UserManager.FindByIdAsync(image.UserId)

        });
            
        await Database.SaveAsync();
    }
       
        /// <summary>
        /// Method delete image from database
        /// </summary>
        /// <param name="image">Image</param>
        public async Task Delete(string imageId)
    {
        var image = Database.ImageManager.GetSingle(imageId);
        if (image != null)
            {
                Database.ImageManager.Delete(image);

                foreach (var like in image.Likes)
                {
                    Database.LikeManager.Delete(like);
                }
                await Database.SaveAsync();
            }
    }

        /// <summary>
        /// Method search image by words in description
        /// </summary>
        /// <param name="searchText">Text to search in description</param>
        ///  <param name="pagingInfo">Information about current page</param>
        ///   <returns>Collection of images</returns>
        public ICollection<ImageDTO> SearchImage(string searchText, PagingInfo pagingInfo)
        {


            var searchWords = searchText.Split(new[] {
                                    ' ', ',', ':', '?', '!', '+', '-', '(', ')' },
                                      StringSplitOptions.RemoveEmptyEntries);
            if (pagingInfo.LastTime == null)
            {
                var images = Database.ImageManager.GetAll().Where(image => searchWords.All(image.Description.Contains)
                                              && image.IsMain==false)
                                             .OrderByDescending(i => i.Date)
                                             .Take(pagingInfo.ItemsPerPage).Where(e => e != null)
                                             .ToList();
                bool isEnd = Database.ImageManager.GetAll().Where(image => searchWords.All(image.Description.Contains)
                                              && image.IsMain == false)
                                             .OrderByDescending(i => i.Date).Count() <= pagingInfo.ItemsPerPage;
                var imagesDTO = mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
                pagingInfo.Update(imagesDTO, isEnd);
                return imagesDTO;

            }
            else
            {
                var images = Database.ImageManager.GetAll().Where(image => searchWords.All(image.Description.Contains)
                             && image.IsMain == false
                             && image.Date <= pagingInfo.LastTime
                             && !pagingInfo.IdCollection.Contains(image.Id))
                              .OrderByDescending(i => i.Date)
                              .Take(pagingInfo.ItemsPerPage).Where(e => e != null)
                             .ToList();
                bool isEnd = Database.ImageManager.GetAll().Where(image => searchWords.All(image.Description.Contains)
                             && image.IsMain == false
                             && image.Date <= pagingInfo.LastTime
                             && !pagingInfo.IdCollection.Contains(image.Id))
                              .OrderByDescending(i => i.Date)
                             .Count() <= pagingInfo.ItemsPerPage;

                var imagesDTO = mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
                pagingInfo.Update(imagesDTO, isEnd);
                return imagesDTO;
            }
            //return mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);

        }



        public void Dispose()
    {
        Database.Dispose();
    }
}


}
