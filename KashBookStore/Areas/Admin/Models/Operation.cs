using KashBookStore.Models.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Areas.Admin.Models
{
    //static class used mostly by admin area views to determine which fields to 
    //display, but also used in admin area book controller and validate class.
    public static class Operation
    {
        public static bool IsAdd(string action) => action.EqualsNoCase("add");
        public static bool IsDelete(string action) => action.EqualsNoCase("delete");
    }
}
