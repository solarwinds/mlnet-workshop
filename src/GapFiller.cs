using System;
using System.Collections.Generic;
using System.Linq;

namespace Anomalies
{
    internal class GapFiller
    {
        public static IEnumerable<Observation> FillGaps(ICollection<Observation> observations, TimeSpan interval)
        {
            TimeSpan tolerance = interval / 2;
            DateTime expectedDate = observations.First().Date;
            float lastValue = observations.First().Value;

            foreach (Observation observation in observations)
            {
                while (expectedDate + tolerance < observation.Date)
                {
                    yield return new Observation
                    {
                        Date = expectedDate,
                        Value = lastValue
                    };

                    expectedDate = expectedDate.Add(interval);
                }

                yield return observation;

                expectedDate = observation.Date.Add(interval);
                lastValue = observation.Value;
            }
        }
    }
}
