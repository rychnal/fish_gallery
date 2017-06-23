using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fish_gallery.Models
{
    public class Fish
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual float Weight { get; set; }
        public virtual float Length { get; set; }
        public virtual Gallery FishGallery { get; set; }
    }
}