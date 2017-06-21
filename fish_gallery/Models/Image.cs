using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fish_gallery.Models
{
    public class Image
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int FishId { get; set; }
        public virtual string Extension { get; set; }
        public virtual Users ImageUser { get; set; }


        public virtual string imagePath { get; set; }
    }
}