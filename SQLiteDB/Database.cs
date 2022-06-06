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

/*
 * Functionality I am going to add for the next iterations
 * 
 * Dungeon Master App
 * Step 1:
 * Read files to populate database with encounter pokemon
 * 
 * Step 2:
 * Continue error correction
 * 
 * Step 3:
 * Allow user to create new database with fields for new campaign
 * This will auto-populate campaign list
 * Campaign creator will take DM through campaign creation steps
 * 
 * Step 4:
 * Allow user to create character profile that tracks levels and stats etc
 * 
 * Step 5:
 * Dungeon Master account that can manage campaigns
 * 
 * Step 6:
 * Manage wifi connections so multiple players can connect to the DM's campaign
 * 
 * Notes:
 * DM profile can host campaigns
 * player profile can only see encountered pokemon
 * allow players to connect remotely or through wifi. 
 * DM profile will control campaign for all players
 * players will only see player available campaign/encounter info
 * DM can see all campaign info and reveal/hide info for players
 *
 * DM Account
 *  Campaign Creator
 *  encounter manager
 *  items
 *  creatures 
 *  etc
 *  
 * Player Account
 *  Hero Builder
 *  Stats
 *  items 
 *  etc
 *  
 * Play Campaign
 *  Join as DM
 *  Join as Player
 *  DM and Player permissions are different
 *  In Game DM pages track campaign, encounters, story, players, etc
 *  In Game Player page shows current team status, player info, team items, missions, etc...
 *  DM can assign missions, items, etc to specific players, and it will update player data.
 * 
 */
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