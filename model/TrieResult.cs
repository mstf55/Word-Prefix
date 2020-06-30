using System.Collections.Generic;
namespace Word_prefix.Model
{
class TrieResult{
        public Node MostCommonWord{get;set;}

        public Node Root{get;set;}
        public int CapitalizedLetterCount{get;set;}
        public SortedDictionary<char,int> CharCount{get;set;}
    }
}