using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace winForms_TicTacToe
{
    public partial class tictactoe_game : Form
    {
        // List which includes all buttons in use
        List<Button> buttons = new List<Button>();
        
        // Board to check for wins
        private string[] boardArr = {"","","","","","","","",""};

        public tictactoe_game()
        {
            InitializeComponent();
        }

        // Method to cast an error
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
            /*
             * 0 | 1 | 2 
             * --+---+---
             * 3 | 4 | 5
             * --+---+---
             * 6 | 7 | 8
            */

            // For-loop to check for wins
            // Returns true if it finds a line
            // Else return false
            for (int i = 0; i < boardArr.Length; i++)
            {
                if ((i+1) % 3 == 0 && boardArr[i] == player && boardArr[i - 1] == player && boardArr[i - 2] == player) // All horizontal lines
                {
                    return true;
                } else if (i >= 6 && boardArr[i] == player && boardArr[i-3] == player && boardArr[i-6] == player) // All vertical lines
                {
                    return true;
                } else if (i == 0 && boardArr[i] == player && boardArr[i+4] == player && boardArr[i+8] == player) // Diagonal line up-down
                {
                    return true;
                } else if (i == 2 && boardArr[i] == player && boardArr[i+2] == player && boardArr[i+4] == player) // DIagonal line down-up
                {
                    return true;
                }
            }
            return false;
        }

        // Method to reset the game to default
        // Or to exit after a player has won
        private void endOrRetry(string player)
        {   
            for (int i = 0; i < boardArr.Length; i++)
            {
                boardArr[i] = "";
            }

            DialogResult button_pressed = MessageBox.Show($"WINNER: {player}", "WINNER FOUND", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            if (button_pressed == DialogResult.Cancel)
            {
                Close();
            } else if (button_pressed == DialogResult.Retry)
            {
                foreach (Button button in buttons)
                {
                    button.Text = "";
                }
                MainGroupBox.Text = "Player: X";
                return;
            } else
            {
                MessageBox.Show("???", "???", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to use when any button s pressed
        private void button_Click(Button button, int index)
        {
            buttons.Add(button); // Add button to the list
            if (!error(button.Text, "Cant place on taken spot")) // Only continue if no error is found 
            {
                button.Text = MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString();
                boardArr[index] = button.Text;
                
                if (hasWon(button.Text))
                {
                    endOrRetry(button.Text);
                }
                
                else if (button.Text == "X")
                    {
                        MainGroupBox.Text = "Player: O";
                    }
                else
                {
                    MainGroupBox.Text = "Player: X";
                }
            }
        }

        // Handle all button clicks
        private void button_1_Click(object sender, EventArgs e)
        {
            button_Click(button_1, 0);
        }
        private void button_2_Click(object sender, EventArgs e)
        {
            button_Click(button_2, 1);
        }
        private void button_3_Click(object sender, EventArgs e)
        {
            button_Click(button_3, 2);
        }
        private void button_4_Click(object sender, EventArgs e)
        {
            button_Click(button_4, 3);
        }
        private void button_5_Click(object sender, EventArgs e)
        {
            button_Click(button_5, 4);
        }
        private void button_6_Click(object sender, EventArgs e)
        {
            button_Click(button_6, 5);
        }
        private void button_7_Click(object sender, EventArgs e)
        {
            button_Click(button_7, 6);
        }
        private void button_8_Click(object sender, EventArgs e)
        {
            button_Click(button_8, 7);
        }
        private void button_9_Click(object sender, EventArgs e)
        {
            button_Click(button_9, 8);
        }
    }
}
