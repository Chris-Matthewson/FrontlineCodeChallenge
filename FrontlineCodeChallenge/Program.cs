using FrontlineCodeChallenge.Models;
using System;
using System.Diagnostics;

namespace FrontlineCodeChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var exit = false;
            while (!exit)
            {
                Console.WriteLine("Please enter a property group string...");
                //get input and check for exit
                var input = Console.ReadLine();
                if (input?.ToLower() == "exit")
                {
                    exit = true;
                    continue;
                }

                //parse input
                var newGroup = new PropertyGroup();
                newGroup.LoadFromString(input);

                //output results
                Console.WriteLine("");
                Console.WriteLine("Output:");
                Console.WriteLine(newGroup.ToString());
                Debug.WriteLine(newGroup.ToString());

                /*  BONUS   */
                Console.WriteLine("Output (Alphabetical):");
                Console.WriteLine(newGroup.ConvertToString(true));
                Console.WriteLine("");
            }
        }
    }
}
