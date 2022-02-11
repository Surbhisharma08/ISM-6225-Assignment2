 using System;
 using System.Collections.Generic;
  using System.Text;
  using System.Linq;
  using System.Text.RegularExpressions;



namespace ISM6225_Assignment_2_Spring_2022
{
    class Program
    {
        public static Dictionary<char, char> dict = new Dictionary<char, char>();
        public static Dictionary<char, string> dict1 = new Dictionary<char, string>();
        public static char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static string[] Morse = new string[] { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", " - ..-", " -.--", "--.." };

        static void Main(string[] args)
        {
            //Question-1
            string sort = Console.ReadLine();
            int target = Convert.ToInt32(Console.ReadLine());
            int[] nums = sort.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            Program Sol = new Program();
            int result = Sol.SearchInsert(nums, target);
            Console.WriteLine(result);

            //Question-2
            string para = Console.ReadLine();
            string[] banned = new string[] { Console.ReadLine() };


            string result1 = Sol.MostCommonWord(para, banned);
            Console.WriteLine(result1.ToLower());

            //Question-3
            string input = Console.ReadLine();
            int[] arr = Array.ConvertAll(input.Split(','), int.Parse);
            int result2 = Sol.FindLucky(arr);
            Console.WriteLine(result2);

            //Question-4
            string secret = Console.ReadLine();
            string guess = Console.ReadLine();



            string result3 = Sol.GetHint(secret, guess);
            Console.WriteLine(result3);

            //Question-5
            string input1 = Console.ReadLine();

            IList<int> result4 = Sol.PartitionLabels(input1);
            foreach (int i in result4)
            {
                Console.WriteLine(i);
            }

            //Question-6

            string w = Console.ReadLine();
            string s = Console.ReadLine().ToLower();
            int[] width = Array.ConvertAll(w.Split(','), int.Parse);

            int[] result5 = Sol.NumberOfLines(width, s);
            Console.WriteLine(result5[0] + " " + result5[1]);

            //Question-7

            string s1 = Console.ReadLine();

            bool result6 = Sol.IsValid(s1);
            Console.WriteLine(result6);


            //Question-8
            string[] s2 = Console.ReadLine().Split(',');
            int result7 = Sol.UniqueMorseRepresentations(s2);
            Console.WriteLine(result7);


            //Question-9
            int[][] grid = new int[][] { new int[] { 0, 2 }, new int[] { 1, 3 } };

            int result9 = Sol.SwimInWater(grid);
            Console.WriteLine(result9);


            //Question-10
            string input3 = Console.ReadLine();
            string input4 = Console.ReadLine();

            int result10 = Sol.minDistance(input3, input4);
            Console.WriteLine(result10);
























        }


        //Question-1
        ////find the mid value compare it with target, if equal then return else if greater than mid value then left value increase by one else right value-1
        int SearchInsert(int[] nums, int target)
        {
            int left = 0;
            int pivot = 0;
            int right = nums.Length - 1;

            while (left <= right)
            {
                pivot = left + (right - left) / 2;
                if (nums[pivot] == target)
                {
                    return pivot;
                }
                if (target < nums[pivot] && pivot != 0)

                    right = pivot - 1;
                else
                    left = pivot + 1;
            }
            while (left != right)// if target not there find the right value and decrease till target is greater than right value return right +1 value.
            {
                if (target > nums[right])
                {

                    return right + 1;

                }
                else if (right > 0)
                    right = right - 1;
                else
                    return 0;



            }
            return -1;

        }
        //Question-2
        //// if banned word then don’t add to dictionary if the value is repeat increase the count
        public string MostCommonWord(string paragraph, string[] banned)
        {
            paragraph = Regex.Replace(paragraph, ",", " ");
            string[] para = paragraph.ToUpper().Split(' ');

            int count = 0;
            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (string s in para)
            {
                foreach (string b in banned)
                {
                    if (s == b.ToUpper())
                    {
                        continue;
                    }
                    if (dict.ContainsKey(s))
                    {
                        dict[s] = dict[s] + 1;
                    }
                    else
                    {
                        dict.Add(s, 1);
                    }
                }
            }
            int max = dict.Values.Max();
            foreach (KeyValuePair<string, int> entry in dict)
            {
                if (entry.Value == max)
                {
                    return entry.Key;
                }
            }
            return "";


        }
        //Question-3

        //maintain a dictionary and check the number of occurrence if it is equal to value and is max return key otherwise -1
        public int FindLucky(int[] arr)
        {
            int result = 0;
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i <= arr.Length - 1; i++)
            {
                if (dict.ContainsKey(arr[i]))
                {

                    dict[arr[i]] = dict[arr[i]] + 1;
                }
                else
                {
                    dict.Add(arr[i], 1);
                }
            }
            foreach (KeyValuePair<int, int> entry in dict)
            {
                result = dict.Values.Max();
                if (entry.Key == entry.Value && entry.Value == result)
                {
                    return entry.Key;
                }
            }
            return -1;
        }

