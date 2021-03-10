using System;

namespace HomeWork2_3
{
    class HomeWork2_3
    {
        static int[] EratosthenesSieve(int maxNumIn)       //参数为范围最大值，返回一个整数数组，元素为范围内所有素数
        {
            int[] resultArray;     //结果数组，用于返回范围内为素数的数字
            int primeNum = maxNumIn - 1;       //计算素数个数
            bool[] isPrimeArray;    //bool型数组，用于判断数字是否为素数，为true则为素数，序号代表要判断的数字
            isPrimeArray = new bool[maxNumIn + 1];  //保持数组元素物理地址与逻辑地址相同
            for (int i = 2; i <= maxNumIn; i++) //从2开始
            {
                isPrimeArray[i] = true;
            }
            double maxSieve = Math.Sqrt(maxNumIn); //筛子最不会超过该值
            for (int sieve = 2; sieve < maxSieve; sieve++)      //筛子初始值为2，最大不超过maxNumIn的开平方
            {
                for (int target = sieve + 1; target <= maxNumIn; target++)   //用筛子对2~maxNumIn范围内数字进行筛选
                    if (target % sieve == 0)
                    {
                        if (isPrimeArray[target] == true)
                        {
                            isPrimeArray[target] = false;   //如果为sieve的倍数则被筛掉
                            primeNum--;                     //素数个数减一
                        }
                    }
            }
            resultArray = new int[primeNum];    //为结果数组分配空间
            int temp = 0;       //从resultArray[0]开始赋值
            for (int i = 2; i <= maxNumIn; i++)     //开始扫描isPrimeArray
            {
                if (isPrimeArray[i] == true)    //如果该序号为素数，将数字赋给结果
                {
                    resultArray[temp] = i;
                    temp++;
                }
            }
            return resultArray;
        }
        static void Main(string[] args)
        {
            string temp = "";
            int maxNum;
            Console.WriteLine("请输入寻找素数的范围(即输入最大值，例：100)");  //搜索2~x范围内的素数
            try
            {
                temp = Console.ReadLine();
                maxNum = Int32.Parse(temp);
                int[] resultArray = EratosthenesSieve(maxNum);
                Console.WriteLine($"2~{maxNum}范围内的素数分别为: ");
                foreach (int prime in resultArray)
                {
                    Console.Write($"{prime} ");
                }

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
