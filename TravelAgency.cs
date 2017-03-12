using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Hotel_Project
{
    
    class TravelAgency
    {

        int id, amount, cardNo;
        string encodedOrder;
        DateTime timestamp = new DateTime();
        public TravelAgency(int i)
        {
            id = i;
            amount = 0;
            cardNo = 6000;
            encodedOrder = null;
        }
        public void TravelAgency_Func()
        {
            amount = 3;
            OrderClass oo = new OrderClass(id, amount, cardNo);
            encodedOrder = Encoder(oo);
            timestamp = DateTime.Now;
            //At this point, the travel agency is ready to send an encoded string to the multicellbuffer

            Monitor.Enter(Program.bo);
            try
            {
                Console.WriteLine("Thread" + Thread.CurrentThread.Name + " Setting Order");
                Program.bo.setOneCell(encodedOrder);
            }
            finally { Monitor.Exit(Program.bo); }
        }

        public void receiveConfirmation()
        {
            Console.WriteLine("The Order Was processed");
        }
        public string Encoder(OrderClass o)
        {
            return o.getOrder();
        }
    }
}
