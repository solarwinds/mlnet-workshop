using System;
using Microsoft.ML.Data;

namespace Anomalies
{
    public class WaitTime
    {
        [LoadColumn(0)] public DateTime Date;
        [LoadColumn(1)] public float QueryWaitTime;

        internal Observation ToObservation()
        {
            return new Observation
            {
                Date = Date,
                Value = QueryWaitTime
            };
        }
    }
}
