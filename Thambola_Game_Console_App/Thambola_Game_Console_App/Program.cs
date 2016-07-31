using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thambola_Game_Console_App
{
    class Thambola
    {
        public static Random Random
            = new Random();
        public int[,] GenerateTicket()
        {
            int[,] ticket = new int[3, 9];
            int rowCount = 5;
            for (int i = 0; i < ticket.GetLength(0); i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    int index = Random.Next(0, ticket.GetLength(1));
                    ticket[i, index] = Random.Next(index*10, index*10 + ticket.GetLength(1));
                }
            }
                Console.WriteLine();
            for (int i = 0; i < ticket.GetLength(0); i++)
            {
                for (int j = 0; j < ticket.GetLength(1); j++)
                {
                    Console.Write($"{ticket[i,j]}\t");
                }
                Console.WriteLine();
            }
                Console.WriteLine();
            return ticket;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Thambola x= new Thambola();
            var ticket = x.GenerateTicket();
            var ticket2 = x.GenerateTicket();
            Console.ReadKey();
        }
    }
}
