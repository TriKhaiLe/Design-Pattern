namespace Facade
{
    using System;

    public class KiemTraTonKho
    {
        public bool KiemTraHangTon(string sanPham, int soLuong)
        {
            bool coHang = new Random().Next(0, 2) == 1;
            if (coHang)
            {
                Console.WriteLine($"{soLuong} {sanPham} trong kho san sang.");
                return true;
            }
            Console.WriteLine($"Kho khong co du {sanPham}!");
            return false;
        }
    }

    public class XuLyThanhToan
    {
        public bool ThanhToan(string phuongThuc)
        {
            bool thanhToanThanhCong = new Random().Next(0, 2) == 1;
            if (thanhToanThanhCong)
            {
                Console.WriteLine($"Thanh toan thanh cong bang {phuongThuc}.");
                return true;
            }
            Console.WriteLine($"Thanh toan bang {phuongThuc} that bai!");
            return false;
        }
    }

    public class VanChuyen
    {
        public bool SapXepVanChuyen()
        {
            bool vanChuyenThanhCong = new Random().Next(0, 2) == 1;
            if (vanChuyenThanhCong)
            {
                Console.WriteLine("Van chuyen da duoc sap xep.");
                return true;
            }
            Console.WriteLine("Sap xep van chuyen khong thanh cong!");
            return false;
        }
    }

    public class DonHangFacade
    {
        private KiemTraTonKho _kiemTraTonKho;
        private XuLyThanhToan _xuLyThanhToan;
        private VanChuyen _vanChuyen;

        public DonHangFacade()
        {
            _kiemTraTonKho = new KiemTraTonKho();
            _xuLyThanhToan = new XuLyThanhToan();
            _vanChuyen = new VanChuyen();
        }

        public bool DatHang(string sanPham, int soLuong, string phuongThucThanhToan)
        {
            Console.WriteLine("Bat dau xu ly don hang...");

            if (!_kiemTraTonKho.KiemTraHangTon(sanPham, soLuong))
            {
                return false;
            }

            if (!_xuLyThanhToan.ThanhToan(phuongThucThanhToan))
            {
                return false;
            }

            if (!_vanChuyen.SapXepVanChuyen())
            {
                return false;
            }

            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DonHangFacade facade = new DonHangFacade();

            while (true)
            {
                Console.WriteLine("Hay nhap ten san pham:");
                string sanPham = Console.ReadLine() ?? "Ao thun";

                int soLuong = 0;
                while (soLuong <= 0)
                {
                    Console.WriteLine("Hay nhap so luong: ");
                    int.TryParse(Console.ReadLine(), out soLuong);
                }

                Console.WriteLine("hay nhap phuong thuc thanh toan: ");
                string phuongThucThanhToan = Console.ReadLine() ?? "Tien mat";

                bool ketQua = facade.DatHang(sanPham, soLuong, phuongThucThanhToan);
                if (ketQua)
                {
                    Console.WriteLine("Dat hang thanh cong!");
                }
                else
                {
                    Console.WriteLine("Dat hang that bai!");
                }

                Console.WriteLine("Ban co muon dat don hang khac khong? (Y/N)");
                if (Console.ReadLine().ToLower() != "y")
                {
                    break;
                }
            }
        }
    }
}
