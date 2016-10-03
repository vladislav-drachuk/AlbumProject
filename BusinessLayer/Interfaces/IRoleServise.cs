using System;
using System.Collections.Generic;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using System.Threading;
using System.Threading.Tasks;
using AlbumProject.BusinessLogicLayer.Infrastructure;

namespace AlbumProject.BusinessLogicLayer.Interfaces
{
    public interface IRoleServise
    {
        Task<OperationDetails> AddToRole(string userName, string roleName);
        Task<OperationDetails> DeleteFromRole(string userName, string roleName);
        ICollection<UserProfileDTO> GetUsersByRole(string roleName);
        Task<bool> IsInRole(string userName, string roleName);
    }
}
