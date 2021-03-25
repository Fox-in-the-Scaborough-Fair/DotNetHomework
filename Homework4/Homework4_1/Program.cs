
using System;

namespace Homework4_1
{
    class Program
    {
        public class Node<T>
        {
            public Node<T> Next { get; set; }
            public T Data { get; set; }
            public Node(T t)
            {
                Next = null;
                Data = t;
            }
        }

        public class GenericList<T> 
        {
            private Node<T> head;
            private Node<T> tail;
            public GenericList()
            {
                tail = head = null;
            }
            public Node<T> Head
            {
                get => head;
            }
            public void Add(T t)
            {
                Node<T> n = new Node<T>(t);
                if (tail == null)
                {
                    head = tail = n;
                }
                else 
                {
                    tail.Next = n;
                    tail = n;
                }
            }
            public void ForEach(Action<T> action)
            {
                Node<T> point = this.head;      //指向访问结点

                while (point != null)   //当前非空
                {
                    action(point.Data);
                    point = point.Next;
                }
            }

        }
        static void Main(string[] args)
        {
            GenericList<int> myNode = new GenericList<int>();
            for (int x = 0; x < 10; x++)    //构造链表0->1->2->3->4->5->6->7->8->9
            {
                myNode.Add(x);
            }

            //打印
            Console.WriteLine("链表元素依次为:");
            myNode.ForEach(m => Console.Write($"{m}  "));
            Console.WriteLine();
            //求最大值
            int max = -2147483648;      //初始化为最小值
            myNode.ForEach(m => max = max > m ? max : m);
            Console.WriteLine($"Max={max}");

            //求最小值
            int min = 2147483647;      //初始化为最小值
            myNode.ForEach(m => min = min < m ? min : m);
            Console.WriteLine($"Min={min}");

            //求和
            int sum = 0;
            myNode.ForEach(m => sum += m) ;
            Console.WriteLine($"Sum={sum}");
        }
    }
}