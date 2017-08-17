using jQueryDataTables.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jQueryDataTables.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Returns the items found in the search function passed to CustomSearch (GetItemsBySearch). 
        /// This method handles interpretting the sortyBy and sortDir vars as well as the pagination vars skip/take.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="GetItemsBySearch"></param>
        /// <returns></returns>
        public Tuple<int, int, string> CustomSearch(DataTableAjaxPostModel model, Func<string, int, int, string, bool, Tuple<int, int, string>> GetItemsBySearch)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;

            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                var order = model.order.FirstOrDefault();

                sortBy = model.columns[order.column].data;
                sortDir = order.dir.ToLower() == "asc";
            }

            var result = GetItemsBySearch(searchBy, take, skip, sortBy, sortDir);

            return result;
        }
    }
}