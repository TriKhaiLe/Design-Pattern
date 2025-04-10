namespace Mediator
{
    using System;
    using System.Collections.Generic;

    public interface ITrafficMediator
    {
        void RegisterLight(TrafficLight light);
        void Notify(TrafficLight sender, string eventType);
    }

    public class TrafficLight
    {
        private string direction;
        private string color;
        private ITrafficMediator mediator;

        public TrafficLight(string direction, ITrafficMediator mediator)
        {
            this.direction = direction;
            this.color = "do"; // Mặc định là đỏ
            this.mediator = mediator;
            mediator.RegisterLight(this);
        }

        public string Direction => direction;
        public string Color => color;

        public void TurnGreen()
        {
            mediator.Notify(this, "turnGreen");
        }

        public void TurnRed()
        {
            mediator.Notify(this, "turnRed");
        }

        public void TurnYellow()
        {
            mediator.Notify(this, "turnYellow");
        }

        public void ChangeColor(string newColor)
        {
            this.color = newColor;
            Console.WriteLine($"Den {direction} chuyen sang mau {color}");
        }
    }

    public class TrafficMediator : ITrafficMediator
    {
        private List<TrafficLight> lights;

        public TrafficMediator()
        {
            lights = new List<TrafficLight>();
        }

        public void RegisterLight(TrafficLight light)
        {
            lights.Add(light);
        }

        public void Notify(TrafficLight sender, string eventType)
        {
            if (eventType == "turnGreen")
            {
                // Khi một đèn chuyển xanh, các đèn khác phải chuyển đỏ
                foreach (var light in lights)
                {
                    if (light != sender)
                    {
                        light.ChangeColor("do");
                    }
                }
                sender.ChangeColor("xanh");
            }
            else if (eventType == "turnRed")
            {
                sender.ChangeColor("do");
            }
            else if (eventType == "turnYellow")
            {
                sender.ChangeColor("vang");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ITrafficMediator mediator = new TrafficMediator();

            // Tạo các TrafficLight và đăng ký với Mediator
            TrafficLight eastLight = new TrafficLight("Dong", mediator);
            TrafficLight westLight = new TrafficLight("Tay", mediator);
            TrafficLight southLight = new TrafficLight("Nam", mediator);
            TrafficLight northLight = new TrafficLight("Bac", mediator);

            // Thay đổi trạng thái của các đèn
            Console.WriteLine("Bat dau dieu khien den giao thong:");
            eastLight.TurnGreen(); // Đèn Đông chuyển xanh, các đèn khác chuyển đỏ
            Console.WriteLine("---");
            westLight.TurnGreen(); // Đèn Tây chuyển xanh, các đèn khác chuyển đỏ
            Console.WriteLine("---");
            southLight.TurnYellow(); // Đèn Nam chuyển vàng
        }
    }
}
