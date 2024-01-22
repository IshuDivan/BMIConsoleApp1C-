﻿using System;
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
            public string Title { get; set; }
            public Dictionary<string, Func<double, double>> ScalingUnits { get; set; }
        }

        public static Measurement[] Parameters =
        {
            new Measurement
            {
                Title = "height",
                ScalingUnits = new Dictionary<string, Func<double, double>>
                {
                    {"meters", value => value},
                    {"centimeters", value => value * 0.01},
                    {"inches", value => value * 0.0254},
                    {"feet", value => value * 0.3048},
                }
            },
            new Measurement
            {
                Title = "weight",
                ScalingUnits = new Dictionary<string, Func<double, double>>
                {
                    {"kilograms", value => value},
                    {"grams", value => value * 0.001},
                    {"pounds", value => value * 0.4536},
                }
            },
            new Measurement
            {
                Title = "temperature",
                ScalingUnits = new Dictionary<string, Func<double, double>>
                {
                    {"Celsius", value => value},
                    {"Kelvin", value => value - 273.15},
                    {"Fahrenheit", value => (value - 32) * 5 / 9},
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
            //double temperature = GetUnit("temperature");
            //Console.WriteLine(temperature.ToString());
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
            for (int j = 0; j < Parameters.Length; j++)
            {
                if (Parameters[j].Title == Title)
                {
                    number = j;
                }
            }
            Console.WriteLine($"Enter units of {Parameters[number].Title} in which you are going to operate:");
            int i = 1;
            foreach (KeyValuePair<string, Func<double, double>> entry in Parameters[number].ScalingUnits)
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
            foreach (KeyValuePair<string, Func<double, double>> entry in Parameters[number].ScalingUnits)
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
            return Parameters[Array.FindIndex(Parameters, p => p.Title == title)].ScalingUnits[scalingUnit](value);
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

