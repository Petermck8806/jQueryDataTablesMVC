using jQueryDataTables.JsonModels;
using jQueryDataTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace jQueryDataTables.Controllers
{
    public class HomeController : Controller
    {
        //db context please
        private ApplicationDbContext _appContext;

        public HomeController(ApplicationDbContext context)
        {
            _appContext = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchEmployeeAction(DataTableAjaxPostModel model)
       {
            int filteredResultsCount;
            int totalResultsCount;

            var result = CustomEmployeeSearch(model, out filteredResultsCount, out totalResultsCount);

            return Json(new
            {
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result.Select(s => new
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    MiddleName = s.MiddleName,
                    StartDate = s.StartDate.Value.ToShortDateString(),
                    BirthDate = s.BirthDate.ToShortDateString()
                })
            });
        }

        private IList<Employee> CustomEmployeeSearch(DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;

            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }

            var result = GetEmployeesBySearch(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            if(result == null)
            {
                return new List<Employee>();
            }

            return result;
        }

        private IList<Employee> GetEmployeesBySearch(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            //
            if (string.IsNullOrEmpty(searchBy))
            {
                sortBy = "Id";
                sortDir = true;
            }

            var result = string.IsNullOrEmpty(searchBy) ?
                _appContext.Employees
                    .OrderBy(f => f.LastName)
                    .Skip(skip)
                    .Take(take)
                    .ToList()
                :
                _appContext.Employees
                    .Where(w => w.FirstName.ToLower().Contains(searchBy) || w.LastName.ToLower().Contains(searchBy))
                    .OrderBy(f => f.LastName)
                    .Skip(skip)
                    .Take(take)
                    .ToList();

            totalResultsCount = _appContext.Employees.Count();
            filteredResultsCount = string.IsNullOrEmpty(searchBy) ? totalResultsCount : _appContext.Employees
                    .Where(w => w.FirstName.ToLower().Contains(searchBy) || w.LastName.ToLower().Contains(searchBy)).Count();

            return result;
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}