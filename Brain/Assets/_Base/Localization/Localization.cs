using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using UnityEngine;

namespace BaseFramework {
    public static class Localization {

        // https://stackoverflow.com/questions/8493195/how-can-i-parse-a-csv-string-with-javascript-which-contains-comma-in-data/8497474#8497474
        // Validate a CSV string having single, double or un-quoted values.
        //     ^                                       # Anchor to start of string.
        //     \s*                                     # Allow whitespace before value.
        //     (?:                                     # Group for value alternatives.
        //     '[^'\\]*(?:\\[\S\s][^'\\]*)*'           # Either Single quoted string,
        //     | "[^"\\]*(?:\\[\S\s][^"\\]*)*"         # or Double quoted string,
        //     | [^,'"\s\\]*(?:\s+[^,'"\s\\]+)*        # or Non-comma, non-quote stuff.
        //     )                                       # End group of value alternatives.
        //     \s*                                     # Allow whitespace after value.
        //     (?:                                     # Zero or more additional values
        // ,                                           # Values separated by a comma.
        //     \s*                                     # Allow whitespace before value.
        //     (?:                                     # Group for value alternatives.
        //     '[^'\\]*(?:\\[\S\s][^'\\]*)*'           # Either Single quoted string,
        //     | "[^"\\]*(?:\\[\S\s][^"\\]*)*"         # or Double quoted string,
        //     | [^,'"\s\\]*(?:\s+[^,'"\s\\]+)*        # or Non-comma, non-quote stuff.
        //     )                                       # End group of value alternatives.
        //     \s*                                     # Allow whitespace after value.
        //     )*                                      # Zero or more additional values
        //     $                   

        private static Dictionary<string, int> _languages = new Dictionary<string, int>();
        private static Dictionary<string, string[]> _localizationText = new Dictionary<string, string[]>();

        private const string DEFAULT_LANGUAGE = "en";
        private static string _localLanguage;

        /// <summary>
        /// 传入csv字符串
        /// </summary>
        /// <param name="textConfig"></param>
        public static void Init(string textConfig, string language) {

            // split line
            string[] lines = textConfig.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.IsNullOrEmpty())
                return;

            // Parse CVS line. Capture next value in named group: 'val'
            Regex pattern = new Regex(@"\s*(?:""(?<val>[^""]*(""""[^""]*)*)""\s*|(?<val>[^,]*))(?:,|$)",
                                      RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

            try {
                lines.ForEach((index, line) => {
                    MatchCollection matchCollection = pattern.Matches(line);
                    List<string> words = new List<string>();
                    for (int i = 0; i < matchCollection.Count; ++i) {
                        string word = matchCollection[i].Groups["val"].Value;
                        if (word.Any())
                            words.Add(word);
                    }
                    if (words.Count == 0) {
                        return;
                    }
                    // 首行是语言标题栏
                    if (index == 0) {
                        if (words.Count < 2)
                            throw new ArgumentException("localization language not define!");

                        // 首行第一列不解析
                        for (int i = 1; i < words.Count; ++i) {
                            _languages.Add(words[i].ToLower(), i - 1);
                        }
                    }
                    // 非首行
                    else {
                        string key = words[0];
                        if (!string.IsNullOrEmpty(key)) {
                            if (_localizationText.ContainsKey(key)) {
                                throw new ArgumentException($"duplicate key:{key}");
                            }
                            _localizationText[key] = new string[_languages.Count];
                            for (int i = 1; i < words.Count; ++i) {
                                _localizationText[key][i - 1] = words[i].Trim(new[] { ' ', '\"' }).Replace("\\n", "\n").Replace("##", "");
                            }
                        }
                    }
                });
            } catch (Exception ex) {
                Log.E("Localization", $"config parse error! {ex.Message}");
                throw ex;
            }

            SetLanguage(language);
        }

        public static void SetLanguage(string language)
        {
            if (_languages.ContainsKey(language))
            {
                _localLanguage = language;
            }
            else
            {
                _localLanguage = DEFAULT_LANGUAGE;
            }
        }

        public static string GetText(string key, string language = "", string defaultValue = "") {
            if (string.IsNullOrEmpty(language)) {
                language = _localLanguage;
            }

            if (string.IsNullOrEmpty(language)
                || _localizationText == null
                || string.IsNullOrEmpty(key)
                || !_localizationText.ContainsKey(key))
                return defaultValue;

            int index = _languages.ContainsKey(language) ? _languages[language] : _languages[DEFAULT_LANGUAGE];
            if (index < 0 || index >= _localizationText[key].Length)
                return defaultValue;

            return _localizationText[key][index];
        }
    }
}