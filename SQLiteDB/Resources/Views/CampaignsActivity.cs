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

namespace SQLiteDB.Resources.Views
{
    [Activity(Label = "CampaignsActivity")]
    public class CampaignsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.campaigns_view);

            var btnPokeDnD = FindViewById<Button>(Resource.Id.btnPokeDnD);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);

            btnPokeDnD.Click += delegate
            {
                StartActivity(typeof(EncounterCalculator));
            };
            btnEdit.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };
        }
    }
}