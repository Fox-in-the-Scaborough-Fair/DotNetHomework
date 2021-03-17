using System;
using System.Threading;
using System.Diagnostics;
using System.Security;
namespace Homework3_2
{
    public abstract class Shape    //抽象类
    {
        public abstract bool IsLegal();     //判断形状是否合法
        public abstract double GetArea();       //计算图形面积
        public abstract string Type { get; }
        public abstract void Display();
    }

    class Rectangle : Shape
    {
        private double length, width;
        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
            this.Display();
        }
        public override bool IsLegal()
        {
            if (length <= 0 || width <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override double GetArea()
        {
            return this.length * this.width;
        }
        public override string Type
        {
            get => "Rectangle";
        }
        public double Length        //接口
        {
            get => length;
        }
        public double Width     //接口
        {
            get => width;
        }
        public override void Display()
        {
            if (this.IsLegal())
            {
                Console.WriteLine($"此图形类型为{this.Type},长为{this.Length},宽为{this.Width},面积为{this.GetArea()}");
            }
            else
            {
                Console.WriteLine("参数输入错误");
            }
        }
    }

    class Square : Shape
    {
        private double length;

        public Square(double length)
        {
            this.length = length;
            this.Display();
        }
        public override bool IsLegal()
        {
            if (length <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override double GetArea()
        {
            return this.length * this.length;
        }
        public override string Type
        {
            get => "Square";
        }
        public double Length        //接口
        {
            get => length;
        }
        public override void Display()
        {
            if (this.IsLegal())
            {
                Console.WriteLine($"此图形类型为{this.Type},边长为{this.Length},面积为{this.GetArea()}");
            }
            else
            {
                Console.WriteLine("参数输入错误");
            }
        }
    }

    class Triangle : Shape
    {
        private double length1, length2, length3;

        public Triangle(double length1, double length2, double length3)
        {
            this.length1 = length1;
            this.length2 = length2;
            this.length3 = length3;
            this.Display();
        }
        public override bool IsLegal()
        {
            if (this.length1 <= 0 || this.length2 <= 0 || this.length3 <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override double GetArea()
        {
            double p = (length1 + length2 + length3);
            return Math.Sqrt(p * (p - length1) * (p - length2) * (p - length3));
        }
        public override string Type
        {
            get => "Triangle";
        }
        public double Length1        //接口
        {
            get => length1;
        }
        public double Length2     //接口
        {
            get => length2;
        }
        public double Length3     //接口
        {
            get => length3;
        }
        public override void Display()
        {
            if (this.IsLegal())
            {
                Console.WriteLine($"此图形类型为{this.Type},三边边长分别为{this.Length1},{this.Length2},{this.Length3}，面积为{this.GetArea()}");
            }
            else
            {
                Console.WriteLine("参数输入错误");
            }
        }
    }


    public class ShapeFactory
    {
        public Shape CreateShape(
            string shapeStyle,
            double length1,
            double length2,
            double length3)
        {
            switch (shapeStyle)
            {
                case "Rectangle": //长方形
                    {
                        return new Rectangle(length1,length2);
                    }
                case "Square":    //正方形
                    {
                        return new Square(length1);
                    }
                default :    //三角形
                    {
                        return new Triangle(length1, length2,length3);
                    }
            }
        }
        public double GetShapeArea() //随机创建一个图形，并且随机建立参数
        {
            double area;
            Random rd = new Random();
            Thread.Sleep(10);
            int type = rd.Next(0, 998) % 3;
            double length1 = rd.Next(1,100); //生成三条边的值，不一定全部用得到
            double length2 = rd.Next(1,100);
            double length3 = rd.Next(1,100);
            switch (type)
            {
                case 0: //长方形
                    Shape rectangle = CreateShape("Rectangle", length1, length2, length3);
                    area = rectangle.GetArea();
                    break;
                case 1: //正方形
                    Shape square = CreateShape("Square", length1, length2, length3);
                    area = square.GetArea();
                    break;
                default:    //三角形
                    Shape triangle = CreateShape("Triangle", length1, length2, length3);
                    area = triangle.GetArea();
                    break;
            }
            return area;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ShapeFactory myFactory = new ShapeFactory();
            double sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += myFactory.GetShapeArea();
            }
            Console.WriteLine($"Sum is {sum}");
        }
    }
}
