using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
namespace fish_gallery
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            //var configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\hibernate.cfg.xml");
            var configurationPath = @"C:\Users\Lukas\Documents\Source\fish_gallery\fish_gallery\Models\hibernate.cfg.xml";
            configuration.Configure(configurationPath);
            var fishGalleryConfigurationFile = @"C:\Users\Lukas\Documents\Source\fish_gallery\fish_gallery\Mappings\FishGallery.hbm.xml";
           // var fishGalleryConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\FishGallery.hbm.xml");
            configuration.AddFile(fishGalleryConfigurationFile);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}