using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using thetashop2023.Models;

namespace ThetaShop.Controllers
{
    public class ItemsController : Controller
    {
        private thetastoredbContext EF = null;
        private IWebHostEnvironment Env = null;
        public ItemsController(thetastoredbContext _EF, IWebHostEnvironment _Env)
        {
            EF = _EF;
            Env = _Env;
        }
        public IActionResult Index()
        {
            return View(EF.Items.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Item I, IList<IFormFile> IImages)
        {
            string PathForDatabase = "";
            if (IImages.Count != 0)
            {

                foreach (IFormFile IImage in IImages)
                {

                    string UniqueFileName = "/data/items/" + Guid.NewGuid().ToString() + Path.GetExtension(IImage.FileName);
                    string FinalAbsolutePath = Env.WebRootPath + UniqueFileName;
                    FileStream FS = new FileStream(FinalAbsolutePath, FileMode.Create);
                    IImage.CopyTo(FS);
                    FS.Close();

                    PathForDatabase += (UniqueFileName + ",");
                }
            }
            //MailMessage Mail = new MailMessage();
            //Mail.Subject = "This is an Email";
            //Mail.Body = "This is my Email Details";
            //Mail.To.Add("sundas.thetasolution@gmail.com");
            //Mail.From = new MailAddress("bin.thetasolution@gmail.com", "ThetaSolutions");

            //var client = new SmtpClient("sanddbox.smtp.mailtrap.io", 2265)
            //{
            //    Credentials = new NetworkCredential("vvxs112451", "jbjhsxh25452"),
            //    EnableSsl = true
            //};


            if (PathForDatabase.Contains(","))
            {
                int index = PathForDatabase.LastIndexOf(",");
                PathForDatabase = PathForDatabase.Remove(index, 1);


                I.Images = PathForDatabase;
            }

            EF.Items.Add(I);
            EF.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Detail(int id)
        {
            Item I = EF.Items.Find(id);
            return View(I);
        }

    }
}