using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;


namespace winForms_TicTacToe
{
    public partial class tictactoe_game : Form
    {
        // List which includes all buttons in use
        List<Button> buttons = new List<Button>();

        int count = 0;
        Button ai_btn = new Button();

        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

        public tictactoe_game()
        {
            InitializeComponent();
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if (ai_thinking.Text == "Thinking...")
            {
                t.Stop();
                ai_thinking.Visible = false;
                ai_thinking.Text = "Thinking";
                ai_btn.Text = "O";
                if (hasWon(ai_btn.Text))
                {
                    endOrRetry(ai_btn.Text);
                }
                return;
            }
            ai_thinking.Text += ".";
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
            DialogResult button_pressed = MessageBox.Show($"WINNER: {p}", "WINNER FOUND", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            if (button_pressed == DialogResult.Cancel)
            {
                Close();
            } else if (button_pressed == DialogResult.Retry)
            {
                MainGroupBox.Text = "Player: X";
                count = 0;
                foreach (Button button in buttons)
                {
                    button.Text = "";
                }
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
                MainGroupBox.Text = "Player: X";
                count = 0;
                foreach (Button button in buttons)
                {
                    button.Text = "";
                }
                return;
            }
            else
            {
                MessageBox.Show("???", "???", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Boolean isEven()
        {
            foreach (Button button in buttons)
            {
                if (button.Text == "")
                {
                    return false;
                }
            }
            return true;
        }

        // All methods for the playing buttons
        
        // Method to use when any button s pressed
        private void button_Click(Button button)
        {
            if (!taken(button)) // Only continue if no error is found 
            {
                button.Text = "X"; // Change button text
                count++;
                if (hasWon(button.Text)) // Checks if anyone won
                {
                    endOrRetry(button.Text);
                    return;
                } else if (isEven()) // Checks if its a tie
                {
                    even();
                    return;
                }
                else
                {
                    ai_btn = findBestMove();
                    ai_thinking.Visible = true;
                    MainGroupBox.Text = "Player: O";
                    
                    
                    Console.WriteLine(t);
                    t.Interval = 1000;
                    t.Start();

                }
            }
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


        // HARD AI Code

        // Method to return a value based on who is winning
        // Return 10 when player is winning (AI)
        // Return -10 when opponent is winning (Human)
        // Return 0 if no one is winning

        private int evaluate()
        {
            if (hasWon("X"))
            {
                return -10;
            } else if (hasWon("O"))
            {
                return 10;
            }
            return 0;
        }


        private int minimax(int depth, Boolean isMax)
        {
            int score = evaluate();
            if (score == 10)
            {
                return score;
            }

            if (score == -10)
            {
                return score;
            }

            if (isEven())
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
                        buttons[i].Text = "O";

                        best = Math.Max(best, minimax(depth + 1, !isMax));

                        buttons[i].Text = "";
                    }
                }
                return best - depth;
            }
            else
            {
                int best = 1000;

                for (int i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].Text == "")
                    {
                        buttons[i].Text = "X";

                        best = Math.Min(best, minimax(depth + 1, !isMax));

                        buttons[i].Text = "";
                    }
                }
                return best + depth;
            }
        }


        private Button findBestMove()
        {
            int bestVal = -1000;
            Button button = new Button();

            if (count == 1)
            {
                if (buttons[4].Text == "")
                {
                    return buttons[4];
                }
                return buttons[1];
            }

            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Text == "")
                {
                    buttons[i].Text = "O";

                    int moveVal = minimax(0, false);

                    buttons[i].Text = "";

                    if (moveVal > bestVal)
                    {
                        bestVal = moveVal;
                        button = buttons[i];
                    }
                }
            }
            return button;
        }

        private void MainGroupBox_Enter(object sender, EventArgs e)
        {
            t.Tick += new EventHandler(TimerEventProcessor);
            add_buttons();
            Console.WriteLine(buttons.Count);
        }
    }
}