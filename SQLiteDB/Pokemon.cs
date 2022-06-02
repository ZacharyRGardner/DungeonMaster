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
using SQLite;

namespace SQLiteDB.Resources.Model
{
    [Table("Pokemon")]
    public class Pokemon
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int CurrentLevel { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public int EncounterRate { get; set; }
        public int EncounterScore { get; set; }
        public string DropQuality { get; set; }
        public string PokemonRoute { get; set; } 
        public string PokemonLocation { get; set; }

        public Pokemon() { }
    }
}