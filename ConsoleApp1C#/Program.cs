using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1C_
{
    internal class Program
    {
        public class Measurement
        {
            public string title;
            public Dictionary<string, double> scalingUnits;
        }
        public static Measurement[] Parameters =
        {
            new Measurement
            {
                title = "height",
                scalingUnits = new Dictionary<string, double>()
                {
                    { "meters" , 1},
                    { "cantimeters" , 0.01},
                    { "inches" , 0.0254},
                    { "feet" , 0.31},

                }

            },
            new Measurement
            {
                title = "weight",
                scalingUnits = new Dictionary<string, double>()
                {
                    { "kilograms" , 1},
                    { "grams" , 0.001},
                    { "pounds" , 0.4536},

                }

            }
        };
        public static double belowAverageBMI = 18.5;
        public static double aboveAverageBMI = 25;
        static void Main()
        {
            BMICategory();
        }
        static void BMICategory()
        {
            double height = GetUnit("height");
            double weight = GetUnit("weight");
            double bmi = CalculateBMI(weight, height);
            string bmiCategory = GetBMICategory(bmi);

            Console.WriteLine($"IBM = {bmi:F3} - {bmiCategory}");
            Console.ReadLine();

        }
        static double GetUnit(string title)
        {
            string scalingUnits = SetscalingUnits(title);
            double value = GetMeasurements(title, scalingUnits);
            value = NormaliseValue(value, title, scalingUnits);
            return value;
        }
        static string SetscalingUnits(string title)
        {
            int number = 0;
            for (int j = 0;j< Parameters.Length; j++)
            {
                if (Parameters[j].title == title)
                {
                    number = j;
                }
            }
            Console.WriteLine($"Enter units of {Parameters[number].title} in which you are going to operate:");
            int i = 1;
            foreach (KeyValuePair<string, double> entry in Parameters[number].scalingUnits)
            {
                Console.WriteLine($"To use {entry.Key} type {i}");
                i++;
            }
            int id;
            while (true)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out id))
                {
                    Console.WriteLine("invalid input. Couldn't find a number in your input. Try again");
                }
                else if (id <= 0.0 | id > Parameters[number].scalingUnits.Count)
                {
                    Console.WriteLine($"invalid input, only enter one of the numbers on the list. Try again");
                }
                else
                {
                    break;
                }
            }
            i = 1;
            foreach (KeyValuePair<string, double> entry in Parameters[number].scalingUnits)
            {
                if (i == id)
                {
                    return entry.Key;
                }
                i++;
            }
            return null;
        }
        static double GetMeasurements(string name, string scalingUnits)
        {
            string input;
            double output;
            while (true)
            {
                Console.WriteLine($"Enter your {name} in {scalingUnits}:");
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
        static double NormaliseValue(double value, string valueTitle, string fromScalingUnitsUnits)
        {
            int number = 0;
            for (int j = 0; j < Parameters.Length; j++)
            {
                if (Parameters[j].title == valueTitle)
                {
                    number = j;
                }
            }
            return value * Parameters[number].scalingUnits[fromScalingUnitsUnits];

        }
        static double CalculateBMI(double weight, double height)
        {
            return weight / (height * height);
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
