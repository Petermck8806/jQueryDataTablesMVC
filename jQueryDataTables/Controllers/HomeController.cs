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

        /// <summary>
        /// Search the Employee entity and return the result as a json action result. JSON must be parsed into array on client side.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Search Employee entities by First and Last name. Paginate the results, sort results. Return the results as json and
        /// the count of filtered results / total results.
        /// </summary>
        /// <param name="searchBy"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        private Tuple<int, int, string> GetEmployeesBySearch(string searchBy, int take, int skip, string sortBy, bool sortDir )
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
    }
}