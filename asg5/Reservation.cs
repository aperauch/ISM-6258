using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asg5
{
    public class Reservation
    {
        //Class attributes
        protected DateTime date_in;
        protected string customer_name;
        protected string creditcard;
        protected float price = 125f;
        static protected float tax = 0.0625f;
        static protected float fee = 15f;

        //Access Properties
        protected float Price { get { return price; } }
        protected DateTime DateIn { get { return date_in; } }
        protected int Nights { get; set; }
        protected int Rooms { get; set; }

        //Constructors
        public Reservation()
        {

        }

        //All properties
        public Reservation(string name, DateTime date, float price, int nights, int rooms, string creditCard)
        {
            customer_name = name;
            date_in = date;
            this.price = price;
            Nights = nights;
            Rooms = rooms;
            creditcard = creditCard;
        }

        //Copy Constructor
        public Reservation(Reservation reservation)
        {
            this.customer_name = reservation.customer_name;
            this.date_in = reservation.DateIn;
            this.price = reservation.price;
            this.Nights = reservation.Nights;
            this.Rooms = reservation.Rooms;
            this.creditcard = reservation.creditcard;
        }

        //Desctructor
        ~Reservation() { }

        public static float ComputeTotalDue(float p, int d, int n)
        {
            float total = 0f;

            total = (p * (1 + tax) + fee) * d * n;

            return total;
        }

        public virtual float ComputeTotalDue()
        {
            float total = 0f;

            if (Nights != 0 && Rooms != 0)
                total = (Price * (1 + tax) + fee) * Nights * Rooms;

            return total;
        }

        public override string ToString()
        {
            string state;
            float totalPrice = ComputeTotalDue();
            state = string.Format("{0,-10:0.00}{1,-15:s}{2,-10:s}{3,-10:0.00}{4,-6:G}{5,-7:G}{6,-5:s}",
                totalPrice, customer_name, DateIn.ToString("MM/dd/yy"), Price, Nights, Rooms, creditcard);

            return state;
        }

    }
}