using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BaseFramework
{
    public class CsvUtil
    {
        public static List<List<string>> LoadCsv(string csvText)
        {
            // split line
            string[] lines = csvText.Split(new char[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);

            if (lines.IsNullOrEmpty())
                return null;

            List<List<string>> result = new List<List<string>>();

            // Parse CVS line. Capture next value in named group: 'val'
            Regex pattern =
                new Regex(@"\s*(?:""(?<val>[^""]*(""""[^""]*)*)""\s*|(?<val>[^,]*))(?:,|$)",
                          RegexOptions.Multiline);
            try
            {
                lines.ForEach((index, line) =>
                              {
                                  MatchCollection matchCollection = pattern.Matches(line);
                                  List<string> words = new List<string>();
                                  for (int i = 0; i < matchCollection.Count; ++i)
                                  {
                                      string word = matchCollection[i].Groups["val"].Value;
                                      words.Add(word);
                                  }

                                  result.Add(words);
                              });
            }
            catch (ArgumentException ex)
            {
                Log.W("CsvUtil", $"config parse error! {ex.Message}");
            }

            return result;
        }
    }
}