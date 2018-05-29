using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAnts
{
    class Program
    {
        private const string defaultAction = "";
        private const string numbersAction = "pocty";

        static void Main(string[] args)
        {

            var action = (args.Length == 0) ? "" : args[0];
            switch (action)
            {
                case defaultAction:
                    PrintDefaultActionMenu();
                    break;
                case numbersAction:
                    PrintNumbersActionMenu();
                    break;
                default:
                    break;
            }
        }

        private static void PrintDefaultActionMenu()
        {
            Console.Write("Zadej jména astroantů první kolonie oddělené mezerou: ");
            var firstGroupString = Console.ReadLine();
            Console.Write("Zadej jména astroantů druhé kolonie oddělené mezerou: ");
            var secondGroupString = Console.ReadLine();
            Console.Write("Zadej počet kroků: ");
            var stepsCountString = Console.ReadLine();

            var firstGroup = firstGroupString.Split(' ');
            var secondGroup = secondGroupString.Split(' ');
            var countParsed = int.TryParse(stepsCountString, out int countOfSteps);

            if (countParsed)
            {
                var astroAntManager = new AstroAntManager(firstGroup, secondGroup);
                astroAntManager.Overstep(countOfSteps);
            }
            else
            {
                Console.WriteLine("Počet kroků byl zadán ve špatném formátu");
            }

            Console.ReadLine();
        }

        private static void PrintNumbersActionMenu()
        {
            Console.Write("Zadej počet astroantů první kolonie: ");
            var firstGroupSizeString = Console.ReadLine();
            Console.Write("Zadej počet astroantů druhé kolonie: ");
            var secondGroupSizeString = Console.ReadLine();
            Console.Write("Zadej počet kroků: ");
            var stepsCountString = Console.ReadLine();

            var firstGroupParsed = int.TryParse(firstGroupSizeString, out int firstGroupSize);
            var secondGroupParsed = int.TryParse(secondGroupSizeString, out int secondGroupSize);
            var countParsed = int.TryParse(stepsCountString, out int countOfSteps);

            if (firstGroupParsed && secondGroupParsed && countParsed)
            {
                var astroAntManager = new AstroAntManager(firstGroupSize, secondGroupSize);
                astroAntManager.Overstep(countOfSteps);
            }
            else
            {
                Console.WriteLine("Byly zadány vstupy v nesprávném formátu.");
            }

            Console.ReadLine();
        }
    }
}
