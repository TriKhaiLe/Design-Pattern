namespace Decorator
{
    using System;
    using System.Collections.Generic;

    public interface ILinh
    {
        List<string> GetQuanTrangList();
        List<string> GetVuKhiList();
        List<string> GetKyNangList();
        string GetQuanHam();
        string SetQuanHam(string quanHam);
        void GetItem();
    }

    public class LinhCoBan : ILinh
    {
        protected List<string> quanTrangList = new List<string> { "Balo", "Quan ao", "Non" };
        protected List<string> vuKhiList = [];
        protected List<string> kyNangList = [];
        protected string quanHam = "Chua co";

        public List<string> GetQuanTrangList() => quanTrangList;
        public List<string> GetVuKhiList() => vuKhiList;
        public List<string> GetKyNangList() => kyNangList;
        public string GetQuanHam() => quanHam;

        public string SetQuanHam(string quanHam)
        {
            this.quanHam = quanHam;
            return this.quanHam;
        }

        public void GetItem()
        {
            Console.WriteLine("Quan trang: " + string.Join(", ", quanTrangList));
            Console.WriteLine("Vu khi: " + (vuKhiList.Count > 0 ? string.Join(", ", vuKhiList) : "Khong co"));
            Console.WriteLine("Ky nang: " + (kyNangList.Count > 0 ? string.Join(", ", kyNangList) : "Khong co"));
            Console.WriteLine("Quan ham: " + quanHam);
            Console.WriteLine();
        }
    }

    public abstract class LinhDecorator : ILinh
    {
        protected ILinh linh;

        public LinhDecorator(ILinh linh) => this.linh = linh;

        public virtual List<string> GetQuanTrangList() => linh.GetQuanTrangList();
        public virtual List<string> GetVuKhiList() => linh.GetVuKhiList();
        public virtual List<string> GetKyNangList() => linh.GetKyNangList();
        public virtual string GetQuanHam() => linh.GetQuanHam();
        public virtual string SetQuanHam(string quanHam) => linh.SetQuanHam(quanHam);
        public virtual void GetItem() => linh.GetItem();
    }

    public class VuKhi : LinhDecorator
    {
        public VuKhi(ILinh linh, string vuKhi) : base(linh) => linh.GetVuKhiList().Add(vuKhi);

        public override void GetItem() => base.GetItem();
    }

    public class KyNang : LinhDecorator
    {
        public KyNang(ILinh linh, string kyNang) : base(linh) => linh.GetKyNangList().Add(kyNang);

        public override void GetItem() => base.GetItem();
    }

    public class QuanHam : LinhDecorator
    {
        public QuanHam(ILinh linh, string quanHam) : base(linh)
        {
            this.linh.SetQuanHam(quanHam);
        }

        public override string GetQuanHam() => base.GetQuanHam();

        public override void GetItem() => base.GetItem();
    }

    class Program
    {
        static void Main(string[] args)
        {
            ILinh linh = new LinhCoBan();
            Console.WriteLine("Linh ban dau:");
            linh.GetItem();

            linh = new VuKhi(linh, "Sung");
            Console.WriteLine("Sau khi trang bi sung:");
            linh.GetItem();

            linh = new KyNang(linh, "Ban sung");
            Console.WriteLine("Sau khi hoc ky nang ban sung:");
            linh.GetItem();

            linh = new QuanHam(linh, "Thieu uy");
            Console.WriteLine("Sau khi thang cap Thieu uy:");
            linh.GetItem();

            linh = new QuanHam(linh, "Trung uy");
            Console.WriteLine("Sau khi thang cap Trung uy:");
            linh.GetItem();

            linh = new QuanHam(linh, "Quan nhan");
            Console.WriteLine("Sau khi giang cap xuong Quan nhan:");
            linh.GetItem();
        }
    }
}
