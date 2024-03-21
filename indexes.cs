using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_3
{
    internal class indexes
    {
    }
    public struct Element
    {
        public string e;
        public int maxbonds;
        public int bondsformed;
        public int number;
        public int lonepairs;
        public bool element_deleted;
        public int atomsbonded;
        public int x;  //as oppose to moving labels list over as well
        public int y;

    }
    public struct bond
    {
        public int enum1;
        public int enum2;
        public Point epoint1;
        public Point epoint2;
        public int size;
        public bool removed;

    }
}
