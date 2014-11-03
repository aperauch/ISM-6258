using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* *
 * Author:  Aron Aperauch
 * Class:  ISM 6258
 * Section:  Afternoon
 * Professor:  Dr. Aytug
 * Date:  10/31/2013
 * 
 * Assignment 1
 */

namespace asg1
{
    class Program
    {
        /// <summary>
        /// Main entry of program.
        /// </summary>
        /// <param name="args">Command line arguements.  Currently, not used.</param>
        static void Main(string[] args)
        {
            string author = "Aron Aperauch";
        
            //Constants
            const decimal rate = 125.00m;
            const decimal tax = 0.0625m;
            const decimal fee = 15.00m;
        
            //Variables
            string name, date, creditCard;
            string menuInput, subMenuInput;
            int reservations = 0, nights = 0, rooms = 0, roomNightTotal = 0;
            decimal total = 0m, grandTotal = 0m;
        
            //Display welcome (Spec. 2)
            Console.WriteLine("Welcome to the Sunnyside Resort.  Copyright by {0:s}, 2013", author);
             
            do
            {
                //Read Menu options (Spec. 3)
                menuInput = askQuestion("Enter 'q' to quit or 'r' to enter the reservation.", false);

                if (menuInput.ToLower().Equals("r"))
                {                    
                    //Gather user details (Spec 4a)
                    name = askQuestion("Name", true);

                    date = askQuestion("Date", true);

                    nights = int.Parse(askQuestion("Nights", true));

                    rooms = int.Parse(askQuestion("Rooms", true));

                    //Calculate total and print to console (Spec 4b)
                    total = (rate * (1 + tax) + fee) * nights * rooms;
                    Console.WriteLine("Your total due is {0:C}.", total);

                    //Read Sub-menu option (Spec. 4c)
                    subMenuInput = askQuestion("Press 'a' to accept or 'c' to cancel.", false);

                    //Get credit card (Spec. 4d)
                    if (subMenuInput.ToLower().Equals("a"))
                    {
                        creditCard = askQuestion("Credit Card number", true);

                        reservations++;
                        grandTotal += total;
                        roomNightTotal += rooms * nights; 
                    }
                }

            } while (menuInput.ToLower().Equals("r"));

            //Output total reservations and grand total (Spec. 7)
            Console.WriteLine("You made {0:G} reservations for {1:G} room-nights totaling {2:C}.", reservations, roomNightTotal, grandTotal);
        
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        /// <summary>
        /// General method intended ask the user a question and record user input.
        /// </summary>
        /// <param name="question">The question to print to the console.</param>
        /// <param name="appendColon">If true, a ':' symbol is appended to the end of the question along with a newline.
        /// If false, the question is printed to the console with a newline.</param>
        /// <returns>The string representation of the user input.</returns>
        static string askQuestion(string question, bool appendColon)
        {
            string answer;

            if (appendColon)
                Console.Write("{0:10s}:", question);
            else
                Console.WriteLine("{0:10s}", question);

            answer = Console.ReadLine();

            return answer;
        }
    }

}
