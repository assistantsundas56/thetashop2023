using ThetaShop.Models;
using Microsoft.AspNetCore.Mvc;
using thetashop2023.Models;

namespace thetashop.Controllers
{
    public class CategoriesController : Controller
    {
        thetastoredbContext EF = null;
        IWebHostEnvironment Env = null;
        public CategoriesController(thetastoredbContext _EF, IWebHostEnvironment _Env)
        {
            EF = _EF;
            Env = _Env;

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category C, IFormFile CImage, IFormFile CV)
        {
            try
            {
                string FinalFileName = "";
                if (CImage != null)
                {
                    string UniqueName = Guid.NewGuid().ToString();
                    string FileName = CImage.FileName;
                    //System.IO.File.
                    string Ext = Path.GetExtension(CImage.FileName);
                    FinalFileName = UniqueName + Ext;
                    string wwwRootPath = Env.WebRootPath;
                    FileStream fs = new FileStream(wwwRootPath + "/data/categories/" + FinalFileName, FileMode.Create);
                    CImage.CopyTo(fs);
                    fs.Close();


                    //using (FileStream fs1 = new FileStream(wwwRootPath + "/data/categories/" + FinalFileName, FileMode.Create))
                    //{
                    //    CImage.CopyTo(fs1);
                    //    fs.Close();
                    //}


                }

                C.Image = "/data/categories/" + FinalFileName;
                EF.Categories.Add(C);

                EF.SaveChanges();

                ViewBag.Message = "Category is saved successfully";


                return RedirectToAction("Index");



            }
            catch
            {
                ViewBag.Message = "Error in saving category";
            }



            return View();
        }

        public IActionResult Delete(int id)
        {
            Category C = EF.Categories.Find(id);

            if (C != null)
            {
                using (var T = EF.Database.BeginTransaction())
                {
                    try
                    {
                        EF.Categories.Remove(C);
                        EF.SaveChanges();
                        //send email

                        Category C2 = new Category();
                        C2.Id = 3;
                        C2.Name = "adf";
                        C2.Status = "lkasjdfkljadsklfjkladsjfklajsdklfj";
                        EF.Categories.Add(C2);
                        EF.SaveChanges();




                        T.Commit();
                        ViewBag.Message = "Operation completed.";
                    }
                    catch
                    {
                        T.Rollback();
                        ViewBag.Message = "Error occured.";
                    }



                }
            }








            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category C = EF.Categories.Find(id);
            return View(C);
        }


        [HttpPost]
        public IActionResult Edit(Category C)
        {
            EF.Categories.Update(C);
            EF.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Detail(int id)
        {
            Category C = EF.Categories.Find(id);
            return View(C);
        }



        public IActionResult Index()
        {

            IList<Category> AllCategories = EF.Categories.ToList<Category>();



            return View(AllCategories);
        }





        public string DeleteAjax(int id)
        {
            try
            {
                EF.Categories.Remove(EF.Categories.Find(id));
                EF.SaveChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return "0";
            }


        }

        public string GetCategoriesCount()
        {
            return EF.Categories.Count().ToString();
        }



        public string GetTable()
        {
            return "<table class='table table-bordered'><tr><td>Test1</td><td>Test1</td><td>Test1</td></tr></table>";
        }



    }
}