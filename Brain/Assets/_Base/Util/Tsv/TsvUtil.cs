using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BaseFramework
{
    public class TsvUtil
    {
        public static List<List<string>> LoadTsv(string csvText)
        {
            // split line
            string[] lines = csvText.Split(new char[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);

            if (lines.IsNullOrEmpty())
                return null;

            List<List<string>> result = new List<List<string>>();

            try
            {
                lines.ForEach((index, line) =>
                              {
                                  List<string> sections = new List<string>(line.Split('\t'));

                                  result.Add(sections);
                              });
            }
            catch (ArgumentException ex)
            {
                Log.W("TsvUtil", $"config parse error! {ex.Message}");
            }

            return result;
        }
    }
}