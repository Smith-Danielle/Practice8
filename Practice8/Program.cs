using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace Practice8
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.WriteLine(HasSubpattern("abbaabbaabba"));
        }
        /*
         */
        public static bool HasSubpattern(string str)
        {
            if (str.Length > 1)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    string checker = str.Substring(0, i + 1);
                    string remainder = str.Substring(i + 1);
                    StringBuilder sb = new StringBuilder();
                    sb.Append('*', checker.Length);
                    var temp = sb + remainder;
                    var nextIndex = temp.IndexOf(checker);
                    if (nextIndex != -1)
                    {
                        checker = str.Substring(0, nextIndex);
                        remainder = str.Substring(nextIndex);
                    }
                    else
                    {
                        return false;
                    }
                    
                    string replace = remainder.Replace(checker, "");

                    if (!replace.Any())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static int[] ThreeAmigos(int[] numbers)
        {
            List<int> lowest = new List<int>();
            int? range = null;
            int start = 0;
            int end = 2;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (end <= numbers.Length - 1)
                {
                    List<int> tempLowest = new List<int> { numbers[start], numbers[start + 1], numbers[start + 2] };
                    if (tempLowest.Where(x => x % 2 == 0).Count() == 0 || tempLowest.Where(x => x % 2 == 0).Count() == 3)
                    {
                        int tempRange = tempLowest.Max() - tempLowest.Min();
                        if (range == null || tempRange < range)
                        {
                            range = tempRange;
                            lowest = tempLowest;
                        }
                    }
                    if (range == 0)
                    {
                        break;
                    }
                    start++;
                    end++;
                }
                else
                {
                    break;
                }
            }
            return lowest.Any() ? lowest.ToArray() : new int[0];
        }
        public static int solveExpression(string expression)
        {
            int missingDigit = -1;

            string op = "";
            List<string> nums = new List<string>();

            string temp = "";
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '+' || expression[i] == '-' || expression[i] == '*')
                {
                    if (expression[i] == '-' && temp.Length == 0)
                    {
                        temp += expression[i];
                    }
                    else if (op.Length == 0 && temp.Length > 0)
                    {
                        op += expression[i];
                        nums.Add(temp);
                        temp = "";
                    }
                }
                if (expression[i] == '=')
                {
                    nums.Add(temp);
                    temp = "";
                }
                if (expression[i] == '?' || char.IsNumber(expression[i]))
                {
                    temp += expression[i];
                }
            }
            if (temp.Length > 0)
            {
                nums.Add(temp);
            }

            for (int i = 0; i < 10; i++)
            {
                var unique = string.Join("", nums).Distinct().Where(x => char.IsNumber(x)).Select(x => Convert.ToInt32(x) - 48);
                if (unique.Contains(i))
                {
                    continue;
                }
                
                var sub = nums.Select(x => string.Join("", x.Select(y => y == '?' ? $"{i}" : y.ToString()))).ToList();
                var toNumbers = sub.Select(x => Convert.ToInt32(x)).ToList();
                
                if (i == 0)
                {
                    if (sub[0].Length > 1)
                    {
                        if (sub[0][0] == '0' || sub[0].Contains("-0"))
                        {
                            continue;
                        }
                    }
                    if (sub[1].Length > 1)
                    {
                        if (sub[1][0] == '0' || sub[1].Contains("-0"))
                        {
                            continue;
                        }
                    }
                    if (sub[2].Length > 1)
                    {
                        if (sub[2][0] == '0' || sub[2].Contains("-0"))
                        {
                            continue;
                        }
                    }
                }
                
                int answer = 0;
                if (op == "+")
                {
                    answer = toNumbers[0] + toNumbers[1];
                }
                if (op == "-")
                {
                    answer = toNumbers[0] - toNumbers[1];
                }
                if (op == "*")
                {
                    answer = toNumbers[0] * toNumbers[1];
                }
                if (answer == toNumbers[2])
                {
                    missingDigit = i;
                    break;
                }
            }

            return missingDigit;
        }
        public static string EncodeRails(string s, int n)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }

            string[] rails = new string[n];

            int current = 0;
            bool plus = true;
            for (int i = 0; i < s.Length; i++)
            {
                
                for (int j = 0; j < rails.Length; j++)
                {
                    if (j == current)
                    {
                        rails[j] += $"{s[i]}*";
                    }
                    else
                    {
                        rails[j] += "**";
                    }
                }

                if (current == 0)
                {
                    plus = true;
                }
                if (current == n - 1)
                {
                    plus = false;
                }
                if (plus)
                {
                    current++;
                }
                else
                {
                    current--;
                }
            }

            return string.Join("", rails.Select(x => string.Join("", x.Where(y => y != '*'))));
        }

        public static string DecodeRails(string s, int n)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }

            string[] rails = new string[n];

            int current = 0;
            bool plus = true;
            for (int i = 0; i < s.Length; i++)
            {

                for (int j = 0; j < rails.Length; j++)
                {
                    if (j == current)
                    {
                        rails[j] += $"~*";
                    }
                    else
                    {
                        rails[j] += "**";
                    }
                }

                if (current == 0)
                {
                    plus = true;
                }
                if (current == n - 1)
                {
                    plus = false;
                }
                if (plus)
                {
                    current++;
                }
                else
                {
                    current--;
                }
            }

            int sIndex = 0;
            for (int i = 0; i < rails.Length; i++)
            {
                while (rails[i].Contains('~'))
                {
                    int first = rails[i].IndexOf('~');
                    string temp = rails[i].Remove(first, 1);
                    temp = temp.Insert(first, s[sIndex].ToString());
                    rails[i] = temp;
                    sIndex++;
                }
            }

            var cleanRails = rails.Select(x => string.Join("", x.Where(y => y != '*'))).ToArray();

            int place = 0;
            bool add = true;
            string decoded = "";
            for (int i = 0; i < s.Length; i++)
            {
                decoded += cleanRails[place][0];
                cleanRails[place] = cleanRails[place].Remove(0, 1);

                if (place == 0)
                {
                    add = true;
                }
                if (place == n - 1)
                {
                    add = false;
                }
                if (add)
                {
                    place++;
                }
                else
                {
                    place--;
                }
            }

            return decoded;
        }
        public static int[][] FlapRotors(string[] linesBefore, string[] linesAfter)
        {
            string flaps = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ?!@#&()|<>.:=-+*/0123456789";

            List<List<int>> rotors = new List<List<int>>();

            for (int i = 0; i < linesBefore.Length; i++)
            {
                int place = 0;
                List<int> temp = new List<int>();
                for (int j = 0; j < linesBefore[i].Length; j++)
                {
                    int indexDiff = flaps.IndexOf(linesBefore[i][j]) - flaps.IndexOf(linesAfter[i][j]) + place;
                    while (indexDiff > 0)
                    {
                        indexDiff -= flaps.Length;
                    }
                    temp.Add(Math.Abs(indexDiff));
                    place += Math.Abs(indexDiff);
                }
                rotors.Add(temp);
            }
            return rotors.Select(x => x.ToArray()).ToArray();
        }
        public static string[] FlapDisplay(String[] lines, int[][] rotors)
        {
            string flaps = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ?!@#&()|<>.:=-+*/0123456789";

            List<string> display = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                int place = 0;
                string temp = "";
                for (int j = 0; j < lines[i].Length; j++)
                {
                    int index = flaps.IndexOf(lines[i][j]) + rotors[i][j] + place;
                    while (index > flaps.Length - 1)
                    {
                        index -= flaps.Length;
                    }
                    temp += flaps[index];
                    place += rotors[i][j];
                }
                display.Add(temp);
            }

            return display.ToArray();
        }
        public static (int, int)[] TwosDifference(int[] array)
        {
            List<(int, int)> dif = new List<(int, int)>();
            Array.Sort(array);
            for (int i = 0; i < array.Length; i++)
            {
                int mover = array[i];
                List<int> nums = array.ToList();
                nums.RemoveAt(i);

                for (int j = 0; j < nums.Count; j++)
                {
                    if (Math.Abs(mover - nums[j]) == 2)
                    {
                        if (!dif.Contains((mover, nums[j])) && !dif.Contains((nums[j], mover)))
                        {
                            dif.Add((mover, nums[j]));
                        }
                    }
                }
            }
            return dif.ToArray();
        }
        public static bool IsBalanced(string s, string caps)
        {
            if (caps.Any())
            {
                string next = "";
                for (int i = 0; i < s.Length; i++)
                {
                    if (caps.Contains(s[i]))
                    {
                        if (next.Length == 0)
                        {
                            if ("}])".Contains(s[i]))
                            {
                                return false;
                            }
                            if (s[i] == '{')
                            {
                                next += '}';
                            }
                            else if (s[i] == '(')
                            {
                                next += ')';
                            }
                            else if (s[i] == '[')
                            {
                                next += ']';
                            }
                            else
                            {
                                next += s[i];
                            }
                        }
                        else
                        {
                            if (s[i] == next.Last())
                            {
                                next = next.Remove(next.Length - 1);
                            }
                            else if ("}])".Contains(s[i]))
                            {
                                return false;
                            }
                            else
                            {
                                if (s[i] == '{')
                                {
                                    next += '}';
                                }
                                else if (s[i] == '(')
                                {
                                    next += ')';
                                }
                                else if (s[i] == '[')
                                {
                                    next += ']';
                                }
                                else
                                {
                                    next += s[i];
                                }
                            }
                        }
                    }
                }
                if (next.Any())
                {
                    return false;
                }
            }
            return true;
        }
        public static string EncodeRotation(int n, string s)
        {
            string str = s;
            for (int i = 0; i < n; i++)
            {
                int index = 0;
                var spaces = str.Select(x => index++ < str.Length && x == ' ' ? $"{index - 1}" : "").Where(x => x != "").Select(x => Convert.ToInt32(x)).ToList();
                string strip = str.Replace(" ", "");
                int shiftStart = 0;
                if (strip.Length > n)
                {
                    shiftStart = strip.Length - n;
                }
                else
                {
                    int tempShift = n;
                    while (tempShift > strip.Length)
                    {
                        tempShift -= strip.Length;
                    }
                    shiftStart = strip.Length - tempShift;
                }
                strip = strip.Substring(shiftStart) + strip.Substring(0, shiftStart);
                for (int k = 0; k < spaces.Count; k++)
                {
                    strip = strip.Insert(spaces[k], " ");
                }
                var words = strip.Split(" ");
                List<string> shiftedWords = new List<string>();
                for (int j = 0; j < words.Length; j++)
                {
                    string tempWord = words[j];
                    int wordShiftStart = 0;
                    if (tempWord.Length > n)
                    {
                        wordShiftStart = tempWord.Length - n;
                    }
                    else
                    {
                        int tempShift = n;
                        while (tempShift > tempWord.Length)
                        {
                            tempShift -= tempWord.Length;
                        }
                        wordShiftStart = tempWord.Length - tempShift;
                    }
                    tempWord = tempWord.Substring(wordShiftStart) + tempWord.Substring(0, wordShiftStart);
                    shiftedWords.Add(tempWord);
                }
                str = string.Join(" ", shiftedWords);
            }
            return $"{n} {str}";
        }
        public static string DecodeRotation(string s)
        {
            string sDecode = s;
            string num = "";
            var temp = sDecode[0]; 
            while (char.IsNumber(temp))
            {
                num += temp;
                sDecode = sDecode.Substring(1);
                temp = sDecode[0];
            }
            sDecode = sDecode.Trim();
            int n = Convert.ToInt32(num);

            string strip = "";
            for (int i = 0; i < n; i++)
            {
                var words = sDecode.Split(" ");
                List<string> shiftedWords = new List<string>();
                for (int j = 0; j < words.Length; j++)
                {
                    string tempWord = words[j];
                    int wordShiftStart = 0;
                    if (tempWord.Length > n)
                    {
                        wordShiftStart = n;
                    }
                    else
                    {
                        int tempShift = n;
                        while (tempShift > tempWord.Length)
                        {
                            tempShift -= tempWord.Length;
                        }
                        wordShiftStart = tempShift;
                    }
                    var removed = tempWord.Substring(wordShiftStart);
                    tempWord = tempWord.Remove(wordShiftStart).Insert(0, removed);
                    shiftedWords.Add(tempWord);
                }
                string sTemp = string.Join(" ", shiftedWords);

                int index = 0;
                var spaces = sTemp.Select(x => index++ < sTemp.Length && x == ' ' ? $"{index - 1}" : "").Where(x => x != "").Select(x => Convert.ToInt32(x)).ToList();
                strip = sTemp.Replace(" ", "");
                int shiftStart = 0;
                if (strip.Length > n)
                {
                    shiftStart = n;
                }
                else
                {
                    int tempShift = n;
                    while (tempShift > strip.Length)
                    {
                        tempShift -= strip.Length;
                    }
                    shiftStart = tempShift;
                }
                var removeStrip = strip.Substring(shiftStart);
                strip = strip.Remove(shiftStart).Insert(0, removeStrip);

                for (int k = 0; k < spaces.Count; k++)
                {
                    strip = strip.Insert(spaces[k], " ");
                }
            }
            return strip;
        }
        
    }
}