using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asg3
{
    public class Reservation
    {
        //Spec 1a
        //Class attributes
        private string CustomerName;
        private string CreditCard;
        private float Price = 125f;
        static private float Tax = 0.065f;
        static private float Fee = 15f;

        //Spec 1c
        //Access Properties
        public string DateIn { get; private set; }
        public int Nights { private get; set; }
        public int Rooms { private get; set; }


        //Spec 1b
        //Constructors
        //Default
        public Reservation()
        {

        }

        //All properties
        public Reservation(string name, string date, int nights, int rooms, string creditCard)
        {
            CustomerName = name;
            DateIn = date;
            Nights = nights;
            Rooms = rooms;
            CreditCard = creditCard;
        }

        //Copy Constructor
        public Reservation(Reservation reservationToCopy)
        {
            this.CustomerName = reservationToCopy.CustomerName;
            this.DateIn = reservationToCopy.DateIn;
            this.Nights = reservationToCopy.Nights;
            this.Rooms = reservationToCopy.Rooms;
            this.CreditCard = reservationToCopy.CreditCard;
        }

        //Spec 1d
        public static float ComputeTotalDue(float price, int nights, int rooms)
        {
            float total = 0f;

            total = (price * (1 + Tax) + Fee) * nights * rooms;

            return total;
        }

        //Sepc 1d
        public float ComputeTotalDue()
        {
            float total = 0f;

            if (Nights != 0 && Rooms != 0)
                total = (Price * (1 + Tax) + Fee) * Nights * Rooms;

            return total;
        }

        //Spec 1e
        public override string ToString()
        {
            string state;

            state = string.Format("{0,-15:s}{1,-10:s}{2,-7:G}{3,-7:G}{4,-7:s}", CustomerName, DateIn, Nights, Rooms, CreditCard);

            return state;
        }

    }
}