        // Question-4
        //Add into dictionary if dict1 key == dict2 key and both values are equal add to bulls if only value matches add to cow. Subtract cow – bull to get unique.
        public string GetHint(string secret, string guess)
        {
            Dictionary<int, int> dict1 = new Dictionary<int, int>();
            Dictionary<int, int> dict2 = new Dictionary<int, int>();

            int[] sec = Array.ConvertAll(secret.ToCharArray(), c => (int)Char.GetNumericValue(c));

            int[] gue = Array.ConvertAll(guess.ToCharArray(), c => (int)Char.GetNumericValue(c));
            int bulls = 0;
            int cows = 0;
            for (int i = 0; i <= sec.Length - 1; i++)
            {
                dict1.Add(i, sec[i]);
            }
            for (int j = 0; j <= gue.Length - 1; j++)
            {
                dict2.Add(j, gue[j]);
            }
            foreach (KeyValuePair<int, int> entry1 in dict1)
            {
                foreach (KeyValuePair<int, int> entry2 in dict2)
                {
                    if (entry1.Key == entry2.Key && entry1.Value == entry2.Value)
                    {
                        bulls += 1;
                    }

                }
                if (dict2.ContainsValue(entry1.Value))
                {
                    cows += 1;
                }
            }

            return bulls.ToString() + "A" + Math.Abs(cows - bulls).ToString() + "B";
        }

        //Question-5
        //The idea is to store the index of the last occurrence of a character and start scanning the string. For every character found, find its last occurrence from the array and set the current last index to be scanned if it is greater.
        public IList<int> PartitionLabels(string S)
        {
            var last_index_array = new int[26];
            var collection = new List<int>();
            int i = 0;

            for (i = 0; i < S.Length; i++)
                last_index_array[S[i] - 'a'] = i;
            i = 0;

            while (i < S.Length)
            {
                int begin = i;
                int lastindex = last_index_array[S[i] - 'a'];

                while (i < lastindex)
                {
                    lastindex = Math.Max(last_index_array[S[i] - 'a'], lastindex);
                    i++;
                    if (lastindex == S.Length - 1) { i = lastindex; break; };
                }
                collection.Add(i - begin + 1);
                i++;
            }

            return collection;
        }
        //Question-6
        // find the index by subtracting ASCII value of ‘a’ with the letters given. If the value in the index is greater than 100 increase line but keep the last value of width.
        public int[] NumberOfLines(int[] widths, string s)
        {
            char[] c = s.ToCharArray();
            int max = 100;
            int width = 0;
            int lines = 1;

            foreach (char i in c)
            {
                int w = widths[i - 'a'];
                width += w;
                if (width > max)
                {
                    width = w;
                    lines++;

                }


            }
            return new int[] { lines, width };
        }
        //Question-7
        // put the given value on dictionary and input value on stack if its key pop to stack if key and value matches push from stack
        public bool IsValid(string bulls_string)
        {

            dict.Add('(', ')');
            dict.Add('{', '}');
            dict.Add('[', ']');
            Stack<char> s = new Stack<char>();
            foreach (char c in bulls_string)
            {
                if (dict.ContainsKey(c))
                {
                    s.Push(c);
                }
                else
                {
                    if (s.Count == 0)
                        return false;
                    char open = s.Pop();
                    if (c != dict[open])
                    {
                        return false;
                    }

                }
            }
            return true;
        }
        // Question-8
        //make dictionary of alphabet and morse value  assign values to the alphabets according to input . put the list on Hash set to get the unique count.

