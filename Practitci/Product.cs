using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practitci
{
    internal class Text
    {
        public string input;
        public int Countwords;
        public string ShortestWord;
        public string LargestWors;
        public int CountPred;
        public int CountGlas;
        public int CountSoglas;
        public Dictionary<Char, int> chast = new Dictionary<char, int>();
        public Text (string input)
        {
            
            this.input = input;

            string[] b = input.Split(' ');
            int countwords = b.Length;
            Countwords = countwords;

            string[] c = input.Split(new[] { ' ', ',', '.', '!', '?', ';' });
            string shortestword = c[0];
            foreach (string word in c)
            {
                if (word.Length < shortestword.Length)
                {
                    shortestword = word;
                }
            }
            ShortestWord = shortestword;    

            string largestword = c[0];
            foreach (string word in c)
            {
                if (word.Length > largestword.Length)
                {
                    largestword = word;
                }
            }
            string LargestWord = largestword;
            LargestWord = largestword;

            string[] d = input.Split(new[] { '.', '!', '?' });
            int count = 0; 
            foreach (string pred in d)
            {
                if(pred.Trim() != " ")
                    count++;
            }
            CountPred = count;

            string glas = "аеиоуыэюя";
            int glass = 0;
            int soglas = 0;
            foreach(char bukv in input.ToLower()){
                if (char.IsLetter(bukv))
                {
                    if (glas.Contains(bukv))
                    {
                        glass++;
                    }
                    else
                    {
                        soglas++;
                    }
                    if (chast.ContainsKey(bukv))
                    {
                        chast[bukv]++;
                    }
                    else
                    {
                        chast[bukv] = 1;
                    }
                }

            }





        }


    }
}
