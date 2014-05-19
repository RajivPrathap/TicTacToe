namespace TicTacToe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        #region Fields

        private List<Button> Col1 = new List<Button>(3);
        private List<Button> Col2 = new List<Button>(3);
        private List<Button> Col3 = new List<Button>(3);
        private List<Button> Diag1 = new List<Button>(3);
        private List<Button> Diag2 = new List<Button>(3);
        private List<Button> Row1 = new List<Button>(3);
        private List<Button> Row2 = new List<Button>(3);
        private List<Button> Row3 = new List<Button>(3);
        private List<List<Button>> Winnings = new List<List<Button>>(8);
        private string _currentPlayer = "X"; // Default starting value

        #endregion Fields

        #region Constructors

        public Form1()
        {
            InitializeComponent();
            Init();
            ResetBoard();
        }

        #endregion Constructors

        #region Properties

        private string currentPlayer
        {
            get { return _currentPlayer; }
            set { _currentPlayer = value;
            toolStripStatusLabel1.Text = string.Format("Turn: Player {0}",value);}
        }

        #endregion Properties

        #region Methods

        private string CheckForWinner()
        {
            //Do the check
            foreach (List<Button> possibility in Winnings)
            {
                if (possibility[0].Text == possibility[1].Text &&
                    possibility[1].Text == possibility[2].Text)
                {
                    return possibility[0].Text;
                }
            }
            return string.Empty;// No winner yet
        }

        private void GameSquareButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(((Button)sender).Text))
            {
                ((Button)sender).Text = currentPlayer;
                string winner = CheckForWinner();
                if (string.IsNullOrEmpty(winner)) SwitchPlayer();
                else
                {
                    WeHaveAWinner(winner);
                }
            }
        }

        void Init()
        {
            Row1.AddRange(new Button[] { button1, button2, button3 });
            Row2.AddRange(new Button[] { button4, button5, button6 });
            Row3.AddRange(new Button[] { button7, button8, button9 });

            Col1.AddRange(new Button[] { button1, button4, button7 });
            Col2.AddRange(new Button[] { button2, button5, button6 });
            Col3.AddRange(new Button[] { button3, button6, button9 });

            Diag1.AddRange(new Button[] { button1, button5, button9 });
            Diag2.AddRange(new Button[] { button3, button5, button7 });

            Winnings.AddRange(new List<Button>[] { Row1, Row2, Row3, Col1, Col2, Col3, Diag1, Diag2 });
        }

        private void ResetBoard()
        {
            foreach (List<Button> buttonList in Winnings)
            {
                foreach (Button button in buttonList)
                {
                    button.Text = string.Empty;
                }
            }
            currentPlayer = "X";
        }

        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == "X" ? "O" : "X";
        }

        private void WeHaveAWinner(string winner)
        {
            toolStripStatusLabel1.Text = string.Format("Winner: Player {0}",winner);
            DialogResult dr = MessageBox.Show("Would you like to play again?", "Rematch?",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
            if (dr == DialogResult.Yes) ResetBoard();
        }

        #endregion Methods
    }
}