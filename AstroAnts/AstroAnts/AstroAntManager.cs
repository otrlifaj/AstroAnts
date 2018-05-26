using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAnts
{
    internal class AstroAntManager
    {
        private readonly int firstGroupIndex = 1;
        private readonly int secondGroupIndex = 2;

        public List<AstroAnt> FirstGroup { get; private set; }
        public List<AstroAnt> SecondGroup { get; private set; }
        public List<AstroAnt> OverstepStatus { get; private set; }

        public AstroAntManager()
        {
            FirstGroup = new List<AstroAnt>();
            SecondGroup = new List<AstroAnt>();
            OverstepStatus = new List<AstroAnt>();
        }

        public AstroAntManager(List<AstroAnt> firstGroup, List<AstroAnt> secondGroup) : this()
        {
            FirstGroup = firstGroup;
            SecondGroup = secondGroup;
            OverstepStatus.AddRange(FirstGroup);
            OverstepStatus.AddRange(SecondGroup);
        }

        public AstroAntManager(string[] firstGroup, string[] secondGroup) : this()
        {
            for (int i = 0; i < firstGroup.Length; i++)
            {
                var astroAnt = new AstroAnt(firstGroup[i], firstGroupIndex, firstGroup.Length - i);
                FirstGroup.Add(astroAnt);
            }
            for (int i = 0; i < secondGroup.Length; i++)
            {
                var astroAnt = new AstroAnt(secondGroup[i], secondGroupIndex, i + 1);
                SecondGroup.Add(astroAnt);
            }
            OverstepStatus.AddRange(FirstGroup);
            OverstepStatus.AddRange(SecondGroup);
        }

        public AstroAntManager(int firstGroupSize, int secondGroupSize) : this()
        {
            for (int i = 0; i < firstGroupSize; i++)
            {
                var ant = new AstroAnt("green" + (i + 1), firstGroupIndex, i + 1);
                FirstGroup.Add(ant);
            }
            FirstGroup = FirstGroup.OrderByDescending(a => a.GroupPosition).ToList();

            for (int i = 0; i < secondGroupSize; i++)
            {
                var ant = new AstroAnt("blue" + (i + 1), secondGroupIndex, i + 1);
                SecondGroup.Add(ant);
            }
            SecondGroup = SecondGroup.OrderBy(a => a.GroupPosition).ToList();
            OverstepStatus.AddRange(FirstGroup);
            OverstepStatus.AddRange(SecondGroup);
        }

        public string GetInfo(int stepNumber)
        {
            string status = "Krok " + stepNumber + ":";
            string names = GetAntNames();
            return new StringBuilder().AppendLine(status).AppendLine(names).ToString();
        }

        public string GetInitialInfo()
        {
            string status = "Počáteční stav:";
            string names = GetAntNames();
            return new StringBuilder().AppendLine(status).AppendLine(names).ToString();
        }

        private string GetAntNames()
        {
            var sb = new StringBuilder();
            foreach (var ant in OverstepStatus)
            {
                sb.Append(ant.Name).Append(' ');
            }
            return sb.ToString();
        }

        public bool OverstepIteration()
        {
            int[] firstGroupIndexes = new int[FirstGroup.Count];
            int i = 0;
            foreach (var astroAnt in FirstGroup)
            {
                int index = OverstepStatus.IndexOf(astroAnt);
                firstGroupIndexes[i++] = index;
            }
            List<int> indexesToSwap = new List<int>();
            foreach (int index in firstGroupIndexes)
            {
                if (index + 1 < OverstepStatus.Count && OverstepStatus[index + 1].Group == secondGroupIndex)
                {
                    indexesToSwap.Add(index);
                }
            }

            if (indexesToSwap.Count == 0)
            {
                return true;
            }
            else
            {
                foreach (var index in indexesToSwap)
                {
                    var temp = OverstepStatus[index];
                    OverstepStatus[index] = OverstepStatus[index + 1];
                    OverstepStatus[index + 1] = temp;
                }
            }
            return false;
        }

        public void Overstep(int iterations)
        {
            Console.Write(GetInitialInfo());
            var finalIteration = iterations;
            for (int i = 1; i <= iterations; i++)
            {
                var isFinished = OverstepIteration();
                if (isFinished)
                {
                    finalIteration = i-1;
                    break;
                }
            }
            if (finalIteration < iterations)
            {
                Console.WriteLine(String.Format("Překračování bylo dokončeno před požadovaným krokem {0}. Poslední provedený krok byl:", iterations));
                Console.Write(GetInfo(finalIteration));
            }
            else
            {
                Console.Write(GetInfo(finalIteration));
            }
        }
    }
}
