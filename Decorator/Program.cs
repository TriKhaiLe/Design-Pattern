namespace Decorator
{
    using System;

    abstract class Linh
    {
        public abstract string GetItem();
    }

    class LinhCoBan : Linh
    {
        public override string GetItem()
        {
            return "\nQuan trang: Balo, quan ao, non";
        }
    }

    abstract class LinhDecorator : Linh
    {
        protected Linh linh;
        public LinhDecorator(Linh linh)
        {
            this.linh = linh;
        }

        public override string GetItem()
        {
            return linh.GetItem();
        }
    }

    class VuKhi : LinhDecorator
    {
        public VuKhi(Linh linh) : base(linh) { }

        public override string GetItem()
        {
            return base.GetItem() + ", Vu khi: Sung, ao giap, luu dan";
        }
    }

    class KyNang : LinhDecorator
    {
        public KyNang(Linh linh) : base(linh) { }

        public override string GetItem()
        {
            return base.GetItem() + ", Ky nang: Ban, chua tri, nau an";
        }
    }

    class QuanHam : LinhDecorator
    {
        readonly string _quanHam;
        public QuanHam(Linh linh, string quanHam) : base(linh) => _quanHam = quanHam;

        public override string GetItem()
        {
            return base.GetItem() + $", Quan ham: {_quanHam}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Linh linh = new LinhCoBan();
            Console.WriteLine("Linh co ban: " + linh.GetItem());

            linh = new VuKhi(linh);
            Console.WriteLine("\nSau khi trang bi vu khi: " + linh.GetItem());

            linh = new KyNang(linh);
            Console.WriteLine("\nSau khi hoc ky nang: " + linh.GetItem());

            linh = new QuanHam(linh, "Thieu Uy");
            Console.WriteLine("\nSau khi thang cap: " + linh.GetItem());
        }
    }
}
