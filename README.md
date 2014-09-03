TVDBSharp
=========

**Nuget Package: https://www.nuget.org/packages/TheTVDBSharp/**

**A C# wrapper for the TVDB API.**

This API provides an easy-to-use interface to the TVDB API by translating the XML responses into objects. 
You won't need any knowledge about the API itself to use this library.

To get started there are a few steps you have to undertake.

**1) Get an API key.**  
This is as easy as filling out a form on TheTVDB's webpage. 
Read the rules and enter your information, you can find your API key under 'Account'.

> http://thetvdb.com/?tab=apiregister

**2) Optionally: familiarize yourself with the API itself.**  
You don't have to do this, but some people might want to read up on the, although slightly outdated, API.

> http://www.thetvdb.com/wiki/index.php?title=Programmers_API

**3) Import the library.**  
Easiest way is to add nuget package via Nuget Package Manager in Visual Studio. Link at the top.

**4) Use it!**  
You can now use TVDBSharp in your own project.

A simple example:

    var tvdb = new TVDBManager("mykey"); // Create a new TVDB object with your API key.
    var results = tvdb.SearchSeries("Scrubs", Language.English); // Search for a show called scrubs with english as language
    
    foreach(var show in results){
     Console.WriteLine("{0}:\t{1} Episodes", show.Title, show.Episodes.Count); // Print every show's name and amount of episodes
    }

**Notes**  
If you encounter any issues or have a suggestion: don't hesitate to open a ticket.  
Should you wish to do so, you can contact me at `viktor.hofer@outlook.com`.
