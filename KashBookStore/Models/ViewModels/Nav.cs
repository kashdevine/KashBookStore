using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Models.ViewModels
{
    //used in the Layout and Admin Layout views to mark a nav link as active.
    //in this folder because, like a traditional view model, it's used in a view.
    public static class Nav
    {
        public static string Active(string value, string current) =>
            (value == current) ? "active" : "";
        public static string Active(int value, int current) =>
            (value == current) ? "active" : "";
    }
}
