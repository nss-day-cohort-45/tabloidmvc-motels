using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TabloidMVC.Models.ViewModels
{
    public class PostCreateViewModel
    {
        public Post Post { get; set; }
        public List<Category> CategoryOptions { get; set; }
        public List<SelectListItem> Tags { get; set; }
        public Tag Tag { get; set; }
        public List<int> SelectedTags { get; set; }
    }
}

