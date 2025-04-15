using System.IO.Pipelines;

namespace Assessment{
    public class Output{
        public static void Main(string[] agrs){
            string[] input = {"abcde", "def"};
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
            string mergedString = str1;
            foreach (char c in str2)
            {
                if (!str1.Contains(c))
                {
                    mergedString = mergedString + c;
                }
            }   
            //Remove Origin String From Input
            inputFile = inputFile.Where((source, index) => index != secondIndex).ToArray();
            inputFile = inputFile.Where((source, index) => index != firstIndex).ToArray();
            //Process Until All String Been Merged
            inputFile = inputFile.Concat(new string [] {mergedString}).ToArray();
           //Console.WriteLine($"Within Merged process input file: \"{inputFile.Length}\""); 
            return GreedyMerge(inputFile);
        }
        }
    }

}