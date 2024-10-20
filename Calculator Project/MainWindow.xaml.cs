using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator_Project


{
    public partial class MainWindow : Window
    {
        Cursor C1 = new Cursor(Application.GetResourceStream(new Uri("C1.cur", UriKind.Relative)).Stream);

        double lastNumber;
        double result;
        SelectedOperator selectedOperator;
        bool isNewNumber = true;
        

        public MainWindow()
        {
            InitializeComponent();
            Window.Cursor = C1;
            displayTextBox.Cursor = C1;
        }

        public enum SelectedOperator
        {
            Addition,
            Subtraction,
            Multiplication,
            Division
        }


        private void ClickEvent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Content.ToString();

            
            displayTextBox.Text += buttonText;

            ChangeButtonColor(sender, e);
        }


        private void OperatorEvent(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            string operatorText = clickedButton.Content.ToString();

            if (!displayTextBox.Text.Contains("+") &&
                !displayTextBox.Text.Contains("_") &&
                !displayTextBox.Text.Contains("*") &&
                !displayTextBox.Text.Contains("/"))
            {

                if (double.TryParse(displayTextBox.Text, out lastNumber))
                { 
                    switch (operatorText)
                    {
                        case "+":
                            selectedOperator = SelectedOperator.Addition;
                            Debug.WriteLine($"{operatorText}");
                            break;
                        case "_":
                            selectedOperator = SelectedOperator.Subtraction;
                            Debug.WriteLine($"{operatorText}");
                            break;
                        case "*":
                            selectedOperator = SelectedOperator.Multiplication;
                            Debug.WriteLine($"{operatorText}");
                            break;
                        case "/":
                            selectedOperator = SelectedOperator.Division;
                            Debug.WriteLine($"{operatorText}");
                            break;
                        default:
                            break;
                    }

                    displayTextBox.Text += operatorText;

                   

                }
            }

            ChangeButtonColor(sender, e);
        }



        private void EqualsEvent(object sender, RoutedEventArgs e)
        {
            string displayText = displayTextBox.Text;

            int operatorIndex = displayText.LastIndexOfAny(new char[] { '+', '_', '*', '/' });

            if (operatorIndex != -1 && operatorIndex < displayText.Length - 1)
            {
                
                string secondNumberText = displayText.Substring(operatorIndex + 1);

              
                if (double.TryParse(secondNumberText, out double secondNumber))
                {
                    
                result = MathService.EqualsEvent(lastNumber, secondNumber, selectedOperator);
                displayTextBox.Text = result.ToString();
                   
                   lastNumber = result;
                   isNewNumber = true;
                   selectedOperator = 0;
                }
               
            }

            ChangeButtonColor(sender, e);

        }



        private void ClearInput(object sender, RoutedEventArgs e)
        {
            displayTextBox.Text = null;
            lastNumber = 0;
            result = 0;
            selectedOperator = 0; 
            isNewNumber = true;   

            ChangeButtonColor(sender, e);
        }

        private void DeleteInput(object sender, RoutedEventArgs e)
        {
            string currentText = displayTextBox.Text;

            if (!string.IsNullOrEmpty(currentText))
            {
                displayTextBox.Text = currentText.Substring(0, currentText.Length - 1);
            }
            ChangeButtonColor(sender, e);

        }

        private void SignEvent(object sender, RoutedEventArgs e)
        {
            
                if (double.TryParse(displayTextBox.Text, out double currentNumber))
                {
                    
                    currentNumber = -currentNumber;

                   
                    displayTextBox.Text = currentNumber.ToString();
                }
              
            ChangeButtonColor(sender, e);
        }

        private void DecimalEvent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Content.ToString();

            displayTextBox.Text += buttonText;
            isNewNumber = false;
            
            ChangeButtonColor(sender, e);
        }
        private void ChangeButtonColor(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Background is SolidColorBrush brush && brush.Color == Colors.LightPink)
                {
                    button.Background = new SolidColorBrush(Colors.Transparent);
                }
                else
                {
                    button.Background = new SolidColorBrush(Colors.LightPink);
                }
            }
        }
    }
}

