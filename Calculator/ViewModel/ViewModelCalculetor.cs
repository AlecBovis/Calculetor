using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator.ViewModel
{
    class ViewModelCalculetor : ModelBase
    {

        #region Propiedades
        String resultText;
        public String ResultText
        {
            get { return resultText; }
            set
            {
                if (resultText != value)
                {
                    resultText = value;
                    OnPropertyChanged();
                }
            }
        }

        int currentState;
        public int CurrentState
        {
            get { return currentState; }
            set
            {
                if (currentState != value)
                {
                    currentState = value;
                    OnPropertyChanged();
                }
            }
        }

        String mathOperator;
        public String MathOperator
        {
            get { return mathOperator; }
            set
            {
                if (mathOperator != value)
                {
                    mathOperator = value;
                    OnPropertyChanged();
                }
            }
        }

        double firstNumber;
        public double FirstNumber
        {
            get { return firstNumber; }
            set
            {
                if (firstNumber != value)
                {
                    firstNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        double secondNumber;
        public double SecondNumber
        {
            get { return secondNumber; }
            set
            {
                if (secondNumber != value)
                {
                    secondNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Comandos 

        public ICommand CurrentStateCom { protected set; get; }

        public ICommand MathOperatorCom { protected set; get; }

        public ICommand FirstNumberCom { protected set; get; }

        public ICommand SecondNumberCom { protected set; get; }

        public ICommand OnSelectNumber { protected set; get; }

        public ICommand OnSelectOperator { protected set; get; }

        public ICommand OnSelectOperatorexecute { protected set; get; }

        public ICommand OnCalculate { protected set; get; }
        

        #endregion

        public ViewModelCalculetor()
        {

            ResultText = "0";
            CurrentState = 1;

            OnSelectNumber = new Command<String>(execute: (string pressed) =>
              {

                  if (ResultText == "0" || currentState < 0)
                  {
                      ResultText = "";
                      if (currentState < 0)
                          currentState *= -1;
                  }

                  ResultText += pressed;

                  double number;
                  if (double.TryParse(this.ResultText, out number))
                  {
                      ResultText = number.ToString("N0");
                      if (currentState == 1)
                      {
                          firstNumber = number;
                      }
                      else
                      {
                          secondNumber = number;
                      }
                  }
              });

            OnSelectOperator = new Command<String>(execute: (string pressed) =>
            {
                currentState = -2;
                mathOperator = pressed;
            });

            OnSelectOperatorexecute = new Command<String>(execute: (string pressed) =>
            {
               currentState = -2;
               mathOperator = pressed;
             });

            OnCalculate = new Command<String>(execute: (string pressed) =>
            {
                if (currentState == 2)
                {
                    var result = SimpleCalculator.Calculate(firstNumber, secondNumber, mathOperator);

                    ResultText = result.ToString();
                    firstNumber = result;
                    currentState = -1;
                }
            });

        }

    }
}
