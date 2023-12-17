// Ignore Spelling: Polska

namespace SemesterProjectUI.Models.Equations
{
    public class PolskaEquation : BaseEquation
    {
        public PolskaEquation(string equation) : base(equation)
        {
        }

        public override void Solve()
        {
            Queue<char> postfix = ConvertToPostfix(Equation!);
            try
            {
                Answer = EvaluatePostfix(postfix);
            }
            catch
            {
                Answer = Double.PositiveInfinity;
            }
        }

        private Queue<char> ConvertToPostfix(string expression)
        {
            Stack<char> operators = new Stack<char>();
            Queue<char> output = new Queue<char>();

            foreach (char c in expression)
            {
                if (char.IsDigit(c))
                {
                    output.Enqueue(c);
                }
                else if (IsOperator(c))
                {
                    while (operators.Count > 0 && IsOperator(operators.Peek()) && Precedence(operators.Peek()) >= Precedence(c))
                    {
                        output.Enqueue(operators.Pop());
                    }
                    operators.Push(c);
                }
            }

            while (operators.Count > 0)
            {
                output.Enqueue(operators.Pop());
            }

            return output;
        }

        private double EvaluatePostfix(Queue<char> postfix)
        {
            Stack<double> operands = new Stack<double>();

            while (postfix.Count > 0)
            {
                char token = postfix.Dequeue();
                if (char.IsDigit(token))
                {
                    operands.Push(double.Parse(token.ToString()));
                }
                else if (IsOperator(token))
                {
                    double operand2 = operands.Pop();
                    double operand1 = operands.Pop();
                    double result = PerformOperation(operand1, operand2, token);
                    operands.Push(result);
                }
            }

            return operands.Pop();
        }

        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        private int Precedence(char c)
        {
            if (c == '+' || c == '-')
            {
                return 1;
            }
            else if (c == '*' || c == '/')
            {
                return 2;
            }
            return 0;
        }

        private double PerformOperation(double operand1, double operand2, char op)
        {
            switch (op)
            {
                case '+':
                    return operand1 + operand2;
                case '-':
                    return operand1 - operand2;
                case '*':
                    return operand1 * operand2;
                case '/':
                    if (operand2 != 0)
                    {
                        return operand1 / operand2;
                    }
                    else
                    {
                        throw new DivideByZeroException("Деление на ноль!");
                    }
                default:
                    throw new ArgumentException("Неверная операция");
            }
        }
    }
}
