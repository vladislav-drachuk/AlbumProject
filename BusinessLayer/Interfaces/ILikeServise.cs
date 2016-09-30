using System;
using System.Collections.Generic;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using System.Threading;
using System.Threading.Tasks;

namespace AlbumProject.BusinessLogicLayer.Interfaces
{
  public interface ILikeServise
    {

        bool IsLiked(string userName, string imageId);
        int GetCountOfIsLikes(string imageId);
        //Task<IEnumerable<UserProfileDTO>> GetUserProfilesByPhotoLike(string imageId);
        Task LikePhoto(string userName, string imageId);
        Task UnLikePhoto(string userName, string imageId);

       // IEnumerable<PhotoDTO> GetLikedPhotoesByUserId(string imageId);

    }
   
}