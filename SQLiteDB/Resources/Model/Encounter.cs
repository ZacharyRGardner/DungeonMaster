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
    public class Encounter
    {
        public string Name { get; set; }
        public int CurrentLevel { get; set; }       
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public int EncounterRate { get; set; }
        public int EncounterScore { get; set; }
        public string DropQuality { get; set; }

        public Encounter(string name, int currentLevel, int minLevel, int maxLevel, int encounterRate, int encounterScore, string dropQuality)
        {
            Name = name;
            CurrentLevel = currentLevel;
            MinLevel = minLevel;
            MaxLevel = maxLevel;
            EncounterRate = encounterRate;
            EncounterScore = encounterScore;
            DropQuality = dropQuality;
        }
    }
}