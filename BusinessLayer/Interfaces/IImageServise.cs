using System;
using System.Collections.Generic;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using System.Threading;
using System.Threading.Tasks;
using AlbumProject.BusinessLogicLayer.Infrastructure;

namespace AlbumProject.BusinessLogicLayer.Interfaces
{
    public interface IImageServise: IDisposable
    {
        ICollection<ImageDTO> GetUserGaleryImages(string userName,PagingInfo pagingInfo);
        Task<ImageDTO> GetMainUserImageById(string userName);
        Task<ICollection<ImageDTO>> GetFavouriteImages(string userName, PagingInfo pagingInfo);
        Task<ICollection<ImageDTO>> GetImageNewsFeed(string userName, PagingInfo pagingInfo);
        ICollection<ImageDTO> SearchImage(string searchText);
        ImageDTO GetImage(string imageId);
        Task CreateImage(ImageDTO image);
        Task Delete(string imageId);

    }

}
