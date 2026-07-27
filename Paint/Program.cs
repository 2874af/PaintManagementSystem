using Paint.Models;

namespace PaintManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            PaintProduct paint1 = new PaintProduct();
            PaintProduct paint2 = new PaintProduct();
            PaintProduct paint3 = new PaintProduct();

            //show all the paint products
            Console.WriteLine("All the Products");
            paint1.DisplayInfo();

            Console.WriteLine();
            paint2.DisplayInfo();

            Console.WriteLine();
            paint2.DisplayInfo();

            Console.WriteLine();
            paint3.DisplayInfo();

            

        }
    }
}
