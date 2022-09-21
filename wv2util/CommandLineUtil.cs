﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wv2util
{
    namespace CommandLineUtil
    {
        public class CommandLine
        {
            public CommandLine(string commandLine)
            {
                m_parts = ParseCommandLine(commandLine);
            }

            public string GetKeyValue(string key)
            {
                return GetKeyValue(m_parts, key);
            }

            public string[] Parts {  get => m_parts.ToArray(); }

            private readonly List<string> m_parts;

            private static string GetKeyValue(List<string> all, string key)
            {
                foreach (string entry in all)
                {
                    if (entry.StartsWith(key + "="))
                    {
                        return entry.Substring(key.Length + 1);
                    }
                }
                return null;
            }

            private static List<string> ParseCommandLine(string commandLine)
            {
                List<string> parts = new List<string>();
                bool inQuote = false;
                string part = "";

                for (int curIdx = 0; curIdx < commandLine.Length; ++curIdx)
                {
                    char curChar = commandLine[curIdx];
                    if (!inQuote && Char.IsWhiteSpace(curChar))
                    {
                        if (part.Length > 0)
                        {
                            parts.Add(part);
                            part = "";
                        }
                    }
                    else if (!inQuote && curChar == '"')
                    {
                        inQuote = true;
                    }
                    else if (inQuote && curChar == '"')
                    {
                        inQuote = false;
                    }
                    else
                    {
                        part += curChar;
                    }
                }

                if (part.Length > 0)
                {
                    parts.Add(part);
                }

                return parts;
            }
        }
    }
}
