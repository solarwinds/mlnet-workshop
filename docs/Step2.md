# Step 2: Load Stock Market Data

Let's load some time-series data.

## Open VS Code

First, open Visual Studio Code in our repository root, for easy access to the **docs** and **data** folders.

```script
cd ..
code .
```

## Look at Stock Market Data

Expand the **data** folder.
Select **big_five_stocks.csv**.
Review the column headings and data rows.

## Create a Stock class

Let's create a `Stock` class to represent a row from that CSV file.
Add a new **Stock.cs** file under the **src** folder.
Type or copy/paste this class definition:

```csharp
using System;
using Microsoft.ML.Data;

namespace Anomalies
{
    public class Stock
    {
        [LoadColumn(0)] public DateTime Date;
        [LoadColumn(1)] public string Name;
        [LoadColumn(2)] public float Open;
        [LoadColumn(3)] public float Close;
        [LoadColumn(4)] public float High;
        [LoadColumn(5)] public float Low;
        [LoadColumn(6)] public float Volume;
    }
}
```

The `LoadColumn` attributes specify the expected column order in the CSV file.

Note: this class could be `internal` instead of `public`, but we're avoiding build warnings the easy way.

## Create an Observation Class

The `Stock` class is great, but we don't really care about all of those columns, and we'll want to load other kinds of time series, like CPU data.
Let's create an `Observation` class to generalize our approach.
Add a new **Observation.cs** file with this class definition:

```csharp
using System;

namespace Anomalies
{
    internal class Observation
    {
        public DateTime Date;
        public float Value;
    }
}
```

## Generate Observations From Stocks

Let's add a helper method to create an `Observation` from a `Stock`.
Go back to **Stocks.cs**.
Add the following method to the `Stock` class:

```csharp
internal Observation ToObservation()
{
    return new Observation
    {
        Date = Date,
        Value = Close
    };
}
```

## Add a TimeSeries Class

There's data for more than one stock in **big_five_stocks.csv**.
Let's create a class to group observations from the same time series.

Add a new **TimeSeries.cs** file with this class definition:

```csharp
using System.Collections.Generic;
using System.Linq;

namespace Anomalies
{
    internal class TimeSeries
    {
        public string Name { get; }
        public Observation[] Observations { get; }

        public TimeSeries(string name, IEnumerable<Observation> observations)
        {
            Name = name;
            Observations = observations.ToArray();
        }
    }
}
```

## Load Stock Data From the CSV File

We've got some data structures now.
Let's create a class to load the data.
Add a new **StockLoader.cs** file with this class definition:

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;

namespace Anomalies
{
    internal static class StockLoader
    {
        public static TimeSeries[] Load()
        {
            // Create an ML.NET machine learning context.
            var context = new MLContext();

            // Get the path to the CSV file.
            string rootFolder = Directory.GetParent(Environment.CurrentDirectory).FullName;
            string csvFile = Path.Combine(rootFolder, "data", "big_five_stocks.csv");

            // Load data from the CSV file.
            Console.WriteLine($"Loading stocks from '{csvFile}'...");
            IDataView dataView = context.Data.LoadFromTextFile<Stock>(
                path: csvFile,
                hasHeader: true,
                separatorChar: ',');
            IEnumerable<Stock> stocks = context.Data.CreateEnumerable<Stock>(
                data: dataView,
                reuseRowObject: false);

            // Group rows by stock name.  Create a TimeSeries for each group.
            TimeSeries[] timeSeriesList = stocks
                .ToLookup(stock => stock.Name)
                .Select(group => new TimeSeries(
                    name: group.Key,
                    observations: group.Select(s => s.ToObservation())))
                .ToArray();

            return timeSeriesList;
        }
    }
}
```

The `StockLoader` uses an `MLContext` from ML.NET to load data from the CSV file into `Stock` objects.
It then groups the rows by stock name and creates a `TimeSeries` for each stock.

## Call the StockLoader

This code doesn't do us any good until something is calling it.
Let's update the `Main` method in `Program` as follows:

```csharp
static void Main(string[] args)
{
    TimeSeries[] stockSeries = StockLoader.Load();
    Console.WriteLine("Finished!");
}
```

## Test

Let's run the application to test our changes.
You should see output similar to this.

```shell
Loading stocks from 'C:\Dev\mlnet-workshop\data\big_five_stocks.csv'...
Finished!
```

## Next

Go to [Step 3: Display a Graph](./Step3.md).
