using System;

namespace FXSharp.EA.OrderManagements
{
    // should be careful about margin level and margin call
    public class MoneyManagement
    {
        private readonly double balance;
        private readonly double riskExposure;

        public MoneyManagement(double riskExposure, double balance)
        {
            this.riskExposure = riskExposure;
            this.balance = balance;
        }

        private double RiskValue
        {
            get { return (riskExposure*balance)/100; }
        }

        private double ValuePerPips
        {
            get { return 10; }
        }

        // should check to pips calculator 

        public double CalculateLotSize(double stopLoss)
        {
            var lot = Math.Round(RiskValue/(ValuePerPips*Pips(stopLoss)), 2);
            return lot > 0.0 ? lot : 0.01;
        }

        private double Pips(double point)
        {
            return point/10;
        }
    }
}