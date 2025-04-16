using System.IO.Pipelines;

namespace Assessment{
    public class Output{
        public static void Main(string[] agrs){
            string[] input = {"wow","ell that en", "hat end", "t ends well", "all is well"};
            string ans = GreedyMerge(input);
            Console.WriteLine($"Merged input file: \"{ans}\""); 
        }

        public static string GreedyMerge(string[] inputFile){
        if (inputFile.Length == 1) {
            return inputFile[0].ToString();
        }else{
            //Find Max Common String First
            HashSet<char>[] charSets = inputFile.Select(s => new HashSet<char>(s)).ToArray();
            int maxCommon = -1;
            int firstIndex = 0;
            int secondIndex = 1;
            for (int i = 0; i < inputFile.Length; i++)
            {
                for (int j = i + 1; j < inputFile.Length; j++)
                {
                    int common = charSets[i].Intersect(charSets[j]).Count();
                    if (common > maxCommon)
                    {
                        maxCommon = common;
                        firstIndex = i;
                        secondIndex = j;
                    }
                }
            }            
            //Merge Two String
            string str1 = inputFile[firstIndex];
            string str2 = inputFile[secondIndex];
            string mergedString = MergeStrings(str1,str2);
            //Remove Origin String From Input
            inputFile = inputFile.Where((source, index) => index != secondIndex).ToArray();
            inputFile = inputFile.Where((source, index) => index != firstIndex).ToArray();
            //Process Until All String Been Merged
            string[] stringArray = new string[]{ mergedString };
            inputFile = stringArray.Concat(inputFile).ToArray();
            return GreedyMerge(inputFile);
        }
        }

        public static string MergeStrings(string a, string b)
            {
                int maxAOverlap = 0;
                int maxBOverlap = 0;
                int maxPossible = Math.Min(a.Length, b.Length);
                for (int k = maxPossible; k >= 1; k--)
                {
                    if (a.Length < k || b.Length < k)
                        continue;
                    
                    string aEnd = a.Substring(a.Length - k);
                    string bEnd = b.Substring(b.Length - k);
                    string aStart = a.Substring(0, k);
                    string bStart = b.Substring(0, k);
                    
                    if (aEnd == bStart)
                    {
                        maxAOverlap = k;
                        break;
                    }
                    if (bEnd == aStart)
                    {
                        maxBOverlap = k;
                        break;
                    }
                }
                if( maxBOverlap != 0 ){
                    return b + a.Substring(maxBOverlap);
                }else{
                    return a + b.Substring(maxAOverlap);
                    }
            }        
        }
}