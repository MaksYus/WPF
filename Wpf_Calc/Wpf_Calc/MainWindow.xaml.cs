using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
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

namespace Wpf_Calc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach (var item in main_grid.Children)
            {
                if (item is Button)
                {
                    (item as Button).Click += Button_Click;
                }
            }
        }
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    but_res.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.Delete:
                    but_clear.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.NumPad9:
                    but_9.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.NumPad8:
                    but_8.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.NumPad7:
                    but_7.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.NumPad6:
                    but_6.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.NumPad5:
                    but_5.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.NumPad4:
                    but_4.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.NumPad3:
                    but_3.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.NumPad2:
                    but_2.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.NumPad1:
                    but_1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.NumPad0:
                    but_0.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.Multiply:
                    butt_mult.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.Add:
                    but_plus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.Subtract:
                    but_min.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.Divide:
                    butt_dec.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                case Key.Decimal:
                    butt_dot.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); break;
                default:
                    break;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var str = (e.OriginalSource as Button).Content.ToString();
            if (input.Text.Length != 0 && Char.IsLetter(input.Text[0]))
                input.Text = "";
            switch (str)
            {
                case "+":
                    if (input.Text.Length != 0)
                        if (Char.IsDigit(input.Text[input.Text.Length - 1]) || input.Text[input.Text.Length - 1] == ')')
                        {
                            input.Text += str;
                        }
                        else
                        {
                            input.Text = input.Text.Remove(input.Text.Length - 1, 1) + str;
                        }
                    break;
                case "-":
                    if (input.Text.Length != 0)
                        if (Char.IsDigit(input.Text[input.Text.Length - 1]) || input.Text[input.Text.Length - 1] == ')')
                        {
                            input.Text += str;
                        }
                        else
                        {
                            input.Text = input.Text.Remove(input.Text.Length - 1, 1) + str;
                        }
                    break;
                case "*":
                    if (input.Text.Length != 0)
                        if (Char.IsDigit(input.Text[input.Text.Length - 1]) || input.Text[input.Text.Length - 1] == ')')
                        {
                            input.Text += str;
                        }
                        else
                        {
                            input.Text = input.Text.Remove(input.Text.Length - 1, 1) + str;
                        }
                    break;
                case "/":
                    if (input.Text.Length != 0)
                        if (Char.IsDigit(input.Text[input.Text.Length - 1]) || input.Text[input.Text.Length - 1] == ')')
                        {
                            input.Text += str;
                        }
                        else
                        {
                            input.Text = input.Text.Remove(input.Text.Length - 1, 1) + str;
                        }
                    break;
                case ",":
                    if (input.Text.Length != 0)
                        if (Char.IsDigit(input.Text[input.Text.Length - 1]))
                        {
                            input.Text += str;
                        }
                        else
                        {
                            input.Text = input.Text.Remove(input.Text.Length - 1, 1) + str;
                        }
                    else
                    {
                        input.Text += "0"+str;
                    }
                    break;
                case "(":
                    if (input.Text.Length != 0)
                            input.Text += str;
                    break;
                case ")":
                    if (input.Text.Length != 0)
                        if (Char.IsDigit(input.Text[input.Text.Length - 1]))
                        {
                            input.Text += str;
                        }
                        else
                        {
                            input.Text = input.Text.Remove(input.Text.Length - 1, 1) + str;
                        }
                    break;
                case "=":
                    try
                    {
                        input.Text = new DataTable().Compute(input.Text.Replace(',','.'), null).ToString();
                    }
                    catch
                    {
                        input.Text = "неверно задано выражение";
                    }
                    break;
                case "AC":
                    input.Text = "";
                    break;
                default:
                    if (!(input.Text.Length == 0 & str == "0"))
                        input.Text += str;
                    break;
            }

        }
    }
}
