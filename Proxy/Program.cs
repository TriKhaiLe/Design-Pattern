namespace Proxy
{
    using System;

    interface ITrackingProxy
    {
        void GetBusLocation();
    }

    class BusTrackingSystem : ITrackingProxy
    {
        public BusTrackingSystem()
        {
            // Giả lập khởi tạo nặng (tải dữ liệu GPS)
            Console.WriteLine("Khoi tao BusTrackingSystem, dang tai du lieu GPS...");
            System.Threading.Thread.Sleep(1000); // Giả lập thời gian khởi tạo 1 giây
            Console.WriteLine("BusTrackingSystem da khoi tao xong");
        }

        public void GetBusLocation()
        {
            Console.WriteLine("Vi tri xe buyt: Ga Trung Tam");
        }
    }

    class Proxy : ITrackingProxy
    {
        private BusTrackingSystem realTrackingSystem; // Chưa khởi tạo ngay

        // Phương thức để khởi tạo đối tượng nặng khi cần
        private void InitializeTrackingSystem()
        {
            if (realTrackingSystem == null)
            {
                Console.WriteLine("Khoi tao BusTrackingSystem lan dau...");
                realTrackingSystem = new BusTrackingSystem();
            }
        }

        public void GetBusLocation()
        {
            // Chỉ khởi tạo đối tượng nặng khi thực sự cần
            InitializeTrackingSystem();
            realTrackingSystem.GetBusLocation();
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine($"Thoi gian hien tai: {DateTime.Now:HH:mm}");
            Console.WriteLine("Khoi tao Proxy...");
            Proxy proxy = new Proxy();

            Console.WriteLine("\nGoi GetBusLocation lan dau...");
            proxy.GetBusLocation(); // Lúc này mới khởi tạo BusTrackingSystem

            Console.WriteLine("\nGoi GetBusLocation lan hai...");
            proxy.GetBusLocation(); // Không khởi tạo lại vì đã có đối tượng
        }
    }
}