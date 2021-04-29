using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilters
{
    interface IActionFilter { }
    interface IFilterMetadata { }
    interface IAsyncActionFilter { }
    interface IResultFilter { }
    interface IAsyncResultFilter { }
    interface IOrderedFilter { }
}
