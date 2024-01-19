using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1C_
{
    internal class Program
    {
        static void Main()
        {
            BMI();
        }
        static void BMI()
        {
            Console.WriteLine("Enter your weight in kilograms:");
            double weight = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter your height in cantimeters:");
            double height = double.Parse(Console.ReadLine());

            double bmi = CalculateBMI(weight, height);
            string bmiCategory = GetBMICategory(bmi);

            Console.WriteLine($"IBM = {bmi:F2} - {bmiCategory}");
            Console.ReadLine();

        }

        static double CalculateBMI(double weight, double height)
        {
            return weight / (height * height/(100*100));
        }

        static string GetBMICategory(double bmi)
        {
            if (bmi < 18.5)
                return "below average";
            else if (bmi >= 18.5 && bmi < 24.9)
                return "Average";
            else
                return "Above average";
        }
    }
}
