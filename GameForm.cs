using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

/// <summary> /////////////////////////////////////////////////////
/// 
///   Project: Hot and Cold
///   Task: Create a number guessing game where user's numeric input 
///         changes form BackColor depending on whether user's guess 
///         is higher or lower than random number between 0 and 1000
///   Dev: Justin Mangan
///   Date: 28 April 2018
/// 
/// </summary> \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
namespace HotAndCold
{

    /// <summary>
    /// Partial class GameForm:Form defines GroupForm model with 
    /// field values and Random/Stopwatch/Timer class objects
    /// </summary>
    public partial class GameForm : Form
    {
        Random randomNum = new Random();
        Stopwatch timer = new Stopwatch();
        TimeSpan ts;
        private string result1;
        private string result2;
        private string result3;
        private string ticked;
        private int random;
        private int min = 0;
        private int max = 1000;

        /// <summary>
        /// Constructs GameForm and initializes Form
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Generate new random int, set controls on Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_Load(object sender, EventArgs e)
        {
            BackColor = Color.PaleGoldenrod;
            textBox1.BackColor = BackColor;
            random = randomNum.Next(min, max);
            timer.Start();
            txtBxGuess.Focus();
            btnReset.Enabled = false;
        }

        /// <summary>
        /// Performs logic on user's numeric input each time it is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBxGuess_TextChanged(object sender, EventArgs e)
        {
            //textBox1.BackColor = BackColor;
            bool isNull = txtBxGuess.Text == "";
            int guess;

            if (!isNull)
            {
                if(int.TryParse(txtBxGuess.Text, out guess) == false)
                {
                    lblClue.ForeColor = Color.Red;
                    lblClue.Text = "Must be a number";
                }
                else {
                    if (guess < min + 1 || guess > max - 1)
                    {
                        BackColor = Color.PaleGoldenrod;                       
                        lblClue.ForeColor = Color.Red;
                        lblClue.Text = guess < min + 1 ? "Must be greater than " + min :
                            "Must be less than " + max;
                    }
                    else
                    {
                        if (guess < random)
                        {
                            BackColor = Color.CadetBlue;
                            lblClue.ForeColor = Color.Blue;
                            lblClue.Text = "Too Low";
                        }
                        else
                        {
                            if (guess > random)
                            {
                                BackColor = Color.IndianRed;
                                lblClue.ForeColor = Color.DarkRed;
                                lblClue.Text = "Too High";                           
                            }
                            else
                            {
                                if (guess == random)
                                {
                                    timer.Stop();
                                    ts = timer.Elapsed;                                
                                    result1 = String.Format("You guessed my number in {0} minutes and {1:00}.{2:00} seconds!",
                                        ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                    result2 = String.Format("You guessed my number in {0} minute and {1:00}.{2:00} seconds!",
                                        ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                    result3 = String.Format("You guessed my number in {0:00}.{1:00} seconds!",
                                        ts.Seconds, ts.Milliseconds / 10);
                                    BackColor = Color.ForestGreen;
                                    lblClue.ForeColor = Color.White;
                                    lblClue.Text = ts.Minutes > 0 ? (ts.Minutes > 1 ? result1 : result2) : result3;
                                    txtBxGuess.ReadOnly = true;
                                    btnReset.Enabled = true;
                                    btnReset.BackColor = Color.Orange;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                lblClue.Text = "";
            }
        }

        /// <summary>
        /// Button event handler generates new random int, resets: timer, controls, field values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            random = randomNum.Next(min, max);
            timer.Reset();
            timer.Start();
            txtBxGuess.Text = "";
            txtBxGuess.ReadOnly = false;
            txtBxGuess.Focus();
            btnReset.Enabled = false;
            btnReset.BackColor = Color.LightGray;
            BackColor = Color.PaleGoldenrod;
            lblClue.ForeColor = Color.Black;
        }

        /// <summary>
        /// Timer event handler sets label text to current value of timer @ea tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            ts = timer.Elapsed;
            ticked = String.Format("{0}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            lblTimer.Text = ticked;
        }

        /// <summary>
        /// Event handler changes textBox1 bg color when GameForm bg changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_BackColorChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = BackColor;
        }
    }
}
