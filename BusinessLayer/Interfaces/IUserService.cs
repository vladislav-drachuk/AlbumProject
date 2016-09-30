using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using AlbumProject.BusinessLogicLayer.Infrastructure;

namespace AlbumProject.BusinessLogicLayer.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO user);
        Task<ClaimsIdentity> Authenticate(UserDTO user);
        Task SetInitialData(UserDTO admin, List<string> roles);

    

    }
}