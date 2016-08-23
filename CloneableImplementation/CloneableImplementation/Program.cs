using PortableSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneableImplementation
{
    class Program
    {
        static void Print(MySpecialClass mySpecialClass)
        {
            if (mySpecialClass == null)
            {
                return;
            }
            foreach (var key in mySpecialClass.Keys)
            {
                Console.Write($"KEY: {key}\t");
                Console.WriteLine(mySpecialClass[key]?.ToString() ?? $"null: {key}");
            }
        }

        static void Main(string[] args)
        {
            Test1();
            //Test2();
        }

        //fails to duplicate list in memory...
        static void Test2()
        {

            List<string> x=new List<string>() {"1","2","3"};
            Dictionary<string,List<string>> obj=new Dictionary<string, List<string>>();
            obj.Add("ABC1", x);
            x.Add("Q");
            obj.Add("ABC2", x);


            Dictionary<string, List<string>> obj1 = new Dictionary<string, List<string>>(obj);
            obj1["ABC2"].Add("90");
            obj.Remove("ABC1");

            foreach (var data in obj)
            {
                Console.WriteLine($"obj:{data.Key}:{string.Join("-",data.Value)}");
                foreach (var val in data.Value)
                {
                    Console.WriteLine($"\tdata :{data.Key}:{val}");
                }
            }

            foreach (var data in obj1)
            {
                Console.WriteLine($"obj1:{data.Key}:{string.Join("-", data.Value)}");
                foreach (var val in data.Value)
                {
                    Console.WriteLine($"\tdata :{data.Key}:{val}");
                }
            }

        }

        static void Test1()
        {

            MySpecialClass mySpecialClass = new MySpecialClass();

            mySpecialClass.Add("ABC", new PortableSource.Data()
            {
                A = "Name",
                B = 4,
            });
            Console.WriteLine($"\nOriginal:\t");
            Print(mySpecialClass);


            var result = mySpecialClass;
            Console.WriteLine($"\nCOPY:\t");
            Print(result);
            result.Remove("ABC");
            Console.WriteLine($"\n\nREMOVED:\t");
            Print(result);

            Console.WriteLine($"\n\nFINAL RESULT:\t");

            Console.WriteLine($"\n\nORIGINAL:\t");
            Print(mySpecialClass);
            Console.WriteLine($"\n\nCOPY:\t");
            Print(result);
            Console.Read();
        }
    }
}
