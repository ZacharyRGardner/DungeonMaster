using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLiteDB.Resources.Model
{
    public class QueryData
    {
        public string Route { get; set; }
        public string Location { get; set; }
        public int NumEncounters { get; set; }
        public QueryData() { }
    }
}