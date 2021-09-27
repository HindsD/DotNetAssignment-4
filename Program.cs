using System;
using System.IO;
using System.Collections.Generic;
using NLog.Web;
using System.Text;
using System.Linq;

namespace DotNetAssignment_4
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
            logger.Info("Program started"); //logs it started

            
            if (!File.Exists(@"E:\code stuff\DotNetAssignment#4\movies.csv")) //checks if the file exists, otherwise prints an exception
            {
                logger.Error("File does not exist: movies.csv"); // logs if error occurs
            }
            else
            {
                string resp ="";

                do{
                    // gives use the option to add a movie or to view them
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("Enter 1 to add a movie to the list.");
                    Console.WriteLine("Enter 2 to display the list of movies.");
                    Console.WriteLine("Enter anything else to quit.");
                
                    resp = Console.ReadLine();
                    logger.Info("User choice: " + resp); // logs  response
                    if (resp == "1")
                    {
                        int movieId = File.ReadAllLines(@"E:\code stuff\DotNetAssignment#4\movies.csv").Length; //gets amount of lines for movieID
                        StreamWriter sw = new StreamWriter(@"E:\code stuff\DotNetAssignment#4\movies.csv", append:true); // allows to not erase old file


                        Console.WriteLine("");
                        Console.WriteLine("What is the Movie Title: ");
                        String title = Console.ReadLine();
                        Console.WriteLine("What is the Movie Genre: ");
                        String genres = Console.ReadLine();
                        


                        
                        movieId++;
                        string movieIdString = movieId.ToString();


                        try{
                            sw.WriteLine(movieIdString+","+title+","+genres);
                            Console.WriteLine(title + " has been added!"); //adds the movie
                        }catch (Exception ex)
                    {
                        logger.Error("Unable to add the movie" + title+"!"); // logs an exception otherwise
                        logger.Error(ex.Message);
                    }

                        sw.Close();
                    }
                    else if (resp == "2") //I couldn't figure out how to get rid of duplicates without messing up a lot of things
                    {
                        var sr = new StreamReader(@"E:\code stuff\DotNetAssignment#4\movies.csv");
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine(); // reads the file of movies to the user
                            Console.WriteLine(line);
                        }
                        sr.Close();
                        
                    }

                }while (resp == "1" || resp == "2");

                
            }

            logger.Info("Program ended");
        }
    
    }
}