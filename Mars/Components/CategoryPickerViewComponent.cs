using Mars.Data;
using Mars.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mars.Components
{
    public class CategoryPickerViewComponent : ViewComponent
    {
        private ICategoryRepository _repo;
        
        public CategoryPickerViewComponent(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke()
        {
            // business logic 
            IEnumerable<Category> categories = _repo.GetAll();
            return View(categories);
        }
    }
}
