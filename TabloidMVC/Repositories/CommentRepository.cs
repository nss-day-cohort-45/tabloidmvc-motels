using TabloidMVC.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


namespace TabloidMVC.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IConfiguration _config;

        public CommentRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }





        public void AddComment(Comment comment)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                INSERT INTO Comment (Subject, Content, PostId, UserProfileId, CreateDateTime)
                OUTPUT INSERTED.ID
                VALUES (@subject, @content, @postId, @userProfileId, @createDateTime);
            ";

                    cmd.Parameters.AddWithValue("@subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@content", comment.Content);
                    cmd.Parameters.AddWithValue("@userProfileId", comment.UserProfileId);
                    cmd.Parameters.AddWithValue("@createDateTime", comment.CreateDateTime);
                    cmd.Parameters.AddWithValue("@postId", comment.PostId);
                    int newlyCreatedId = (int)cmd.ExecuteScalar();

                    comment.Id = newlyCreatedId;

                }
            }
        }





        public List<Comment> GetAllComments()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  c.Id, 
                                c.PostId,
                                p.Title AS Title 
                                c.UserProfileId,
                                up.FirstName AS Author,
                                c.Subject, 
                                c.Content, 
                                c.CreateDateTime
                        FROM Comment c
                        LEFT JOIN Post p ON c.PostId = p.id
                        LEFT JOIN UserProfile up ON c.UserProfileId = up.id
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Comment> comments = new List<Comment>();
                    while (reader.Read())
                    {
                        Comment comment = new Comment
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Author = new UserProfile() { FirstName = reader.GetString(reader.GetOrdinal("Author"))}, 
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        };
                        comments.Add(comment);
                    }

                    reader.Close();

                    return comments;
                }
            }
        }





        public List<Comment> GetAllCommentsByPost(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  c.Id, 
                                c.PostId,
                                p.Title AS Title, 
                                c.UserProfileId,
                                up.FirstName AS Author,
                                c.Subject, 
                                c.Content, 
                                c.CreateDateTime
                        FROM Comment c
                        LEFT JOIN Post p ON c.PostId = p.id
                        LEFT JOIN UserProfile up ON c.UserProfileId = up.id
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Comment> comments = new List<Comment>();
                    while (reader.Read())
                    {
                        Comment comment = new Comment
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Author = new UserProfile() { FirstName = reader.GetString(reader.GetOrdinal("Author")) },
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        };
                        comments.Add(comment);
                    }

                    reader.Close();

                    return comments;
                }
            }
        }





        public Comment GetCommentById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  c.Id, 
                                c.PostId,
                                p.Title AS Title, 
                                c.UserProfileId,
                                up.FirstName AS Author,
                                c.Subject, 
                                c.Content, 
                                c.CreateDateTime
                        FROM Comment c
                        LEFT JOIN Post p ON c.PostId = p.id
                        LEFT JOIN UserProfile up ON c.UserProfileId = up.id
                        WHERE c.Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Comment comment = new Comment
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Author = new UserProfile() { FirstName = reader.GetString(reader.GetOrdinal("Author")) },
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        };

                        reader.Close();
                        return comment;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }




            public void EditComment(Comment comment)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            UPDATE Comment
                            SET 
                                Subject = @subject, 
                                Content = @content
                            WHERE Id = @id";


                        cmd.Parameters.AddWithValue("@subject", comment.Subject);
                        cmd.Parameters.AddWithValue("@content", comment.Content);
                        cmd.Parameters.AddWithValue("@id", comment.Id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            public void DeleteComment(int commentId)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            DELETE FROM Comment
                            WHERE Id = @id
                        ";

                        cmd.Parameters.AddWithValue("@id", commentId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

    }
}