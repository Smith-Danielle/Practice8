using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Practice8
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.WriteLine(RepeatAdjacent("ccccoodeffffiiighhhhhhhhhhttttttts"));
        }
        /*
         */
        public static int RepeatAdjacent(string s)
        {
            int letterCount = 0;
            string letter = "";
            int lastLetterCount = 0;
            int tempGroup = 0;
            int group = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (letter == s[i].ToString())
                {
                    letterCount++;
                }
                else
                {
                    if (letter.Length > 0)
                    {
                        if (letterCount > 1)
                        {
                            tempGroup++;
                        }
                        else
                        {
                            if (tempGroup > 1)
                            {
                                group++;
                            }
                            tempGroup = 0;
                        }
                        lastLetterCount = letterCount;
                    }
                    letter = s[i].ToString();
                    letterCount = 1;
                }
            }
            if (letterCount > 1)
            {
                tempGroup++;
            }
            if (tempGroup > 1)
            {
                group++;
            }
            return group;

        }
        public static string HackMyTerminal(int passLength, string machineCode)
        {
            if (string.IsNullOrEmpty(machineCode) || passLength == 0)
            {
                return null;
            }
            var trim = string.Join("", machineCode.Select(x => char.IsLetter(x) ? x : ' ')).Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Where(x => x.Length == passLength).ToList();
            if (trim.Count() == 1)
            {
                return trim.First();
            }
            StringBuilder sb = new StringBuilder();
            sb.Append('T', trim.Count);
            for (int i = 0; i < passLength; i++)
            {
                var placeCheck = trim.Select(x => x[i]).ToList();
                if (placeCheck.Distinct().Count() != placeCheck.Count)
                {
                    var counts = placeCheck.Select(x => new { Letter = x, Amount = placeCheck.Where(y => y == x).Count() }).Where(x => x.Amount != 1).ToList();
                    for (int j = 0; j < counts.Count; j++)
                    {
                        int removeIndex = placeCheck.IndexOf(counts[j].Letter);
                        sb[removeIndex] = 'F';
                        placeCheck[removeIndex] = ' ';
                    }
                }
            }
            return trim[sb.ToString().IndexOf("T")];
        }
        public static int missing(string s)
        {
            for (int i = 0; i < Math.Floor((double)s.Length / 2); i++)
            {
                if (i > 8)
                {
                    return -1;
                }
                int currentNum = Convert.ToInt32(s.Substring(0, i + 1));
                string remaining = s.Substring(i + 1);
                int missing = -1;
                bool isValid = true;
                while (isValid && remaining.Length > 0)
                {
                    if (remaining.IndexOf((currentNum + 1).ToString()) == 0)
                    {
                        currentNum++;
                        remaining = remaining.Substring((currentNum).ToString().Length);
                    }
                    else if (remaining.IndexOf((currentNum + 2).ToString()) == 0 && missing == -1)
                    {
                        missing = currentNum + 1;
                        currentNum += 2;
                        remaining = remaining.Substring((currentNum).ToString().Length);
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                if (isValid)
                {
                    return missing;
                }
            }
            return -1;
        }
        public static bool IsMadhavArray(int[] a)
        {
            if (a.Length <= 1)
            {
                return false;
            }
            List<List<int>> groups = new List<List<int>>();
            int amount = 1;
            int startPoint = 0;
            while (startPoint < a.Length)
            {
                try
                {
                    groups.Add(a.ToList().GetRange(startPoint, amount));
                }
                catch
                {
                    return false;
                }
                amount++;
                startPoint += groups.Last().Count();
            }
            var sums = groups.Select(x => x.Sum());
            if (sums.All(x => x == sums.First()))
            {
                return true;
            }
            return false;
        }
        public static bool IsPrime(int number)
        {
            if (number <= -1)
            {
                number = number * -1;
            }
            if (number == 0 || number == 1)
            {
                return true;
            }
            if (number == 2)
            {
                return true;
            }
            var primeCheck = Enumerable.Range(2, number - 2).Where(x => number % x == 0);
            if (primeCheck.Any())
            {
                return false;
            }
            return true;
        }
        public static string Encode1(int n, string s)
        {
            string current = s;
            for (int z = 0; z < n; z++)
            {
                int index = 0;
                var spaces = current.Select(x => index++ < current.Length && x == ' ' ? $"{index - 1}" : "").Where(x => x != "").Select(x => Convert.ToInt32(x)).ToList();
                string noSpace = current.Replace(" ", "");
                for (int i = 0; i < n; i++)
                {
                    noSpace = noSpace[noSpace.Length - 1] + noSpace.Substring(0, noSpace.Length - 1);
                }

                int spaceIndex = 0;
                while (spaceIndex < spaces.Count)
                {
                    if (spaces[spaceIndex] < noSpace.Length)
                    {
                        noSpace = noSpace.Insert(spaces[spaceIndex], " ");
                    }
                    else
                    {
                        noSpace += " ";
                    }
                    spaceIndex++;
                }
                current = noSpace;

                var currentList = current.Split(" ").Where(x => !x.Contains(" ") && x != "").ToArray();
                for (int i = 0; i < currentList.Length; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        currentList[i] = currentList[i][currentList[i].Length - 1] + currentList[i].Substring(0, currentList[i].Length - 1);
                    }
                }
                noSpace = string.Join("", currentList);

                spaceIndex = 0;
                while (spaceIndex < spaces.Count)
                {
                    if (spaces[spaceIndex] < noSpace.Length)
                    {
                        noSpace = noSpace.Insert(spaces[spaceIndex], " ");
                    }
                    else
                    {
                        noSpace += " ";
                    }
                    spaceIndex++;
                }
                current = noSpace;
            }
            return $"{n} {current}";
        }

        public static string Decode1(string s)
        {
            string num = "";
            string mess = s;
            while (char.IsNumber(mess[0]))
            {
                num += mess[0];
                mess = mess.Substring(1);
            }
            mess = mess.Substring(1);

            for (int z = 0; z < Convert.ToInt32(num); z++)
            {
                int index = 0;
                var spaces = mess.Select(x => index++ < mess.Length && x == ' ' ? $"{index - 1}" : "").Where(x => x != "").Select(x => Convert.ToInt32(x)).ToList();
                string noSpace = mess.Replace(" ", "");

                var messList = mess.Split(" ").Where(x => !x.Contains(" ") && x != "").ToArray();
                for (int i = 0; i < messList.Length; i++)
                {
                    for (int j = 0; j < Convert.ToInt32(num); j++)
                    {
                        messList[i] = messList[i].Substring(1) + messList[i][0];
                    }
                }
                noSpace = string.Join("", messList);

                for (int i = 0; i < Convert.ToInt32(num); i++)
                {
                    noSpace = noSpace.Substring(1) + noSpace[0];
                }

                int spaceIndex = 0;
                while (spaceIndex < spaces.Count)
                {
                    if (spaces[spaceIndex] < noSpace.Length)
                    {
                        noSpace = noSpace.Insert(spaces[spaceIndex], " ");
                    }
                    else
                    {
                        noSpace += " ";
                    }
                    spaceIndex++;
                }
                mess = noSpace;
            }
            return mess;
        }
        public static string MixedFraction(string s)
        {
            var fraction = s.Split('/');
            int num = Convert.ToInt32(fraction[0]) % Convert.ToInt32(fraction[1]);
            if (num == 0)
            {
                return (Convert.ToInt32(fraction[0]) / Convert.ToInt32(fraction[1])).ToString();
            }
            return "";
        }
        public static string ROT135(string input)
        {
            string alpha = "abcdefghijklmnopqrstuvwxyz";
            string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string num = "0123456789";
            var rot = input.Select(x => alpha.Contains(x) ? alpha.IndexOf(x) + 13 > 25 ? alpha[alpha.IndexOf(x) + 13 - 26] : alpha[alpha.IndexOf(x) + 13] :
                                        upper.Contains(x) ? upper.IndexOf(x) + 13 > 25 ? upper[upper.IndexOf(x) + 13 - 26] : upper[upper.IndexOf(x) + 13] :
                                        num.Contains(x) ? num.IndexOf(x) + 5 > 9 ? num[num.IndexOf(x) + 5 - 10] : num[num.IndexOf(x) + 5] :
                                        x);
            return string.Join("", rot);
        }
        public static string solve(string s)
        {
            if (s == string.Join("", s.ToCharArray().Reverse()))
            {
                return "OK";
            }
            for (int i = 0; i < s.Length; i++)
            {
                string forward = s.Remove(i, 1);
                string backwards = string.Join("", forward.ToCharArray().Reverse());
                if (forward == backwards)
                {
                    return "remove one";
                }
            }
            return "not possible";
        }
        public static string[] Intersect(params string[][] arrays)
        {
            List<string> words = new List<string>();
            for (int i = 0; i < arrays.Length; i++)
            {
                for (int j = 0; j < arrays[i].Length; j++)
                {
                    var containsWord = arrays.Select(x => x.Contains(arrays[i][j]));
                    if (containsWord.All(x => x == true) && !words.Contains(arrays[i][j]))
                    {
                        words.Add(arrays[i][j]);
                    }
                }
            }
            return words.ToArray();
        }
        public static bool IsOnionArray(int[] arr)
        {
            int end = arr.Length - 1;
            for (int i = 0; i < Math.Floor((double)arr.Length / 2); i++)
            {
                if (arr[i] + arr[end] <= 10)
                {
                    end--;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public string StolenLunch(string note)
        {
            Dictionary<string, string> code = new Dictionary<string, string>
            {
                {"0","a"},
                {"1","b"},
                {"2","c"},
                {"3","d"},
                {"4","e"},
                {"5","f"},
                {"6","g"},
                {"7","h"},
                {"8","i"},
                {"9","j"}
            };
            return string.Join("", note.Select(x => code.ContainsKey(x.ToString()) ? code[x.ToString()] : code.ContainsValue(x.ToString()) ? code.Where(y => y.Value == x.ToString()).Select(y => y.Key).First() : x.ToString()));
        }
        public static string DoublyNotLess(string n)
        {
            string forward = n;
            string backward = string.Join("", n.ToCharArray().Reverse());
            BigInteger counter = BigInteger.Parse(n);
            while (counter > BigInteger.Parse(forward) || counter > BigInteger.Parse(backward))
            {
                counter++;
                forward = counter.ToString();
                backward = string.Join("", counter.ToString().ToCharArray().Reverse());
            }
            return forward;
        }
        public static int NthSmallest(int[][] arr, int n)
        {
            var nums = string.Join(" ", arr.Select(x => string.Join(" ", x)));
            return nums.Split(" ").Select(x => Convert.ToInt32(x)).OrderBy(x => x).Take(n).Last();
        }
        public static int CalculateBlank(List<object> objectList)
        {
            int running = 0;
            int total = 0;
            string action = "";
            bool final = false;
            string nullAction = "+";
            for (int i = 0; i < objectList.Count; i++)
            {
                if (final)
                {
                    total = Convert.ToInt32(objectList[i]);
                    break;
                }
                if (objectList[i] is String && "+-".Contains(objectList[i].ToString()))
                {
                    action = objectList[i].ToString();
                }
                if (objectList[i] is int || objectList[i] == null)
                {
                    if (action == "-")
                    {
                        running += -1 * (objectList[i] == null ? 0 : Convert.ToInt32(objectList[i]));
                        if (objectList[i] == null)
                        {
                            nullAction = "-";
                        }
                    }
                    else
                    {
                        running += objectList[i] == null ? 0 : Convert.ToInt32(objectList[i]);
                    }
                }
                if (objectList[i] is String && objectList[i].ToString() == "=")
                {
                    final = true;
                }
            }
            return (total - running) * (nullAction == "-" ? -1 : 1);
        }
        public static string SixColumnEncryption(string msg)
        {
            msg = msg.Replace(' ', '.');

            List<string> columns = new List<string>();
            
            for (int i = 0; i < msg.Length; i+=6)
            {
                if (i + 6 > msg.Length - 1)
                {
                    string temp = msg.Substring(i);
                    StringBuilder sb = new StringBuilder();
                    sb.Append('.', 6 - temp.Length);
                    columns.Add($"{temp}{sb}");
                }
                else
                {
                    columns.Add(msg.Substring(i, 6));
                }
            }

            string encrypt = "";

            for (int i = 0; i < 6; i++)
            {
                string temp = "";
                for (int j = 0; j < columns.Count; j++)
                {
                    temp += columns[j][i];
                }
                encrypt += $"{temp} ";
            }

            return encrypt.Trim();
        }
        
        public static string Ordinal(int number, bool brief = false)
        {
            string num = number.ToString();
            if (num.Length > 1)
            {
                if (num[num.Length - 2] == '1')
                {
                    if (num[num.Length - 1] == '1' || num[num.Length - 1] == '2' || num[num.Length - 1] == '3')
                    {
                        return "th";
                    }
                }
            }
            if ("0456789".Contains(num.Last()))
            {
                return "th";
            }
            if (num.Last() == '1')
            {
                return "st";
            }
            if ("23".Contains(num.Last()))
            {
                if (brief)
                {
                    return "d";
                }
                else
                {
                    return num.Last() == '2' ? "nd" : "rd";
                }
            }
            return "";
        }
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