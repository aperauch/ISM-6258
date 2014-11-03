using System;
using C = System.Console;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/* *
 * Author:  Aron Aperauch
 * Class:  ISM 6258
 * Section:  Afternoon
 * Professor:  Dr. Aytug
 * Date:  11/08/2013
 * 
 * Assignment 2
 */

namespace asg2
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
            string tableHeaders = String.Format("{0,-15:s}{1,-15:s}{2,-15:s}{3,-15:s}", "Name", "Date In", "n-nights", "Total ($)");

            //Variables
            string name, date, creditCard;
            string menuInput, subMenuInput;
            int reservations = 0, nights = 0, rooms = 0, roomNights = 0, roomNightsTotal = 0;
            decimal total = 0m, grandTotal = 0m;
            List<string> confirmedReservations = new List<string>();

            //Display welcome (Spec. 2)
            C.WriteLine("Welcome to the Sunnyside Resort.  Copyright by {0:s}, 2013.\n", author);

            do
            {
                //Read Menu options (Spec. 3)
                menuInput = askQuestion("Enter 'q' to quit or 'r' to enter the reservation.", false);

                if (menuInput.ToLower().Equals("r"))
                {
                    //Gather user details (Spec 4a)
                    C.Write("Name:  ");
                    name = ReadName();

                    C.Write("Date:  ");
                    date = ReadDate();

                    C.Write("Nights:  ");
                    nights = ReadInt();

                    C.Write("Rooms:  ");
                    rooms = ReadInt();

                    //Calculate total and print to console (Spec 4b)
                    total = (rate * (1 + tax) + fee) * nights * rooms;
                    Console.WriteLine("Your total due is {0:C}.", total);

                    //Read Sub-menu option (Spec. 4c)
                    subMenuInput = askQuestion("Press 'a' to accept or 'c' to cancel.", false);

                    //Get credit card (Spec. 4d)
                    if (subMenuInput.ToLower().Equals("a"))
                    {
                        C.Write("Credit Card:  ");
                        creditCard = ReadCC();

                        reservations++;
                        grandTotal += total;
                        roomNights = rooms * nights;
                        roomNightsTotal += roomNights;

                        //Format reservation details to string for display and then add to the confirmation list.
                        string line = string.Format("{0,-15:s}{1,-15:s}{2,-15:G}{3,-15:C}", name, date, roomNights, total);
                        confirmedReservations.Add(line);
                    }
                }

            } while (menuInput.ToLower().Equals("r"));
            
            //Output line items from confirmed reservation list if any.  Display as a table (Spec. 7a)
            if (confirmedReservations.Count > 0)
            {
                C.WriteLine(tableHeaders);
                foreach (var tableRow in confirmedReservations)
                {
                    C.WriteLine(tableRow);
                }
            }

            //Output total reservations and grand total (Spec. 7b)
            C.WriteLine("\nYou made {0:G} reservations for {1:G} room-nights totaling {2:C}.", reservations, roomNightsTotal, grandTotal);
            C.WriteLine("\nPress enter to exit.");
            C.ReadLine();
        }

        /// <summary>
        /// General method intended as the user a question and record user input.
        /// </summary>
        /// <param name="question">The question to print to the console.</param>
        /// <param name="appendColon">If true, a ':' symbol is appended to the end of the question along with a newline.
        /// If false, the question is printed to the console with a newline.</param>
        /// <returns>The string representation of the user input.</returns>
        static string askQuestion(string question, bool appendColon)
        {
            string answer;

            if (appendColon)
                C.Write("{0,-10:s}:", question);
            else
                C.WriteLine("{0,-10:s}", question);

            answer = C.ReadLine();

            return answer;
        }

        /// <summary>
        /// Read input from console, and then validates the input a positive integer.
        /// Repeats until a valid input is accepted.
        /// </summary>
        /// <returns>Returns a valid, positive integer.</returns>
        static int ReadInt()
        {
            int input = 0;
            string str = C.ReadLine();
            bool validInput = false;
           
            //While the input cannot be parsed, print error message and get new input.         
            while (!validInput)
            {  
                if (int.TryParse(str, out input))
                {
                    if (input >= 0)
                        validInput = true;
                    else
                    {
                        C.Write("Incorrect value, please enter a positive integer:  ");
                        str = C.ReadLine();
                    }
                }
                else
                {
                    C.Write("Incorrect value, please enter a valid integer:  ");
                    str = C.ReadLine();
                }
            }

            return input;
        }

        /// <summary>
        /// Reads the name of a person making a hotel reservation as a string.  Then the method validates the
        /// string by checking for non-letters and whitespace name.
        /// Repeats until a valid input is accepted.
        /// </summary>
        /// <returns>Returns the valid string representaiton of the user inputted name.</returns>
        static string ReadName()
        {
            string name = C.ReadLine();
            bool validInput = false;
            bool hasAlpha = false;

            while (!validInput)
            {
                int i = 0;
                foreach (char c in name)
                {
                    if (!char.IsLetter(c) && c != ' ')
                        break;
                    else if (char.IsLetter(c))
                        hasAlpha = true;

                    i++;
                }

                if (i == name.Length && hasAlpha == true)
                {
                    validInput = true;
                }
                else
                {
                    C.Write("Incorrect value, please enter a valid name:  ");
                    name = C.ReadLine();
                }
            }

            return name;
        }

        /// <summary>
        /// Reads the user input as a string and validates it as date in the form mm/dd/yy.
        /// Repeats until a valid input is accepted.
        /// </summary>
        /// <returns>Returns the string representation of the user inputted date in the form mm/dd/yy.</returns>
        static string ReadDate()
        {
            CultureInfo enUS = new CultureInfo("en-US");
            string date = C.ReadLine();

            DateTime result;
            while (!DateTime.TryParseExact(date, "MM/dd/yy", enUS, DateTimeStyles.None, out result))
            {
                C.Write("Incorrect value, please enter a valid date as format mm/dd/yy:  ");
                date = C.ReadLine();
            }

            date = result.ToString("MM/dd/yy");
            return date;
        }

        /// <summary>
        /// Read the user input string and validates the string is composed of only digits and has a length of exactly four.
        /// Repeats until a valid input is accepted.
        /// </summary>
        /// <returns>Returns the valid string representation of the credit card number.</returns>
        static string ReadCC()
        {
            string creditCard = C.ReadLine();
            bool validInput = false;

            while (!validInput)
            {
                int i = 0;

                if (creditCard.Length == 4)
                {
                    foreach (char c in creditCard)
                    {
                        if (!char.IsDigit(c))
                            break;
                        i++;
                    }
                   
                }

                if (i == creditCard.Length)
                {
                    validInput = true;
                }
                else
                {
                    C.Write("Incorrect value, please enter a 4 digit number:  ");
                    creditCard = C.ReadLine();
                }
            }

            return creditCard;
        }

        /// <summary>
        /// Not used.
        /// Reads the string representation of the user input and validates that it is a real, positive number.
        /// Repeats until a valid input is accepted.
        /// </summary>
        /// <returns>Returns the float representation of the valid user input.</returns>
        static float ReadFloat()
        {
            float input = 0f;
            string str = C.ReadLine();
            bool validInput = false;

            //While the input cannot be parsed, print error message and get new input.
            while (!validInput)
            {
                if (float.TryParse(str, out input))
                {
                    if (input >= 0f)
                    {
                        validInput = true;
                    }
                    else
                    {
                        C.Write("Incorrect value, please enter a positive float:  ");
                        str = C.ReadLine();
                    }
                }
                else
                {
                    C.Write("Incorrect value, please enter a valid float:  ");
                    str = C.ReadLine();
                }
            }

            return input;
        }

    }//End Class Program

}//End Namespace
