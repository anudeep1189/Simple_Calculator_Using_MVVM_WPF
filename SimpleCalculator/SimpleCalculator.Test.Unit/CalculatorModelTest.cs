using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleCalculator.Model;


namespace SimpleCalculator.Test.Unit
{
    

    [TestClass]
    public class CalculatorModelTest
    {

        private readonly CalculatorModel obj = new CalculatorModel();



        //TODO:Write other test case for other operators.

       [TestMethod]
        public void AddTwoNumberAndReturnResult()
        {
            string firstOperator = "1";
            string secondOperator = "2";
            string selectedOperator = "+";

            double result = double.Parse(obj.calculateResult(firstOperator, secondOperator, selectedOperator));

            Assert.AreEqual(3, result);
        }
    }
}
