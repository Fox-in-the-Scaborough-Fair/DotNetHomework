using System;

namespace HomeWork2_2
{
    class HomeWork2_2
    {
        static void FindInArray(int[] arrayIn, out int max, out int min, out int sum, out double average)
        {
            if (arrayIn.Length > 0)
            {
                max = min = arrayIn[0];
                sum = 0;
                foreach (int member in arrayIn)
                {
                    if (member > max)
                    {
                        max = member;
                    }
                    if (member < min)
                    {
                        min = member;
                    }
                    sum += member;
                }
                average = Convert.ToDouble(sum) / arrayIn.GetLength(0);
            }
            else
            {
                max = min = sum = 0;
                average = 0.0;
            }
        }

        static void Main(string[] args)
        {
            string temp = "";
            int[] array;
            int num;
            int max, min, sum;
            double average;
            Console.WriteLine("请输入数组元素数量");
            temp = Console.ReadLine();
            try
            {
                num = Int32.Parse(temp);
                array = new int[num];
                for (int i = 0; i < num; i++)
                {
                    Console.WriteLine($"请输入第{i + 1}个元素的值");
                    int numTemp;
                    temp = Console.ReadLine();
                    numTemp = Int32.Parse(temp);
                    array[i] = numTemp;
                }
                FindInArray(array, out max, out min, out sum, out average);
                Console.WriteLine($"该数组最大值为{max}");
                Console.WriteLine($"该数组最小值为{min}");
                Console.WriteLine($"该数组平均值为{average}");
                Console.WriteLine($"该数组所有元素之和为{sum}");
            }
            catch (FormatException)     //处理非整数异常
            {
                Console.WriteLine("输入错误！请输入正整数");
            }
            catch (OverflowException)   //处理符数异常
            {
                Console.WriteLine("输入错误！请输入正整数");
            }
        }
    }
}
