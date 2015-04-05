using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KiwiTaxAndIncom
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private income income;
        public MainPage()
        {
            this.InitializeComponent();
            income = new income();
        }

        private void anualIncomeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            anualIncomeTextBox.Text = income.totalIncome;
        }

        private void anualIncomeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            performCalculation();
        }

        private void anualIncomeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            anualIncomeTextBox.Text = "";
            deductionPercentRun.Text = "0.00%";
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            performCalculation();
        }
        private void studentLoanRadioButton_Click(object sender, RoutedEventArgs e)
        {
            performCalculation();
        }

        private void showIn_Click(object sender, RoutedEventArgs e)
        {
            performCalculation();            
        }
        
        
        
        private void performCalculation()
        {
            string incomeBeforeTax = anualIncomeTextBox.Text;
            var selectRadioKiwiSaver = myStackPanel1.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);
            double kiwisaverPercent = double.Parse(selectRadioKiwiSaver.Tag.ToString());

            var selectRadioStudentLoan = studentLoanStackPanel.Children.OfType<RadioButton>().FirstOrDefault(s => s.IsChecked == true);
            string studentLoanPayable = selectRadioStudentLoan.Tag.ToString();
            //var studentLoanPayable = trySelect.SelectionBoxItem.ToString();
            //trySelectBox.Text = studentLoanPayable;

            var selectRadioPeriod = periodStackPanel.Children.OfType<RadioButton>().FirstOrDefault(p => p.IsChecked == true);
            string periods = selectRadioPeriod.Tag.ToString();
            

            //calculate tax using method
            income.calculateTax(incomeBeforeTax, kiwisaverPercent, studentLoanPayable, periods);

            //set values to Views
            grossIncomeRun.Text = income.totalIncome;
            taxPaidRun.Text = income.taxPaid;
            ACCPaidRun.Text = income.ACCPaid;
            KiwiSaverRun.Text = income.KiwiSaverPaid;
            studentLoanRun.Text = income.studentLoanPaid;
            deductionPercentRun.Text = income.deductionPercentage;
            //totalDeductionRun.Text = income.totalDeductions;
            cashInHandTextBlock.Text = income.cashInHand;
            //caculatePeriod.Text = periods;
            byPeriodRun.Text = periods;
            
        }

    }
}
