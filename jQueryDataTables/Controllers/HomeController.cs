using jQueryDataTables.JsonModels;
using jQueryDataTables.Models;
using jQueryDataTables.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace jQueryDataTables.Controllers
{
    public class HomeController : BaseController
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
            var result = CustomSearch(model, GetEmployeesBySearch);

            var x = Json(new
            {
                draw = model.draw,
                recordsFiltered = result.Item1,
                recordsTotal = result.Item2,
                data = result.Item3
            });

            return x;
        }

        private Tuple<int, int, string> GetEmployeesBySearch(string searchBy, int take, int skip, string sortBy, bool sortDir)
        {
            var query = string.IsNullOrEmpty(searchBy) ?
                _appContext.Employees.AsQueryable()
                :
                _appContext.Employees
                    .Where(w => w.FirstName.ToLower().Contains(searchBy) || w.LastName.ToLower().Contains(searchBy));

            var result = sortDir ? query.OrderBy(sortBy)
                                        .Skip(skip)
                                        .Take(take)
                                        .ToList()
                                 : query.OrderByDescending(sortBy)
                                        .Skip(skip)
                                        .Take(take)
                                        .ToList();

            int totalResultsCount = _appContext.Employees.Count();
            int filteredResultsCount = string.IsNullOrEmpty(searchBy) ? totalResultsCount : _appContext.Employees
                    .Where(w => w.FirstName.ToLower().Contains(searchBy) || w.LastName.ToLower().Contains(searchBy)).Count();

            return Tuple.Create(filteredResultsCount, totalResultsCount, JsonConvert.SerializeObject(result.ToList(), new IsoDateTimeConverter() { DateTimeFormat = "MM/dd/yyyy" }));
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