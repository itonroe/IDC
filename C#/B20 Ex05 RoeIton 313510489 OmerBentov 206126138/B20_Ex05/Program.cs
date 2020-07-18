using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using B20_EX02;

namespace B20_Ex05
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FormSettings formSettings = new FormSettings();
            bool playing = true;

            while (playing)
            {
                formSettings.ShowDialog();

                FormGame formGame = new FormGame(formSettings.Player1Name, formSettings.Player2Name, formSettings.BoardSize, formSettings.GameMode);
                formGame.ShowDialog();
            }
        }
    }

    public class FormSettings : Form
    {
        private int m_IndexBoardSize = 0;
        private List<string> m_ListBoardSizes;
        private Label m_LabelPlayerName1, m_LabelPlayerName2, m_LabelBoardSize;
        private TextBox m_TextBoxPlayerName1, m_TextBoxPlayerName2;
        private Button m_ButtonAgainstFriend, m_ButtonBoardSize, m_ButtonStart;

        public string Player1Name
        {
            get
            {
                return m_TextBoxPlayerName1.Text;
            }
        }

        public string Player2Name
        {
            get
            {
                return m_TextBoxPlayerName2.Text;
            }
        }

        public string BoardSize
        {
            get
            {
                return m_ListBoardSizes[m_IndexBoardSize];
            }
        }

        public char GameMode 
        { 
            get
            {
                return m_TextBoxPlayerName2.Enabled ? 'M' : 'S';
            }
        }

        public FormSettings()
        {
            setBoardSizes();
            initializeComponents();
        }

        private void initializeComponents()
        {
            const int marginBorders = 12;

            // Label - Player 1 Name
            m_LabelPlayerName1 = new Label();
            m_LabelPlayerName1.Text = "First Player Name:";
            m_LabelPlayerName1.ForeColor = Color.Black;
            m_LabelPlayerName1.AutoSize = true;
            m_LabelPlayerName1.Left = marginBorders;
            m_LabelPlayerName1.Top = marginBorders;
            this.Controls.Add(m_LabelPlayerName1);

            // Label - Player 2 Name
            m_LabelPlayerName2 = new Label();
            m_LabelPlayerName2.Text = "Second Player Name:";
            m_LabelPlayerName2.ForeColor = Color.Black;
            m_LabelPlayerName2.AutoSize = true;
            m_LabelPlayerName2.Left = marginBorders;
            m_LabelPlayerName2.Top = marginBorders + m_LabelPlayerName1.Bottom;
            this.Controls.Add(m_LabelPlayerName2);

            // Label - Board Size
            m_LabelBoardSize = new Label();
            m_LabelBoardSize.Text = "Board Size:";
            m_LabelBoardSize.ForeColor = Color.Black;
            m_LabelBoardSize.AutoSize = true;
            m_LabelBoardSize.Left = marginBorders;
            m_LabelBoardSize.Top = marginBorders + m_LabelPlayerName2.Bottom + 4;
            this.Controls.Add(m_LabelBoardSize);

            // TextBox - Player 1 Name
            m_TextBoxPlayerName1 = new TextBox();
            m_TextBoxPlayerName1.ForeColor = Color.Black;
            m_TextBoxPlayerName1.AutoSize = true;
            m_TextBoxPlayerName1.Left = marginBorders + Math.Max(m_LabelPlayerName1.Right, m_LabelPlayerName2.Right);
            m_TextBoxPlayerName1.Top = marginBorders;
            this.Controls.Add(m_TextBoxPlayerName1);

            // TextBox - Player 2 Name
            m_TextBoxPlayerName2 = new TextBox();
            m_TextBoxPlayerName2.Text = "- computer -";
            m_TextBoxPlayerName2.ForeColor = Color.Black;
            m_TextBoxPlayerName2.AutoSize = true;
            m_TextBoxPlayerName2.Left = marginBorders + Math.Max(m_LabelPlayerName1.Right, m_LabelPlayerName2.Right);
            m_TextBoxPlayerName2.Top = marginBorders + m_TextBoxPlayerName1.Bottom;
            m_TextBoxPlayerName2.Enabled = false;
            this.Controls.Add(m_TextBoxPlayerName2);

            // Button - Against a Friend
            m_ButtonAgainstFriend = new Button();
            m_ButtonAgainstFriend.Text = "Against a Friend";
            m_ButtonAgainstFriend.FlatStyle = FlatStyle.Standard;
            m_ButtonAgainstFriend.AutoSize = true;
            m_ButtonAgainstFriend.Left = marginBorders + m_TextBoxPlayerName2.Right;
            m_ButtonAgainstFriend.Top = m_TextBoxPlayerName2.Top;
            m_ButtonAgainstFriend.Click += new EventHandler(againstFriend_Click);
            this.Controls.Add(m_ButtonAgainstFriend);

            // Button - Board Size
            m_ButtonBoardSize = new Button();
            m_ButtonBoardSize.Text = m_ListBoardSizes[m_IndexBoardSize];
            m_ButtonBoardSize.BackColor = Color.FromArgb(191, 191, 255);
            m_ButtonBoardSize.FlatStyle = FlatStyle.Standard;
            m_ButtonBoardSize.Size = new Size(Math.Max(m_LabelPlayerName1.Width, m_LabelPlayerName2.Width), 60);
            m_ButtonBoardSize.Left = marginBorders;
            m_ButtonBoardSize.Top = m_LabelBoardSize.Bottom + (marginBorders / 2);
            m_ButtonBoardSize.Click += new EventHandler(boardSize_Click);
            this.Controls.Add(m_ButtonBoardSize);

            // Button - Start
            m_ButtonStart = new Button();
            m_ButtonStart.Text = "Start!";
            m_ButtonStart.BackColor = Color.FromArgb(0, 192, 0);
            m_ButtonStart.FlatStyle = FlatStyle.Standard;
            m_ButtonStart.Left = m_ButtonAgainstFriend.Right - m_ButtonStart.Width;
            m_ButtonStart.Top = m_ButtonBoardSize.Bottom - m_ButtonStart.Height;
            m_ButtonStart.Click += new EventHandler(start_Click);
            this.Controls.Add(m_ButtonStart);

            // Form - This
            this.Text = "Memory Game - Settings";
            this.ClientSize = new Size(m_ButtonAgainstFriend.Right + marginBorders, m_ButtonBoardSize.Bottom + marginBorders);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = true;
        }

        private void setBoardSizes()
        {
            m_ListBoardSizes = new List<string>();

            for (int i = 4; i < 7; i++)
            {
                for (int j = 4; j < 7; j++)
                {
                    if ((i * j) % 2 == 0)
                    {
                        m_ListBoardSizes.Add($"{i} x {j}");
                    }
                }
            }
        }

        void againstFriend_Click(object sender, EventArgs e)
        {
            m_TextBoxPlayerName2.Enabled = !m_TextBoxPlayerName2.Enabled;
            m_TextBoxPlayerName2.Text = m_TextBoxPlayerName2.Enabled ? string.Empty : "- computer -";
            m_ButtonAgainstFriend.Text = m_TextBoxPlayerName2.Enabled ? "Against Computer" : "Against a Friend";
        }

        void boardSize_Click(object sender, EventArgs e)
        {
            m_IndexBoardSize++;

            if (m_IndexBoardSize == m_ListBoardSizes.Count)
            {
                m_IndexBoardSize = 0;
            }

            m_ButtonBoardSize.Text = m_ListBoardSizes[m_IndexBoardSize];
        }

        void start_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class FormGame : Form
    {
        Game m_Game;
        string m_FirstCard, m_SecondCard;

        Card[,] m_ButtonCards;
        Color m_Player1Color, m_Player2Color;
        Label m_LabelCurrentPlayer, m_LabelPlayer1Score, m_LabelPlayer2Score;

        Timer m_TimerSleep;
        int m_SecondsSleep;

        bool m_InternetConnection;

        public FormGame(string i_Player1Name, string i_Player2Name, string i_BoardSize, char i_GameMode)
        {
            // Init Internet Connection
            m_InternetConnection = true;

            // Init Timer
            m_TimerSleep = new Timer();
            m_TimerSleep.Interval = 1000;
            m_TimerSleep.Tick += timer_Tick;
            m_SecondsSleep = 0;

            // Init Colors
            m_Player1Color = Color.FromArgb(192, 255, 192);
            m_Player2Color = Color.FromArgb(191, 191, 255);

            // Init Cards
            m_ButtonCards = new Card[int.Parse(i_BoardSize[0].ToString()), int.Parse(i_BoardSize[4].ToString())];

            initializeGame(i_Player1Name, i_Player2Name, i_BoardSize, i_GameMode);
            initializeComponents();
        }

        private void initializeGame(string i_Player1Name, string i_Player2Name, string i_BoardSize, char i_GameMode)
        {
            m_Game = new Game();
            m_Game.Player1_Name = i_Player1Name;
            m_Game.Player2_Name = i_Player2Name;
            m_Game.GameMode = i_GameMode;

            m_Game.SetWidth(i_BoardSize[0].ToString());
            m_Game.SetHeight(i_BoardSize[4].ToString());

            m_Game.SetDifficulty("1");

            m_Game.BuildGame();
            m_Game.GameIsOn = true;
        }

        private void initializeComponents()
        {
            const int marginBorders = 12;
            const int squareCardSize = 100;

            // Button - Cards
            Dictionary<char, Image> cardImages = new Dictionary<char, Image>();

            for (int i = 0; i < m_ButtonCards.GetLength(0); i++)
            {
                for (int j = 0; j < m_ButtonCards.GetLength(1); j++)
                {
                    char cardSymbol = m_Game.GetCardValueByIndexes(i, j);

                    m_ButtonCards[i, j] = new Card();

                    if (!cardImages.ContainsKey(cardSymbol))
                    {
                        cardImages.Add(cardSymbol, m_ButtonCards[i, j].CardImage = GetRandomImage());
                    }
                    else
                    {
                        m_ButtonCards[i, j].CardImage = cardImages[cardSymbol];
                    }

                    m_ButtonCards[i, j].X = i;
                    m_ButtonCards[i, j].Y = j;
                    m_ButtonCards[i, j].CardSymbol = cardSymbol;
                    m_ButtonCards[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    m_ButtonCards[i, j].Size = new Size(squareCardSize, squareCardSize);
                    m_ButtonCards[i, j].Left = (i * (squareCardSize + marginBorders)) + marginBorders;
                    m_ButtonCards[i, j].Top = (j * (squareCardSize + marginBorders)) + marginBorders;
                    m_ButtonCards[i, j].Click += buttonCard_Click;
                    this.Controls.Add(m_ButtonCards[i, j]);
                }
            }

            // Label - Current Player
            m_LabelCurrentPlayer = new Label();
            m_LabelCurrentPlayer.Text = $"Current Player: {m_Game.PlayerTurn_Name}";
            m_LabelCurrentPlayer.BackColor = m_Game.PlayerTurn_Name.Equals(m_Game.Player1_Name) ? m_Player1Color : m_Player2Color;
            m_LabelCurrentPlayer.ForeColor = Color.Black;
            m_LabelCurrentPlayer.AutoSize = true;
            m_LabelCurrentPlayer.Left = marginBorders;
            m_LabelCurrentPlayer.Top = m_ButtonCards[0, m_ButtonCards.GetLength(1) - 1].Bottom + marginBorders;
            this.Controls.Add(m_LabelCurrentPlayer);

            // Label - Player 1 Score
            m_LabelPlayer1Score = new Label();
            m_LabelPlayer1Score.Text = $"{m_Game.Player1_Name}: {m_Game.Player1_Score} Pairs";
            m_LabelPlayer1Score.BackColor = m_Player1Color;
            m_LabelPlayer1Score.ForeColor = Color.Black;
            m_LabelPlayer1Score.AutoSize = true;
            m_LabelPlayer1Score.Left = marginBorders;
            m_LabelPlayer1Score.Top = m_LabelCurrentPlayer.Bottom + marginBorders;
            this.Controls.Add(m_LabelPlayer1Score);

            // Label - Player 2 Score
            m_LabelPlayer2Score = new Label();
            m_LabelPlayer2Score.Text = $"{m_Game.Player2_Name}: {m_Game.Player2_Score} Pairs";
            m_LabelPlayer2Score.BackColor = m_Player2Color;
            m_LabelPlayer2Score.ForeColor = Color.Black;
            m_LabelPlayer2Score.AutoSize = true;
            m_LabelPlayer2Score.Left = marginBorders;
            m_LabelPlayer2Score.Top = m_LabelPlayer1Score.Bottom + marginBorders;
            this.Controls.Add(m_LabelPlayer2Score);

            // Form - This
            this.Text = "Memory Game";
            this.ClientSize = new Size(m_ButtonCards[m_ButtonCards.GetLength(0) - 1, m_ButtonCards.GetLength(1) - 1].Right + marginBorders, m_LabelPlayer2Score.Bottom + marginBorders);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.FormClosing += formGame_FormClosing;
        }

        private void buttonCard_Click(object sender, EventArgs e)
        {
            if (m_FirstCard != null && m_SecondCard != null)
            {
                return;
            }

            Card card = (sender as Card);

            int i = card.X;
            int j = card.Y;

            string selectedCard = parsePointToString(i, j);
            m_Game.OpenCard(selectedCard);

            m_ButtonCards[i, j].Enabled = false;
            m_ButtonCards[i, j].BackColor = m_Game.PlayerTurn_Name.Equals(m_Game.Player1_Name) ? m_Player1Color : m_Player2Color;
            if (m_InternetConnection)
            {
                m_ButtonCards[i, j].BackgroundImage = card.CardImage;
            }
            else
            {
                m_ButtonCards[i, j].Text = card.CardSymbol.ToString();
            }

            if (m_FirstCard == null)
            {
                m_FirstCard = selectedCard;
            }
            else
            {
                m_SecondCard = selectedCard;

                m_SecondsSleep = 0;
                m_TimerSleep.Start();
            }
        }
        
        private string parsePointToString(int i_X, int i_Y)
        {
            return (char)('A' + i_X) + (i_Y + 1).ToString();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            const int finsihedSeconds = 2;

            if (m_SecondsSleep >= finsihedSeconds && m_FirstCard != null && m_SecondCard == null)
            {
                m_TimerSleep.Stop();
                m_TimerSleep.Enabled = false;
            }
            else if (m_SecondsSleep >= finsihedSeconds && m_FirstCard != null && m_SecondCard != null)
            {
                int card1_x = (int)(m_FirstCard[0] - 'A');
                int card1_y = (int)(m_FirstCard[1] - '1');

                int card2_x = (int)(m_SecondCard[0] - 'A');
                int card2_y = (int)(m_SecondCard[1] - '1');

                if (!m_Game.MatchingCards(m_FirstCard, m_SecondCard))
                {
                    m_ButtonCards[card1_x, card1_y].BackgroundImage = null;
                    m_ButtonCards[card2_x, card2_y].BackgroundImage = null;

                    m_ButtonCards[card1_x, card1_y].Text = string.Empty;
                    m_ButtonCards[card2_x, card2_y].Text = string.Empty;

                    m_ButtonCards[card1_x, card1_y].BackColor = Color.Empty;
                    m_ButtonCards[card2_x, card2_y].BackColor = Color.Empty;

                    m_ButtonCards[card1_x, card1_y].Enabled = true;
                    m_ButtonCards[card2_x, card2_y].Enabled = true;
                }
                else
                {
                    m_LabelPlayer1Score.Text = $"{m_Game.Player1_Name}: {m_Game.Player1_Score} Pairs";
                    m_LabelPlayer2Score.Text = $"{m_Game.Player2_Name}: {m_Game.Player2_Score} Pairs";
                }

                m_TimerSleep.Stop();

                isGameEnded();

                m_Game.ChangeTurn();
                m_LabelCurrentPlayer.Text = $"Current Player: {m_Game.PlayerTurn_Name}";
                m_LabelCurrentPlayer.BackColor = m_Game.PlayerTurn_Name.Equals(m_Game.Player1_Name) ? m_Player1Color : m_Player2Color;

                m_FirstCard = null;
                m_SecondCard = null;

                if (m_Game.GameMode.Equals('S') && m_Game.PlayerTurn_Name.Equals("- computer -"))
                {
                    pcPick();
                }
            }
            else
            {
                m_SecondsSleep++;
            }
        }

        private void pcPick()
        {
            System.Threading.Thread.Sleep(2000);
            m_Game.PcSelectCards();

            int[] pcCard1Point = m_Game.PcOpenCard(1);
            int[] pcCard2Point = m_Game.PcOpenCard(2);

            m_ButtonCards[pcCard1Point[1], pcCard1Point[0]].PerformClick();
            m_ButtonCards[pcCard2Point[1], pcCard2Point[0]].PerformClick();
        }

        private void isGameEnded()
        {
            if (m_Game.IsEnded())
            {
                Close();
            }
        }

        private void formGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_Game.IsEnded())
            {
                string alertTitle = "Sudden Exit";
                string alertMessage = "The game is not ended yet, are you sure you want to end it?";

                DialogResult exitResult = MessageBox.Show(alertMessage, alertTitle, MessageBoxButtons.YesNo);

                if (exitResult == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (askForRematch() == DialogResult.No)
            {
                MessageBox.Show("Thank you for playing, hope to see you soon!");
                Environment.Exit(0);
            }
        }

        private DialogResult askForRematch()
        {
            string scoreTitle = "Game Ended";

            StringBuilder scoreMessage = new StringBuilder();

            scoreMessage.AppendLine($"The score is:\n    {m_Game.Player1_Name} - {m_Game.Player1_Score} points.\n    {m_Game.Player2_Name} - {m_Game.Player2_Score} points.");
            scoreMessage.AppendLine($"The winner is {m_Game.GetWinnerName()}");

            scoreMessage.AppendLine("\n Do you wanna play again?");

            MessageBoxButtons scoreBox = MessageBoxButtons.YesNo;
            DialogResult rematchResult = MessageBox.Show(scoreMessage.ToString(), scoreTitle, scoreBox);

            return rematchResult;
        }

        private Image downloadImageFromUrl(string i_ImageUrl)
        {
            Image image = null;

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(i_ImageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                WebResponse webResponse = webRequest.GetResponse();

                Stream stream = webResponse.GetResponseStream();

                image = Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Just letting you know that because your'e not connected to\nthe internet we will now use images in the game and only letters. Thank you and enjoy.");
                m_InternetConnection = false;
                return null;
            }

            return image;
        }

        private Image GetRandomImage()
        {
            return m_InternetConnection ? downloadImageFromUrl("https://picsum.photos/80") : null;
        }
    }

    public class Card : Button
    {
        private int m_X;
        private int m_Y;
        private Image m_Image;
        private char m_Symbol;

        public int X
        {
            get
            {
                return m_X;
            }

            set
            {
                m_X = value;
            }
        }

        public int Y
        {
            get
            {
                return m_Y;
            }

            set
            {
                m_Y = value;
            }
        }

        public Image CardImage 
        {
            get
            {
                return m_Image;
            }

            set
            {
                m_Image = value;
            }
        }

        public char CardSymbol
        {
            get
            {
                return m_Symbol;
            }

            set
            {
                m_Symbol = value;
            }
        }
    }
}
