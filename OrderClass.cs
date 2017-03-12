using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Hotel_Project
{
    class OrderClass
    {

        private int senderID, amount, cardNo;

        public OrderClass(int s, int a, int c)
        {
            senderID = s;
            amount = a;
            cardNo = c;
        }

        public String getOrder()
        {
            return senderID + " " + amount + " " + cardNo;
        }

        public int getCardNo() { return this.cardNo; }
        public int getSenderId() { return this.senderID; }
        public int getamount() { return this.amount;  }
        public void setSenderId(int code) { this.senderID = code; }
        public void setamount(int code) { this.amount = code; }
        public void setCardNo(int code) { this.cardNo = code; }
    }
}
