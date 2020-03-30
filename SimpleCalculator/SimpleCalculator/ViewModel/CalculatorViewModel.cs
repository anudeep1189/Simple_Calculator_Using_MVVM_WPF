using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleCalculator.Model;
using SimpleCalculator.Command;
using System.Windows.Input;
using System.Windows;
using System.Collections;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using SimpleCalculator.View;

namespace SimpleCalculator.ViewModel
{
    class CalculatorViewModel : INotifyPropertyChanged
    {
        #region Constructor
        public CalculatorViewModel()
        {

        }
        #endregion
        string selectedOperator = string.Empty;

        #region Fields And Properties
        private string display;
        private string displayCompleteEquation = "0";

        private List<string> completeEquation = new List<string>();

        public string Display
        {
            get { return display; }
            set { display = value; OnPropertyChanged("Display"); }

        }

        public List<string> CompleteEquation
        {
            get { return completeEquation; }
            set { completeEquation = value; }
        }

        public string DisplayCompleteEquation
        {
            get { return displayCompleteEquation; }
            set { displayCompleteEquation = value; OnPropertyChanged("DisplayCompleteEquation"); }
        }


        private string firstOperand; 
        private string secondOperand;
        private bool isNewOperand = false;

        StringBuilder buildDisplayInput = new StringBuilder();
        #endregion

        #region INotifyPropertyChnage implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        CalculatorModel objModel = new CalculatorModel(); //object of model //TODO:try using dependencyInjection

        private ICommand operatorAndRelatedFunction;
        public ICommand OperatorAndRelatedFunction
        {
            get
            {
                if (operatorAndRelatedFunction == null)
                {
                    operatorAndRelatedFunction = new RelayCommand(ExecuteaddOperandAndOperator, CanExecuteaddOperandAndOperator, false);

                }
                return operatorAndRelatedFunction;
            }
        }

        private void ExecuteaddOperandAndOperator(object parameter)
        {
            //switch case to check any special function in default assign value to display property
            try
            {
                switch (parameter.ToString())
                {
                    case "c":   //clear and reset everything
                        buildDisplayInput.Clear();
                        CompleteEquation.Clear();
                        firstOperand = null;
                        selectedOperator = string.Empty;
                        Display = string.Empty;
                        DisplayCompleteEquation = "0";
                        break;
                    case "+/-":
                        if (Display != string.Empty || Display != "0")
                        {
                            if (!Display.Contains("-"))
                            {
                                Display = "-" + Display;
                            }
                            else
                            {
                                Display = Display.Remove(0, 1);
                            }
                        }
                        break;
                    case "%":
                        double percentageConverstion = double.Parse(Display) / 100;
                        Display = percentageConverstion.ToString();
                        break;
                    case ".":
                        Display = Display + ".";
                        break;
                    default:

                        if (isNewOperand == false)
                        {
                            Display = Display + parameter.ToString();
                        }
                        else if (isNewOperand == true)
                        {
                            Display = string.Empty;
                            Display = Display + parameter.ToString();
                            isNewOperand = false;
                        }

                        //creating complete equation for displaying 
                        CompleteEquation.Add(parameter.ToString());
                        buildDisplayInput.Clear();
                        foreach (string dis in CompleteEquation)
                        {
                            buildDisplayInput.Append(dis);
                        }
                        DisplayCompleteEquation = buildDisplayInput.ToString();
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("{0}",e.ToString());
               
            }
        }
        
            
        private bool CanExecuteaddOperandAndOperator(object parameter)
        {
            return true;
        }

        #region  BasicOperator
        private ICommand basicOperator;
        public ICommand BasicOperator
        {
            get
            {
                if (basicOperator == null)
                {
                    basicOperator = new RelayCommand(ExecuteBasicOperator, CanExecuteBasicOperator, false);
                }
                return basicOperator;
            }
        }

        private void ExecuteBasicOperator(object parameter)
        {
            try
            {
                isNewOperand = true;
                if (parameter.ToString() != "=" && selectedOperator == string.Empty)
                {
                    selectedOperator = parameter.ToString();
                }

                if (firstOperand == null)
                {
                    firstOperand = Display;
                }
                else
                {
                    secondOperand = Display;
                    if (firstOperand != null && secondOperand != null)
                    {
                        firstOperand = objModel.calculateResult(firstOperand, secondOperand, selectedOperator);
                    }
                    Display = objModel.Result.ToString();

                    if (parameter.ToString() != "=")
                    {
                        selectedOperator = parameter.ToString();
                    }
                }

                if (parameter.ToString() == "=")
                {
                    //resetting the value to default
                    buildDisplayInput.Clear();
                    CompleteEquation.Clear();
                    firstOperand = null;
                    selectedOperator = string.Empty;

                    //replacing complete eqaution value with the results after "="
                    DisplayCompleteEquation = objModel.Result.ToString();
                    CompleteEquation.Add(DisplayCompleteEquation);
                }
                else
                {
                    CompleteEquation.Add(parameter.ToString());
                    buildDisplayInput.Clear();
                    foreach (string dis in CompleteEquation)
                    {
                        buildDisplayInput.Append(dis);
                    }
                    DisplayCompleteEquation = buildDisplayInput.ToString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("{0}",e.ToString());
            }
        }

        private bool CanExecuteBasicOperator(object parameter)
        {
            return true;
        }
    }

    #endregion



}