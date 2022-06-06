using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SQLiteDB.Resources.Helper;
using SQLiteDB.Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLiteDB.Resources.Views
{
    [Activity(Label = "EncounterCalculator")]
    public class EncounterCalculator : Activity
    {
        private RadioGroup radioGroup1;
        Database db;
        /*
         * Initialize RadioGroup and RadioButtons
         */
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.encounter_layout);
            initializeMultipleRadioButtons();
            db = new Database();
            db.CreateDatabase();

            // initialize controls
            var spinner1 = FindViewById<Spinner>(Resource.Id.spinner1);
            var editText1 = FindViewById<EditText>(Resource.Id.editText1);
            var btnCalcEncounter = FindViewById<Button>(Resource.Id.button1);


            spinner1.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner1_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                this, Resource.Array.routes_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner1.Adapter = adapter;

            // get route, location, and number of encounters
            // query database, calculate encounter and item drop quality
            // use init method to display results
            btnCalcEncounter.Click += delegate {
                // new QueryData object to story query input
                List<Pokemon> pokemonList = new List<Pokemon>();
                List<Encounter> encounterList = new List<Encounter>();
                QueryData query = new QueryData();
                string queryText = "You chose ";
                // new List<Pokemon

                // Get Route with spinner
                string route = spinner1.SelectedItem.ToString();
                query.Route = route;               
                queryText += route;

                // Get Location with radio array
                int checkedItemId = radioGroup1.CheckedRadioButtonId;
                RadioButton checkedRadioButton = FindViewById<RadioButton>(checkedItemId);
                string location = Convert.ToString(checkedRadioButton.Text);
                query.Location = location;
                queryText += location;

                // Get Number of Encounters with textedit
                string numEncounters = editText1.Text.ToString();

                try
                {
                    query.NumEncounters = int.Parse(numEncounters);
                }
                catch(FormatException e)
                {
                    Log.Info("FormatException", e.Message);
                    Toast.MakeText(this, "Incorrect input, \n Try again!", ToastLength.Long).Show();
                    return;
                }
                queryText += numEncounters;

                // Query DB for selected route and location
                // Calculate Encounter and Item Drop Quality
                pokemonList = QueryRoute(query);

                // Input number of encounters and pokemon list that matches route and location
                // All possible encounter pokemon are added to new List<Encounter>
                if (pokemonList.Count > 0)
                {
                    encounterList = Encounter(query.NumEncounters, pokemonList);
                }
                else
                {
                    Toast.MakeText(this, "No results, \n Try again!", ToastLength.Long).Show();
                    return;
                }
                //calculates pokemon to encounter and adds them to List<Encounter>
                encounterList = CalcEncounter(encounterList, query.NumEncounters);

                // Display Encounter List using Init method
                Init(encounterList);

                // Toast for testing data input
                Toast.MakeText(this, queryText, ToastLength.Long).Show();
            };
        }
        
        
        private void Spinner1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            // use spinner value to select file to use.  Next task is getting xml deserializing to work.
            Spinner spinner1 = (Spinner)sender;
            string route = spinner1.GetItemAtPosition(e.Position).ToString();
            Toast.MakeText(this, route, ToastLength.Long).Show();
        }

        public void initializeMultipleRadioButtons()
        {
            radioGroup1 = FindViewById<RadioGroup>(Resource.Id.radioGroup1);

            radioGroup1.CheckedChange += radioGroup1_CheckedChange;
        }

        public void radioGroup1_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            int checkedItemId = radioGroup1.CheckedRadioButtonId;
            RadioButton checkedRadioButton = FindViewById<RadioButton>(checkedItemId);
            Toast.MakeText(this, Convert.ToString(checkedRadioButton.Text), ToastLength.Short).Show();
        }

        //Called by Calculate Encounter Button

        // Query local db for encounter list
        public List<Pokemon> QueryRoute(QueryData input)
        {
            // Pokemon List for return List
            List<Pokemon> pokemonList = new List<Pokemon>();                        
       
            // Selects pokemon list based one route and location
            pokemonList = db.SelectEncounter(input.Route, input.Location);                 
            
            return pokemonList;
                                  
        }
        public List<Encounter> Encounter(int numEncounters, List<Pokemon> pokemonList)
        {
            List<Encounter> returnList = new List<Encounter>();

            // adds all possible encounter pokemon to finalPokemon
            foreach (Pokemon p in pokemonList)
            {
                for (int i = 0; i < numEncounters; i++)
                {
                    returnList.Add(new Encounter(p.Name, p.CurrentLevel, p.MinLevel, p.MaxLevel, p.EncounterRate, p.EncounterScore, p.DropQuality));
                }
            }
                   
            return returnList;
        }
        // Calculate encounter and return result list
        public List<Encounter> CalcEncounter(List<Encounter> encounter, int numEncounters)
        {
            Random random = new Random();

            foreach (Encounter e in encounter)
            {
                e.EncounterScore = random.Next(100) * e.EncounterRate;
                e.DropQuality = CalculateDrop(e);
                e.CurrentLevel = random.Next(e.MinLevel, e.MaxLevel + 1);
            }
            // Solution for sorting in place https://stackoverflow.com/questions/3309188/how-to-sort-a-listt-by-a-property-in-the-object
            encounter.Sort((y, x) => x.EncounterScore.CompareTo(y.EncounterScore));

            List<Encounter> encounterList = new List<Encounter>();

            for (int i = 0; i < numEncounters; i++)
            {
                encounterList.Add(encounter[i]);
            }
            return encounterList;
        }
        // Calculate quality of item drop
        public String CalculateDrop(Encounter encounter)
        {
            int DropQuality;
            Random random = new Random();
            int quality = random.Next(100);
            int didDrop = random.Next(100);
            if (didDrop < 20)
            {
                if (quality < 50)
                {
                    DropQuality = 1;
                }
                else if (quality > 49 && quality < 75)
                {
                    DropQuality = 2;
                }
                else if (quality > 74 && quality < 90)
                {
                    DropQuality = 3;
                }
                else if (quality > 89 && quality < 100)
                {
                    DropQuality = 4;
                }
                else
                {
                    DropQuality = 0;
                }
                return DropQuality switch
                {
                    1 => encounter.DropQuality = "Low Quality",
                    2 => encounter.DropQuality = "Medium Quality",
                    3 => encounter.DropQuality = "High Quality",
                    4 => encounter.DropQuality = "Legendary Quality",
                    _ => encounter.DropQuality = "No Drop",
                };
            }
            else
            {
                return encounter.DropQuality = "No Drop";
            }
        }
        // Display Encounter results
        public void Init(List<Encounter> ListPokemon)
        {

            TableLayout tableLayout1 = (TableLayout)FindViewById(Resource.Id.tableLayout1);

            tableLayout1.RemoveAllViews();

            tableLayout1.StretchAllColumns = true;
            TableRow theader = new TableRow(this);

            TextView pokeName = new TextView(this);
            pokeName.Text = "Name";

            theader.AddView(pokeName);

            TextView pokeLevel = new TextView(this);
            pokeLevel.Text = "Level";
            theader.AddView(pokeLevel);


            TextView pokeItem = new TextView(this);
            pokeItem.Text = "Item";
            theader.AddView(pokeItem);
            tableLayout1.AddView(theader);

            foreach (Encounter p in ListPokemon)
            {
                TableRow resultRow = new TableRow(this);

                TextView name = new TextView(this);
                name.Text = p.Name;
                resultRow.AddView(name);

                TextView level = new TextView(this);
                level.Text = p.CurrentLevel.ToString();
                resultRow.AddView(level);


                TextView item = new TextView(this);
                item.Text = p.DropQuality;
                resultRow.AddView(item);
                tableLayout1.AddView(resultRow);
            }

        }

    }
}
