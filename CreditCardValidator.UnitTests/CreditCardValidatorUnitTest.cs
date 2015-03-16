using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreditCardValidator;

namespace CreditCardValidator.UnitTests
{
    [TestClass]
    public class CreditCardValidatorUnitTests
    {
        [TestMethod]
        public void CheckCardNumberIsNumeric()
        {
            CreditCard cardTest1 = new CreditCard("1159785426");
            Assert.IsTrue(cardTest1.checkCardNumber(cardTest1.CardNumber));
        }

        [TestMethod]
        public void CheckCardNumberIsNonNumeric()
        {
            CreditCard cardTest2 = new CreditCard("1sewrwe4regda23");
            Assert.IsFalse(cardTest2.checkCardNumber(cardTest2.CardNumber));
        }


        CreditCard cardTest3 = new CreditCard("4929178188847820");

        [TestMethod]
        public void CheckValidCardIsValid()
        {
            Assert.IsTrue(cardTest3.validateCardNumber(cardTest3.CardNumber));
        }

        [TestMethod]
        public void CheckCardIssuerVisa()
        {
            Assert.AreEqual("Visa", cardTest3.checkCardIssuer(cardTest3.CardNumber));
        }

        [TestMethod]
        public void CheckCardIssuerMaestro()
        {
            CreditCard cardTest4 = new CreditCard("5038390278970363");
            Assert.AreEqual("Maestro", cardTest4.checkCardIssuer(cardTest4.CardNumber));
        }

        [TestMethod]
        public void CheckNonValidCardIsNonValid()
        {
            CreditCard cardTest5 = new CreditCard("1234567989");

            Assert.IsFalse(cardTest5.validateCardNumber(cardTest5.CardNumber));
        }
    }
}
