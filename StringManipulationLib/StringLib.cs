using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringManipulationLib
{
    public class StringLib
    {
        public static List<string> GetStringsSeparatedBy(char Separator, string word)
        {
            if (string.IsNullOrEmpty(word))
                return new List<string>();

            List<int> SeparatorIndexes = FindSeparatorIndexes(Separator, word); ;

            return ExtractSubstrings(word, SeparatorIndexes);
        }

        private static List<string> ExtractSubstrings(string word, List<int> SeparatorIndexes)
        {
            List<string> Result = new List<string>();
            for (int i = 0; i < SeparatorIndexes.Count - 1; i++)
            {
                Result.Add(
                    word.Substring(SeparatorIndexes[i] + 1, SeparatorIndexes[i + 1] - SeparatorIndexes[i] - 1)
                );
            }

            Result.RemoveAll(s => s == "");

            return Result;
        }

        private static List<int> FindSeparatorIndexes(char Separator, string word)
        {
            List<int> Result= new List<int>();
            
            if (word[0] != Separator)
            {
                Result.Add(-1);
            }

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == Separator)
                    Result.Add(i);
            }

            if (word.Last() != Separator)
            {
                Result.Add(word.Length);
            }

            return Result;
        }
    }
}
