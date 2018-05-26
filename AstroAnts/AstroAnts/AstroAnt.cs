using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAnts
{
    internal class AstroAnt
    {
        public string Name { get; private set; }

        public int Group { get; private set; }

        public int GroupPosition { get; private set; }

        public AstroAnt(string name, int group, int groupPosition)
        {
            Name = name;
            Group = group;
            GroupPosition = groupPosition;
        }
    }
}
