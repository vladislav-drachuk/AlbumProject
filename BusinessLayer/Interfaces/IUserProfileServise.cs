using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using AlbumProject.BusinessLogicLayer.Infrastructure;

namespace AlbumProject.BusinessLogicLayer.Interfaces
{
    public interface  IUserProfileServise
    {
        Task<UserProfileDTO> FindProfileByName(string userName);
        Task<UserProfileDTO> FindProfileById(string userId);

    }
}
