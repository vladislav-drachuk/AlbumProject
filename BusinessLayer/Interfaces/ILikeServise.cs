using System;
using System.Collections.Generic;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using System.Threading;
using System.Threading.Tasks;
using AlbumProject.BusinessLogicLayer.Infrastructure;

namespace AlbumProject.BusinessLogicLayer.Interfaces
{
  public interface ILikeServise
    {

        bool IsLiked(string userName, string imageId);
        int GetCountOfIsLikes(string imageId);
        Task<OperationDetails> LikePhoto(string userName, string imageId);
        Task<OperationDetails> UnLikePhoto(string userName, string imageId);

    }
   
}