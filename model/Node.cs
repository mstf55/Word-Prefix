using System;
using System.Collections.Generic;
namespace Word_prefix.Model
{
class Node :IComparable<Node>
    {
        public List<Node> ChildrenNodes = new List<Node>();
        public Node Parent { get; set; }
        public int NumberOfVisitCount { get; set; }
        public int EndOfWordCount { get; set; }
        public char DisplayChar { get; set; }

        public int PrefixCount {get {
            return NumberOfVisitCount-EndOfWordCount; // this gives us prefix count because "in" does not contain "in"
        }
        private set{}
        }
        public int CompareTo(Node other)
        {
            return  other.PrefixCount.CompareTo(this.PrefixCount);
        }
    }
}