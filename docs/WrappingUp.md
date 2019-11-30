# Wrapping Up

Over the course of this workshop, we built a small console application that can load multiple time-series data sets from CSV files, analyzed them with multiple forecasting algorithms, provided a numerical evaluation of the quality of fit, and charted the results.

## Lessons Learned

We learned some useful things along the way:

* ML.NET provides several capabilities to make it easier to work with time-series data.
* You need to understand your data and possibly clean and transform it to get good results.
* Different types of time-series data (stocks vs. database wait times) may benefit from different prediction algorithms or the selection of different parameters (e.g., window size or horizon).
* Evaluate results numerically where possible to make objective decisions.

## Anomaly Detection

We didn't actually perform any anomaly detection in this workshop.
Detecting an anomaly requires you can first predict the expected behavior.
Only then can you evaluate when a time series varies from that behavior.
You might identify an anomaly whenever a data point is two or three standard deviations above or below the predicted value for that point in time, for example.

ML.NET provides additional classes like the [IidSpikeEstimator](https://docs.microsoft.com/en-us/dotnet/api/microsoft.ml.transforms.timeseries.iidspikeestimator)/[IidSpikeDetector](https://docs.microsoft.com/en-us/dotnet/api/microsoft.ml.transforms.timeseries.iidspikedetector), and [SrCnnAnomalyEstimator](https://docs.microsoft.com/en-us/dotnet/api/microsoft.ml.transforms.timeseries.srcnnanomalyestimator)/[SrCnnAnomalyDetector](https://docs.microsoft.com/en-us/dotnet/api/microsoft.ml.transforms.timeseries.srcnnanomalydetector) to detect anomalies in a more direct way.
The [DetectAnomalyBySrCnn](https://github.com/dotnet/machinelearning/blob/master/docs/samples/Microsoft.ML.Samples/Dynamic/Transforms/TimeSeries/DetectAnomalyBySrCnn.cs) sample in ML.NET [TimeSeries samples](https://github.com/dotnet/machinelearning/tree/master/docs/samples/Microsoft.ML.Samples/Dynamic/Transforms/TimeSeries) is a great example of how a very small amount of ML.NET code can find anomalies.

## Further Exploration

Where should you go to learn more?

### ML.NET

* Microsoft [ML.NET](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet) documentation
* [Tutorial: Detect anomalies in product sales with ML.NET](https://docs.microsoft.com/en-us/dotnet/machine-learning/tutorials/sales-anomaly-detection) (tutorial from Microsoft)
* [Announcing ML.NET 1.1 and Model Builder updates (Machine Learning for .NET)](https://devblogs.microsoft.com/dotnet/announcing-ml-net-1-1-and-model-builder-updates-machine-learning-for-net/) (blog by Cesar De la Torre, Principal Program Manager, .NET)
* [Using ML.NET in Jupyter notebooks](https://devblogs.microsoft.com/cesardelatorre/using-ml-net-in-jupyter-notebooks/) (blog by Cesar De la Torre, Principal Program Manager, .NET)
* [NimbusML](https://docs.microsoft.com/en-us/NimbusML/overview) (A Python module with bindings for ML.NET)
* [TimeSeries samples](https://github.com/dotnet/machinelearning/tree/master/docs/samples/Microsoft.ML.Samples/Dynamic/Transforms/TimeSeries) (several code samples for working with time series in ML.NET)

### General Time Series Information

* [Almost Everything You Need to Know About Time Series](https://towardsdatascience.com/almost-everything-you-need-to-know-about-time-series-860241bdc578) (blog by Marco Peixeiro on Towards Data Science)
* [The Numenta Anomaly Benchmark (NAB)](https://github.com/numenta/NAB) (A framework for evaluating anomaly detection algorithms, including 50+ time series with anomalies labeled)
