using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace winForms_TicTacToe
{
    public partial class tictactoe_game : Form
    {
        // List which includes all buttons in use
        List<Button> buttons = new List<Button>();
        
        // Board to check for wins
        private string[] boardArr = {"","","","","","","","",""};

        string player, ai;


        public tictactoe_game()
        {
            InitializeComponent();
        }

        // Method to cast an error
        private Boolean error(string button_name, string error)
        {
            if (button_name == "X" || button_name == "O")
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }

        private Boolean hasWon(string player)
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
                MainGroupBox.Text = "Player: ";
                radioButton_X.Show();
                radioButton_O.Show();
                return;
            } else
            {
                MessageBox.Show("???", "???", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void even()
        {
            for (int i = 0; i < boardArr.Length; i++)
            {
                boardArr[i] = "";
            }

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
            foreach (string spot in boardArr)
            {
                if (spot != "")
                {
                    spot_count += 1;
                }
            }
            return spot_count == 9;
        }

        // Method to use when any button s pressed
        private void button_Click(Button button, int index)
        {
            if (!buttons.Contains(button))
            {
                buttons.Add(button); // Add button to the list
            }

            if (!error(button.Text, "Cant place on taken spot") && MainGroupBox.Text != "Player: ") // Only continue if no error is found 
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
            
            if (isEven())
            {
                even();
            }
        }

        // Handle all button clicks
        private void button_1_Click(object sender, EventArgs e)
        {
            button_Click(button_1, 0);
            ai_hard_makeMove();
        }
        private void button_2_Click(object sender, EventArgs e)
        {
            button_Click(button_2, 1);
            easy_GetMove(MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString());
        }
        private void button_3_Click(object sender, EventArgs e)
        {
            button_Click(button_3, 2);
            easy_GetMove(MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString());
        }
        private void button_4_Click(object sender, EventArgs e)
        {
            button_Click(button_4, 3);
            easy_GetMove(MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString());
        }
        private void button_5_Click(object sender, EventArgs e)
        {
            button_Click(button_5, 4);
            easy_GetMove(MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString());
        }
        private void button_6_Click(object sender, EventArgs e)
        {
            button_Click(button_6, 5);
            easy_GetMove(MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString());
        }
        private void button_7_Click(object sender, EventArgs e)
        {
            button_Click(button_7, 6);
            easy_GetMove(MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString());
        }
        private void button_8_Click(object sender, EventArgs e)
        {
            button_Click(button_8, 7);
            easy_GetMove(MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString());
        }
        private void button_9_Click(object sender, EventArgs e)
        {
            button_Click(button_9, 8);
            easy_GetMove(MainGroupBox.Text[MainGroupBox.Text.Length - 1].ToString());
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
        }



        //================================================
        // EASY AI - Code

        private void easy_GetMove(string player)
        {
            Random r = new Random();
            while (true)
            {
                int rand = r.Next(0, boardArr.Length);
                if (boardArr[rand] == "")
                {
                    switch(rand)
                    {
                        case 0:
                            buttons.Add(button_1);
                            break;
                        case 1:
                            buttons.Add(button_2);
                            break;
                        case 2:
                            buttons.Add(button_3);
                            break;
                        case 3:
                            buttons.Add(button_4);
                            break;
                        case 4:
                            buttons.Add(button_5);
                            break;
                        case 5:
                            buttons.Add(button_6);
                            break;
                        case 6:
                            buttons.Add(button_7);
                            break;
                        case 7:
                            buttons.Add(button_8);
                            break;
                        case 8:
                            buttons.Add(button_9);
                            break;
                    }
                    button_Click(buttons[buttons.Count-1], rand);
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

            if (score == 10 || score == -10 || isEven())
            {
                return score;
            }

            if (isMax)
            {
                int best = -1000;

                for (int i = 0; i < boardArr.Length; i++)
                {
                    if (boardArr[i] == "")
                    {
                        boardArr[i] = ai;

                        best = Math.Min(best, minimax(depth + 1, !isMax));

                        boardArr[i] = "";
                    }
                }
                return best;
            }
            else
            {
                int best = 1000;

                for (int i = 0; i < boardArr.Length; i++)
                {
                    if (boardArr[i] == "")
                    {
                        boardArr[i] = player;

                        best = Math.Min(best, minimax(depth + 1, !isMax));

                        boardArr[i] = "";
                    }
                }
                return best;
            }
        }


        private int findBestMove()
        {
            int bestVal = -1000;
            int pos = -1;

            for (int i = 0; i < boardArr.Length; i++)
            {
                if (boardArr[i] == "")
                {
                    boardArr[i] = ai;

                    int moveVal = minimax(0, false);

                    boardArr[i] = "";

                    if (moveVal > bestVal)
                    {
                        pos = i;
                        bestVal = moveVal;
                    }
                }
            }
            return pos;
        }

        private void ai_hard_makeMove()
        {
            int pos = findBestMove();
            switch (pos)
            {
                case 0:
                    buttons.Add(button_1);
                    break;
                case 1:
                    buttons.Add(button_2);
                    break;
                case 2:
                    buttons.Add(button_3);
                    break;
                case 3:
                    buttons.Add(button_4);
                    break;
                case 4:
                    buttons.Add(button_5);
                    break;
                case 5:
                    buttons.Add(button_6);
                    break;
                case 6:
                    buttons.Add(button_7);
                    break;
                case 7:
                    buttons.Add(button_8);
                    break;
                case 8:
                    buttons.Add(button_9);
                    break;
            }
            buttons[buttons.Count - 1].Text = ai;
        }
    }
}


