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
    }
}
