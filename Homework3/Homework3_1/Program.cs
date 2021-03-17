using System;

namespace Homework3_1
{
    
    //使用接口实现
    interface IShape      //接口定义
    {
        bool IsLegal();
        double GetArea();
        void display();
    }

    class Rectangle : IShape
    {
        private double length, width;
        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
            this.display();
        }
        public bool IsLegal()
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
        public double GetArea()
        {
            return this.length * this.width;
        }
        public string Type
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
        public void display()
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

    class Square : IShape
    {
        private double length;
        public Square(double length)
        {
            this.length = length;
            this.display();
        }
        public bool IsLegal()
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
        public double GetArea()
        {
            return this.length * this.length;
        }
        public string Type
        {
            get => "Square";
        }
        public double Length        //接口
        {
            get => length;
        }
        public void display()
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

    class Triangle : IShape
    {
        private double length1, length2, length3;

        public Triangle(double length1, double length2, double length3)
        {
            this.length1 = length1;
            this.length2 = length2;
            this.length3 = length3;
            this.display();
        }

        public bool IsLegal()
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
        public double GetArea()
        {
            double p = (length1 + length2 + length3);
            return Math.Sqrt(p * (p - length1) * (p - length2) * (p - length3));
        }
        public string Type
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
        public void display()
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
    /*

    //使用抽象类实现
    abstract class Shape    //抽象类
    {
        public abstract bool IsLegal();     //判断形状是否合法
        public abstract double GetArea();       //计算图形面积
        public abstract string Type { get; }
        public abstract void display();
    }

    class Rectangle : Shape 
    {
        private double length,width;
        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
            this.display();
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
        public override double GetArea ()
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
        public override void display()
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
            this.display();
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
            return this.length*this.length;
        }
        public override string Type
        {
            get => "Square";
        }
        public double Length        //接口
        {
            get => length;
        }
        public override void display()
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
        private double length1,length2,length3;

        public Triangle(double length1, double length2, double length3)
        {
            this.length1 = length1;
            this.length2 = length2;
            this.length3 = length3;
            this.display();
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
        public override void display()
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
    }*/

    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rectangle1 = new Rectangle(10.5, 8.4);
            Square square1 = new Square(10.5);
            Triangle triangle1 = new Triangle(10.5, 8.4, 7.3);
        }
    }
}
