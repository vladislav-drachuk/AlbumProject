using System;
using System.Collections.Generic;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using System.Threading;
using System.Threading.Tasks;

namespace AlbumProject.BusinessLogicLayer.Interfaces
{
    public interface ICommentServise 
    {
        Task Create(CommentDTO comment);
        Task<IEnumerable<UserProfileDTO>> GetUserProfilesByPhotoComments(string photoId);
        IEnumerable<CommentDTO> GetAllPhotoComment(string photoId);
    }
}
