using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Pos
    {
        private int x;
        private int y;
        public int GetX()
        { return x; }
        public int GetY()
        { return y; }
        public void ReX(int t)
        { x = t; }
        public void ReY(int t)
        { y = t; }
        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class Queues
    {
        private Pos[] ele;
        private int front;
        private int rear;
        private int max;
        public Queues(int size)
        {
            ele = new Pos[size];
            front = 0;
            rear = -1;
            max = size;
        }
        public void Enqueue(Pos x)
        {

            if (rear != max)
            {
                ++rear;
                ele[rear] = x; 
            }
        }
        public bool IsEmpty()
        {
            if (front > rear)
                return true;
            return false;
        }
        public int GetMax()
        { return max; }
        public int GetFront()
        { return front; }
        public int GetRear()
        { return rear; }
        public Pos GetPos(int x)
        {
            return ele[x];
        }
        public Pos Dequeue()
        {
            if ((rear == front - 1) || (rear == -1))
            {
                return null;  
            }
            else
            {
                Pos pos = ele[front];
                ++front;                        // not empty
                return pos;
            }
        }


        public void Clear()
        {
            for (int i = 0; i <= rear; i++)
            {
                ele[i] = null;
            }
            front = 0;
            rear = -1;
        }

        //public void Print()
        //{
        //    if ((front == rear - 1) || (rear == -1))
        //    {
        //        Console.WriteLine("Empty");
        //    }
        //    else if (front <= rear)
        //    {
        //        for (int i = front; i <= rear; ++i)
        //        {
        //            Console.Write("{0} ", ele[i]);
        //        }
        //    }
        //    else if (front > rear)
        //    {
        //        for (int i = front; i < max; ++i)
        //            Console.Write("{0} ", ele[i]);
        //        for (int i = 0; i <= rear; ++i)
        //            Console.Write("{0} ", ele[i]);
        //    }
        //}
    }
}
