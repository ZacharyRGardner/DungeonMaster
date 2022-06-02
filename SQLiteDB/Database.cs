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
using Android.Util;
using SQLite;
using SQLiteDB.Resources.Model;

namespace SQLiteDB.Resources.Helper
{
    class Database
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "RouteEncountersTest.db")))
                {
                    connection.CreateTables<Pokemon, Pokemon>();
                    return true;
                }
            }
            catch(SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        // Insert Operation
        public bool InsertIntoTable(Pokemon pokemon)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "RouteEncountersTest.db")))
                {
                    connection.Insert(pokemon);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        // Get table/list
        public List<Pokemon> SelectTable()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "RouteEncountersTest.db")))
                {
                    return connection.Table<Pokemon>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }
        // Edit Operation
        public bool UpdateTable(Pokemon pokemon)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "RouteEncountersTest.db")))
                {
                    connection.Query<Pokemon>
        ("UPDATE Pokemon set Name=?, MinLevel=?, MaxLevel=?, EncounterRate=?, PokemonRoute=?,PokemonLocation=? Where ID=?",pokemon.Name, pokemon.MinLevel, pokemon.MaxLevel, pokemon.EncounterRate, pokemon.PokemonRoute, pokemon.PokemonLocation, pokemon.ID);
                    return true;  
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        public bool RemoveTable(Pokemon pokemon)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "RouteEncountersTest.db")))
                {
                    connection.Delete(pokemon);
                    return true;
                }
            }
            catch(SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        public bool SelectTable(int ID)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "RouteEncountersTest.db")))
                {
                    connection.Query<Pokemon>("SELECT * FROM Pokemon Where ID=?", ID);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public List<Pokemon> SelectEncounter(string route, string location)
        {
            List<Pokemon> pokemon = new List<Pokemon>();
            List<Pokemon> resultList = new List<Pokemon>();
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "RouteEncountersTest.db")))
                {
                    pokemon = SelectTable();

                    var query =
                        from p in pokemon
                        where p.PokemonRoute == route
                        && p.PokemonLocation == location
                        select p;

                    foreach (var p in query)
                    {
                        resultList.Add(p);
                    }

                    return resultList;
                }
            }

            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }
    }
}