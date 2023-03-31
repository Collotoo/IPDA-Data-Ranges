using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cAlgo.API;
using cAlgo.API.Collections;
using cAlgo.API.Indicators;
using cAlgo.API.Internals;

namespace cAlgo
{
    [Indicator(IsOverlay = true, AccessRights = AccessRights.None, TimeZone = TimeZones.EAfricaStandardTime)]
    public class IPDADataRanges : Indicator
    {
        

        
        
        
        protected override void Initialize()
        {
            // To Print(Message);
        }

        public override void Calculate(int index)
        {
        
            if (!IsLastBar) return;

            // Returns the min/max of last 20 bars
            var minMax = GetMinMax(Bars.OpenTimes[index - 20], Bars.OpenTimes[index]);
            var fortyMinMax = GetMinMax(Bars.OpenTimes[index-40], Bars.OpenTimes[index-21]);
            var sixtyMinMax = GetMinMax(Bars.OpenTimes[index - 60], Bars.OpenTimes[index - 41]);

            Chart.DrawVerticalLine("20 period separator", Bars.OpenTimes[index - 20], Color.Red);
            Chart.DrawVerticalLine("40 period separator", Bars.OpenTimes[index - 40], Color.Red);
            Chart.DrawVerticalLine("60 period separator", Bars.OpenTimes[index - 60], Color.Red);


            Chart.DrawTrendLine("20 Day Low", minMax.Item2, minMax.Item1, Bars[index].OpenTime, minMax.Item1, Color.YellowGreen);
            //Chart.DrawText("2O Day text", "20 Period Low",Bars.OpenTimes[index] ,minMax.Item1, Color.Black);
            Chart.DrawTrendLine("20 Day High", minMax.Item4, minMax.Item3, Bars[index].OpenTime, minMax.Item3, Color.YellowGreen);
            //Chart.DrawText("2O Day text", "20 Period High",Bars.OpenTimes[index] ,minMax.Item1, Color.Black);
            Chart.DrawTrendLine("40 Day Low", fortyMinMax.Item2, fortyMinMax.Item1, Bars[index].OpenTime, fortyMinMax.Item1, Color.Blue);
            //Chart.DrawText("40 Day text", "40 Day Low",Bars.OpenTimes[index] ,fortyMinMax.Item1, Color.Black);
            Chart.DrawTrendLine("40 Day High", fortyMinMax.Item4, fortyMinMax.Item3, Bars[index].OpenTime, fortyMinMax.Item3, Color.Blue);
            //Chart.DrawText("40 Day text", "40 Day High",Bars.OpenTimes[index] ,fortyMinMax.Item3, Color.Black);
            Chart.DrawTrendLine("60 Day Low", sixtyMinMax.Item2, sixtyMinMax.Item1, Bars[index].OpenTime, sixtyMinMax.Item1, Color.Black);
             //Chart.DrawText("60 Day text", "60 Day Low",Bars.OpenTimes[index] ,sixtyMinMax.Item1, Color.Black);
            Chart.DrawTrendLine("60 Day High", sixtyMinMax.Item4, sixtyMinMax.Item3, Bars[index].OpenTime, sixtyMinMax.Item3, Color.Black);
            //Chart.DrawText("60 Day text", "60 Day High",Bars.OpenTimes[index] ,sixtyMinMax.Item3, Color.Black);
            
        
        
        }
        /// <summary>
        /// Returns the minimum/maximum prices levels during an specific time period
        /// </summary>
        /// <param name="startTime">Start Time (Inclusive)</param>
        /// <param name="endTime">End Time (Inclusive)</param>
        /// <returns>Tuple<double, double> (Item1 will be minimum price and Item2 will be maximum price)</returns>
        private Tuple<double, DateTime, double, DateTime > GetMinMax(DateTime startTime, DateTime endTime)
        {
            var min = double.MaxValue;
            var max = double.MinValue;
            DateTime minTime= new DateTime();
            DateTime maxTime = new DateTime();
                
            
            

            for (int barIndex = 0; barIndex < Bars.Count; barIndex++)
            {
                var bar = Bars[barIndex];

                if (bar.OpenTime < startTime || bar.OpenTime > endTime)
                {
                    if (bar.OpenTime > endTime) break;

                    continue;
                }

                min = Math.Min(min, bar.Low);
                max = Math.Max(max, bar.High);

                if (min == bar.Low)
                {
                    minTime = bar.OpenTime;
                }
                else if(max == bar.High)
                {
                    maxTime = bar.OpenTime;
                }
                     

            }

            return new Tuple<double, DateTime, double, DateTime>(min, minTime, max, maxTime);
        }
    }
}