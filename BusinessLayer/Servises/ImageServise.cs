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

   public ICollection<ImageDTO> GetUserGaleryImages(string userName, PagingInfo paggingInfo)
       {
            if (paggingInfo.LastTime == null)
            {
                var images = Database.ImageManager.FindBy(i => i.ApplicationUser.UserName == userName).OrderByDescending(i => i.Date).Take(paggingInfo.ItemsPerPage).ToList();
                bool isEnd = Database.ImageManager.FindBy(i => i.ApplicationUser.UserName == userName).OrderByDescending(i => i.Date).Count() <= paggingInfo.ItemsPerPage;
                var imagesDTO = mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
                paggingInfo.Update(imagesDTO, isEnd);
                
                return imagesDTO;
            }
            else
            {
                var images = Database.ImageManager.FindBy(i => i.ApplicationUser.UserName == userName
                             && i.Date<=paggingInfo.LastTime
                             && !paggingInfo.IdCollection.Contains(i.Id))
                             .OrderByDescending(i => i.Date)
                             .Take(paggingInfo.ItemsPerPage)
                             .ToList();
                bool isEnd = Database.ImageManager.FindBy(i => i.ApplicationUser.UserName == userName
                             && i.Date <= paggingInfo.LastTime
                             && !paggingInfo.IdCollection.Contains(i.Id))
                             .OrderByDescending(i => i.Date)
                             .Count() <= paggingInfo.ItemsPerPage;
                
                var imagesDTO = mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
                paggingInfo.Update(imagesDTO, isEnd);
                return imagesDTO;
            }
          
       }
     
   public async Task<ImageDTO> GetMainUserImageById(string userName)
      {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
            return mapper.Map<ImageDTO>(Database.ImageManager.FindBy
                 (i => i.ApplicationUserId == user.Id && i.IsMain == true).FirstOrDefault());
    }

   public async Task<ICollection<ImageDTO>> GetImageNewsFeed(string userName, PagingInfo pagingInfo)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
            if (pagingInfo.LastTime == null)
            {
                var images = Database.ImageManager.FindBy(img => user.Followers.Select(f => f.Id).Contains(img.ApplicationUserId))
                                             .OrderByDescending(i => i.Date).Take(pagingInfo.ItemsPerPage).ToList();
                bool isEnd = Database.ImageManager.FindBy(img => user.Followers.Select(f => f.Id).Contains(img.ApplicationUserId))
                                             .OrderByDescending(i => i.Date).Count() <= pagingInfo.ItemsPerPage;
                var imagesDTO = mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
                pagingInfo.Update(imagesDTO, isEnd);
                return imagesDTO;
            }
            else
            {
                var images = Database.ImageManager.FindBy(img => user.Followers.Select(f => f.Id).Contains(img.ApplicationUserId)
                             && img.Date <= pagingInfo.LastTime
                             && !pagingInfo.IdCollection.Contains(img.Id))
                             .OrderByDescending(img => img.Date)
                             .Take(pagingInfo.ItemsPerPage)
                             .ToList();
                bool isEnd = Database.ImageManager.FindBy(img => user.Followers.Select(f => f.Id).Contains(img.ApplicationUserId)
                             && img.Date <= pagingInfo.LastTime
                             && !pagingInfo.IdCollection.Contains(img.Id))
                             .OrderByDescending(img => img.Date)
                             .Count() <= pagingInfo.ItemsPerPage;

                var imagesDTO = mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
                pagingInfo.Update(imagesDTO, isEnd);
                return imagesDTO;
            }
        }

   public async Task<ICollection<ImageDTO>> GetFavouriteImages(string userName, PagingInfo pagingInfo)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
            if (pagingInfo.LastTime == null)
            {
             
                var images = Database.ImageManager.FindBy(image => image.Likes.All(l => l.ApplicationUser.UserName == userName))
                                             .OrderByDescending(i => i.Date)
                                             .Take(pagingInfo.ItemsPerPage).Where(e => e != null).ToList();
                                            /* .Select(like => like.Image)
                                             .OrderByDescending(i => i.Date)
                                             .Take(pagingInfo.ItemsPerPage).Where(e => e != null)
                                             .ToList();*/
                bool isEnd = Database.LikeManager.FindBy(like => like.ApplicationUser.UserName == user.UserName)
                                             .OrderByDescending(i => i.Date).Count() <= pagingInfo.ItemsPerPage;
                var imagesDTO = mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
                pagingInfo.Update(imagesDTO, isEnd);
                return imagesDTO;
            }
            else
            {
                var images = Database.ImageManager.FindBy(image => image.Likes.All(l => l.ApplicationUser.UserName == userName)
                             && image.Date <= pagingInfo.LastTime
                             && !pagingInfo.IdCollection.Contains(image.Id))
                              .OrderByDescending(i => i.Date)
                              .Take(pagingInfo.ItemsPerPage).Where(e => e != null)
                             .ToList();
                bool isEnd = Database.ImageManager.FindBy(image => image.Likes.All(l => l.ApplicationUser.UserName == userName)
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
    
   public async Task CreateImage(ImageDTO image)
    {
        Database.ImageManager.Create(new Image
        {
            Id = image.Id,
            Url = image.Url,
            IsMain = image.IsMain,
            Description = image.Description,
            ApplicationUserId = image.UserId,
            ApplicationUser = await Database.UserManager.FindByIdAsync(image.UserId)

        });
        await Database.SaveAsync();
    }

   public async Task Delete(string imageId)
    {
        var image = Database.ImageManager.GetSingle(imageId);
        Database.ImageManager.Delete(image);
           
            foreach (var like in image.Likes)
            {
                Database.LikeManager.Delete(like);
            }
            await Database.SaveAsync();

    }

   public ICollection<ImageDTO> SearchImage(string searchText)
        {


            var searchWords = searchText.Split(new[] {
                                    ' ', ',', ':', '?', '!', '+', '-', '(', ')' },
                                      StringSplitOptions.RemoveEmptyEntries);

            var images = Database.ImageManager.GetAll().Where(p => searchWords.All(p.Description.Contains)).ToList();

            return mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images);
        }

   private bool ContatinsSearchWords(string description, string[] searchwords)
        {
            bool isContain = true;
            if (searchwords.Count() == 0)
            {
                return false;
            }
            foreach (var word in searchwords)
            {
                isContain = description.Contains(word);
                if (isContain == false)
                {
                    return false;
                }

            }
            return isContain;
        }

        public void Dispose()
    {
        Database.Dispose();
    }
}


}
