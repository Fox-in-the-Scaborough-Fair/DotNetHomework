using System;
using System.Threading;

namespace Timer
{


    //声明委托类型
    public delegate void ClockWork(object sender, ClockArgs args); 

    //声明参数类型
    public class ClockArgs 
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
    }
    //定义事件源
    public class ClockTikTok        
    {
        //声明事件
        public event ClockWork Tick;
        public event ClockWork Alarm;
        private int hour,minute,second;
        public ClockTikTok(int hour, int minute, int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }
        public void TikTok()
        {
            this.second++;   //时间前进1s
            if (this.second >= 60)
            {
                this.second = this.second % 60;
                this.minute++;
                if (this.minute >= 60)
                {
                    this.minute = this.minute % 60;
                    this.hour++;
                    if (this.hour >= 24)
                    {
                        this.hour = this.hour % 24;
                    }
                }
            }
            ClockArgs args = new ClockArgs()
            {
                Hour = this.hour, Minute = this.minute, Second = this.second
            };
            Tick(this, args);
            Alarm(this, args);
        }
    }

    public class UseClock
    {
        static void Main(string[] args)
        {
            uint batteryCapacity = 14630400;        //模拟闹钟可运行十天(24*60*60秒)

            ClockTikTok myClock = new ClockTikTok(23, 59, 50);  //初始化闹钟

            //注册事件
            myClock.Tick += ShowTick;
            myClock.Alarm += ShowAlarm;

            //事件处理方法
            static void ShowTick(object sender, ClockArgs now)
            {
                Console.WriteLine($"TikTok! Time is {now.Hour}:{now.Minute}:{now.Second}");
            }

            static void ShowAlarm(object sender, ClockArgs now) //设置每过一分钟闹钟触发一次
            {
                if (now.Second == 0)
                {
                    Console.WriteLine($"Alarm! DingRing! DingRing! DingRing! DingRing! DingRing!");
                }
            }
            while (batteryCapacity > 0)
            {
                System.Threading.Thread.Sleep(1000);
                myClock.TikTok();
                batteryCapacity--;
            }
        }
    }
}
