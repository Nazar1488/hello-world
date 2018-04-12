using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лабораторна_1
{
    struct Rule
    {
        public string From
        {
            get;
            set;
        }
        public string To
        {
            get;
            set;
        }

        public Rule(string _from, string _to)
        {
            From = _from;
            To = _to;
        }
    }

    class Program
    {
        static string MarkovAlgorithm(List<Rule> listRules, string someSting)
        {
            while (true)
            {
                bool flag = false;
                foreach (Rule rule in listRules)
                {
                    if (someSting.Contains(rule.From))
                    {
                        someSting = someSting.Replace(rule.From, rule.To);
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    break;
                }
            }
            return someSting;
        }

        static void Main(string[] args)
        {
            List<Rule> rules = new List<Rule>();
            rules.Add(new Rule("yx", "xy"));
            rules.Add(new Rule("zx", "xz"));
            rules.Add(new Rule("xw", "wx"));
            rules.Add(new Rule("zy", "yz"));
            rules.Add(new Rule("yw", "wy"));
            rules.Add(new Rule("zw", "wz"));
            string input;
            Console.WriteLine("<---=== TASK 1 ===--->\nInput string (A=(x,y,z,w)):");
            input = Console.ReadLine();
            Console.WriteLine("Result: " + MarkovAlgorithm(rules, input));
            Console.ReadKey();
            string input2;
            Console.WriteLine("<---=== TASK 2 ===--->\nInput string (A=(o,s,q,r)):");
            input2 = Console.ReadLine();
            List<Rule> rules2 = new List<Rule>();
            rules2.Add(new Rule("o", ""));
            rules2.Add(new Rule("s", ""));
            input2 = MarkovAlgorithm(rules2, input2);
            Console.WriteLine(input2);
            if (input2.Contains("qq"))
            {
                string sub_str = "qq";
                int i = input2.IndexOf(sub_str);
                input2 = input2.Remove(i, sub_str.Length).Insert(i, "rr");
            }
            Console.WriteLine(input2);
            Console.ReadKey();
        }
    }
}
