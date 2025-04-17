namespace Strategy
{
    using System;

    public interface IPaymentStrategy
    {
        bool ProcessPayment(double amount);
    }

    public class CreditCardPayment : IPaymentStrategy
    {
        public bool ProcessPayment(double amount)
        {
            Console.WriteLine($"Processing Credit Card payment of ${amount}");
            return true; 
        }
    }

    public class PayPalPayment : IPaymentStrategy
    {
        public bool ProcessPayment(double amount)
        {
            Console.WriteLine($"Processing PayPal payment of ${amount}");
            return true; 
        }
    }

    public class BankTransferPayment : IPaymentStrategy
    {
        public bool ProcessPayment(double amount)
        {
            Console.WriteLine($"Processing Bank Transfer payment of ${amount}");
            return true; 
        }
    }

    public class PaymentContext(IPaymentStrategy strategy)
    {
        private IPaymentStrategy _strategy = strategy;

        public void SetStrategy(IPaymentStrategy strategy)
        {
            _strategy = strategy;
        }

        public bool ExecutePayment(double amount)
        {
            return _strategy.ProcessPayment(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PaymentContext context = new PaymentContext(new CreditCardPayment());
            context.ExecutePayment(100.50); 

            context.SetStrategy(new PayPalPayment());
            context.ExecutePayment(200.75); 

            context.SetStrategy(new BankTransferPayment());
            context.ExecutePayment(300.00); 
        }
    }
}
