using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Hotel_Project
{
    class HotelSupplier
    {
        public delegate void priceCutEvent();
        public event priceCutEvent pricecut;
        Random random = new Random();
        private int p;
        protected double old_price;
        private int[] day_of_week_array = new int[7];   //Just some hard-coded pricing data to be used in PricingModel.

        public HotelSupplier()
        {
             p = 10;                     //p is the counter which determines if the thread will terminate
             old_price = 11.5;      //arbitrarily sets old_price equal to monday with 20 rooms available

        }
     

        //HotelSupplier_Func is to be called in a thread in main
        public void HotelSupplier_Func()
        {
            double price = PricingModel();
            
            if (price < old_price)
            {
                if (pricecut != null)
                    pricecut();     //event!!
            }
            old_price = price;
            
            OrderProcessing OrderProcessingObject = new OrderProcessing(Decoder(Program.bo.getOneCell()), price);
            OrderProcessingObject.confirmOrder += new OrderProcessing.OrderConfirmationEvent(Program.travelagency1.receiveConfirmation);
            OrderProcessingObject.confirmOrder += new OrderProcessing.OrderConfirmationEvent(Program.travelagency2.receiveConfirmation);
            OrderProcessingObject.confirmOrder += new OrderProcessing.OrderConfirmationEvent(Program.travelagency3.receiveConfirmation);
            OrderProcessingObject.confirmOrder += new OrderProcessing.OrderConfirmationEvent(Program.travelagency4.receiveConfirmation);
            OrderProcessingObject.confirmOrder += new OrderProcessing.OrderConfirmationEvent(Program.travelagency5.receiveConfirmation);
            Thread OrderProcessingThread = new Thread(new ThreadStart(OrderProcessingObject.OrderProcessing_Func));
            OrderProcessingThread.Start();
        }


        //PricingModel -> recieves order object, returns the price of rooms
        protected double PricingModel()
        {
            day_of_week_array[0] = 13; //cost of monday rental
            day_of_week_array[1] = 12; //cost of tuesday rental
            day_of_week_array[2] = 12; //cost of wednesday rental
            day_of_week_array[3] = 15; //cost of thursday rental
            day_of_week_array[4] = 17; //cost of friday rental
            day_of_week_array[5] = 17; //cost of saturday rental
            day_of_week_array[6] = 15; //cost of sunday rental

            double price = 0;
            int rooms_available = random.Next(0, 40);
            int day_of_week = random.Next(0, 7);

            price = day_of_week_array[day_of_week] * 1.5 - rooms_available * 0.4; // algorithm to determine room price
            return price; 
        }

        public static OrderClass Decoder(string code)
        { 
            string[] words = code.Split(' ');
            int senderID = Convert.ToInt32(words[0]);
            int amount = Convert.ToInt32(words[1]);
            int cardno = Convert.ToInt32(words[2]);

            OrderClass OrderObject = new OrderClass(senderID, amount, cardno);
            return OrderObject;
        }
    }
}
