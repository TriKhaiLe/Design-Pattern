namespace State
{
    using System;

    interface IState
    {
        void Handle(Lawsuit lawsuit);
        void NextState(Lawsuit lawsuit);
    }

    class FillingState : IState
    {
        public void Handle(Lawsuit lawsuit)
        {
            Console.WriteLine("Dang xu ly ho so vu an");
        }

        public void NextState(Lawsuit lawsuit)
        {
            lawsuit.SetState(new TrialState());
            Console.WriteLine("Chuyen den trang thai xet xu");
        }
    }

    class TrialState : IState
    {
        public void Handle(Lawsuit lawsuit)
        {
            Console.WriteLine("Dang xet xu vu an");
        }

        public void NextState(Lawsuit lawsuit)
        {
            lawsuit.SetState(new AwaitingJudgmentState());
            Console.WriteLine("Chuyen den trang thai cho phan quyet");
        }
    }

    class AwaitingJudgmentState : IState
    {
        public void Handle(Lawsuit lawsuit)
        {
            Console.WriteLine("Dang cho phan quyet cua vu an");
        }

        public void NextState(Lawsuit lawsuit)
        {
            lawsuit.SetState(new ClosedState());
            Console.WriteLine("Chuyen den trang thai dong vu an");
        }
    }

    class ClosedState : IState
    {
        public void Handle(Lawsuit lawsuit)
        {
            Console.WriteLine("Vu an da dong");
        }

        public void NextState(Lawsuit lawsuit)
        {
            Console.WriteLine("Vu an da ket thuc, khong the chuyen trang thai");
        }
    }

    class Lawsuit
    {
        private IState currentState;

        public Lawsuit()
        {
            currentState = new FillingState();
        }

        public void SetState(IState state)
        {
            currentState = state;
        }

        public void Handle()
        {
            currentState.Handle(this);
        }

        public void NextState()
        {
            currentState.NextState(this);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Lawsuit lawsuit = new Lawsuit();
            lawsuit.Handle();
            lawsuit.NextState();
            Console.WriteLine();

            lawsuit.Handle();
            lawsuit.NextState();
            Console.WriteLine();

            lawsuit.Handle();
            lawsuit.NextState();
            Console.WriteLine();

            lawsuit.Handle();
            lawsuit.NextState();
        }
    }
}
