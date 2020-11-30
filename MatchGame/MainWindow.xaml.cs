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
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "😂", "😂",
                "😊","😊",
                "🤣","🤣",
                "❤","❤",
                "😍", "😍",
                "😒","😒",
                "👌","👌",
                "😘","😘",

            };

            Random random = new Random(); //create a new random number generator 

            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) //Loop finds every TextBlock in the main grid and repeat the follwoing statements for each of them
            {
                if(textBlock.Name != "timeTextBlock")
                {
                    int index = random.Next(animalEmoji.Count); //Pick a random number between 0 and the number of emoji lef tin the list and call it "index"
                    string nextEmoji = animalEmoji[index]; //Use the random number called "index" to get a random emoji from the list
                    textBlock.Text = nextEmoji; //Update the TextBlock with the random emoji from the list
                    animalEmoji.RemoveAt(index); //Remove the random emoji from the list
                }
               
            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false; //keeps track of whether or not the player clicked on the first animal in a pair and is not trying to find match

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if(findingMatch == false) //Player clicked the first animal in a pair, so it makes that animal hidden and keeps track of its TextBlock in case it needs to make it visible again
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if(textBlock.Text == lastTextBlockClicked.Text) //The player found a match. Makes the second emoji invisible and resets findingMatch so the next animal clicked on is the first one in a pair again
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else //Player clicked on a pair that doesnt match. 
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }

        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
