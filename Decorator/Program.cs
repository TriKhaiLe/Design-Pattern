namespace Decorator
{
    using System;
    using System.Collections.Generic;

    abstract class Linh
    {
        protected List<string> quanTrangList = new List<string>();
        protected List<string> vuKhiList = new List<string>();
        protected List<string> kyNangList = new List<string>();
        protected string quanHam = "Chua co";

        public List<string> GetQuanTrangList() => quanTrangList;
        public List<string> GetVuKhiList() => vuKhiList;
        public List<string> GetKyNangList() => kyNangList;
        public string GetQuanHam() => quanHam;

        public void SetQuanHam(string quanHam)
        {
            this.quanHam = quanHam;
        }

        public abstract void GetItem(string quanHam);
    }

    class LinhCoBan : Linh
    {
        public LinhCoBan()
        {
            quanTrangList.Add("Balo");
            quanTrangList.Add("Quan ao");
            quanTrangList.Add("Non");
        }

        public override void GetItem(string quanHam)
        {
            Console.WriteLine("Quan trang: " + string.Join(", ", quanTrangList));
            Console.WriteLine("Vu khi: " + (vuKhiList.Count > 0 ? string.Join(", ", vuKhiList) : "Khong co"));
            Console.WriteLine("Ky nang: " + (kyNangList.Count > 0 ? string.Join(", ", kyNangList) : "Khong co"));
            Console.WriteLine("Quan ham: " + quanHam);
            Console.WriteLine();
        }
    }

    abstract class LinhDecorator : Linh
    {
        protected Linh linh;

        public LinhDecorator(Linh linh)
        {
            this.linh = linh;
            this.quanTrangList = linh.GetQuanTrangList();
            this.vuKhiList = linh.GetVuKhiList();
            this.kyNangList = linh.GetKyNangList();
            this.quanHam = linh.GetQuanHam();
        }

        public override void GetItem(string quanHam)
        {
            linh.GetItem(quanHam);
        }
    }

    class VuKhi : LinhDecorator
    {
        public VuKhi(Linh linh, string vuKhi) : base(linh)
        {
            vuKhiList.Add(vuKhi);
        }
    }

    class KyNang : LinhDecorator
    {
        public KyNang(Linh linh, string kyNang) : base(linh)
        {
            kyNangList.Add(kyNang);
        }
    }

    class QuanHam : LinhDecorator
    {
        public QuanHam(Linh linh, string quanHamMoi) : base(linh)
        {
            this.quanHam = quanHamMoi;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Linh linh = new LinhCoBan();
            Console.WriteLine("Linh ban dau:");
            linh.GetItem(linh.GetQuanHam());

            linh = new VuKhi(linh, "Sung");
            Console.WriteLine("Sau khi trang bi sung:");
            linh.GetItem(linh.GetQuanHam());

            linh = new KyNang(linh, "Ban sung");
            Console.WriteLine("Sau khi hoc ky nang ban sung:");
            linh.GetItem(linh.GetQuanHam());

            linh = new QuanHam(linh, "Thieu uy");
            Console.WriteLine("Sau khi thang cap Thieu uy:");
            linh.GetItem(linh.GetQuanHam());

            linh = new QuanHam(linh, "Trung uy");
            Console.WriteLine("Sau khi thang cap Trung uy:");
            linh.GetItem(linh.GetQuanHam());

            linh = new QuanHam(linh, "Quan nhan");
            Console.WriteLine("Sau khi giang cap xuong Quan nhan:");
            linh.GetItem(linh.GetQuanHam());
        }
    }
}
