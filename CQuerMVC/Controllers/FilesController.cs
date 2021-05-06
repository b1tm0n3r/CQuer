using Common.DTOs;
using CQuerMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQuerMVC.Controllers
{
    public class FilesController : Controller
    {
        public IActionResult Index()
        {
            //TODO: add client implementation
            var files = new List<FileReferenceDto>() {
                new FileReferenceDto()
                {
                    Id = 14,
                    FileName = "test",
                    SHA256Hash = "test",
                    UploadDate = DateTime.Now
                }
            };
            var viewModel = new FilesViewModel()
            {
                Files = files
            };
            return View(viewModel);
        }
        public IActionResult Delete(int id)
        {
            return View(new IdViewModel(id));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(IdViewModel idViewModel)
        {
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}