using ComicBookGallery.Data;
using ComicBookGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComicBookGallery.Controllers
{
    public class ComicBooksController : Controller
    {
        private ComicBookRepository _comicBookRepository = null;

        // constructor -  no return value and shares the same name as the class name
        public ComicBooksController ()
        {
            _comicBookRepository = new ComicBookRepository();
        }

        // index - list page
        public ActionResult Index()
        {
            var comicBooks = _comicBookRepository.GetComicBooks();
            return View(comicBooks);
        }

        // allow nullable
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var comicBook = _comicBookRepository.GetComicBook((int)id);

            // strongly typed view - by putting object into the view vs. ViewBag.ComicBook = comicBook;
            return View(comicBook);  // will automatically look in the views folder
        }

        public ActionResult Detail2()
        {
            if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
            {
                // return new RedirectResult("/");
                return Redirect("/");
            }

            //return new ContentResult()
            //{
            //    Content = "This is a test."
            //};
            return Content("This is a test.");
        }

        public ActionResult CreateDocument()
        {
            // Create a new workbook
            using (var excelEngine = new ExcelEngine())
            {
                var workbook = excelEngine.Excel.Workbooks.Add(1);

                // Add a worksheet
                var worksheet = workbook.Worksheets[0];

                // Add some data
                worksheet.Range["A1"].Text = "Hello";
                worksheet.Range["B1"].Text = "World!";

                // Save the workbook to a file
                workbook.SaveAs("newdocument.xlsx", ExcelSaveType.SaveAsXLS, HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog);
            }

            return Content("Excel document created and saved as workbook!");
        }

    }
}