using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asg5
{
    public class Domestic : Reservation
    {
        //Constructors
        public Domestic() : base()
        {

        }

        public Domestic(string name, DateTime date, float price, int nights, int rooms, string creditCard)
            : base(name, date, price, nights, rooms, creditCard)
        {
            customer_name = name;
            date_in = date;
            this.price = price;
            Nights = nights;
            Rooms = rooms;
            creditcard = creditCard;
        }

        public Domestic(Domestic reservation) : base(reservation)
        {
            this.customer_name = reservation.customer_name;
            this.date_in = reservation.DateIn;
            this.price = reservation.price;
            this.Nights = reservation.Nights;
            this.Rooms = reservation.Rooms;
            this.creditcard = reservation.creditcard;
        }

        //Destructor
        ~Domestic() { }

        //Methods
        public override float ComputeTotalDue()
        {
            float total = 0f;

            if (Nights != 0 && Rooms != 0)
                total = (Price * (1 + tax) + fee) * Nights * Rooms;

            return total;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Domestic d = obj as Domestic;
            if ((System.Object)d == null)
                return false;
            else
            {
                if (this.customer_name == d.customer_name && this.DateIn == d.DateIn)
                    return true;
                else
                    return false;
            }
        }

    }
}
