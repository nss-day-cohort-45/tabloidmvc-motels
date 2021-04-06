using TabloidMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();
        Tag GetTag(int id);
        void InsertTag(Tag tag);
        void UpdateTag(Tag tag);
        void DeleteTag(int tagId);

        List<Tag> GetTagsByPostId();
    }
}