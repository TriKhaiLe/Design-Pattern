namespace State
{
    using System;

    // 2. Giao diện State
    public interface IOrderState
    {
        void ProcessOrder();
        void CancelOrder();
    }

    // 1. Lớp Context
    public class Order
    {
        private IOrderState _currentState;
        public Order() {
            // Trạng thái ban đầu là Processing
            _currentState = new ProcessingState(this);
        }
        public void SetState(IOrderState state) {
            _currentState = state;
            Console.WriteLine($"Trạng thái thay đổi thành: {state.GetType().Name}");
        }
        public void Process() {
            _currentState.ProcessOrder();
        }
        public void Cancel() {
            _currentState.CancelOrder();
        }
    }

    // 3. Các lớp Concrete State
    public class ProcessingState : IOrderState
    {
        private readonly Order _context;

        public ProcessingState(Order context)
        {
            _context = context;
        }

        public void ProcessOrder()
        {
            Console.WriteLine("Đơn hàng đang được xử lý và chuyển sang trạng thái Đã giao hàng.");
            _context.SetState(new ShippedState(_context));
        }

        public void CancelOrder()
        {
            Console.WriteLine("Đơn hàng đã bị hủy từ trạng thái Đang xử lý.");
            _context.SetState(new CancelledState(_context));
        }
    }

    public class ShippedState : IOrderState
    {
        private readonly Order _context;

        public ShippedState(Order context)
        {
            _context = context;
        }

        public void ProcessOrder()
        {
            Console.WriteLine("Đơn hàng đã được giao, không thể xử lý thêm.");
        }

        public void CancelOrder()
        {
            Console.WriteLine("Đơn hàng đã giao, không thể hủy.");
        }
    }

    public class CancelledState : IOrderState
    {
        private readonly Order _context;

        public CancelledState(Order context)
        {
            _context = context;
        }

        public void ProcessOrder()
        {
            Console.WriteLine("Đơn hàng đã hủy, không thể xử lý.");
        }

        public void CancelOrder()
        {
            Console.WriteLine("Đơn hàng đã ở trạng thái hủy.");
        }
    }

    // 4. Lớp Client
    public class Client
    {
        public static void Main()
        {
            Order order = new Order();

            Console.WriteLine("Thử xử lý đơn hàng:");
            order.Process();

            Console.WriteLine("\nThử xử lý đơn hàng lần nữa:");
            order.Process();

            Console.WriteLine("\nThử hủy đơn hàng:");
            order.Cancel();

            Order newOrder = new Order();
            Console.WriteLine("\nThử hủy đơn hàng mới:");
            newOrder.Cancel();
        }
    }
}
