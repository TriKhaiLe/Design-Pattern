namespace Proxy
{
    using System;
    using System.Collections.Generic;

    public interface IClothingImage
    {
        void Display();
    }

    public class ClothingImage : IClothingImage
    {
        private string _fileName;
        private long _size;

        public ClothingImage(string fileName, long size)
        {
            _fileName = fileName;
            _size = size;
            LoadImageFromServer();
        }

        private void LoadImageFromServer()
        {
            Console.WriteLine($"Dang tai anh {_fileName} tu server... (Kich thuoc: {_size}TB)");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine($"Da tai xong anh {_fileName}");
        }

        public void Display()
        {
            Console.WriteLine($"Hien thi anh: {_fileName}");
        }
    }

    public class ProxyClothingImage : IClothingImage
    {
        private ClothingImage? _realImage;
        private string _fileName;
        private long _size;

        public ProxyClothingImage(string fileName, long size)
        {
            _fileName = fileName;
            _size = size;
        }

        public void Display()
        {
            _realImage ??= new ClothingImage(_fileName, _size);
            _realImage.Display();
        }
    }

    public class ClothingItem
    {
        private string _name;
        private decimal _price;
        private IClothingImage _imageProxy;

        public ClothingItem(string name, decimal price, IClothingImage imageProxy)
        {
            _name = name;
            _price = price;
            _imageProxy = imageProxy;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"San pham: {_name} - Gia: {_price:C}");
        }

        public void DisplayImage()
        {
            _imageProxy.Display();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<ClothingItem> clothingItems =
            [
                new ClothingItem("Ao so mi trang", 29.99m, new ProxyClothingImage("white_shirt.jpg", 1500)),
                new ClothingItem("Quan jeans xanh", 49.99m, new ProxyClothingImage("blue_jeans.jpg", 2000)),
                new ClothingItem("Vay da hoi", 99.99m, new ProxyClothingImage("evening_dress.jpg", 3000))
            ];

            Console.WriteLine("Danh sach san pham:");
            foreach (var item in clothingItems)
            {
                item.DisplayInfo();
            }

            Console.WriteLine("\nNguoi dung yeu cau xem anh chi tiet san pham dau tien:");
            clothingItems[0].DisplayImage();

            Console.WriteLine("\nNguoi dung xem lai anh:");
            clothingItems[0].DisplayImage();
        }
    }
}