        public int UniqueMorseRepresentations(string[] words)
        {
            for (int i = 0; i <= Morse.Length - 1; i++)
            {
                dict1.Add(alphabet[i], Morse[i]);
            }
            string word = "";
            HashSet<string> list = new HashSet<string>();

            for (int i = 0; i <= words.Length - 1; i++)
            {
                char[] c = words[i].ToCharArray();

                for (int j = 0; j < c.Length - 1; j++)
                {

                    if (dict1.ContainsKey(c[j]))
                    {
                        word = dict1[c[j]];
                    }

                }
                list.Add(word);

            }
            return list.Count;
        }
        //Question-9
        //perform binary search to get the time, this will give the smallest value. To check whether time is possible, we perform a simple depth-first search where we can only walk in squares that are at most. 
        public int SwimInWater(int[][] grid)
        {
            if (grid == null || grid.Length == 0)
            {
                return 0;
            }

            (int, int)[] directions = new (int, int)[] { (1, 0), (0, 1), (-1, 0), (0, -1) };
            int maxTime = grid.Length * grid.Length;
            int low = grid[0][0];
            int high = maxTime - 1;

            while (low < high)
            {
                int mid = low + (high - low) / 2;

                if (isValid(grid, mid, directions))
                {
                    high = mid;
                }

                else
                {
                    low = mid + 1;
                }
            }

            return low;
        }

        private bool isValid(int[][] grid, int height, (int, int)[] directions)
        {
            bool[,] visited = new bool[grid.Length, grid[0].Length];
            visited[0, 0] = true;
            return dfs(grid, 0, 0, height, directions, visited);
        }

        private bool dfs(int[][] grid, int row, int column, int height, (int x, int y)[] directions, bool[,] visited)
        {
            if (row == grid.Length - 1 && column == grid[0].Length - 1)
            {
                return true;
            }

            foreach (var d in directions)
            {
                int newRow = row + d.x;
                int newColumn = column + d.y;

                if (newRow >= 0 && newColumn >= 0 && newRow < grid.Length && newColumn < grid[0].Length
                  && !visited[newRow, newColumn] && grid[newRow][newColumn] <= height)
                {
                    visited[newRow, newColumn] = true;
                    if (dfs(grid, newRow, newColumn, height, directions, visited))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        //Question-10
        // check for all possible edit sequences and choose the shortest one in-between. Matrix[I,j] first ith and jth values of word1 and word2.if (i == 0) matrix[i, j] = j or else if j==0 matrix[I,j]=j. if matrix[i]!=matrix[j]we have to take into account the replacement of the last character during the conversion

        public int minDistance(String word1, String word2)
        {
            var matrix = new int[word1.Length + 1, word2.Length + 1];
            for (int i = 0; i <= word1.Length; ++i) for (int j = 0; j <= word2.Length; ++j)
                {
                    if (i == 0) matrix[i, j] = j;
                    else if (j == 0) matrix[i, j] = i;
                    else matrix[i, j] =
                        Math.Min(
                            (word1[i - 1] == word2[j - 1] ? 0 : 1) + matrix[i - 1, j - 1],
                            1 + Math.Min(
                                matrix[i, j - 1],
                                matrix[i - 1, j]
                            )
                        );
                }
            return matrix[word1.Length, word2.Length];
        }



    }




    }

