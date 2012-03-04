using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace CalculatorOperations
{
    public class COperations
    {
        static int getOpVal(char c)
        {
            if (c == '(')
                return 1;
            else if (c == ')')
                return 2;
            else if (c == '+' || c == '-')
                return 3;
            else if (c == '*' || c == '/' || c == '%')
                return 4;
            else if (c == '^')
                return 5;
            /*
            else if (c == '!')
                return 6;
            */
            /*
            else if (c == "sin" || c == "cos" || c == "tan" ||
                c == "sec" || c == "csc" || c == "cot" || 
                c == "asin" || c == "acos" || c == "atan")
            {
                return 9;
            }
            */

            return -1;
        }

        //Factorial may not work ->[calculate factorials beforehand]
        public static Queue<string> infix_to_postfix(string infix)
        {
            Queue<string> postfix = new Queue<string>();
            Stack<char> operators = new Stack<char>();
            string parse = infix;

            for (int i = 0; i < parse.Length; i++)
            {
                char curr = parse[i];
                int currOpVal = getOpVal(curr);
                int topOpVal = 0;
                if (operators.Count > 0)
                    topOpVal = getOpVal(operators.Peek());

                if (currOpVal > 0)
                {
                    if (i!= 0) // Enqueue the operand(number) before the operator
                        postfix.Enqueue(parse.Substring(0, i));

                    if (currOpVal == 2) //close parenthesis
                    {
                        for (; operators.Peek() != '('; )
                        {
                            postfix.Enqueue(Convert.ToString(operators.Pop()));
                        }
                        operators.Pop();
                        if(parse.Length == i+1) // if '(' is the end of the expression
                        {
                            for(; operators.Count > 0;)
                                postfix.Enqueue(Convert.ToString(operators.Pop()));
                        }
                    }
                    else if (currOpVal > topOpVal || 
                        topOpVal == 1 || currOpVal == 1) //higher precedence operator
                    {
                        operators.Push(curr);
                    }
                    else if (currOpVal == 5 && topOpVal == 5) //exponents
                    {
                        operators.Push(curr);
                    }
                    else //lower precedence operator
                    {

                        for (; operators.Count > 0 && 
                            (currOpVal <= getOpVal(operators.Peek()) && operators.Peek() != '('); )
                        {
                            postfix.Enqueue(Convert.ToString(operators.Pop()));
                        }
                        operators.Push(curr);
                    }

                    parse = parse.Substring(i + 1);
                    i = -1;
                    continue;
                }
                else if ((Char.IsDigit(curr) || curr == '.') &&
                    (parse.LastIndexOfAny(new char[8] {'+', '-', '*', '/', '%', '^', '(', ')'}) < i))
                {
                    postfix.Enqueue(parse);
                    for (; operators.Count > 0; )
                    {
                        postfix.Enqueue(Convert.ToString(operators.Pop()));
                    }
                    break;
                }
                /*
                else if (Char.IsLetter(curr))
                {
                    if(parse.Length > i+3)
                    {
                        string func = parse.Substring(i, 3);
                        if(func == "sin")
                        {

                        }
                        else if (func == "cos")
                        {

                        }
                    }
                }
                */
            }

            return postfix;
        }

        public static string preformat(string format)
        {
            format = format.Trim();
            format = format.Replace(" ", "");

            //include constants(e, pi, g) and functions(sin, cos, tan) when adding in '*'
            for (int i = format.IndexOf('('); i != -1; i = format.IndexOf('(', i+1) )
            {
                if(i != 0 && (Char.IsDigit(format[i-1]) || format[i-1] == ')'))
                {
                    format = format.Insert(i, "*");
                    i++;
                }
            }
            for (int i = format.IndexOf(')'); i != -1; i = format.IndexOf(')', i+1) )
            {
                if(format.Length-1 > i && Char.IsDigit(format[i+1]))
                    format = format.Insert(i+1, "*");
            }

            for (int i = format.IndexOf('!'); i != -1; i = format.IndexOf('!', i+1))
            {
                if(format.Length-1 > i && 
                    (Char.IsDigit(format[i+1]) || format[i+1] == '(')  )
                {
                    format = format.Insert(i + 1, "*");
                    i++;
                }
            }

            for (int i = format.IndexOfAny(new char[3] {'e', 'π', 'g'}); i != -1;
                i = format.IndexOfAny(new char[3] {'e', 'π', 'g'}, i))
            {
                if(i != 0 && 
                    (Char.IsDigit(format[i-1]) || 
                    format[i-1] == 'e' || format[i-1] == 'π' || format[i-1] == 'g' || 
                    format[i-1] == ')') )
                {
                    format = format.Insert(i, "*");
                    i++;
                    continue;
                }
                if(i != format.Length-1 &&
                    (Char.IsDigit(format[i+1]) ||
                    format[i+1] == 'e' || format[i+1] == 'π' || format[i+1] == 'g' || 
                    format[i+1] == '('))
                {
                    format = format.Insert(i + 1, "*");
                    i++;
                    continue;
                }
                i++;
            }

            //doesn't work with secant function
            /* Possible solution
            format = format.Replace("sec", "AAA");
            format = format.Replace("e", Math.E + "");
            format = format.Replace("AAA", "sec");
            */

            format = format.Replace("e", Math.E + "");
            format = format.Replace("π", Math.PI + "");
            format = format.Replace("g", "9.80665");

            return format;
        }
        //(10!)/(10-7)! gives error [ (10)! ]
        public static string preCalculate(string exp)
        {
            for (int i = exp.IndexOf('!'); i != -1; i = exp.IndexOf('!'))
            {
                double temp = 0;
                int ntp = 0;
                for (int j = i-1; j >= 0; j--)
                {
                    if (j == i-1 && exp[j] == ')')
                    {
                        //continue;
                        //DELETE in favour of precalculating parenthetical expressions?
                    }
                    if (!Char.IsDigit(exp[j]) && exp[j] != '.')
                    {
                        temp = Convert.ToDouble(exp.Substring(j + 1, i - j - 1));
                        temp = factorial(temp);
                        exp = exp.Remove(j + 1, i - j);
                        exp = exp.Insert(j + 1, temp + "");
                        ntp = j + 1 + temp.ToString().Length;
                        if (exp.Length > ntp && Char.IsDigit(exp[ntp]))
                            exp = exp.Insert(ntp, "*");
                        break;
                    }
                    else if (j == 0)
                    {
                        temp = Convert.ToDouble(exp.Substring(0, i));
                        temp = factorial(temp);
                        exp = exp.Remove(0, i + 1);
                        exp = exp.Insert(0, temp + "");
                        ntp = temp.ToString().Length;
                        if (exp.Length > ntp && Char.IsDigit(exp[ntp]))
                            exp = exp.Insert(ntp, "*");
                        break;
                    }
                }
            }

            if(exp.Contains("NaN"))
                return "NaN";

            return exp;
        }

        //Error:    1+3+4+5*3*(1-5^2)*(3-1)/(3.141592)     FIXED
        //Postfix:  13+4+53*152^-*31-*3.141592/+
        public static double calculate(Queue<string> expression)
        {
            double temp = 0;
            string top;
            Stack<double> operands = new Stack<double>();

            for (; expression.Count > 0 ; )
            {
                top = expression.Peek();
                if (top == "+" || top == "-" || top == "*" || top == "/" || top == "%" || top == "^")
                {
                    double first = operands.Pop();
                    double second = operands.Pop();

                    if (top == "+")
                        temp = second + first;
                    else if (top == "-")
                        temp = second - first;
                    else if (top == "*")
                        temp = second * first;
                    else if (top == "/")
                        temp = second / first;
                    else if (top == "%")
                        temp = second % first;
                    else if (top == "^")
                        temp = Math.Pow(second, first);

                    operands.Push(temp);
                    expression.Dequeue();
                }
                else if (top == "!")
                {
                    temp = factorial(operands.Pop());
                    operands.Push(temp);
                    expression.Dequeue();
                }
                else
                {
                    operands.Push(Convert.ToDouble(expression.Dequeue()));
                }
            }

            return operands.Pop();
        }

        public static double factorial(double arg)
        {
            double product = 1;
            for (; arg > 0; arg--)
            {
                product *= arg;
            }
            if (arg != 0)
                return Double.NaN;

            return product;
        }


        public static string evalParenthesis(string expr)
        {
            for (int i = expr.IndexOf('('); i != -1; i = expr.IndexOf('('))
            {

            }

            return expr;
        }
    }
}