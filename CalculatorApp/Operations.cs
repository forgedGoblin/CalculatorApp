using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    internal class Operations
    {
        public string operation { get; set; }
        public double num1 { get; set; }
        public double num2 { get; set; }

        public double PerformCalculation()
        {
            // perform calculations based on operation
            switch (operation)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "x":
                    return num1 * num2;
                case "/":
                    return num1 / num2;
                default:
                    throw new InvalidOperationException("Invalid operation.");
            }
        }

        public double Equals(string expression)
        {
            char[] operators = { '+', '-', 'x', '/' };

            // split expression into operands
            string[] operands = expression.Split(operators, StringSplitOptions.RemoveEmptyEntries);

            // check if expression format is correct (operand operator operand)
            if (operands.Length == 2 && expression.Any(c => operators.Contains(c)))
            {
                // find operator
                char operation = expression.First(c => operators.Contains(c));

                // parse string to double
                if (double.TryParse(operands[0], out double num1) &&
                    double.TryParse(operands[1], out double num2))
                {
                    // set values
                    this.num1 = num1;
                    this.num2 = num2;
                    this.operation = operation.ToString();

                    // call method
                    return PerformCalculation();
                }
                else
                {
                    throw new ArgumentException("Invalid operands.");
                }
            }
            else
            {
                throw new ArgumentException("Invalid expression.");
            }
        }
    }
}
