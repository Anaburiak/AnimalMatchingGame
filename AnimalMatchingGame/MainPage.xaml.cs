using System.Security.Cryptography.X509Certificates;

namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
        
        InitializeComponent();
        }

        int matchesFound;
        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            matchesFound = 0;
            AnimalButtons.IsVisible = true;
            PlayAgainButton.IsVisible = false;
            List<string> animalEmoji = [
    
        "🐴", "🐴",
        "🐔", "🐔",
        "🦊", "🦊",
        "🐶", "🐶",
        "🦄", "🦄",
        "🦓", "🦓",
        "🦁", "🦁",
        "🐮", "🐮"
    ];
            


                foreach (var button in AnimalButtons.Children.OfType<Button>())
                {
                    int index = Random.Shared.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    button.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick); 
            }


        int tenthsOfSecondsElapsed = 0;
        private bool TimerTick()
        {
            if (!this.IsLoaded) return false;

            tenthsOfSecondsElapsed++;

            TimeElapsed.Text = "Time elapsed: " + (tenthsOfSecondsElapsed / 10.0).ToString("0.0s");
            if (PlayAgainButton.IsVisible)
            {
                tenthsOfSecondsElapsed = 0;
                return false; // Stop the timer when the Play Again button is visible
            }
            return true; // Continue the timer
        }

        Button lastClicked = null!;
        bool findingMatch = false;
        

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button buttonClicked)
            {
                if (!string.IsNullOrWhiteSpace(buttonClicked.Text) && (findingMatch == false))
                {
                    buttonClicked.BackgroundColor = Colors.Red;
                    lastClicked = buttonClicked;
                    findingMatch = true;
                }
                else
                {
                    if ((buttonClicked != lastClicked) && (buttonClicked.Text == lastClicked.Text)
                        && (!String.IsNullOrWhiteSpace(buttonClicked.Text)))
                    {
                        matchesFound++;
                        lastClicked.Text = " ";
                        buttonClicked.Text = " ";
                    }
                   
                        lastClicked.BackgroundColor = Colors.LightBlue;
                    
                    
                    buttonClicked.BackgroundColor = Colors.LightBlue;
                    findingMatch = false;
                }
            }

            if (matchesFound == 8)
            {
                matchesFound = 0;
                AnimalButtons.IsVisible = false;
                PlayAgainButton.IsVisible = true;
            }
        }
    }
}