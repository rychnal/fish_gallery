using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fish_gallery.Models
{
    public class Users
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual Image UserImage { get; set; }
    }
}