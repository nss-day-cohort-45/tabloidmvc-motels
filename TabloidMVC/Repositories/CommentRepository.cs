﻿using TabloidMVC.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


namespace TabloidMVC.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
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
                            PostTitle = reader.GetString(reader.GetOrdinal("Title")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Author = reader.GetString(reader.GetOrdinal("Author")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
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
                            PostTitle = reader.GetString(reader.GetOrdinal("Title")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Author = reader.GetString(reader.GetOrdinal("Author")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
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
                            PostTitle = reader.GetString(reader.GetOrdinal("Title")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Author = reader.GetString(reader.GetOrdinal("Author")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
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
    }
}