using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using fish_gallery.Controllers;
using fish_gallery.Models;

namespace fish_gallery.Controllers
{
    
    [TestClass]
    public class GalleryTestController
    {

        [TestMethod]
        public void TestDetailsViewData()
        {
            var controller = new ImageController();
            var result = controller.Details(2) as ViewResult;
            var image = (Image)result.ViewData.Model;
            Assert.AreEqual("DSC06762.JPG", image.Name);
        }
    }
}