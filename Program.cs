
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Hotel_Project
{
    class Program
    {

        public static HotelSupplier hotelsupplier1 = new HotelSupplier();
        public static HotelSupplier hotelsupplier2 = new HotelSupplier();

        public static TravelAgency travelagency1 = new TravelAgency(1);
        public static TravelAgency travelagency2 = new TravelAgency(2);
        public static TravelAgency travelagency3 = new TravelAgency(3);
        public static TravelAgency travelagency4 = new TravelAgency(4);
        public static TravelAgency travelagency5 = new TravelAgency(5);
        public static MultiCellBuffer bo = new MultiCellBuffer();
        static void Main(string[] args)
        {

            //******Subscriptions to pricecut => callback method is TravelAgency_Func****//
            hotelsupplier1.pricecut += new HotelSupplier.priceCutEvent(travelagency1.TravelAgency_Func);
            hotelsupplier1.pricecut += new HotelSupplier.priceCutEvent(travelagency2.TravelAgency_Func);
            hotelsupplier1.pricecut += new HotelSupplier.priceCutEvent(travelagency3.TravelAgency_Func);
            hotelsupplier1.pricecut += new HotelSupplier.priceCutEvent(travelagency4.TravelAgency_Func);
            hotelsupplier1.pricecut += new HotelSupplier.priceCutEvent(travelagency5.TravelAgency_Func);

            hotelsupplier2.pricecut += new HotelSupplier.priceCutEvent(travelagency1.TravelAgency_Func);
            hotelsupplier2.pricecut += new HotelSupplier.priceCutEvent(travelagency2.TravelAgency_Func);
            hotelsupplier2.pricecut += new HotelSupplier.priceCutEvent(travelagency3.TravelAgency_Func);
            hotelsupplier2.pricecut += new HotelSupplier.priceCutEvent(travelagency4.TravelAgency_Func);
            hotelsupplier2.pricecut += new HotelSupplier.priceCutEvent(travelagency5.TravelAgency_Func);
            //***************************************//

            

            //********Hotel Supplier functions to be run as threads**************//
            Thread hotelsupplierThread1 = new Thread(new ThreadStart(hotelsupplier1.HotelSupplier_Func));
            Thread hotelsupplierThread2 = new Thread(new ThreadStart(hotelsupplier2.HotelSupplier_Func));
            hotelsupplierThread1.Name = "Hotel Thread 1";
            hotelsupplierThread2.Name = "Hotel Thread 2";
            hotelsupplierThread1.Start();
            hotelsupplierThread2.Start();
            //************************************************//

            //**************TravelAgency functions to be run as threads*************//
            Thread travelagency1thread = new Thread(new ThreadStart(travelagency1.TravelAgency_Func));
            Thread travelagency2thread = new Thread(new ThreadStart(travelagency2.TravelAgency_Func));
            Thread travelagency3thread = new Thread(new ThreadStart(travelagency3.TravelAgency_Func));
            Thread travelagency4thread = new Thread(new ThreadStart(travelagency4.TravelAgency_Func));
            Thread travelagency5thread = new Thread(new ThreadStart(travelagency5.TravelAgency_Func));
            travelagency1thread.Name = "TA thread 1";
            travelagency2thread.Name = "TA thread 2";
            travelagency3thread.Name = "TA thread 3";
            travelagency4thread.Name = "TA thread 4";
            travelagency5thread.Name = "TA thread 5";
            travelagency1thread.Start();
            travelagency2thread.Start();
            travelagency3thread.Start();
            travelagency4thread.Start();
            travelagency5thread.Start();
            //************************************************//
            
            
            travelagency1thread.Join();
            travelagency2thread.Join();
            travelagency3thread.Join();
            travelagency4thread.Join();
            travelagency5thread.Join();
            hotelsupplierThread1.Join();
            hotelsupplierThread2.Join();

            Console.WriteLine("Done");

        }
    }
}

