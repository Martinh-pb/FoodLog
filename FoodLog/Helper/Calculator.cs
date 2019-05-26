using System;
namespace FoodLog.Helper
{
    public enum Macro
    {
        Carbs,
        Protein,
        Fat
    }

    public static class Calculator
    {
        public static double CalculatePercentage(Macro macro, double carbs, double fat, double protein)
        {
            double total = carbs + fat + protein;

            if (Math.Abs(total) < 1)
                return 0;

            double toCalc = 0;
            switch (macro)
            {
                case Macro.Carbs:
                    toCalc = carbs;
                    break;
                case Macro.Fat:
                    toCalc = fat;
                    break;
                case Macro.Protein:
                    toCalc = protein;
                    break;
            }

            return (toCalc / total) * 100;
        }

        public static string GetRoundedValue(double value, string macro, bool inPercentage)
        {
            string ret = $"{macro}: {Math.Round(value, 1, MidpointRounding.ToEven):N2}";

            if (inPercentage)
                ret += "%";

            return ret;
        }
    }
}
