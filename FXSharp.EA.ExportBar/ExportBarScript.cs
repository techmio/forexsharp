using System.Collections.Generic;
using FXSharp.TradingPlatform.Exts;
using System.IO;

namespace FXSharp.EA.ExportBar
{
    public class ExportBarScript : EExpertAdvisor
    {
        protected override int DeInit()
        {
            return 0;
        }

        protected override int Init()
        {
            return 0;
        }


        // create candlestick with ohlc and datetime
        protected override int Start()
        {
            var bar = Bars;
            
            var contents = new List<string>();
            for (var i = bar - 1; i > 0; i--)
            {
                contents.Add(Close[i].ToString());
            }

            File.WriteAllLines("EURUSD_close.txt", contents);

            return 0;
        }
    }
}