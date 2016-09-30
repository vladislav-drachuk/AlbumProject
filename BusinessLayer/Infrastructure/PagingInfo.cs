using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;

namespace AlbumProject.BusinessLogicLayer.Infrastructure
{
    public class PagingInfo
    {
        public int ItemsPerPage { get; set; }
        public List<string> IdCollection { get; set; }
        public DateTime? LastTime { get; set; }
        public bool IsEnd { get; set; }
        public PagingInfo(int itemsPerPage)
        {
            ItemsPerPage = itemsPerPage;
           
        }
        public void Update(ICollection<ImageDTO> images,bool isEnd)
        {
            IsEnd = isEnd;
            if (images.Count!=0)
            {
                
                LastTime = images.Where(img =>img!=null).ToList().Min(y => y.Date);
                IdCollection = images.Where(img => img != null && img.Date == LastTime)
                                     .Select(i => i.Id).ToList();
            }
        }
    }
}
