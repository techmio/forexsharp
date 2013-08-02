using System;
using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.OrderManagements
{
    public class ProgressiveProtifProtector : IProfitProtector
    {
        private readonly Order _order;

        private double _latestProtectedLevel = 50;
        private const double LimitProtectedLevel = 50;

        public ProgressiveProtifProtector(Order order)
        {
            _order = order;
        }

        public void TryProtectProfit()
        {
           var profitPoints = _order.ProfitPoints;
           if (profitPoints > _latestProtectedLevel)
            {
                _order.ProtectProfit(profitPoints - LimitProtectedLevel);
                _latestProtectedLevel = profitPoints;
            }
        }
    }
}