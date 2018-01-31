using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestForVacancyProject
{

    public abstract class AreaCount
    {
        public abstract double Area();
        public abstract double Perimeter();
    }

    public class TriangleArea : AreaCount
    {

        double a, b, c;
        public TriangleArea(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        ///<summary>
        ///<para> Возвращает периметр треугольника </para>
        ///</summary>
        public override double Perimeter()
        {
            return Math.Round((a + b + c) / 2, 2);
        }

        ///<summary>
        ///<para> Возвращает площадь треугольника </para>
        ///</summary>
        public override double Area()
        {
            double p = Perimeter();
            return Math.Round(Math.Sqrt(p * (p - a) * (p - b) * (p - c)), 2);
        }

    }
    public class RoundArea: AreaCount
    {
        double radius;
        public RoundArea(double r)
        {
            this.radius = r;
        }

        ///<summary>
        ///<para> Возвращает площадь круга.</para>
        ///</summary>
        public override double Area()
        {
            return Math.Round((Math.Pow(Convert.ToDouble(radius), 2)) * 3.1415, 2);
        }

        ///<summary>
        ///<para> Возвращает периметр круга.</para>
        ///</summary>
        public override double Perimeter()
        {
            return Math.Round(radius * 2 * 3.1415, 2);
        }
    }

    public class AnyArea:AreaCount
    {
        Point[] dots;

        public  AnyArea(Point[] dots)
        {
            this.dots = dots;
        }

        ///<summary>
        ///<para> Возвращает периметр произвольной фигуры.</para>
        ///</summary>
        public override double Perimeter()
        {
            double per = default(double);

            for (int i=0; i< dots.Length-1; i++)
            {
                per += Math.Sqrt(Math.Pow(dots[i + 1].X - dots[i].X, 2) + Math.Pow(dots[i + 1].Y - dots[i].Y, 2));
            }
            return Math.Round(per, 2);
        }

        ///<summary>
        ///<para> Возвращает площадь произвольной фигуры.</para>
        ///</summary>
        public override double Area()
        {
            double area = default(double);

            for (int i = 0; i < dots.Length - 1; i++)
            {
                area += dots[i].X * dots[i + 1].Y - dots[i + 1].X * dots[i].Y;
            }
            //1*3 - 2*1=1
            //2*4 - 5*3=-7
            //5*2 - 4*4=-6
            //4*1 - 1*2 = 2
            area += dots[dots.Length-1].X * dots[0].Y - dots[dots.Length-1].Y * dots[0].X;

            return Math.Round(Math.Abs(area / 2), 2);
        }
    }
}
