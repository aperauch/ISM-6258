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
 * Date:  11/27/2013
 * 
 * Assignment 4
 */

namespace asg4
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
            string tableHeaders = String.Format("\n{0,-10:s}{1,-15:s}{2,-10:s}{3, -10:s}{4,-6:s}{5,-7:s}{6,-5:s}{7,-5:s}", 
                "Total ($)","Name", "Date In", "Price", "rooms", "nights", "C/C", "Passport");

            //Variables
            bool isInternational = false;
            DateTime date;
            string name, creditCard, passport, reservationType;
            string menuInput, subMenuInput;
            int resNum = 0, nights = 0, rooms = 0, roomNights = 0, roomNightsTotal = 0;
            float price = 0f, total = 0f, grandTotal = 0f;
            Reservation[] reservations = new Reservation[10];

            //Display welcome
            C.WriteLine("Welcome to the Sunnyside Resort.  Copyright by {0:s}, 2013.\n", author);

            do
            {
                //Read Menu options
                C.Write("Enter 'q' to quit or 'r' to enter the reservation:  ");
                menuInput = C.ReadLine().ToLower();

                if (menuInput.Equals("r"))
                {
                    //Gather user details
                    C.Write("Please enter your name:  ");
                    name = ReadName();

                    C.Write("Please enter date-in:  ");
                    date = ReadDate();

                    C.Write("Plase enter price:  ");
                    price = ReadFloat();

                    C.Write("Please enter number of nights:  ");
                    nights = ReadInt();

                    C.Write("Please enter number of rooms:  ");
                    rooms = ReadInt();

                    C.Write("Press d for domestic and i for international:  ");
                    reservationType = C.ReadLine().ToLower();

                    //Determine if reservation is domestic or international.
                    //Calculate appropriate total based on reservation type.
                    if (reservationType.Equals("d"))
                    {
                        total = Reservation.ComputeTotalDue(price, nights, rooms);
                    }
                    else if (reservationType.Equals("i"))
                    {
                        isInternational = true;
                        total = International.ComputeTotalDue(price, nights, rooms);
                    }

                    C.WriteLine("Your total due is {0:C}", total);

                    //Read Sub-menu option
                    C.Write("Press 'a' to accept or 'c' to cancel:  ");
                    subMenuInput = C.ReadLine().ToLower();

                    //Get credit card
                    if (subMenuInput.Equals("a"))
                    {
                        Reservation r;

                        C.Write("Please enter a credit card number:  ");
                        creditCard = ReadCC();

                        if (isInternational)
                        {
                            C.Write("Please enter a passport number:  ");
                            passport = ReadCC();
                            r = new International(name, date, price, nights, rooms, creditCard, passport);
                        }
                        else
                        {
                            r = new Domestic(name, date, price, nights, rooms, creditCard);
                        }

                        grandTotal += total;
                        roomNights = rooms * nights;
                        roomNightsTotal += roomNights;

                        reservations[resNum] = r;
                        resNum++;
                    }
                }

            } while (menuInput.Equals("r"));

            //Output line items from confirmed reservation list if any.  Display as a table
            C.WriteLine(tableHeaders);
            for (int i = 0; i < resNum; i++)
            {
                string line = reservations[i].ToString();
                C.WriteLine(line);
            }


            //Output total reservations and grand total (Spec. 7b)
            C.WriteLine("\nYou made {0:G} reservations for {1:G} room-nights totaling {2:C}.", resNum, roomNightsTotal, grandTotal);
            C.WriteLine("\nPress enter to exit.");
            C.ReadLine();
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
        static DateTime ReadDate()
        {
            CultureInfo enUS = new CultureInfo("en-US");
            string date = C.ReadLine();

            DateTime result;
            string[] formats = { "MM/dd/yy", "M/d/yy" };
            while (!DateTime.TryParseExact(date, formats, enUS, DateTimeStyles.None, out result))
            {
                C.Write("Incorrect value, please enter a valid date as format mm/dd/yy:  ");
                date = C.ReadLine();
            }

            date = result.ToString("MM/dd/yy");

            return Convert.ToDateTime(date);
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
                    C.Write("Incorrect value, please enter a four digit number:  ");
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
                    C.Write("Incorrect value, please enter a valid number:  ");
                    str = C.ReadLine();
                }
            }

            return input;
        }

    }//End Class Program

}//End Namespace