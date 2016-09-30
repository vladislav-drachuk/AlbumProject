using System;
using System.Collections.Generic;
using AlbumProject.DataLayer.Entities;
using AlbumProject.DataLayer.Interfaces;
using AlbumProject.BusinessLogicLayer.DataTransferObjects;
using AlbumProject.BusinessLogicLayer.Interfaces;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;


namespace AlbumProject.BusinessLogicLayer.Servises
{
   /* public class CommentServise: ICommentServise
    {
        private IUnitOfWork db;
        public CommentServise(IUnitOfWork iuow)
        {
            db = iuow;
        }
        public async Task Create(CommentDTO comment)
        {
            var photo = db.PhotoManager.GetSingle(comment.PhotoId);
            if(photo != null)
            {
                db.CommentManager.Create(new Comment
                {
                    Id = comment.Id,
                    ApplicationUserId = comment.CommentedUserId,
                    Text = comment.Text,
                    PhotoId = comment.PhotoId,
                    Date = comment.Date,
                });
                await db.SaveAsync();
            }
            else
            {
                throw new InvalidOperationException("comment");
            }

        }

        public async Task<IEnumerable<UserProfileDTO>> GetUserProfilesByPhotoComments(string photoId)
        {
            List<UserProfileDTO> users = new List<UserProfileDTO>();
            var comments = db.CommentManager.FindBy(c => c.PhotoId == photoId);
            foreach (var comment in comments)
            {
                var user = await db.UserManager.FindByIdAsync(comment.ApplicationUserId);

                users.Add(new UserProfileDTO { Id = user.Id, UserName = user.UserName });
            }
            return users;
        }

        public IEnumerable<CommentDTO> GetAllPhotoComment(string photoId)
        {
            List<CommentDTO> comments = new List<CommentDTO>();
            var photo = db.PhotoManager.GetSingle(photoId);
            if (photo != null)
            {
               var coms = db.CommentManager.FindBy(c => c.PhotoId == photoId);
               foreach(var comment in coms)
                {
                    comments.Add(new CommentDTO
                    {
                        Id = comment.Id,
                        CommentedUserId = comment.ApplicationUserId,
                        PhotoId = comment.PhotoId,
                        Text = comment.Text,
                        Date = comment.Date

                    });
                }
                return comments;
            }
            else
            {
                throw new InvalidOperationException("comment");
            }
        }

      
    }*/
}