using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using SQLiteDB.Resources.Helper;
using SQLiteDB.Resources.Model;
using System.Collections.Generic;

namespace SQLiteDB.Resources.Views
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        ListView listViewData;
        List<Pokemon> listSource = new List<Pokemon>();
        Database db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            db = new Database();
            db.CreateDatabase();
            listViewData = FindViewById<ListView>(Resource.Id.listView);
            var edtName = FindViewById<EditText>(Resource.Id.edtName);
            var edtMinLevel = FindViewById<EditText>(Resource.Id.edtMinLevel);
            var edtMaxLevel = FindViewById<EditText>(Resource.Id.edtMaxLevel);
            var edtEncounterRate = FindViewById<EditText>(Resource.Id.edtEncounterRate);
            var edtRoute = FindViewById<EditText>(Resource.Id.edtRoute);
            var edtLocation = FindViewById<EditText>(Resource.Id.edtLocation);
            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnRemove = FindViewById<Button>(Resource.Id.btnRemove);
            var btnMenu = FindViewById<Button>(Resource.Id.btnMenu);

            // Load Data
            LoadData();
            // Event
            btnAdd.Click += delegate
            {
                Pokemon pokemon = new Pokemon()
                {
                    // I need to update the int value parsing.  It is getting the storage location instead of the value I think.
                    Name = edtName.Text,
                    MinLevel = int.Parse(edtMinLevel.Text.ToString()),
                    MaxLevel = int.Parse(edtMaxLevel.Text.ToString()),
                    EncounterRate = int.Parse(edtEncounterRate.Text.ToString()),
                    PokemonRoute = edtRoute.Text,
                    PokemonLocation = edtLocation.Text
                };
                db.InsertIntoTable(pokemon);
                LoadData();
            };
            btnEdit.Click += delegate
            {
                Pokemon pokemon = new Pokemon()
                {
                    ID = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                    MinLevel = int.Parse(edtMinLevel.Text.ToString()),
                    MaxLevel = int.Parse(edtMaxLevel.Text.ToString()),
                    EncounterRate = int.Parse(edtEncounterRate.Text.ToString()),
                    PokemonRoute = edtRoute.Text,
                    PokemonLocation = edtLocation.Text
                };
                db.UpdateTable(pokemon);
                LoadData();
            };
            btnRemove.Click += delegate
            {
                Pokemon pokemon = new Pokemon()
                {
                    ID = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                    MinLevel = int.Parse(edtMinLevel.Text.ToString()),
                    MaxLevel = int.Parse(edtMaxLevel.Text.ToString()),
                    EncounterRate = int.Parse(edtEncounterRate.Text.ToString()),
                    PokemonRoute = edtRoute.Text,
                    PokemonLocation = edtLocation.Text
                };
                db.RemoveTable(pokemon);
                LoadData();
            };
            btnMenu.Click += delegate
            {
                StartActivity(typeof(MenuActivity));
            };
            listViewData.ItemClick += (s, e) =>
            {
                // Set background for item
                for (int i = 0; i < listViewData.Count; i++)
                {
                    if (e.Position == i)
                    {
                        listViewData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Black);
                    }
                    else
                    {
                        listViewData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                    }
                }
                // Binding Data
                var txtName = e.View.FindViewById<TextView>(Resource.Id.txtView_Name);
                var txtMinLevel = e.View.FindViewById<TextView>(Resource.Id.txtView_Min_Level);
                var txtMaxLevel = e.View.FindViewById<TextView>(Resource.Id.txtView_Max_Level);
                var txtEncounterRate = e.View.FindViewById<TextView>(Resource.Id.txtView_Encounter_Rate);
                var txtPokemonRoute = e.View.FindViewById<TextView>(Resource.Id.txtView_Route);
                var txtPokemonLocation = e.View.FindViewById<TextView>(Resource.Id.txtView_Location);

                edtName.Text = txtName.Text;
                edtName.Tag = e.Id;
                edtMinLevel.Text = txtMinLevel.Text.ToString();
                edtMaxLevel.Text = txtMaxLevel.Text.ToString();
                edtEncounterRate.Text = txtEncounterRate.Text.ToString();
                edtRoute.Text = txtPokemonRoute.Text;
                edtLocation.Text = txtPokemonLocation.Text;

            };
        }
         private void LoadData()
        {
            listSource = db.SelectTable();
            var adapter = new SQLiteDB.Resources.Model.ListViewAdapter(this, listSource);
            listViewData.Adapter = adapter;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}