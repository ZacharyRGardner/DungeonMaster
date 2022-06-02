using Android.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Collections.Generic;

namespace SQLiteDB.Resources.Model
{
    public class ViewHolder: Java.Lang.Object
    {
        public TextView txtName { get; set; }
        public TextView txtMinLevel { get; set; }
        public TextView txtMaxLevel { get; set; }
        public TextView txtEncounterRate { get; set; }
        public TextView txtRoute { get; set; }
        public TextView txtLocation { get; set; }
    }
    public class ListViewAdapter : BaseAdapter
    {
        public Activity activity;
        public List<Pokemon> listPokemon;
        public ListViewAdapter(Activity activity, List<Pokemon> listPokemon)
        {
            this.activity = activity;
            this.listPokemon = listPokemon;
        }
        public override int Count
        {
            get { return listPokemon.Count; }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return listPokemon[position].ID;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_view, parent, false);
            var txtName = view.FindViewById<TextView>
        (Resource.Id.txtView_Name);
            var txtMinLevel = view.FindViewById<TextView>
        (Resource.Id.txtView_Min_Level);
            var txtMaxLevel = view.FindViewById<TextView>
        (Resource.Id.txtView_Max_Level);
            var txtEncounterRate = view.FindViewById<TextView>
        (Resource.Id.txtView_Encounter_Rate);
            var txtRoute = view.FindViewById<TextView>
        (Resource.Id.txtView_Route);
            var txtLocation = view.FindViewById<TextView>
        (Resource.Id.txtView_Location);
            txtName.Text = listPokemon[position].Name;
            txtMinLevel.Text = listPokemon[position].MinLevel.ToString();
            txtMaxLevel.Text = listPokemon[position].MaxLevel.ToString();
            txtEncounterRate.Text = listPokemon[position].EncounterRate.ToString();
            txtRoute.Text = listPokemon[position].PokemonRoute;
            txtLocation.Text = listPokemon[position].PokemonLocation;
            return view;
        }
    }
}