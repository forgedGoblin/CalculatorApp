using System;
using System.Linq;

namespace CalculatorApp
{
    internal class Operations
    {
        private class BinaryTreeNode
        {
            public string Value { get; set; }
            public BinaryTreeNode Left { get; set; }
            public BinaryTreeNode Right { get; set; }
        }

        public double EvaluateExpression(string expression)
        {
            try
            {
                return EvaluateExpressionTree(BuildExpressionTree(expression));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid expression.", ex);
            }
        }

        private BinaryTreeNode BuildExpressionTree(string expression)
        {
            // First, handle parentheses
            int openParenIndex = expression.LastIndexOf('(');

            if (openParenIndex != -1)
            {
                int closeParenIndex = expression.IndexOf(')', openParenIndex);

                if (closeParenIndex != -1)
                {
                    // Extract the expression inside the parentheses
                    string insideParenExpression = expression.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1);

                    // Replace the expression inside the parentheses with a placeholder
                    string placeholder = Guid.NewGuid().ToString("N");
                    expression = expression.Replace($"({insideParenExpression})", placeholder);

                    // Recursively build the expression tree with the replaced expression
                    return BuildExpressionTree(expression.Replace(placeholder, EvaluateExpression(insideParenExpression).ToString()));
                }
                else
                {
                    throw new ArgumentException("Mismatched parentheses.");
                }
            }

            char[] operators = { '+', '-', '*', '/' };

            // split expression into operands
            string[] operands = expression.Split(operators, StringSplitOptions.RemoveEmptyEntries);

            // find the leftmost operator with the highest precedence
            int operatorIndex = FindLeftmostOperatorIndex(expression, operators);

            // If there is an operator, split the expression and create a node
            if (operatorIndex != -1)
            {
                string currentOperator = expression[operatorIndex].ToString();

                BinaryTreeNode node = new BinaryTreeNode
                {
                    Value = currentOperator,
                    Left = BuildExpressionTree(expression.Substring(0, operatorIndex)),
                    Right = BuildExpressionTree(expression.Substring(operatorIndex + 1))
                };

                return node;
            }
            else
            {
                // If there is no operator, this is a leaf node representing a number
                if (double.TryParse(expression, out double operand))
                {
                    return new BinaryTreeNode { Value = operand.ToString() };
                }
                else
                {
                    throw new ArgumentException("Invalid operand.");
                }
            }
        }

        private int FindLeftmostOperatorIndex(string expression, char[] operators)
        {
            int highestPrecedence = int.MaxValue;
            int operatorIndex = -1;

            for (int i = 0; i < expression.Length; i++)
            {
                if (operators.Contains(expression[i]))
                {
                    int precedence = GetOperatorPrecedence(expression[i]);
                    if (precedence <= highestPrecedence)  // Change to <= to prioritize leftmost operators
                    {
                        highestPrecedence = precedence;
                        operatorIndex = i;
                    }
                }
            }

            return operatorIndex;
        }

        private int GetOperatorPrecedence(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return 0; // Default precedence for non-operators
            }
        }

        private double EvaluateExpressionTree(BinaryTreeNode root)
        {
            if (root == null)
            {
                return 0; // or throw an exception for an invalid tree
            }

            if (double.TryParse(root.Value, out double operand))
            {
                return operand; // Leaf node, return the operand
            }

            double leftResult = EvaluateExpressionTree(root.Left);
            double rightResult = EvaluateExpressionTree(root.Right);

            switch (root.Value)
            {
                case "+":
                    return leftResult + rightResult;
                case "-":
                    return leftResult - rightResult;
                case "*":
                    return leftResult * rightResult;
                case "/":
                    return leftResult / rightResult;
                default:
                    throw new InvalidOperationException("Invalid operator.");
            }
        }

        public double Equals(string expression)
        {
            return EvaluateExpression(expression);
        }
    }
}
