using System;
using System.Collections.Generic;
using System.Text;

namespace KiwiTaxAndIncom
{
    public class income
    {
        public string totalIncome { get; set; }
        public string taxPaid { get; set; }
        public string ACCPaid { get; set; }
        public string KiwiSaverPaid { get; set;}
        public string studentLoanPaid { get; set; }
        public string cashInHand { get; set; }
        public string calculatePeriod { get; set; }
        public string totalDeductions { get; set; }
        public string deductionPercentage { get; set; }

        //set static constant for tax rates and stuff.
        //updated time for constants
        //static string AppTaxRatesUpdateTime ="22-Sep-2014" ;
        //taxable income groups, the fourth group will be ones greater than 70,000
        static double taxFirstGroupAmount = 14000.00;
        static double taxSecondGroupAmount = 48000.00;
        static double taxThirdGroupAmount = 70000.00;

        static double taxRateFirstGroup = 0.105;
        static double taxRateSecondGroup = 0.175;
        static double taxRateThirdGroup = 0.3;
        static double taxRateFourthGroup = 0.33;

        //ACC rates
        static double ACCRates = 0.0145;

        //student loan rates
        static double payableAmountStudentLoan =19084.00;
        static double studentLoanRates = 0.12;

        public income() // constructor 
        {
            this.totalIncome = String.Empty;
            this.taxPaid = String.Empty;
            this.ACCPaid = String.Empty;
            this.KiwiSaverPaid = String.Empty;
            this.studentLoanPaid = String.Empty;
            this.calculatePeriod = "Annually";
            this.deductionPercentage = String.Empty;
        }

        public void calculateTax(string totalIncome, double kiwiSaverPercent, string studentLoanPayable, string period) 
        {
            double grossIncome = 0.00;
            double taxToPay = 0.00;
            double ACCToPay = 0.00;
            double kiwiSaverToPay = 0.00;
            double studentLoanToPay = 0.00;
            double deductions = 0.00;
            double deductionRates = 0.00000;
            int denominator = 1;

            //calculate period
            if (period == "Monthly")
                denominator = 12;
            if (period == "Fortnightly")
                denominator = 52/2;
            if (period == "Weekly")
                denominator = 52;

            if (double.TryParse(totalIncome.Replace('$',' '),out grossIncome))
            {
                //calculate tax deductions:
                if (grossIncome <= taxFirstGroupAmount)
                {
                    taxToPay = grossIncome * taxRateFirstGroup;
                }
                else if (grossIncome > taxRateFirstGroup && grossIncome <= taxSecondGroupAmount)
                {
                    taxToPay = taxFirstGroupAmount * taxRateFirstGroup;
                    taxToPay += (grossIncome - taxFirstGroupAmount) * taxRateSecondGroup;
                }
                else if (grossIncome > taxSecondGroupAmount && grossIncome <= taxThirdGroupAmount)
                {
                    taxToPay = taxFirstGroupAmount * taxRateFirstGroup;
                    taxToPay += (taxSecondGroupAmount- taxFirstGroupAmount)* taxRateSecondGroup;
                    taxToPay += (grossIncome - taxSecondGroupAmount) * taxRateThirdGroup;
                }
                else {
                    taxToPay = taxFirstGroupAmount * taxRateFirstGroup;
                    taxToPay += (taxSecondGroupAmount - taxFirstGroupAmount) * taxRateSecondGroup;
                    taxToPay += (taxThirdGroupAmount - taxSecondGroupAmount) * taxRateThirdGroup;
                    taxToPay += (grossIncome - taxThirdGroupAmount) * taxRateFourthGroup;
                }
                taxToPay /= denominator;
                // calculate acc to pay
                ACCToPay = grossIncome * ACCRates / denominator;

                //calculate kiwisaver to pay
                kiwiSaverToPay = grossIncome * kiwiSaverPercent /denominator;

                //calculate student loan
                if ((studentLoanPayable == "yes" || studentLoanPayable == "Yes") && grossIncome > 19084)
                {
                    studentLoanToPay = (grossIncome - payableAmountStudentLoan) * studentLoanRates / denominator; // paying rates is 12%
                } 

            }
            
            deductions =  (taxToPay + ACCToPay + kiwiSaverToPay + studentLoanToPay)*denominator;
            deductionRates = (grossIncome!=0)?(deductions / grossIncome):0.00;

            this.totalDeductions = String.Format("{0:C}", deductions);//currency
            this.deductionPercentage = String.Format("{0:p}", deductionRates);//percentage

            this.totalIncome = String.Format("{0:C}", grossIncome); // this will ensure the total income is anuall income
            grossIncome /=denominator; // this will calculate according to the period chosen by users
            this.calculatePeriod = period;
            this.taxPaid = String.Format("{0:C}", taxToPay);
            this.ACCPaid = String.Format("{0:C}", ACCToPay);
            this.KiwiSaverPaid = String.Format("{0:C}", kiwiSaverToPay);
            this.studentLoanPaid = String.Format("{0:C}", studentLoanToPay);
            deductions = taxToPay + ACCToPay + kiwiSaverToPay + studentLoanToPay;
            this.cashInHand = String.Format("{0:C}", grossIncome-deductions);
        }

    }
}
