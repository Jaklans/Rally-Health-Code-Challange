// Written by John Shull

/*     -------NOTE-------
 * If you are only interested
 * in my calulations to reach
 * the desired outcome, skip 
 * to the Calculations section.
 * However, since this is 
 * theoreticly designed for 
 * people to use, I also did 
 * my best to make a reasonably 
 * responsive and intelegent UI
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally_Health_Code_Challange
{
    class Program
    {
        static void Main(string[] args)
        {
            //---Properties---


            List<int> runners = new List<int>() { 0 };
            //A list to store the runners the user inputs.
            //The index is the number of runners that the 
            //runner needs to see before they will run, and 
            //the value is the number of runners that fall
            //into this description

            int rallyEmployeeCount = 0;
            //Keeps track of how many employees must be added
            //to make everyone run

            int runnersRunning = 0;
            //Tracks how many people are running durring the 
            //calculation



            string instructionsOutput =
                "Welcome to our HealthFest 5K Calculator!\n\n" +
                "Please enter your input. For each type of runner,\n" +
                "give the following two numbers, one after the other:\n\n" +
                " 1) The number of people these runners need to see running before they will run\n" +
                " 2) The number of people who need to see this amound of other runners before they run\n\n" +
                "You can overide an entry at any time.\n" +
                "When you are finished entering data, hit enter without any new numbers, and\n" +
                "the number of Rally employees that must be added to the croud will be calculated!\n\n";
            //A string that will be printed every update
            //( see Print() )



            //---Main Code---


            //User Input

            while (true)
            {
                Print(-1);

                //Get the first peice of data
                //(How many runners are required)
                int firstInput = GetInput();


                //Ends the loop if the user gave no input
                if (firstInput == -1)
                {
                    break;
                }


                //Adds to the List to make sure their is room
                while (runners.Count < firstInput + 1)
                {
                    runners.AddRange(new int[] {0, 0, 0, 0});
                }

                Print(firstInput);


                //Removes the placeholder or existing data and 
                //inserts the next user input into the index of
                //the previous user input
                runners.RemoveAt(firstInput);

                runners.Insert(firstInput, GetInput());
            }



            //Calculations

            for (int i = 0; i < runners.Count; i++)
            {
                //For each type of runner that their is at least
                //one of and does not already have their condition
                //for running met, add employees until that group 
                //will run
                if (i > runnersRunning && runners[i] != 0)
                {
                    rallyEmployeeCount += i - runnersRunning;
                    runnersRunning += i - runnersRunning;
                }

                //Add the group that is now running to the total so
                //they can inspire latter groups
                runnersRunning += runners[i];
            }

            //Print out the results of the calculation!
            Console.WriteLine(
                "\n" +
                rallyEmployeeCount + 
                " Rally Health Employees must be added for everyone to run!");

            //Hold for user to hit enter before closing the program
            Console.ReadLine();



            //---Functions---

            //This method gets and parses the input. It rejects
            //non ints and negative numbers, and returns -1 if 
            //the user gave no input 
            int GetInput()
            {
                string input = Console.ReadLine();

                int parsedVal = -1;

                if (!int.TryParse(input, out parsedVal) || parsedVal < 0)
                {
                    //If the user left the input blank, return -1 signify the loop should be left
                    if (input == "")
                    {
                        return -1;
                    }

                    Console.Write("Bad Input! Please enter only positive integers!\n" +
                        "Retry your input: ");
                    return GetInput();
                }

                return parsedVal;
            }


            //This method organizes and prints out the 
            //text that serves as the user interface.
            //It is called whenever new information should
            //be drawn. The input is only used if the user
            //has already entered the first element of a 
            //set, and is othewise set to -1 and ignored
            void Print(int firstInput)
            {
                //Clear the previous text
                Console.Clear();

                //Write out instruction block
                Console.WriteLine(instructionsOutput);

                //Write out each previously entered data pair and what they mean
                for (int i = 0; i < runners.Count; i++)
                {
                    if (runners[i] != 0)
                    {
                        Console.Write(i + " " + runners[i]);

                        //Check if either of the ints are 1, and adjusts the noun
                        //accordingly (plural vs non-plural)
                        if (runners[i] == 1)
                        {
                            Console.Write(" (\"" + runners[i] + " person will need to see " + i);
                        }
                        else
                        {
                            Console.Write(" (\"" + runners[i] + " people will need to see " + i);
                        }

                        if(i == 1)
                        {
                            Console.WriteLine(" runner before running\")");
                        }
                        else
                        {
                            Console.WriteLine(" runners before running\")");
                        }
                    }
                }

                //Prompt the user for input
                Console.Write("\n\nEnter the first number of the next set: ");

                if (firstInput != -1)
                {
                    Console.WriteLine(firstInput);
                    Console.Write("Enter the second number of the this set: ");
                }
            }
        }
    }
}
