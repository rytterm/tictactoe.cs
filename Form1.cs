using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winForms_TicTacToe
{
    public partial class tictactoe_game : Form
    {
        private string[,] boardArr =
        {
            {"","",""},
            {"","",""},
            {"","",""}
        };

        public tictactoe_game()
        {
            InitializeComponent();
        }

        private bool error(string button_name, string error)
        {
            if (button_name == "X" || button_name == "O")
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }

        private bool hasWon(string player)
        {
            // Horizontal lines
            for (int y = 0; y < boardArr.GetLength(1); y++)
            {
                int count = 0;
                for (int x = 0; x < boardArr.GetLength(0); x++) 
                { 
                    if (boardArr[x, y] == player)
                    {
                        count++;
                    }
                }
                if (count == 3)
                {
                    return true;
                }
            }
            return false;
            // Vertical lines
        }

        private void won(string player)
        {
            var button_pressed = MessageBox.Show($"Player {player} has won", "Winner found", MessageBoxButtons.RetryCancel);
            if (button_pressed == DialogResult.Retry)
            {
                // Clear the array
                for (int y = 0; y < boardArr.GetLength(1); y++)
                {
                    for (int x = 0; x < boardArr.GetLength(0); x++)
                    {
                        boardArr[x, y] = "";
                    }
                }

                // Clear the buttons
                button_1.Text = "1";
                button_2.Text = "2";
                button_3.Text = "3";
                button_4.Text = "4";
                button_5.Text = "5";
                button_6.Text = "6";
                button_7.Text = "7";
                button_8.Text = "8";
                button_9.Text = "9";

                MainGroupBox.Text = "Player: X";

            } else if (button_pressed == DialogResult.Cancel)
            {
                Close();
            } else
            {
                MessageBox.Show("???", "???", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void button_1_Click(object sender, EventArgs e)
        {
            if (!error(button_1.Text, "Can't place on taken spot"))
            {
                button_1.Text = MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString();
                boardArr[0, 0] = button_1.Text;

                if (hasWon(button_1.Text))
                {
                    won(button_1.Text);
                }

                else if (button_1.Text == "X")
                {
                    MainGroupBox.Text = "Player: O";
                }
                else
                {
                    MainGroupBox.Text = "Player: X";
                }
            }
        }

        private void button_2_Click(object sender, EventArgs e)
        {
            if (!error(button_2.Text, "Can't place on taken spot"))
            {
                button_2.Text = MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString();
                boardArr[1,0] = button_2.Text;

                if (hasWon(button_2.Text))
                {
                    won(button_2.Text);
                }

                if (button_2.Text == "X")
                {
                    MainGroupBox.Text = "Player: O";
                }
                else
                {
                    MainGroupBox.Text = "Player: X";
                }
            }
        }

        private void button_3_Click(object sender, EventArgs e)
        {
            if (!error(button_3.Text, "Can't place on taken spot"))
            {
                button_3.Text = MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString();
                boardArr[2,0] = button_3.Text;

                if (hasWon(button_3.Text))
                {
                    won(button_3.Text);
                }

                if (button_3.Text == "X")
                {
                    MainGroupBox.Text = "Player: O";
                }
                else
                {
                    MainGroupBox.Text = "Player: X";
                }
            }
        }

        private void button_4_Click(object sender, EventArgs e)
        {
            if (!error(button_4.Text, "Can't place on taken spot"))
            {
                button_4.Text = MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString();
                boardArr[0,1] = button_4.Text;

                if (hasWon(button_4.Text))
                {
                    won(button_4.Text);
                }

                if (button_4.Text == "X")
                {
                    MainGroupBox.Text = "Player: O";
                }
                else
                {
                    MainGroupBox.Text = "Player: X";
                }
            }
        }

        private void button_5_Click(object sender, EventArgs e)
        {
            if (!error(button_5.Text, "Can't place on taken spot"))
            {
                button_5.Text = MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString();
                boardArr[1, 1] = button_5.Text;

                if (hasWon(button_5.Text))
                {
                    won(button_5.Text);
                }

                if (button_5.Text == "X")
                {
                    MainGroupBox.Text = "Player: O";
                }
                else
                {
                    MainGroupBox.Text = "Player: X";
                }
            }
        }

        private void button_6_Click(object sender, EventArgs e)
        {
            if (!error(button_6.Text, "Can't place on taken spot"))
            {
                button_6.Text = MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString();
                boardArr[2,1] = button_6.Text;

                if (hasWon(button_6.Text))
                {
                    won(button_6.Text);
                }

                if (button_6.Text == "X")
                {
                    MainGroupBox.Text = "Player: O";
                }
                else
                {
                    MainGroupBox.Text = "Player: X";
                }
            }
        }

        private void button_7_Click(object sender, EventArgs e)
        {
            if (!error(button_7.Text, "Can't place on taken spot"))
            {
                button_7.Text = MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString();
                boardArr[2, 0] = button_7.Text;

                if (hasWon(button_7.Text))
                {
                    won(button_7.Text);
                }

                if (button_7.Text == "X")
                {
                    MainGroupBox.Text = "Player: O";
                }
                else
                {
                    MainGroupBox.Text = "Player: X";
                }
            }
        }

        private void button_8_Click(object sender, EventArgs e)
        {
            if (!error(button_8.Text, "Can't place on taken spot"))
            {
                button_8.Text = MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString();
                boardArr[2, 1] = button_8.Text;

                if (hasWon(button_8.Text))
                {
                    won(button_8.Text);
                }

                if (button_8.Text == "X")
                {
                    MainGroupBox.Text = "Player: O";
                }
                else
                {
                    MainGroupBox.Text = "Player: X";
                }
            }
        }

        private void button_9_Click(object sender, EventArgs e)
        {
            if (!error(button_9.Text, "Can't place on taken spot"))
            {
                button_9.Text = MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString();
                boardArr[2, 1] = button_9.Text;

                if (hasWon(button_9.Text))
                {
                    won(button_9.Text);
                }

                if (button_9.Text == "X")
                {
                    MainGroupBox.Text = "Player: O";
                }
                else
                {
                    MainGroupBox.Text = "Player: X";
                }
            }
        }
    }
}
