using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarSweep
{
    public static class DepthChecker
    {
        public static int GetIncreasesByMeasurement(List<int> measurements)
        {
            var prev = measurements.FirstOrDefault();
            var increases = 0;
            measurements.ForEach(m =>
            {
                if (m > prev)
                {
                    increases++;
                }
                prev = m;
            });
            return increases;
        }

        public static int GetIncreasesByWindow(List<int> measurements)
        {
            var windowSums = new List<int>();
            for( int i =0; i < measurements.Count - 2 ;i++)
            {
                var window = measurements.Skip(i).Take(3).Sum();
                windowSums.Add(window);
            }
            return GetIncreasesByMeasurement(windowSums);
        }
    }
}
