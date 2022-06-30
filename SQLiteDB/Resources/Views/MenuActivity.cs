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
    [Activity(Label = "MenuActivity", MainLauncher = true)]
    public class MenuActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here

            SetContentView(Resource.Layout.menu_activity);

            var btnCampaigns = FindViewById<Button>(Resource.Id.btnCampaigns);
            var btnCreateCampaign = FindViewById<Button>(Resource.Id.btnCreateCampaign);
            var btnExit = FindViewById<Button>(Resource.Id.btnExit);

            btnCampaigns.Click += delegate
            {
                StartActivity(typeof(CampaignsActivity));
            };
            btnCreateCampaign.Click += delegate
            {
                
            };
            //btnExit.Click += delegate
            //{
            // This will save current state and exit the 
            // application.
            //};

        }
    }
}