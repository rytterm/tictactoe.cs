using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace winForms_TicTacToe
{
    public partial class tictactoe_game : Form
    {
        // List which includes all buttons in use
        List<Button> buttons = new List<Button>();
        

        string player, ai;
        Boolean over;


        public tictactoe_game()
        {
            InitializeComponent();
        }

        private void add_buttons()
        {
            buttons.Add(button_1);
            buttons.Add(button_2);
            buttons.Add(button_3);
            buttons.Add(button_4);
            buttons.Add(button_5);
            buttons.Add(button_6);
            buttons.Add(button_7);
            buttons.Add(button_8);
            buttons.Add(button_9);
        }


        // Method to cast an error
        private Boolean taken(Button button)
        {
            if (button.Text == "X" || button.Text == "O")
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }

        private Boolean hasWon(string p)
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
            for (int i = 0; i < buttons.Count; i++)
            {
                if ((i+1) % 3 == 0 && buttons[i].Text == p && buttons[i - 1].Text == p && buttons[i - 2].Text == p) // All horizontal lines
                {
                    return true;
                } else if (i >= 6 && buttons[i].Text == p && buttons[i-3].Text == p && buttons[i-6].Text == p) // All vertical lines
                {
                    return true;
                } else if (i == 0 && buttons[i].Text == p && buttons[i+4].Text == p && buttons[i+8].Text == p) // Diagonal line up-down
                {
                    return true;
                } else if (i == 2 && buttons[i].Text == p && buttons[i+2].Text == p && buttons[i+4].Text == p) // DIagonal line down-up
                {
                    return true;
                }
            }
            return false;
        }

        // Method to reset the game to default
        // Or to exit after a player has won
        private void endOrRetry(string p)
        {
            over = true;
            DialogResult button_pressed = MessageBox.Show($"WINNER: {p}", "WINNER FOUND", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            if (button_pressed == DialogResult.Cancel)
            {
                Close();
            } else if (button_pressed == DialogResult.Retry)
            {
                foreach (Button button in buttons)
                {
                    button.Text = "";
                }
                MainGroupBox.Text = "Player: ";
                radioButton_X.Show();
                radioButton_O.Show();
            } else
            {
                MessageBox.Show("???", "???", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void even()
        {
            DialogResult button_pressed = MessageBox.Show($"NO WINNER", "even", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            if (button_pressed == DialogResult.Cancel)
            {
                Close();
            }
            else if (button_pressed == DialogResult.Retry)
            {
                foreach (Button button in buttons)
                {
                    button.Text = "";
                }
                MainGroupBox.Text = "Player: ";
                radioButton_X.Show();
                radioButton_O.Show();
                return;
            }
            else
            {
                MessageBox.Show("???", "???", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private Boolean isEven()
        {
            int spot_count = 0;
            foreach (Button spot in buttons)
            {
                if (spot.Text != "")
                {
                    spot_count += 1;
                }
            }
            return spot_count == 9;
        }

        // Method to use when any button s pressed
        private void button_Click(Button button)
        {
            if (!taken(button) && MainGroupBox.Text != "Player: ") // Only continue if no error is found 
            {
                button.Text = MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString();

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
                if (!over)
                {
                    findBestMove();
                }
            }
            
            if (isEven())
            {
                even();
            }
            over = false;
        }

        // Handle all button clicks
        private void button_1_Click(object sender, EventArgs e)
        {
            button_Click(button_1);
        }
        private void button_2_Click(object sender, EventArgs e)
        {
            button_Click(button_2);
        }
        private void button_3_Click(object sender, EventArgs e)
        {
            button_Click(button_3);
        }
        private void button_4_Click(object sender, EventArgs e)
        {
            button_Click(button_4);
        }
        private void button_5_Click(object sender, EventArgs e)
        {
            button_Click(button_5);
        }
        private void button_6_Click(object sender, EventArgs e)
        {
            button_Click(button_6);
        }
        private void button_7_Click(object sender, EventArgs e)
        {
            button_Click(button_7);
        }
        private void button_8_Click(object sender, EventArgs e)
        {
            button_Click(button_8);
        }
        private void button_9_Click(object sender, EventArgs e)
        {
            button_Click(button_9);
        }

        private void radioButton_X_CheckedChanged(object sender, EventArgs e)
        {
            MainGroupBox.Text = "Player: X";
            player = "X";
            ai = "O";
            radioButton_X.Checked = false;
            radioButton_O.Checked = false;
            radioButton_X.Hide();
            radioButton_O.Hide();
            add_buttons();
        }

        private void radioButton_O_CheckedChanged(object sender, EventArgs e)
        {
            MainGroupBox.Text = "Player: O";
            player = "O";
            ai = "X";
            radioButton_X.Checked = false;
            radioButton_O.Checked = false;
            radioButton_X.Hide();
            radioButton_O.Hide();
            add_buttons();
        }



        //================================================
        // EASY AI - Code

        private void easy_GetMove()
        {
            Random r = new Random();
            while (true)
            {
                int rand = r.Next(0, buttons.Count);
                if (buttons[rand].Text == "")
                {
                    buttons[rand].Text = ai;
                    MainGroupBox.Text = $"Player: {player}";
                    break;
                }
            }
        }
        //================================================
        // HARD AI Code

        // Method to return a value based on who is winning
        // Return 10 when player is winning (AI)
        // Return -10 when opponent is winning (Human)
        // Return 0 if no one is winning

        private Boolean willWin(string p)
        {
            int row = 0; 
            int count = 0;
            // Columns
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (buttons[row + j].Text == p)
                    {
                        count++;
                    }
                    row += 3;
                }
            }
        }

        private int evaluate()
        {
            if (hasWon(player))
            {
                return 10;
            } else if (hasWon(ai))
            {
                return -10;
            }
            return 0;
        }

        private int minimax(int depth, Boolean isMax)
        {
            int score = evaluate();

            if (score == 10 || score == -10)
            {
                return score;
            }

            if (!isEven())
            {
                return 0;
            }

            if (isMax)
            {
                int best = -1000;

                for (int i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].Text == "")
                    {
                        buttons[i].Text = ai;

                        best = Math.Min(best, minimax(depth + 1, !isMax));

                        buttons[i].Text = "";
                    }
                }
                return best;
            }
            else
            {
                int best = 1000;

                for (int i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].Text == "")
                    {
                        buttons[i].Text = player;

                        best = Math.Min(best, minimax(depth + 1, !isMax));

                        buttons[i].Text = "";
                    }
                }
                return best;
            }
        }


        private void findBestMove()
        {
            int bestVal = -1000;
            Button button = new Button();

            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Text == "")
                {
                    buttons[i].Text = ai;

                    int moveVal = minimax(0, false);

                    buttons[i].Text = "";

                    if (moveVal > bestVal)
                    {
                        bestVal = moveVal;
                        button = buttons[i];
                    }
                }
            }
            button.Text = ai;
            MainGroupBox.Text = $"Player: {player}";
        }
        /*
        private void ai_hard_makeMove()
        {
            Button button = findBestMove();
            
        }
        */
    }
}


