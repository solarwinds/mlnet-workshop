using System;

namespace Anomalies
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSeries[] stockSeries = StockLoader.Load();
            Console.WriteLine("Finished!");
        }
    }
}
