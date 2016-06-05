using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringGenerator
{
    class StringGenerator
    {
        StreamWriter file;
        Regex regexPattern;
        List<Entry> Options;
        MatchCollection matches;
        List<string> generatedStrings;

        public StringGenerator(string Filename,string RegexPattern)
        {
            file = new System.IO.StreamWriter(Filename, true);
            regexPattern = new Regex(RegexPattern);
            Options = new List<Entry>();
            generatedStrings = new List<string>();
        }

        public void ProcessString(string value)
        {
            matches = regexPattern.Matches(value);
             
            for (Byte index = 0; index < matches.Count; ++index)
            {
                string[] splits = matches[index].Groups[1].Value.Split('|');
                Entry temp = new Entry(matches[index].Value, splits);
                Options.Add(temp);
            }
            if (Options.Count > 0)
            {
                GenerateString(value, Options, 0);
            }
            else
            {
                generatedStrings.Add(value);
            }
            Options.Clear();
        }

        public List<string> GetGeneratedStrings()
        {
            return generatedStrings;
        }

        public void ClearGeneratedStrings()
        {
            generatedStrings.Clear();
        }

        public void WriteToFile()
        {
            foreach(string value in generatedStrings)
            {
                file.WriteLine(value);
                file.Flush();
            }

        }
        
        private void GenerateString(string String, List<Entry> Options, Byte CurrentIndex)
        {
            for (Byte index = 0; index < Options[CurrentIndex].entries.Length; ++index)
            {
                // Check if the next step is at the bottom
                string Value = String.Replace(Options[CurrentIndex].match, Options[CurrentIndex].entries[index]);
                if (CurrentIndex + 1 >= Options.Count)
                {
                    generatedStrings.Add(Value);
                    continue;
                }
                else {
                    GenerateString(Value, Options, ++CurrentIndex);
                    --CurrentIndex;
                }
            }
        }
    }
}
