using System;

namespace HomeWork2_4
{
    class HomeWork2_4
    {
        static void DisplayArray(int[,] arrayIn)       //打印目标数组
        {
            for (int i = 0; i < arrayIn.GetLength(0); i++)
            {
                for (int j = 0; j < arrayIn.GetLength(1); j++)
                {
                    Console.Write($"{arrayIn[i, j]}  ");
                }
                Console.WriteLine("");
            }
            return;
        }

        static bool IsToplitzArray(int[,] arrayIn)
        {
            bool isToplitzArray = true;
            for (int column = 0; column < arrayIn.GetLength(0); column++)       //先按列检查
            {
                int tempRow = 0;
                int tempColumn = column;    //从(column,0)开始向下检查对角线
                while (tempRow < arrayIn.GetLength(1) - 1 && tempColumn < arrayIn.GetLength(0) - 1)
                {
                    if (arrayIn[tempColumn, tempRow] != arrayIn[tempColumn + 1, tempRow + 1])
                    {
                        isToplitzArray = false;
                    }
                    tempColumn++;
                    tempRow++;
                }
            }
            for (int row = 0; row < arrayIn.GetLength(1); row++)       //按行检查
            {
                int tempRow = row;
                int tempColumn = 0;      //从(0,row)开始向右检查对角线
                while (tempRow < arrayIn.GetLength(1) - 1 && tempColumn < arrayIn.GetLength(0) - 1)
                {
                    if (arrayIn[tempColumn, tempRow] != arrayIn[tempColumn + 1, tempRow + 1])
                    {
                        isToplitzArray = false;
                    }
                    tempColumn++;
                    tempRow++;

                }
            }
            return isToplitzArray;
        }

        static void Main(string[] args)
        {
            int[,] target;
            string temp = "";
            try
            {
                Console.WriteLine("请输入矩阵的行数");
                temp = Console.ReadLine();
                int width = Int32.Parse(temp);
                Console.WriteLine("请输入矩阵的列数");
                temp = Console.ReadLine();
                int length = Int32.Parse(temp);
                target = new int[width, length];
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        Console.WriteLine($"请输入矩阵第{i + 1}行第{j + 1}列元素的值");
                        temp = Console.ReadLine();
                        target[i, j] = Int32.Parse(temp);
                    }
                }
                Console.WriteLine("输入数组为:");
                DisplayArray(target);
                if (IsToplitzArray(target))
                {
                    Console.WriteLine("该矩阵是托普利茨矩阵");
                }
                else
                {
                    Console.WriteLine("该矩阵不是托普利茨矩阵");
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
