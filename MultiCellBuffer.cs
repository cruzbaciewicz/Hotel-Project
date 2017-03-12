using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Hotel_Project
{
    class MultiCellBuffer
    {
        const int SIZE = 3;
        int head = 0, tail = 0, nElements = 0;
        string[] buffer = new string[SIZE];

        Semaphore sem = new Semaphore(3, 3);
        // Semaphore read = new Semaphore(2, 2);


        public void setOneCell(string n)
        {
            sem.WaitOne();
            Console.WriteLine("Thread : " + Thread.CurrentThread.Name + "Entered Write");
            lock (this)
            {
                while (nElements == SIZE)
                {
                    Monitor.Wait(this);
                }

                buffer[tail] = n;
                tail = (tail + 1) % SIZE;
                Console.WriteLine("Write to the buffer: {0}, {1}, {2}", n, DateTime.Now, nElements);
                nElements++;
                Console.WriteLine("Thread : " + Thread.CurrentThread.Name + "Leaving Write");
                sem.Release();
                Monitor.Pulse(this);

            }

        }

        public string getOneCell()
        {
            sem.WaitOne();
            Console.WriteLine("Thread : " + Thread.CurrentThread.Name + "Entred Read");
            lock (this)
            {
                string element;
                while (nElements == 0)
                {
                    Monitor.Wait(this);
                }

                element = buffer[head];
                head = (head + 1) % SIZE;
                nElements--;
                Console.WriteLine("Get from the buffer: {0} , {1}, {2}", element, DateTime.Now, nElements);
                Console.WriteLine("Thread : " + Thread.CurrentThread.Name + "leaving Read");
                sem.Release();
                Monitor.Pulse(this);
                return element;
            }

        }
    }
}
