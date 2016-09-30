using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;

namespace AlbumProject.BusinessLogicLayer.Interfaces
{
    public interface ISearchServise
    {
        ICollection<ImageDTO> FindImageByText(string searchText);
    }
    
}
