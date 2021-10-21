using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Minesweaper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private byte colCount = 20; //The column count on the board
        private byte rowCount = 20; //The row count on the board
        private Button[,] buttonArray; //A 2d array of buttons
        private aknakereso game;
        private byte mineCount=140;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        //---------------------------------------------------------------------------------
        /// <summary>
        /// Generates the buttons for the board and sets up the grid
        /// </summary>
        private void GenerateButtons()
        {
            GameBoard.Children.Clear();
            GameBoard.RowDefinitions.Clear();
            GameBoard.ColumnDefinitions.Clear();
            buttonArray = new Button[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
            {
                GameBoard.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < colCount; i++)
            {
                GameBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    Button createdButton = new Button()
                    {
                        Name = "b" + i + "b" + j,
                        Margin = new Thickness(2),
                        FontSize = 24,
                    };
                    buttonArray[i, j] = createdButton;
                    //Add the click event to the button
                    createdButton.Click += Piece_Click;
                    //Add the place flag event to the button
                    createdButton.MouseRightButtonDown += PlaceFlag;
                    //Set it's place in the grid matrix
                    Grid.SetRow(createdButton, i);
                    Grid.SetColumn(createdButton, j);
                    GameBoard.Children.Add(createdButton);
                }
            }
        }

        /// <summary>
        /// Creates a new game
        /// </summary>
        private void NewGame()
        {
            game = new aknakereso(rowCount,colCount,mineCount);
            GenerateButtons();
        }

        //----------------------------------------------------------------------------
        /// <summary>
        /// Flips the button so it shows it's content to the user
        /// </summary>
        /// <param name="rowIndex">The index of the cell</param>
        /// <param name="colIndex">The index of the cell</param>
        /// <param name="cellType">What value does the cell has</param>
        private void FlipButton(byte rowIndex, byte colIndex, CellType cellType)
        {
            Button selected = buttonArray[rowIndex, colIndex];
            if (selected.Content != null)
                return;

            selected.Background = Brushes.White;

            //A failsafe check
            if (cellType != CellType.None && cellType != CellType.Mine)
            {
                selected.Content = (byte)cellType;

                //Changes the color of the font according to the number it has
                switch (cellType)
                {
                    case CellType.One:
                        selected.Foreground = Brushes.Blue;
                        break;

                    case CellType.Two:
                        selected.Foreground = Brushes.Green;
                        break;

                    case CellType.Three:
                        selected.Foreground = Brushes.Red;
                        break;

                    case CellType.Four:
                        selected.Foreground = Brushes.Purple;
                        break;

                    case CellType.Five:
                        selected.Foreground = Brushes.Maroon;
                        break;

                    case CellType.Six:
                        selected.Foreground = Brushes.Turquoise;
                        break;

                    case CellType.Seven:
                        selected.Foreground = Brushes.Black;
                        break;

                    case CellType.Eight:
                        selected.Foreground = Brushes.Gray;
                        break;

                    default:
                        break;
                }
            }
            if (cellType==CellType.Mine)
            {
                selected.Content = "m";
            }
        }

        //**************************************************************************
        //Handlers

        /// <summary>
        /// Called when the user wants a new game
        /// </summary>
        private void New_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        //----------------------------------------------------------------
        /// <summary>
        /// Called when one of the pieces is clicked on
        /// </summary>
        private void Piece_Click(object sender, RoutedEventArgs e)
        {
            string[] splittedName = (sender as Button).Name.Split('b');
            byte selectedRow = Convert.ToByte(splittedName[1]);
            byte selectedCol = Convert.ToByte(splittedName[2]);
            FlipButton(selectedRow, selectedCol, game.GetCellType(selectedRow,selectedCol));
        }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Places a flag on the button
        /// </summary>
        private void PlaceFlag(object sender, MouseButtonEventArgs e)
        {
            string[] splittedName = (sender as Button).Name.Split('b');
            byte selectedRow = Convert.ToByte(splittedName[1]);
            byte selectedCol = Convert.ToByte(splittedName[2]);

            if (buttonArray[selectedRow, selectedCol].Content == null)
            {
                Image flagImg = new Image();
                flagImg.Source = new BitmapImage(new Uri("flag.png", UriKind.Relative));
                buttonArray[selectedRow, selectedCol].Content = flagImg;
            }
            else
            {
                buttonArray[selectedRow, selectedCol].Content = null;
            }
        }
    }
}