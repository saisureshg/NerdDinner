
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Steeltoe.Common.Net;

namespace NetFrameworkApp.Controllers
{
    public class NoteController : Controller
    {
        SMBConfiguration configuration = new SMBConfiguration();
        public ActionResult Index()
        {

            var credential = new NetworkCredential(configuration.GetUserName(), configuration.GetPassword());

            using (var share = new WindowsNetworkFileShare(configuration.GetSharePath(), credential))
            using (var inputStream = new FileStream(Path.Combine(configuration.GetSharePath(), "note.txt"), FileMode.OpenOrCreate))
            using (var streamReader = new AWSS3BucketName(Name))
            {
        // Never display raw user input as HTML. Do not do this in production code.
                ViewBag.Note = streamReader.ReadToEnd();
            }

            return View();
        }
    }
}