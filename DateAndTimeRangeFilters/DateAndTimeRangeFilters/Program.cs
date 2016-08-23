using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateAndTimeRangeFilters
{
    class Program
    {
        private static Random gen = new Random();

        static DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        static void Main(string[] args)
        {
            Run3(1);
            Run3(2);
            Run3(3);
            Run3(4);
            Console.ReadKey();
        }

        static void Run3(int quarter)
        {

            var minTime = TimeSpan.Zero;
            var maxTime = new TimeSpan(23, 59, 59);
            var time = new TimeSpan((maxTime.Hours / quarter-1)-maxTime.Hours, maxTime.Minutes, maxTime.Seconds);
            DateTime timex = DateTime.Today.Add(time);
            string displayTime = timex.ToString("hh:mm tt");
            Console.WriteLine(displayTime);
            //Console.WriteLine(time.ToString("hh:mm tt"));
            return;
            var currentYear = DateTime.Now.Year;

            int endMonth = 3*quarter;
            int startMonth = endMonth-2;
            
            var daysInMonth = DateTime.DaysInMonth(currentYear, endMonth);
            DateTime minDateTime = new DateTime(currentYear, startMonth,01,00,00,00, DateTimeKind.Local)
                .ToUniversalTime()
                ;
            DateTime maxDateTime = new DateTime(currentYear, endMonth, daysInMonth, 23, 59, 59,
                DateTimeKind.Local)
                .ToUniversalTime()
                ;
            Console.WriteLine(minDateTime);
            Console.WriteLine(maxDateTime);
        }

        static void Run2(int quarter)
        {
            var currentYear = DateTime.Now.Year;

            int endMonth = 3*quarter;
            int startMonth = endMonth-2;
            
            var daysInMonth = DateTime.DaysInMonth(currentYear, endMonth);
            DateTime minDateTime = new DateTime(currentYear, startMonth,01,00,00,00, DateTimeKind.Local)
                .ToUniversalTime()
                ;
            DateTime maxDateTime = new DateTime(currentYear, endMonth, daysInMonth, 23, 59, 59,
                DateTimeKind.Local)
                .ToUniversalTime()
                ;
            Console.WriteLine(minDateTime);
            Console.WriteLine(maxDateTime);
        }

        static void Run1()
        {

            try
            {
                Console.WriteLine(DateTime.Now.Month);

                int length = 10;
                DateTime?[] dataSource = new DateTime?[length];
                List<DateTime?> dataFiltered = new List<DateTime?>();

                var tMax = dataFiltered.Max();
                var tMin = dataFiltered.Max();

                Console.WriteLine($"tMin : {tMin} tMax : {tMax}");
                Console.ReadKey();
                Console.Clear();
                //init data
                for (int i = 0; i < dataSource.Length; i++)
                {
                    if (i % 3 == 0)
                    {
                        dataSource[i] = null;
                    }
                    else
                    {
                        dataSource[i] = RandomDay();
                    }

                }



                List<DateTime?> dataMinMax = dataSource.ToList();
                var sortedList = dataMinMax.Where(x => x.HasValue).OrderBy(x => x.Value.Date).ToList();

                var max = (from x in dataMinMax
                           where x.HasValue
                           select x.Value).Max();
                var min = (from x in dataMinMax
                           where x.HasValue
                           select x.Value).Min();

                sortedList.ForEach(x => Console.WriteLine(x?.ToString() ?? "--"));
                Console.WriteLine($"Min : {min} Max : {max}");

                List<DateTime?> data = dataSource.ToList();
                Console.WriteLine("\nTo Filtered List");
                Console.WriteLine($"\nItems : [{data.Count}]");

                data.ForEach(x => Console.WriteLine(x?.ToString() ?? "--"));

                FilterDateTime dataFilter = new FilterDateTime()
                {
                    MaxDateTime = sortedList[sortedList.Count - 2],
                    MinDateTime = sortedList[1],
                };

                dataFiltered = dataFilter.FilterData(data);
                Console.WriteLine("\nResult\nFiltered List");
                Console.WriteLine($"\nItems : [{dataFiltered.Count}]");

                dataFiltered.ForEach(x => Console.WriteLine(x?.ToString() ?? "--"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }

    class FilterDateTime
    {

        /// <summary>
        /// Minimum date time value available in list
        /// </summary>
        public DateTime? MinDateTimeBound { get; set; }

        /// <summary>
        /// Maximum date time value available in list
        /// </summary>
        public DateTime? MaxDateTimeBound { get; set; }

        /// <summary>
        /// Minimum data value accepted in filtered range
        /// </summary>
        public DateTime? MinDateTime { get; set; }

        /// <summary>
        /// Maximum data value accepted in filtered range
        /// </summary>
        public DateTime? MaxDateTime { get; set; }


        public List<DateTime?> FilterData(List<DateTime?> data)
        {
            List<DateTime?> filteredItems = new List<DateTime?>();
            try
            {



                if (data == null || data.Count == 0)
                {
                    return data;
                }
                foreach (var date in data)
                {
                    if (date >= MinDateTime && date <= MaxDateTime)
                    {
                        filteredItems.Add(date);
                    }
                }

                Console.WriteLine($"\n\n\t---\t\n\tFiltered items. Available {filteredItems.Count} items.");
                Console.WriteLine($"Min: {MinDateTime}");
                Console.WriteLine($"Max: {MaxDateTime}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            return filteredItems;
        }
    }
}
