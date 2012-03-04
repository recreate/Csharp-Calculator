using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using CalculatorEvents;

namespace CalculatorInterface
{

    public class mainRun
    {
        public static void Main()
        {
            System.Windows.Forms.Application.Run(new CWindow("CS-Calculator"));
        }
    }

    public class CWindow : System.Windows.Forms.Form
    {
        public static System.Windows.Forms.TextBox input;
        public static TextBox output;
        public static string angleUnit = "rad";

        public CWindow(string name)
        {
            this.Text = name;
            this.Size = new System.Drawing.Size(405, 550);
            
            System.Windows.Forms.MainMenu menu = new System.Windows.Forms.MainMenu();
            System.Windows.Forms.MenuItem file = new System.Windows.Forms.MenuItem("File");
            MenuItem edit = new MenuItem("Edit");
            MenuItem tools = new MenuItem("Tools");
            MenuItem advanced = new MenuItem("Advanced");

            MenuItem settings = new MenuItem("Settings");
                MenuItem angleMeasurement = new MenuItem("Angle");
                    MenuItem degrees = new MenuItem("Degree");
                    angleMeasurement.MenuItems.Add(degrees);
                    MenuItem radians = new MenuItem("Radians");
                    radians.Checked = true;
                    angleMeasurement.MenuItems.Add(radians);
                    MenuItem gradians = new MenuItem("Gradians");
                    angleMeasurement.MenuItems.Add(gradians);
                settings.MenuItems.Add(angleMeasurement);
            MenuItem help = new MenuItem("Help");
                MenuItem about = new MenuItem("About");
                help.MenuItems.Add(about);

            menu.MenuItems.Add(file);
            menu.MenuItems.Add(edit);
            menu.MenuItems.Add(tools);
            menu.MenuItems.Add(advanced);
            menu.MenuItems.Add(settings);
            menu.MenuItems.Add(help);
            this.Menu = menu;
            
            //TextBoxes
            output = new System.Windows.Forms.TextBox();
            output.Font = new Font("Courier New", 10);
            output.Multiline = true;
            output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            output.Location = new System.Drawing.Point(20, 10);
            output.MinimumSize = new System.Drawing.Size(350, 110);
            output.MaximumSize = new System.Drawing.Size(350, 110);
            this.Controls.Add(output);

            input = new System.Windows.Forms.TextBox();
            input.Font = new Font("Courier New", 10);
            input.Multiline = true;
            input.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            input.Location = new System.Drawing.Point(20, 125);
            input.MinimumSize = new System.Drawing.Size(350, 45);
            input.MaximumSize = new System.Drawing.Size(350, 45);
            this.Controls.Add(input);


            System.Windows.Forms.Label angle = new Label();
            angle.Font = new Font("Arial", 10);
            angle.Text = "RAD";
            angle.TextAlign = ContentAlignment.MiddleCenter;
            angle.BorderStyle = BorderStyle.FixedSingle;
            angle.Size = new Size(50, 20);
            angle.Location = new Point(20, 180);
            this.Controls.Add(angle);


            //Buttons
            System.Windows.Forms.Button enter = new System.Windows.Forms.Button();
            enter.Font = new System.Drawing.Font("Arial", 12);
            enter.Text = "=";
            enter.Size = new System.Drawing.Size(50, 30);
            enter.Location = new System.Drawing.Point(325, 445);
            enter.Click += new EventHandler(CEvents.enter_Click);
            this.Controls.Add(enter);

            Button addition = new Button();
            addition.Font = new System.Drawing.Font("Arial", 12);
            addition.Text = "+";
            addition.Size = new Size(40, 30);
            addition.Location = new System.Drawing.Point(335, 410);
            addition.Click += new EventHandler(CEvents.button_Click);
            this.Controls.Add(addition);

            Button subtraction = new Button();
            subtraction.Font = new System.Drawing.Font("Arial", 12);
            subtraction.Text = "-";
            subtraction.Size = new Size(40, 30);
            subtraction.Location = new Point(335, 375);
            subtraction.Click += new EventHandler(CEvents.button_Click);
            this.Controls.Add(subtraction);

            Button multiplication = new Button();
            multiplication.Font = new System.Drawing.Font("Arial", 12);
            multiplication.Text = "*";
            multiplication.Size = new System.Drawing.Size(40, 30);
            multiplication.Location = new System.Drawing.Point(335, 340);
            multiplication.Click += new EventHandler(CEvents.button_Click);
            this.Controls.Add(multiplication);

            Button division = new Button();
            division.Font = new System.Drawing.Font("Arial", 12);
            division.Text = "/";
            division.Size = new System.Drawing.Size(40, 30);
            division.Location = new System.Drawing.Point(335, 305);
            division.Click += new EventHandler(CEvents.button_Click);
            this.Controls.Add(division);

            Button exponentiation = new Button();
            exponentiation.Font = new Font("Arial", 12);
            exponentiation.Text = "^";
            exponentiation.Size = new Size(40, 30);
            exponentiation.Location = new Point(335, 270);
            exponentiation.Click += new EventHandler(CEvents.button_Click);
            this.Controls.Add(exponentiation);

            Button clear = new Button();
            clear.Font = new Font("Arial", 12);
            clear.Text = "CLR";
            clear.Size = new Size(50, 25);
            clear.Location = new Point(325, 230);
            clear.Click += new EventHandler(CEvents.clear_Click);
            this.Controls.Add(clear);

            Button delete = new Button();
            delete.Font = new Font("Arial", 12);
            delete.Text = "DEL";
            delete.Size = new Size(50, 25);
            delete.Location = new Point(270, 230);
            delete.Click += new EventHandler(CEvents.delete_Click);
            this.Controls.Add(delete);

            Button backspace = new Button();
            backspace.Font = new Font("Arial", 10);
            backspace.Text = "BKSP";
            backspace.Size = new Size(50, 25);
            backspace.Location = new Point(215, 230);
            backspace.Click += new EventHandler(CEvents.backspace_Click);
            this.Controls.Add(backspace);

            Button answer = new Button();
            answer.Font = new Font("Arial", 12);
            answer.Text = "ANS";
            answer.Size = new Size(50, 25);
            answer.Location = new Point(160, 230);
            this.Controls.Add(answer);

            Button previous = new Button();
            previous.Font = new Font("Arial", 10);
            previous.Text = "PREV";
            previous.Size = new Size(50, 25);
            previous.Location = new Point(20, 230);
            this.Controls.Add(previous);

            Button degree = new Button();
            degree.Font = new Font("Arial", 10);
            degree.Text = "DEG";
            degree.Size = new Size(50,  25);
            degree.Location = new Point(20, 180);
            //this.Controls.Add(degree);

            Button right = new Button();
            right.Font = new Font("Arial", 16);
            right.Text = "→";
            right.Size = new Size(50, 20);
            right.Location = new Point(325, 180);
            right.Click += new EventHandler(CEvents.right_Click);
            this.Controls.Add(right);

            Button rightEnd = new Button();
            rightEnd.Font = new Font("Arial", 16);
            rightEnd.Text = "→|";
            rightEnd.Size = new Size(50, 20);
            rightEnd.Location = new Point(325, 205);
            rightEnd.Click += new EventHandler(CEvents.rightEnd_Click);
            this.Controls.Add(rightEnd);

            Button left = new Button();
            left.Font = new Font("Arial", 16);
            left.Text = "←";
            left.Size = new Size(50, 20);
            left.Location = new Point(270, 180);
            left.Click += new EventHandler(CEvents.left_Click);
            this.Controls.Add(left);

            Button leftEnd = new Button();
            leftEnd.Font = new Font("Arial", 16);
            leftEnd.Text = "|←";
            leftEnd.Size = new Size(50, 20);
            leftEnd.Location = new Point(270, 205);
            leftEnd.Click += new EventHandler(CEvents.leftEnd_Click);
            this.Controls.Add(leftEnd);

            //Numberpad
            Button zero = new Button();
            zero.Font = new Font("Arial", 12);
            zero.Text = "0";
            zero.Size = new Size(45, 30);
            zero.Location = new Point(170, 445);
            zero.Click += new EventHandler(CEvents.button_Click);
            zero.BackColor = Color.CornflowerBlue;
            this.Controls.Add(zero);

            Button one = new Button();
            one.Font = new Font("Arial", 12);
            one.Text = "1";
            one.Size = new Size(45, 30);
            one.Location = new Point(170, 410);
            one.Click += new EventHandler(CEvents.button_Click);
            one.BackColor = Color.CornflowerBlue;
            this.Controls.Add(one);

            Button two = new Button();
            two.Font = new Font("Arial", 12);
            two.Text = "2";
            two.Size = new Size(45, 30);
            two.Location = new Point(220, 410);
            two.Click += new EventHandler(CEvents.button_Click);
            two.BackColor = Color.CornflowerBlue;
            this.Controls.Add(two);

            Button three = new Button();
            three.Font = new Font("Arial", 12);
            three.Text = "3";
            three.Size = new Size(45, 30);
            three.Location = new Point(270, 410);
            three.Click += new EventHandler(CEvents.button_Click);
            three.BackColor = Color.CornflowerBlue;
            this.Controls.Add(three);

            Button four = new Button();
            four.Font = new Font("Arial", 12);
            four.Text = "4";
            four.Size = new Size(45, 30);
            four.Location = new Point(170, 375);
            four.Click += new EventHandler(CEvents.button_Click);
            four.BackColor = Color.CornflowerBlue;
            this.Controls.Add(four);

            Button five = new Button();
            five.Font = new Font("Arial", 12);
            five.Text = "5";
            five.Size = new Size(45, 30);
            five.Location = new Point(220, 375);
            five.Click += new EventHandler(CEvents.button_Click);
            five.BackColor = Color.CornflowerBlue;
            this.Controls.Add(five);

            Button six = new Button();
            six.Font = new Font("Arial", 12);
            six.Text = "6";
            six.Size = new Size(45, 30);
            six.Location = new Point(270, 375);
            six.Click += new EventHandler(CEvents.button_Click);
            six.BackColor = Color.CornflowerBlue;
            this.Controls.Add(six);

            Button seven = new Button();
            seven.Font = new Font("Arial", 12);
            seven.Text = "7";
            seven.Size = new Size(45, 30);
            seven.Location = new Point(170, 340);
            seven.Click += new EventHandler(CEvents.button_Click);
            seven.BackColor = Color.CornflowerBlue;
            this.Controls.Add(seven);

            Button eight = new Button();
            eight.Font = new Font("Arial", 12);
            eight.Text = "8";
            eight.Size = new Size(45, 30);
            eight.Location = new Point(220, 340);
            eight.Click += new EventHandler(CEvents.button_Click);
            eight.BackColor = Color.CornflowerBlue;
            this.Controls.Add(eight);

            Button nine = new Button();
            nine.Font = new Font("Arial", 12);
            nine.Text = "9";
            nine.Size = new Size(45, 30);
            nine.Location = new Point(270, 340);
            nine.Click += new EventHandler(CEvents.button_Click);
            nine.BackColor = Color.CornflowerBlue;
            this.Controls.Add(nine);

            Button negative = new Button();
            negative.Font = new Font("Arial", 12);
            negative.Text = "(-)";
            negative.Size = new Size(45, 30);
            negative.Location = new Point(270, 445);
            negative.Click += new EventHandler(CEvents.button_Click);
            this.Controls.Add(negative);

            Button decimalPoint = new Button();
            decimalPoint.Font = new Font("Arial", 20);
            decimalPoint.Text = ".";
            decimalPoint.Size = new Size(45, 30);
            decimalPoint.Location = new Point(220, 445);
            decimalPoint.Click += new EventHandler(CEvents.button_Click);
            decimalPoint.TextAlign = ContentAlignment.BottomCenter;
            this.Controls.Add(decimalPoint);

            Button openParenthesis = new Button();
            openParenthesis.Font = new Font("Arial", 12);
            openParenthesis.Text = "(";
            openParenthesis.Size = new Size(45, 30);
            openParenthesis.Location = new Point(220, 305);
            openParenthesis.Click += new EventHandler(CEvents.button_Click);
            this.Controls.Add(openParenthesis);

            Button closedParenthesis = new Button();
            closedParenthesis.Font = new Font("Arial", 12);
            closedParenthesis.Text = ")";
            closedParenthesis.Size = new Size(45, 30);
            closedParenthesis.Location = new Point(270, 305);
            closedParenthesis.Click += new EventHandler(CEvents.button_Click);
            this.Controls.Add(closedParenthesis);

            Button Enotation = new Button();
            Enotation.Font = new Font("Arial", 12);
            Enotation.Text = "EE";
            Enotation.Size = new Size(45, 30);
            Enotation.Location = new Point(170, 305);
            Enotation.Click += new EventHandler(CEvents.button_Click);
            this.Controls.Add(Enotation);

            Button sine = new Button();
            sine.Font = new Font("Arial", 12);
            sine.Text = "sin";
            sine.Size = new Size(45, 30);
            sine.Location = new Point(20, 270);
            this.Controls.Add(sine);

            Button cosine = new Button();
            cosine.Font = new Font("Arial", 12);
            cosine.Text = "cos";
            cosine.Size = new Size(45, 30);
            cosine.Location = new Point(70, 270);
            this.Controls.Add(cosine);

            Button tangent = new Button();
            tangent.Font = new Font("Arial", 12);
            tangent.Text = "tan";
            tangent.Size = new Size(45, 30);
            tangent.Location = new Point(120, 270);
            this.Controls.Add(tangent);

            Button euler = new Button();
            euler.Font = new Font("Arial", 12);
            euler.Text = "e";
            euler.Size = new Size(45, 30);
            euler.Location = new Point(170, 270);
            euler.Click += new EventHandler(CEvents.button_Click);
            this.Controls.Add(euler);

            Button pi = new Button();
            pi.Font = new Font("Arial", 12);
            pi.Text = "π";
            pi.Size = new Size(45, 30);
            pi.Location = new Point(220, 270);
            pi.Click += new EventHandler(CEvents.button_Click);
            this.Controls.Add(pi);

            Button gravity = new Button();
            gravity.Font = new Font("Arial", 12);
            gravity.Text = "g";
            gravity.Size = new Size(45, 30);
            gravity.Location = new Point(270, 270);
            gravity.Click += new EventHandler(CEvents.button_Click);
            this.Controls.Add(gravity);

            Button derivative = new Button();
            derivative.Font = new Font("Arial", 12);
            derivative.Text = "d/dx";
            derivative.Size = new Size(45, 30);
            derivative.Location = new Point(20, 375);
            this.Controls.Add(derivative);

            Button integral = new Button();
            integral.Font = new Font("Arial", 12);
            integral.Text = "∫";
            integral.Size = new Size(45, 30);
            integral.Location = new Point(70, 375);
            this.Controls.Add(integral);

            Button comma = new Button();
            comma.Font = new Font("Arial", 20);
            comma.Text = ",";
            comma.Size = new Size(45, 30);
            comma.Location = new Point(120, 375);
            comma.TextAlign = ContentAlignment.BottomCenter;
            this.Controls.Add(comma);

            Button logarithm = new Button();
            logarithm.Font = new Font("Arial", 12);
            logarithm.Text = "log";
            logarithm.Size = new Size(45, 30);
            logarithm.Location = new Point(20, 410);
            this.Controls.Add(logarithm);

            Button naturalLog = new Button();
            naturalLog.Font = new Font("Arial", 12);
            naturalLog.Text = "ln";
            naturalLog.Size = new Size(45, 30);
            naturalLog.Location = new Point(70, 410);
            this.Controls.Add(naturalLog);

            Button root = new Button();
            root.Font = new Font("Arial", 12);
            root.Text = "√";
            root.Size = new Size(45, 30);
            root.Location = new Point(120, 410);
            this.Controls.Add(root);

            Button factorial = new Button();
            factorial.Font = new Font("Courier New", 16);
            factorial.Text = "!";
            factorial.Size = new Size(45, 30);
            factorial.Location = new Point(20, 445);
            factorial.Click += new EventHandler(CEvents.button_Click);
            this.Controls.Add(factorial);
        }
    }
}