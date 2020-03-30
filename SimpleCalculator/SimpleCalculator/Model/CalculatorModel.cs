using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleCalculator.Model
{
    public class CalculatorModel
    {
        private string firstOperand;
        private string secondOperand;
        private string operation;
        private double result;
        private string display;

        public string FirstOperand
        {
            get { return firstOperand; }
            set { firstOperand = value; }
        }
        public string SecondOperand
        {
            get { return secondOperand; }
            set { secondOperand = value; }
        }
        public string Operation
        {
            get { return operation; }
            set { operation = value; }
        }
        public double Result
        {
            get { return result; }
            set { result = value; }
        }
        public string Display
        {
            get { return display; }
            set { display = value; }
        }

        public string calculateResult(string firstOperator, string secondOperator , string selectedOperator)
        {
            try
            {
                switch (selectedOperator)
                {
                    case "+": Result = double.Parse(firstOperator) + double.Parse(secondOperator); break;
                    case "-": Result = double.Parse(firstOperator) - double.Parse(secondOperator); break;
                    case "*": Result = double.Parse(firstOperator) * double.Parse(secondOperator); break;
                    case "/": Result = double.Parse(firstOperator) / double.Parse(secondOperator); break;
                }
                return Result.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show("{0}",e.ToString());
                return "0";
            }
        }
    }
}