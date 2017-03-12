using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Project
{
    class OrderProcessing
    {
        public delegate void OrderConfirmationEvent();
        public event OrderConfirmationEvent confirmOrder;
        private double total_charge;
        private bool card_is_valid;

        public OrderProcessing(OrderClass OrderObject, double price)
        {

            if (OrderObject.getCardNo() > 7000 || OrderObject.getCardNo() < 5000) { set_card_is_valid(false); }
            else { set_card_is_valid(true); }

            //calculate total amount of charge = unitPrice*numOfRooms + Tax + LocationCharge
            double num = price * OrderObject.getamount() + .075 + 2;
            set_total_charge(num);
        }


        public void OrderProcessing_Func()
        {
            double charge = get_total_charge();
            bool valid = get_card_is_valid();

            Console.WriteLine("Card Transaction Successful: {0} || Time the order was processed: {1} || Total Charge: {2}", valid, DateTime.Now, charge);
            confirmOrder();
        }



        public bool get_card_is_valid() { return this.card_is_valid; }
        public double get_total_charge() { return this.total_charge; }
        public void set_card_is_valid(bool p) { this.card_is_valid = p; }
        public void set_total_charge(double num) { this.total_charge = num; }
    }
}
