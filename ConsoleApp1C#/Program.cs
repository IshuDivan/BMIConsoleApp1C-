using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1C_
{
    internal class Program
    {
        public class UnitOfMeasurement
        {
            public string name;
            public double transitionValue;
        }

        public static double belowAverageBMI = 18.5;
        public static double aboveAverageBMI = 24.9;
        public static UnitOfMeasurement[] lengthUnits = {  
            new UnitOfMeasurement { name = "meters", transitionValue = 1 },
            new UnitOfMeasurement { name = "cantimeters", transitionValue = 0.01 },
            new UnitOfMeasurement { name = "inches", transitionValue = 0.0254 } 
        };
        public static UnitOfMeasurement[] weightUnits = {   
            new UnitOfMeasurement { name = "kilograms", transitionValue = 1 },
            new UnitOfMeasurement { name = "grams", transitionValue = 0.001 },
            new UnitOfMeasurement { name = "pounds", transitionValue = 0.0254 } 
        };
        static void Main()
        {
            BMICategory();
        }
        static void BMICategory()
        {
            double weight = getMeasurements("Weight", "kilograms");
            double height = getMeasurements("Height", "cantimeters");

            height = NormaliseHeight(height, "cantimeters");

            double bmi = CalculateBMI(weight, height);
            string bmiCategory = GetBMICategory(bmi);

            Console.WriteLine($"IBM = {bmi:F2} - {bmiCategory}");
            Console.ReadLine();

        }
        static double getMeasurements(string name, string units)
        {
            string input;
            double output;
            while (true)
            {
                Console.WriteLine($"Enter your {name} in {units}:");
                input = Console.ReadLine();
                if (!double.TryParse(input, out output))
                {
                    Console.WriteLine("invalid input. Couldn't find a number in your input. Try again");
                }
                else if (output <= 0.0)
                {
                    Console.WriteLine($"invalid input, {name} can only be measured to be a positive number. Try again");
                }
                else
                {
                    return output;
                }
            }
        }
        static double CalculateBMI(double weight, double height)
        {
            return weight / (height * height);
        }
        static double NormaliseHeight(double value, string fromUnits, string toUnits = "meters")
        {
            for (int i =0; i< lengthUnits.Length;i++)
            {
                if (fromUnits == lengthUnits[i].name & toUnits == "meters")
                {
                    return value * lengthUnits[i].transitionValue;
                }
            }
            return value;
        }

        static string GetBMICategory(double bmi)
        {
            if (bmi < belowAverageBMI)
                return "Below average";
            else if (bmi >= belowAverageBMI && bmi < aboveAverageBMI)
                return "Average";
            else
                return "Above average";
        }
    }
}
