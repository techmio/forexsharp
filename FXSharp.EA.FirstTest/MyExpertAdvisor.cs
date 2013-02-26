﻿//using TradePlatform.MT4.Data;
using FXSharp.TradingPlatform.Exts;
using System;

namespace FXSharp.EA.FirstTest
{
    public class MyExpertAdvisor : EExpertAdvisor
    {
        Order order = null;
        int count = 0;

        private QuoteBeat beat;

        private double threshold = 3;

        private bool isInitialize;

        protected override int Init()
        {
            beat = new QuoteBeat();
            beat.Add(new Quote(Bid, Ask));

            isInitialize = true;

            return 1;
        }

        protected override int DeInit()
        {
            
            return 1;
        }

        protected override int Start()
        {
            if (!isInitialize)
            {
                Init();
            }

            // should introduce event and directly buy or sell 
            // should create new object for decision making. 

            beat.Add(new Quote(Bid, Ask));

            PrintReport(beat);

            if (beat.MaxUp >= threshold)
            {
                if (IsNoOpenOrder())
                {
                    order = Buy(0.1);
                }
                else
                {
                    if (IsAlreadyOpenBuy()) return 1;

                    order.Close();
                    order = Buy(0.1);
                }
            }
            else if (Math.Abs(beat.MaxDown) >= threshold)
            {
                if (IsNoOpenOrder())
                {
                    order = Sell(0.1);
                }
                else
                {
                    if (IsAlreadyOpenSell()) return 1;

                    order.Close();
                    order = Sell(0.1);
                }
            }

            return 1;
        }

        private void PrintReport(QuoteBeat beat)
        {
            Console.WriteLine("Delta Bid : {0}, Ask : {1}", beat.DeltaBid, beat.DeltaAsk);
            Console.WriteLine("Max Up : {0}, Down : {1}", beat.MaxUp, beat.MaxDown);
        }

        private bool IsAlreadyOpenSell()
        {
            return order.OrderType == TradePlatform.MT4.SDK.API.ORDER_TYPE.OP_SELL;
        }

        private bool IsAlreadyOpenBuy()
        {
            return order.OrderType == TradePlatform.MT4.SDK.API.ORDER_TYPE.OP_BUY;
        }

        private bool IsNoOpenOrder()
        {
            return order == null;
        }
    }
}
