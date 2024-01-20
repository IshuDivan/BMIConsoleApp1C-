using System;
using System.Collections.Generic;

namespace ConsoleApp1C_
{
    internal class Program
    {
        public class Measurement
        {
            public string Title { get; set; }
            public Dictionary<string, double> ScalingUnits { get; set; }
        }

        public static Measurement[] Parameters =
        {
            new Measurement
            {
                Title = "height",
                ScalingUnits = new Dictionary<string, double>
                {
                    {"meters", 1},
                    {"centimeters", 0.01},
                    {"inches", 0.0254},
                    {"feet", 0.3048},
                }
            },
            new Measurement
            {
                Title = "weight",
                ScalingUnits = new Dictionary<string, double>
                {
                    {"kilograms", 1},
                    {"grams", 0.001},
                    {"pounds", 0.4536},
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
        static double GetUnit(string Title)
        {
            string ScalingUnits = SetScalingUnits(Title);
            double value = GetMeasurements(Title, ScalingUnits);
            value = NormalizeValue(value, Title, ScalingUnits);
            return value;
        }
        static string SetScalingUnits(string Title)
        {
            int number = 0;
            for (int j = 0;j< Parameters.Length; j++)
            {
                if (Parameters[j].Title == Title)
                {
                    number = j;
                }
            }
            Console.WriteLine($"Enter units of {Parameters[number].Title} in which you are going to operate:");
            int i = 1;
            foreach (KeyValuePair<string, double> entry in Parameters[number].ScalingUnits)
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
                else if (id <= 0.0 | id > Parameters[number].ScalingUnits.Count)
                {
                    Console.WriteLine($"invalid input, only enter one of the numbers on the list. Try again");
                }
                else
                {
                    break;
                }
            }
            i = 1;
            foreach (KeyValuePair<string, double> entry in Parameters[number].ScalingUnits)
            {
                if (i == id)
                {
                    return entry.Key;
                }
                i++;
            }
            return null;
        }
        static double GetMeasurements(string name, string ScalingUnits)
        {
            string input;
            double output;
            while (true)
            {
                Console.WriteLine($"Enter your {name} in {ScalingUnits}:");
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
        private static double NormalizeValue(double value, string title, string scalingUnit)
        {
            return value * Parameters[Array.FindIndex(Parameters, p => p.Title == title)].ScalingUnits[scalingUnit];
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
};

        

