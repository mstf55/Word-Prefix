
using System;
using System.IO;
using System.Net.Http;
using Word_prefix.Model;
using Word_prefix.Utility;

namespace Word_prefix.Calculator
{

class WordCalculator
{
    static void DisplayResult(TrieResult trieResult)
    {
        NodeUtility.DisplayCharOccurences(trieResult.CharCount);
        NodeUtility.DisplayNumberOfCapitalizedWord(trieResult);
        NodeUtility.DisplayMostCommonWord(trieResult.MostCommonWord, trieResult.Root);
        NodeUtility.DisplayMostCommonPrefix(trieResult.Root);
    }
    public static async void Run(String fileUrl)
    {
            try 
            {
                // var stream = await new HttpClient().GetStreamAsync(fileUrl);
                // var streamReader = new StreamReader(stream);
                var root = NodeUtility.CreateMainNode();
                var testString="FFnFunnFunyFunFunnniestFundamentalFurtherFunFunnyFurther";
                var trieResult = NodeUtility.BuildTrie(testString, root);
                DisplayResult(trieResult);
            }
            catch(Exception exception){
                Console.WriteLine(exception.Message); //just for now
            }

    }

}
}