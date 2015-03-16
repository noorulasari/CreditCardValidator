using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace CreditCardValidator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //cursor automatically starts in textbox
            tbCardNumber.Focus();
        }


        //Clear Button removes display and contents in textbox
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbCardNumber.Clear();
            labelValidator.Content = "";
            labelIssuer.Content = "";
        }


        //Validate Button checks card number in input box
        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            //remove previous display
            labelIssuer.Content = "";

            bool valid = false;
            string cardIssuer;
            string cardNumber = tbCardNumber.Text;

            CreditCard card = new CreditCard(cardNumber);

            //check textbox is not empty and input is of type int
            if (cardNumber != "" && card.checkCardNumber(cardNumber))
            {
                //Validate card number
                valid = card.validateCardNumber(cardNumber);

                //display Valid/Not Valid message
                if (valid)
                {
                    labelValidator.Foreground = new SolidColorBrush(Colors.Black);
                    labelValidator.Content = "Card Number: " + cardNumber + " is Valid";

                    cardIssuer = card.checkCardIssuer(cardNumber);
                    labelIssuer.Content = "Card Issuer: " + cardIssuer;
                }
                else
                {
                    labelValidator.Foreground = new SolidColorBrush(Colors.Red);
                    labelValidator.Content = "Card Number: " + cardNumber + " is Not Valid";
                }
            }
            //no number entered or input not of type int
            else
            {
                labelValidator.Foreground = new SolidColorBrush(Colors.Red);
                labelValidator.Content = "Please enter a Card Number";
            }
        }


        //able to use Enter Key for textbox
        private void tbCardNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnValidate_Click(this, new RoutedEventArgs());
            }
        }
    }
}
