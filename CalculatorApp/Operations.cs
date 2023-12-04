using System;
using System.Linq;

namespace CalculatorApp
{
    internal class Operations
    {
        // binary expression tree class
        private class BinaryTreeNode
        {
            public string Value { get; set; }
            public BinaryTreeNode Left { get; set; }
            public BinaryTreeNode Right { get; set; }
        }

        // evaluator method
        public double EvaluateExpression(string expression)
        {
            try
            {
                // build expression tree and evaluate
                return EvaluateExpressionTree(BuildExpressionTree(expression));
            }
            catch (Exception ex)
            {
                // exception handling for invalid mathematical expression
                throw new ArgumentException("Invalid expression.", ex);
            }
        }

        // build expression tree using recursion
        private BinaryTreeNode BuildExpressionTree(string expression)
        {
            // first, handle parentheses
            int openParenIndex = expression.LastIndexOf('(');

            if (openParenIndex != -1)
            {
                int closeParenIndex = expression.IndexOf(')', openParenIndex);

                if (closeParenIndex != -1)
                {
                    // extract the expression inside the parentheses
                    string insideParenExpression = expression.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1);

                    // replace the expression inside the parentheses with a placeholder
                    string placeholder = Guid.NewGuid().ToString("N");
                    expression = expression.Replace($"({insideParenExpression})", placeholder);

                    // recursively build the expression tree with the replaced expression
                    return BuildExpressionTree(expression.Replace(placeholder, EvaluateExpression(insideParenExpression).ToString()));
                }
                else
                {
                    // exception handler for mismatched parentheses
                    throw new ArgumentException("Mismatched parentheses.");
                }
            }

            // define operators
            char[] operators = { '+', '-', '*', '/' };

            // split expression into operands
            string[] operands = expression.Split(operators, StringSplitOptions.RemoveEmptyEntries);

            // find the leftmost operator with the highest precedence
            int operatorIndex = FindLeftmostOperatorIndex(expression, operators);

            // ff there is an operator, split the expression and create a node
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
                // ff there is no operator, this is a leaf node representing a number
                if (double.TryParse(expression, out double operand))
                {
                    return new BinaryTreeNode { Value = operand.ToString() };
                }
                else
                {
                    // exception handling for invalid operands
                    throw new ArgumentException("Invalid operand.");
                }
            }
        }

        // helper method to find the leftmost operator with the highest precedence (following PEMDAS rule)
        private int FindLeftmostOperatorIndex(string expression, char[] operators)
        {
            int highestPrecedence = int.MaxValue;
            int operatorIndex = -1;

            for (int i = 0; i < expression.Length; i++)
            {
                if (operators.Contains(expression[i]))
                {
                    int precedence = GetOperatorPrecedence(expression[i]);
                    if (precedence <= highestPrecedence)  // prioritize leftmost operators
                    {
                        highestPrecedence = precedence;
                        operatorIndex = i;
                    }
                }
            }

            return operatorIndex;
        }

        // helper method to get the precedence of an operator
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

        // recursive method to evaluate the expression tree
        private double EvaluateExpressionTree(BinaryTreeNode root)
        {
            if (root == null)
            {
                // return 0 for an invalid tree
                return 0;
            }

            if (double.TryParse(root.Value, out double operand))
            {
                // if leaf node, return the operand
                return operand;
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
                    // exception handling for an invalid operator
                    throw new InvalidOperationException("Invalid operator.");
            }
        }

        public double Equals(string expression)
        {
            return EvaluateExpression(expression);
        }
    }
}
