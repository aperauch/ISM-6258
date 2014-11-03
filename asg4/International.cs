using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asg4
{
    public class International : Reservation
    {
        //Attributes
        new private static float fee = 25f;
        private static float surcharge = 5f;
        private string passport;

        //Constructors
        public International() : base()
        {

        }

        public International(string name, DateTime date, float price, int nights, int rooms, string creditCard, string passport)
            : base(name, date, price, nights, rooms, creditCard)
        {
            customer_name = name;
            date_in = date;
            this.price = price;
            Nights = nights;
            Rooms = rooms;
            creditcard = creditCard;
            this.passport = passport;
        }

        public International(International reservation)
            : base(reservation)
        {
            this.customer_name = reservation.customer_name;
            this.date_in = reservation.DateIn;
            this.price = reservation.price;
            this.Nights = reservation.Nights;
            this.Rooms = reservation.Rooms;
            this.creditcard = reservation.creditcard;
            this.passport = reservation.passport;
        }

        //Destructor
        ~International() { }

        //Methods
        public new static float ComputeTotalDue(float p, int d, int n)
        {
            float total = 0f;

            total = (p * (1 + tax) + fee) * d * n + surcharge;

            return total;
        }

        public override float ComputeTotalDue()
        {
            float total = 0f;
           
            if (Nights != 0 && Rooms != 0)
                total = (Price * (1 + tax) + fee) * Nights * Rooms + surcharge;

            return total;
        }

        public override string ToString()
        {
            string state;
            float totalPrice = ComputeTotalDue();

            state = string.Format("{0,-10:0.00}{1,-15:s}{2,-10:s}{3,-10:0.00}{4,-6:G}{5,-7:G}{6,-5:s}{7,-5:s}",
                totalPrice, customer_name, DateIn.ToString("MM/dd/yy"), Price, Nights, Rooms, creditcard, passport);

            return state;
        }
    }
}
