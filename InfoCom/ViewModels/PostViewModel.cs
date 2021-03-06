﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace InfoCom.ViewModels
{
    public class PostViewModel
    {
        public virtual int Id { get; set; }
        public virtual User Author { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Title is required")]
        [StringLength(30, ErrorMessage ="The title can't be longer than 30 chars.")]
        public virtual string Title { get; set; }
        [Required(ErrorMessage ="You have to write something.")]
        public virtual string Content { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string Formal { get; set; }
        public virtual bool ShowHidden { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<PostFile> Files { get; set; }
        public virtual List<Post> PostList { get; set; }
        public virtual List<PostFile> FileList { get; set; }
        public virtual ICollection<Category> CategoryList { get; set; }
        public virtual IEnumerable<SelectListItem> Categories { get; set; }
        public virtual string NewCategory { get; set; }
        public PostViewModel()
        {
            Categories = new List<SelectListItem>();
        }
    
        }

    }
