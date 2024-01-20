using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1C_
{
    internal class Program
    {
        public class measurement
        {
            public string title;
            public Dictionary<string, double> units;
        }
        public static measurement[] parameters =
        {
            new measurement
            {
                title = "height",
                units = new Dictionary<string, double>()
                {
                    { "meters" , 1},
                    { "cantimeters" , 0.01},
                    { "inches" , 0.0254},
                    { "feet" , 0.31},

                }

            },
            new measurement
            {
                title = "weight",
                units = new Dictionary<string, double>()
                {
                    { "kilograms" , 1},
                    { "grams" , 0.001},
                    { "pounds" , 0.4536},

                }

            }
        };
        public static double belowAverageBMI = 18.5;
        public static double aboveAverageBMI = 24.9;
        static void Main()
        {
            BMICategory();
        }
        static void BMICategory()
        {
            double height = getUnit("height");
            double weight = getUnit("weight");
            double bmi = CalculateBMI(weight, height);
            string bmiCategory = GetBMICategory(bmi);

            Console.WriteLine($"IBM = {bmi:F2} - {bmiCategory}");
            Console.ReadLine();

        }
        static double getUnit(string title)
        {
            string units = setunits(title);
            double value = getMeasurements(title, units);
            value = NormaliseValue(value, title, units);
            return value;
        }
        static string setunits(string title)
        {
            int number = 0;
            for (int j = 0;j< parameters.Length; j++)
            {
                if (parameters[j].title == title)
                {
                    number = j;
                }
            }
            Console.WriteLine($"Enter units of {parameters[number].title} in which you are going to operate:");
            int i = 1;
            foreach (KeyValuePair<string, double> entry in parameters[number].units)
            {
                Console.WriteLine($"To use {entry.Key} type {i}");
                i++;
            }
            int id = int.Parse(Console.ReadLine());
            i = 1;
            foreach (KeyValuePair<string, double> entry in parameters[number].units)
            {
                if (i == id)
                {
                    return entry.Key;
                }
                i++;
            }
            return null;
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
        static double NormaliseValue(double value, string valueTitle, string fromUnits)
        {
            int number = 0;
            for (int j = 0; j < parameters.Length; j++)
            {
                if (parameters[j].title == valueTitle)
                {
                    number = j;
                }
            }
            return value * parameters[number].units[fromUnits];

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
