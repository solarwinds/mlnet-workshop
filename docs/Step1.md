# Step 1: Create a Project

## Create a Console Application

Create a new console application (**Anomalies.csproj**) in an **src** subfolder.

```shell
dotnet new console --output src --name Anomalies
```

Change directory into the **src** folder.

```shell
cd src
```

## Add NuGet Packages

Add a few NuGet packages we'll need.

```shell
dotnet add package Microsoft.ML.TimeSeries
dotnet add package XPlot.Plotly
dotnet add package FSharp.Core
```

We'll be using **Microsoft.ML.TimeSeries** to make predictions based on time-series data.
We'll use **XPlot.Plotly** and **FSharp.Core** to chart our results.

## Build

Make sure everything builds.

```shell
dotnet build
```

## Run

Make sure everything runs.

```shell
dotnet run
```

If you see "Hello World!", you have a working console application.

## Next

Go to [Step 2: Loading Stock Market Data](./Step2.md).
