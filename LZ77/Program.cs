using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZ77
{
    class Program
    {
        static void Main(string[] args)
        {
            string word = "abracadabra";
            //string word = args[0];
            List<word> table = new List<LZ77.word>();
            List<word> variable;
            for(int currentLocation = 0; currentLocation < word.Length; currentLocation++)
            {
                //does it exist previously?
                if (word.Substring(0,currentLocation).Contains(word[currentLocation]))
                {
                    //if exist go and find it
                    for (int searchBuffer = currentLocation-1; searchBuffer > 0; searchBuffer--)
                    {
                        //when found it
                        if (word[currentLocation] == word[searchBuffer])
                        {
                            //how long it is the match?
                            variable = new List<LZ77.word>();
                            int lookAhead = 0;
                            string temp = "" + word[searchBuffer];
                            for(int searchIndex = searchBuffer; searchIndex < currentLocation; searchIndex++)
                            {
                                if (currentLocation+lookAhead < word.Length)
                                {
                                    //is the lookAhead equal to the next character in buffer
                                    if (word[searchIndex] == word[lookAhead])
                                    {
                                        temp = temp + word[searchIndex];
                                        lookAhead++;
                                    }
                                    else
                                    {
                                        variable.Add(new LZ77.word(temp,searchBuffer,temp.Length));
                                        break;
                                    }
                                }
                                else
                                {
                                    variable.Add(new LZ77.word(temp, searchBuffer, temp.Length));
                                    break;
                                }
                            }
                            //order and find the longest match
                            word longest = variable.OrderByDescending(x => x.Length).First();
                            if (currentLocation + longest.Length < word.Length)
                            {
                                table.Add(new word(""+word[currentLocation+longest.Length], longest.Offset, longest.Length));
                            }
                            else
                            {
                                table.Add(new word("", longest.Offset, longest.Length ));
                            }
                        }
                    }
                }
                else
                {
                    table.Add(new word(""+word[currentLocation],0,0));
                }
            }
            Console.WriteLine("Offset\tLength\tSymbol");
            foreach (word w in table)
            {
                Console.WriteLine(w.Offset + "\t" + w.Length + "\t" + w.Name);
            }
            Console.ReadLine();
        }

    }

    class word
    {
        string name;
        int length;
        int offset;

        public word(string name, int offset, int length)
        {
            this.name = name;
            this.length = length;
            this.Offset = offset;
        }

        public string Name { get => name; set => name = value; }
        public int Length { get => length; set => length = value; }
        public int Offset { get => offset; set => offset = value; }
    }
}
