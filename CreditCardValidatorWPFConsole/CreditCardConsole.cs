using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CreditCardValidatorWPFConsole
{
    //With Console Output of Calculations
    class CreditCardConsole
    {
        private int cardNumberLength = 0;
        private int checkDigitNum = 0;
        private int unitTotalDigit = 0;
        private int totalDigit = 0;
        private char checkDigit;
        private string cardNumberCheckDigitRemoved;
        private string cardNumberReversed;


        public string CardNumber { get; set; }


        public CreditCardConsole()
        { }

        public CreditCardConsole(string cardNumber)
        {
            CardNumber = cardNumber;
            cardNumberLength = cardNumber.Length;
        }

        //Check Card Number entered only has numeric values
        public bool checkCardNumber(string cardNumber)
        {
            bool isCharNum = false;

            foreach (var item in cardNumber)
            {
                isCharNum = Char.IsNumber(item);
                if (!isCharNum)
                {
                    return false;
                }
            }
            return true;
        }

        //Validates Card Number entered using Luhn Algorithm
        public bool validateCardNumber(string cardNumber)
        {
            Console.WriteLine();

            //Value of Check Digit i.e last digit
            checkDigit = cardNumber[cardNumberLength - 1];
            checkDigitNum = int.Parse(checkDigit.ToString());

            Console.WriteLine("Card Number: {0}", cardNumber);
            Console.WriteLine("Card Length: {0}", cardNumberLength);
            Console.WriteLine("Check Digit: {0}", checkDigit);

            //Luhn Algorithm
            //Step 1:Remove Check Digit
            cardNumberCheckDigitRemoved = cardNumber.Remove(cardNumberLength - 1);

            Console.WriteLine("Card Number w/ Check Digit removed: {0}", cardNumberCheckDigitRemoved);
            Console.WriteLine("New Card Length: {0}", cardNumberCheckDigitRemoved.Length);

            //Step2: Reverse Credit Card Number string
            //convert to char array then reverse
            //convert back to string
            char[] cardArray = cardNumberCheckDigitRemoved.ToCharArray();
            Array.Reverse(cardArray);
            cardNumberReversed = new string(cardArray);

            int cardNumberReversedLength = cardNumberReversed.Length;

            Console.WriteLine("Card Number Reversed: {0}", cardNumberReversed);

            //List to hold interger values for each char value in Card Number string
            List<int> cardNumberList = new List<int>();

            //convert each item in list to int type
            foreach (var item in cardNumberReversed)
            {
                cardNumberList.Add(Convert.ToInt32(item.ToString()));
            }

            //Step 3: Double digits in odd positions
            //Minus 9 from result if result greater than 9
            for (int i = 0; i < cardNumberReversedLength; i = i + 2)
            {
                Console.WriteLine("Odd Index: {0} Number: {1}", i + 1, cardNumberList[i]);
                cardNumberList[i] = cardNumberList[i] * 2;

                Console.WriteLine("Odd Index * 2 = {0}", cardNumberList[i]);

                if (cardNumberList[i] > 9)
                {
                    cardNumberList[i] = cardNumberList[i] - 9;
                }

                Console.WriteLine("Odd Index * 2 - 9 = {0}", cardNumberList[i]);
            }

            for (int i = 0; i < cardNumberReversedLength; i++)
            {
                Console.WriteLine("New Digit: {0}", cardNumberList[i]);
            }

            //Step 4: Sum all digits
            foreach (int item in cardNumberList)
            {
                totalDigit += item;
            }

            Console.WriteLine("Sum of Digits = {0}", totalDigit);

            Console.WriteLine("Sum of Digits % 10 = {0}", totalDigit % 10);

            //Step 5: Obtain the units value of Total
            //Minus unit digit from 10
            //Result used to compare against actual Check Digit
            if (totalDigit % 10 == 0)
            {
                unitTotalDigit = 0;
            }
            else
            {
                unitTotalDigit = 10 - (totalDigit % 10);
            }

            Console.WriteLine("Unit Value of Total = 10 - {0} = {1}", totalDigit, unitTotalDigit);
            Console.WriteLine("Check Digit = {0}", checkDigitNum);
            Console.WriteLine("Compare Check Digit and Unit of Total: {0} == {1}", checkDigit, unitTotalDigit);

            if (unitTotalDigit == checkDigitNum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Check Issuer of Card Number
        public string checkCardIssuer(string cardNumber)
        {
            //Issuer Identification Number (IIN) Ranges
            string patternAmex = @"^(34|37)";
            string patternMaestro = @"^(5018|5020|5038|5893|6304|6759|6761|6762|6763)";
            string patternMasterCard = @"^(51|52|53|54|55)";
            string patternSolo = @"^(6334|6767)";
            string patternSwitch = @"^(4903|4905|4911|4936|564182|633110|6333|6759)";
            string patternVisa = @"^4";
            string patternVisaElectron = @"^(4026|417500|4508|4844|4913|4917)";

            //Check IIN and Length of Card Number entered
            if (Regex.IsMatch(cardNumber, patternAmex) && cardNumberLength == 15)
            {
                return "American Express";
            }
            else if (Regex.IsMatch(cardNumber, patternMaestro) && (cardNumberLength >= 12 && cardNumberLength <= 19))
            {
                return "Maestro";
            }
            else if (Regex.IsMatch(cardNumber, patternMasterCard) && (cardNumberLength == 16))
            {
                return "MasterCard";
            }
            else if (Regex.IsMatch(cardNumber, patternSolo) && (cardNumberLength == 16 || cardNumberLength == 18 || cardNumberLength == 19))
            {
                return "Solo";
            }
            else if (Regex.IsMatch(cardNumber, patternSwitch) && (cardNumberLength == 16 || cardNumberLength == 18 || cardNumberLength == 19))
            {
                return "Switch";
            }
            else if (Regex.IsMatch(cardNumber, patternVisa))
            {
                if (Regex.IsMatch(cardNumber, patternVisaElectron) && (cardNumberLength == 16))
                {
                    return "Visa Electron";
                }
                else if (cardNumberLength == 13 || cardNumberLength == 16)
                {
                    return "Visa";
                }
            }
            return "Not Known";
        }
    }
}
