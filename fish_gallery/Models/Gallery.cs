using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fish_gallery.Models
{
    public class Gallery
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Int16 PublicGallery { get; set; }
        public virtual string Password { get; set; }
        public virtual int UserId { get; set; }
        public virtual string Url { get; set; }
        //public virtual string User { get; set; }
    }
}