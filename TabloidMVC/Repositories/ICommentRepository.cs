using TabloidMVC.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;


namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetAllComments();
        List<Comment> GetAllCommentsByPost(int id);
        Comment GetCommentById(int id);
    }
}
