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
        private string studentLoanChoice = "no";
        private double kiwiSaverPercent = 0.00;
        private string period = "Anually";
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            income = new income();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
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

        private void kiwisaverComboBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            kiwiSaverPercent = 0.00;
            var kiwisaverComboBoxValue = kiwisaverComboBox.SelectionBoxItem == null || kiwisaverComboBox.SelectionBoxItem.ToString() == "none" ? "" : kiwisaverComboBox.SelectionBoxItem.ToString();
            if (kiwisaverComboBoxValue == "3%")
                kiwiSaverPercent = 0.03;
            else if (kiwisaverComboBoxValue == "4%")
                kiwiSaverPercent = 0.04;
            else if (kiwisaverComboBoxValue == "8%")
                kiwiSaverPercent = 0.08;
            performCalculation();
        }

        private void studentLoanComboBox_Click(object sender, TappedRoutedEventArgs e)
        {
            studentLoanChoice = studentLoanComboBox.SelectionBoxItem == null ? "no":studentLoanComboBox.SelectionBoxItem.ToString();
            performCalculation();
        }

        private void periodComboBox_Click(object sender, TappedRoutedEventArgs e)
        {
            period = periodComboBox.SelectionBoxItem !=null ? periodComboBox.SelectionBoxItem.ToString():"";
            performCalculation();
        }

        private void performCalculation()
        {
            string incomeBeforeTax = anualIncomeTextBox.Text;
            var selectRadioKiwiSaver = myStackPanel1.Children.OfType<ComboBoxItem>().FirstOrDefault(r => r.IsSelected == true);

            double kiwisaverPercent = selectRadioKiwiSaver ==null? 0.00 :double.Parse(selectRadioKiwiSaver.Tag.ToString());


            //calculate tax using method
            income.calculateTax(incomeBeforeTax, kiwiSaverPercent, studentLoanChoice, period);

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
            byPeriodRun.Text = period;
            


        }



    }
}
