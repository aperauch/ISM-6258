using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asg5
{
    public partial class Form1 : Form
    {
        private List<Reservation> reservations = new List<Reservation>();

        public Form1()
        {
            InitializeComponent();
            domesticRadioBtn.Checked = true;
            passportLabel.Visible = false;
            passportBox.Visible = false;
            totalBox.ReadOnly = true;

            string tableHeaders = String.Format("\n{0,-10:s}{1,-15:s}{2,-10:s}{3, -10:s}{4,-6:s}{5,-7:s}{6,-5:s}{7,-5:s}",
                "Total($)", "Name", "Date In", "Price", "rooms", "nights", "C/C", "Passport");

            listBox1.Items.Add(tableHeaders);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            bool validInternational = false;

            if (nameBox.TextLength > 0 && dateBox.TextLength > 0 && nightsBox.TextLength > 0 
                && roomsBox.TextLength > 0 && priceBox.TextLength > 0 && ccBox.TextLength > 0)
            {
                
                if (internationalRadioBtn.Checked)
                {
                    if (passportBox.TextLength > 0)
                    {
                        validInternational = true;
                    }
                    else
                    {
                        //Throw message box
                        MessageBox.Show("Please input a passport number.");
                    }
                }
                else
                {
                    Reservation res;

                    string name = nameBox.Text;
                    DateTime date = DateTime.Parse(dateBox.Text);
                    int nights = int.Parse(nightsBox.Text);
                    int rooms = int.Parse(roomsBox.Text);
                    float price = float.Parse(priceBox.Text);
                    string cc = ccBox.Text;

                    if(validInternational)
                    {
                        string passport = passportBox.Text;
                        res = new International(name, date, price, nights, rooms, cc, passport);
                    }
                    else
                    {
                        res = new Domestic(name, date, price, nights, rooms, cc);
                    }

                    //Check for duplicates
                    bool duplicateExists = false;
                    foreach (Reservation r in reservations)
                    {
                        if (res.Equals(r))
                        {
                            duplicateExists = true;
                            break;
                        }
                    }
                    Reservation result = reservations.Find(item => item.Equals(res));
                    //Add to list if no duplicate exists
                    if (!duplicateExists)
                    {
                        reservations.Add(res);
                        listBox1.Items.Add(res.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Cannot add reservation; a duplicate exists.");
                    }
                }
            }
            else
            {
                //Throw message box
                MessageBox.Show("Please complete all fields.");
            }



        }

        private void internationalRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            passportLabel.Visible = true;
            passportBox.Visible = true;
        }

        private void domesticRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            passportLabel.Visible = false;
            passportBox.Visible = false;
        }
    }
}
