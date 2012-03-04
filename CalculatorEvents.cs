using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using CalculatorInterface;
using CalculatorOperations;

namespace CalculatorEvents
{
    public class CEvents
    {
        public static void enter_Click(object sender, EventArgs e)
        {
            string expr = COperations.preformat(CWindow.input.Text);
            expr = COperations.preCalculate(expr);
            //expr = COperations.preformat(expr);
            Console.WriteLine(expr);
            Queue<string> equation = COperations.infix_to_postfix(expr);

            /*
            for (; equation.Count > 0; )
            {
                Console.Write(equation.Dequeue());
            }
            Console.WriteLine();
            */
            
            double result = COperations.calculate(equation);
            if (CWindow.output.Text.Length != 0) 
                { CWindow.output.AppendText(Environment.NewLine); }
            CWindow.output.AppendText(CWindow.input.Text + Environment.NewLine);
            CWindow.output.AppendText(result + "");

            CWindow.input.Clear();
            CWindow.input.Focus();
            CWindow.output.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        }

        public static void degreeMenu_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MenuItem mi = sender as System.Windows.Forms.MenuItem;
            mi.Checked = true;
            if(sender is System.Windows.Forms.MenuItem)
            {
                System.Windows.Forms.MenuItem g = (System.Windows.Forms.MenuItem)sender;
            }
        }

        public static void button_Click(object sender, EventArgs e)
        {
            int caretPos = CWindow.input.SelectionStart;
            CWindow.input.Text = 
                CWindow.input.Text.Substring(0, caretPos)
                + sender.ToString()[sender.ToString().Length - 1]
                + CWindow.input.Text.Substring(caretPos + CWindow.input.SelectionLength);

            CWindow.input.Select(caretPos+1, 0);
            CWindow.input.Focus();
        }

        public static void clear_Click(object sender, EventArgs e)
        {
            CWindow.input.Clear();
            CWindow.input.Focus();
        }

        public static void delete_Click(object sender, EventArgs e)
        {
            int loc = CWindow.input.SelectionStart;
            CWindow.input.Text = CWindow.input.Text.Remove(loc,CWindow.input.SelectionLength);
            CWindow.input.SelectionStart = loc;
            CWindow.input.SelectionLength = 0;
            CWindow.input.Focus();
        }

        //possible error when text is highlighted from left to right
        //deletes(backspaces) at the start of the selection rather than where the caret is
        public static void backspace_Click(object sender, EventArgs e)
        {
            int loc = CWindow.input.SelectionStart - 1;
            if (loc >= 0)
            {
                CWindow.input.Text = CWindow.input.Text.Remove(loc, 1);
                CWindow.input.SelectionStart = loc;
            }
            CWindow.input.SelectionLength = 0;
            CWindow.input.Focus();
        }

        //error when text is selected(highlighted) -FIXED
        public static void left_Click(object sender, EventArgs e)
        {
            int loc = CWindow.input.SelectionStart - 1;
            if (loc != -1)
                CWindow.input.SelectionStart = loc;
            CWindow.input.SelectionLength = 0;
            CWindow.input.Focus();
        }

        public static void leftEnd_Click(object sender, EventArgs e)
        {
            CWindow.input.SelectionStart = 0;
            CWindow.input.SelectionLength = 0;
            CWindow.input.Focus();
        }

        public static void right_Click(object sender, EventArgs e)
        {
            int loc = CWindow.input.SelectionStart + CWindow.input.SelectionLength + 1;
            CWindow.input.SelectionStart = loc;
            CWindow.input.SelectionLength = 0;
            CWindow.input.Focus();
        }

        public static void rightEnd_Click(object sender, EventArgs e)
        {
            CWindow.input.SelectionStart = CWindow.input.Text.Length;
            CWindow.input.SelectionLength = 0;
            CWindow.input.Focus();
        }
    }
}