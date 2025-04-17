namespace Observer
{
    using System;
    using System.Collections.Generic;

    interface IObserver
    {
        void Update(string status);
    }

    class Customer : IObserver
    {
        private string name;
        private string currentStatus;

        public Customer(string name)
        {
            this.name = name;
        }

        public void Update(string status)
        {
            currentStatus = status;
            DisplayStatus();
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"Khach hang {name} nhan thong bao: Trang thai don hang - {currentStatus}");
        }
    }

    class Order
    {
        private string status;
        private List<IObserver> customers = new List<IObserver>();

        public void RegisterCustomer(IObserver customer)
        {
            customers.Add(customer);
        }

        public void RemoveCustomer(IObserver customer)
        {
            customers.Remove(customer);
        }

        public void NotifyCustomers()
        {
            foreach (var customer in customers)
            {
                customer.Update(status);
            }
        }

        public void UpdateStatus(string newStatus)
        {
            status = newStatus;
            Console.WriteLine($"Cap nhat trang thai don hang thanh: {status}");
            NotifyCustomers();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order();

            Customer customer1 = new Customer("Nguyen Van A");
            Customer customer2 = new Customer("Tran Thi B");

            order.RegisterCustomer(customer1);
            order.RegisterCustomer(customer2);

            order.UpdateStatus("dang chuan bi");
            Console.WriteLine();

            order.UpdateStatus("da san sang de giao");
            Console.WriteLine();

            order.RemoveCustomer(customer2);

            order.UpdateStatus("dang giao hang");
            Console.WriteLine();

            order.UpdateStatus("hoan tat");
        }
    }
}
