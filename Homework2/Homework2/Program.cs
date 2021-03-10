using System;

namespace HomeWork2_1
{
    class HomeWork2_1
    {

        static string FindFactor(int numIn)      //递归寻找素数因子,numIn为目标数，结果以字符串形式返回
        {
            string result = "";
            if (numIn == 0 || numIn == 1)
            {
                return "";  //若目标数为0或1则无素数因子
            }
            bool isPrime = false;
            double square = Math.Sqrt(numIn);
            for (int i = 2; i <= square; i++)       //在2~square范围内寻找目标数的一个最小的因子
            {
                if (numIn % i == 0)                //i为素数则结果中添加此因子，重复除以因子直到目标数不含该因子为止
                {
                    result = result + i + " ";
                    do
                    {
                        numIn /= i;
                    } while (numIn % i == 0);
                    result = result + FindFactor(numIn);    //递归寻找除以因子以后目标数的其他因子
                    isPrime = false;
                    break;      //找到一个因子即可跳出循环，否则将一直循环
                }
                else
                    isPrime = true;       //若在最后一次循环中未找到因子则说明该目标数为素数
            }
            if (isPrime == true && numIn != 1)     //若目标本身为素数则直接添入结果字符串中
            {
                result = result + numIn;
            }
            return result;
        }
        static void Main(string[] args)
        {
            string temp = "";
            int num;
            Console.WriteLine("请输入一个整数");       //输入目标数
            temp = Console.ReadLine();
            try                                        //检测输入内容类型
            {
                num = Int32.Parse(temp);
                Console.WriteLine($"素数因子分别为: " + FindFactor(num));     //调用FindFactor函数,返回值为string类型
            }
            catch (FormatException)
            {
                Console.WriteLine("输入错误！");
            }
        }
    }
}
