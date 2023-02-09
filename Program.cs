using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParsing
{
    class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();
            obj.HandleObjects();
        }

        public void HandleObjects()
        {
            ParseData obj = new ParseData();
            Console.WriteLine();
            obj.ParsingData();
            Console.WriteLine();
            obj.PrintData();
            Console.WriteLine();
            obj.ParseDivs();
            Console.WriteLine();
        }
    }
}
