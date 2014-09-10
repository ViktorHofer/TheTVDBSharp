﻿using System;
using TheTVDBSharp;

namespace SimpleSearchConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create new TheTVDB manager which allows to perform api calls. Enter your api key here.
            // If the api key is not valid the server returns a 404 (.... crap ....) so I was not able
            // to create a unique exception for that case. TheTVDB triggers 404 also in many other cases.
            // So you get an aggregate exception with a more understandable INNER Exception (404).

            var tvdb = new TheTVDBManager(GlobalConfiguration.API_KEY);  // <--- API KEY required

            while (true)
            {
                Console.Write("Enter a series name: ");
                var searchQuery = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("| Searching the entire TheTVDB database |");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine();

                // Search for a series by name and with a specified language.
                var searchResult = tvdb.SearchSeries(searchQuery, TheTVDBSharp.Models.Language.English).Result;

                Console.WriteLine("{0} shows found... Downloading each show", searchResult.Count);
                Console.WriteLine();

                // Search call returns a readonly collection which can be enumerated.
                foreach (var series in searchResult)
                {
                    // To get more details of a series (not just metadata) like all episodes or banners 
                    // or actors call GetSeries(seriesId, language)
                    var fullSeries = tvdb.GetSeries(series.Id, TheTVDBSharp.Models.Language.English).Result;

                    Console.WriteLine("- {0} ({1} Episodes)", series.Title, fullSeries.Episodes.Count);
                }

                Console.WriteLine();
                Console.Write("<<Press Enter to perform a new search>>");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
