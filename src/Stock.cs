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
