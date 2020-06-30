using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Word_prefix.Extension;
using Word_prefix.Model;

namespace Word_prefix.Utility
{
class NodeUtility
{

    public static TrieResult BuildTrie(string testString, Node root)
    {

        Node roamer = root, mostCommonWordNode = root;  //firstly roamer and mostCommonWordNode points to root
        int numberOfCapitalizedLetter = 0;
        var charOccurences = new SortedDictionary<char, int>();


       for(var i=0;i<testString.Length;i++){
            var currentNodeChar = testString[i];
            charOccurences.Increment(Char.ToLower(currentNodeChar)); //keep lowercase letter because 'A' and 'a' counted as 'a'
            if (Char.IsUpper(currentNodeChar))  // if upper then it is a new word
            {
                mostCommonWordNode = NodeUtility.UpdateMostCommonWord(roamer, mostCommonWordNode);
                roamer = root; // roamer points to root again cause new word has started
                numberOfCapitalizedLetter++;
            }
            var currentNode = roamer.ChildrenNodes.Find(n => n.DisplayChar == currentNodeChar); // check whether this node already created
            if (currentNode == null)
            {
                currentNode = new Node() { DisplayChar = currentNodeChar, Parent = roamer };
                roamer.ChildrenNodes.Add(currentNode);
            }
            currentNode.NumberOfVisitCount++; // we pass through this node so increase its visit Counter
            roamer = currentNode; 
        }
        mostCommonWordNode = NodeUtility.UpdateMostCommonWord(roamer, mostCommonWordNode); 

        return new TrieResult
        {
            MostCommonWord = mostCommonWordNode,
            Root = root,
            CapitalizedLetterCount = numberOfCapitalizedLetter,
            CharCount = charOccurences
        };
    }
    public static Node CreateMainNode()
    {
        return new Node()
        {
            NumberOfVisitCount = 0,
            EndOfWordCount = -1,
            DisplayChar = '\0'
        };
    }

    /*
    increase roamer endCounter because new word has just started
    if roamer endCounter is higher then already existing mostCommonWord then replace it
     */
    public static void DisplayCharOccurences(SortedDictionary<char, int> charOccurences)
    {
        IDictionaryEnumerator myEnumeretor = charOccurences.GetEnumerator();

        while (myEnumeretor.MoveNext())
        {
            Console.WriteLine($"{myEnumeretor.Value} times {myEnumeretor.Key}");
        }
    }
     public static void DisplayMostCommonPrefix(Node root)
    {
        List<Node> nodes = new List<Node>();
        nodes = root.ChildrenNodes.SelectMany(n => n.ChildrenNodes).ToList(); // second level of three contains 2 charecter prefix
        nodes.Sort();
        _GetMostCommonPrefix(nodes[0], root);

    }
     public static void DisplayMostCommonWord(Node leafNode, Node root)
    {
        var times = leafNode.EndOfWordCount;
        var word = _GetWordFromLeafNode(leafNode, root);
        Console.WriteLine($"'{word}' word is most common word and it is seen {times} times");
    }
    public static void DisplayNumberOfCapitalizedWord(TrieResult TrieResult)
    {
        Console.WriteLine($"there are {TrieResult.CapitalizedLetterCount} capitalized letter");
    }
      public static Node UpdateMostCommonWord(Node roamer, Node mostCommonWordNode)
    {
        roamer.EndOfWordCount++;
        if (roamer.EndOfWordCount > mostCommonWordNode.EndOfWordCount)
        {
            mostCommonWordNode = roamer;
        }
        return mostCommonWordNode;
    }

    private static void _GetMostCommonPrefix(Node leafNode, Node root)
    {
        var times = leafNode.PrefixCount;
        var prefix = _GetWordFromLeafNode(leafNode, root);
        Console.WriteLine($"'{prefix}' prefix is most common prefix and it is seen {times} times");
    }

    private static string _GetWordFromLeafNode(Node leafNode, Node root)
    {
        var wordArray = new List<char>();
        while (leafNode != root)
        {
            wordArray.Add(leafNode.DisplayChar);
            leafNode = leafNode.Parent;
        }
        wordArray.Reverse(); //because of ordering is from a child to its parent so need to reverse the array to get the word
        return string.Concat(wordArray);
    }

   

}
    
}