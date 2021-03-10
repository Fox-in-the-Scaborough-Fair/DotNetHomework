using System;

namespace HomeWork1_1
{
    class Calculator
    {
        static void Main(string[] args)
        {
            string temp = "";
            double num1, num2;
            Console.WriteLine("请输入两个数字");
            temp = Console.ReadLine();
            num1 = double.Parse(temp);
            temp = Console.ReadLine();
            num2 = double.Parse(temp);
            Console.WriteLine("请输入运算符");
            temp = Console.ReadLine();
            switch (temp)
            {
                case "+":
                    Console.WriteLine($"运算结果为:{num1 + num2}");
                    break;
                case "-":
                    Console.WriteLine($"运算结果为:{num1 - num2}");
                    break;
                case "*":
                    Console.WriteLine($"运算结果为:{num1 * num2}");
                    break;
                case "/":
                    if (num2 == 0)
                        Console.WriteLine("输入错误，结果无效！");
                    else
                        Console.WriteLine($"运算结果为:{num1 / num2}");
                    break;
                case "%":
                    if (num2 == 0)
                        Console.WriteLine("输入错误，结果无效！");
                    else
                        Console.WriteLine($"运算结果为:{num1 % num2}");
                    break;
                default:
                    Console.WriteLine("输入错误，结果无效！");
                    break;
            }
        }
    }
}
