using AdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.DTO;
using ServiceLayer.Services;

namespace AdminDashboard.Controllers
{
    public class BlogsController : Controller
    {
        private readonly BlogServices _blogServices;

        public BlogsController(UnitOfWorkServices unitOfWorkServices)
        {
            _blogServices=unitOfWorkServices.BlogServices;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<JsonResult> GetDataTable(DataTableModel dataTable) 
        {
            var data = await _blogServices.ListWithPaging(
            orderBy: dataTable.OrderBy,
            pageSize: dataTable.Length,
            page: dataTable.Start,
            isDescending: dataTable.isDescending);

            return Json(new { data = data.EntityData, recordsTotal = data.Count, recordsFiltered = data.Count, lastquestion = dataTable.Start.ToString() });
        }

        public async Task<IActionResult> Edit(int Id) 
        {
            var data = await _blogServices.GetById(Id);
            BlogModel model= new BlogModel
            { 
                Id = Id,
                BlogMainText=data.BlogMainText,
                Title = data.Title,
                IsPublished = data.IsPublished,
                BlogMainImage=data.BlogMainImage,
            };
            return View(model);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(BlogModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            var data = new BlogDTO
            {
                Id=model.Id,
                BlogMainText=model.BlogMainText,
                Title = model.Title,
                IsPublished = model.IsPublished,
                BlogMainImage = model.BlogMainImage,
                Image=model.Image,
            };

            await _blogServices.Edit(data);
            
            return RedirectToAction(nameof(Edit), new {Id=model.Id});
        }

        public async Task<IActionResult> Delete(int Id) 
        {
            await _blogServices.Delete(Id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(BlogModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var data = new BlogDTO
            {
                BlogMainText = model.BlogMainText,
                Title = model.Title,
                IsPublished = model.IsPublished,
                BlogMainImage = model.BlogMainImage,
                Image = model.Image,
            };

            await _blogServices.Create(data);
            return RedirectToAction(nameof(Index));
        }
    }
}
