using System;
using static Calculator_Project.MainWindow;

namespace Calculator_Project
{
    
}
public static class MathService
{
    public static double EqualsEvent(double firstnumber, double secondnumber, SelectedOperator selectedOperator)
    {
        switch (selectedOperator)
        {
            case SelectedOperator.Addition:
                return firstnumber + secondnumber;
            case SelectedOperator.Subtraction:
                return firstnumber - secondnumber;
            case SelectedOperator.Multiplication:
                return firstnumber * secondnumber;
            case SelectedOperator.Division:
               
                return firstnumber != 0 ? firstnumber / secondnumber : double.NaN;
            default:
                throw new ArgumentException("Invalid operator");
        }

    }
}
