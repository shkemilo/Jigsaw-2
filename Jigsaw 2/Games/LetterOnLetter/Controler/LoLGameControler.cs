using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using Jigsaw_2.MainPage;
using Jigsaw_2.Score;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Jigsaw_2.Games.LetterOnLetter
{
    public class LoLGameControler : Game, ILoLGameBehavior
    {
        private readonly LoLEngine engine;
        private readonly LoLGUI mainDisplay;

        private readonly Display wordDisplay;
        private readonly TextBox checkDisplay;

        private string answer;

        public LoLGameControler(LoLGUI mainDisplay, LoLEngine engine, Grid gameGrid) : base(gameGrid, "letteronletter")
        {
            this.engine = engine;
            this.mainDisplay = mainDisplay;

            engine.Subscribe(mainDisplay);
            engine.Broadcast(engine.GetLetters());

            answer = string.Empty;

            wordDisplay = new Display(Finder.FindElementWithTag(allControls, "CurrentWord"));
            checkDisplay = Finder.FindElementWithTag(allControls, "CheckFeedback") as TextBox;

            graphicalElements.Add(mainDisplay);
            GUIElements.Add(wordDisplay);
            anims.Add(mainDisplay);
        }

        public void Select(Button button)
        {
            button.IsEnabled = false;
            answer += button.Content;

            wordDisplay.Update(answer);
            checkDisplay.Text = string.Empty;
        }

        public void Undo(Button button)
        {
            button.IsEnabled = true;
            answer = answer.Remove(answer.Length - 1);

            wordDisplay.Update(answer);
            checkDisplay.Text = string.Empty;
        }

        public void Feedback()
        {
            if (engine.Check(answer))
            {
                checkDisplay.Text = "Ova rec postoji u recniku.";
            }
            else
            {
                checkDisplay.Text = "Ova rec ne postoji u recniku.";
            }
        }

        public void Start()
        {
            ScoreInterface.Instance.StartTimeControler();

            changeSSImage();

            setLetterDisplayEnabled(true);

            Finder.FindElementWithTag(allControls, "UndoButton").IsEnabled = true;
            Finder.FindElementWithTag(allControls, "CheckerButton").IsEnabled = true;
        }

        public void Uncover()
        {
            mainDisplay.Next();
        }

        public async Task Confirm()
        {
            MessageDialogResult exitResult = await (Application.Current.MainWindow as MetroWindow)?.ShowMessageAsync("Jigsaw", "Are you sure you want to submit your current word?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { NegativeButtonText = "No", AffirmativeButtonText = "Yes" });

            if (exitResult == MessageDialogResult.Affirmative)
            {
                GameOver();
            }
        }

        public override void Grader()
        {
            if (engine.Check(answer))
            {
                score += answer.Length * 2;

                if (answer.Length == engine.GetLongestWord().Length)
                {
                    score += 6;
                }

                ScoreInterface.Instance.ScoreEngine.ChangePoints(score);
            }
        }

        public override void GameOver()
        {
            ScoreInterface.Instance.StopTimeControler();

            disableAllControls();

            (Finder.FindElementWithTag(allControls, "LongestWord") as TextBox).Text = engine.GetLongestWord();

            Feedback();
            Grader();

            GameManager.Instance.NextGame();
        }

        private void setLetterDisplayEnabled(bool isEnabled)
        {
            mainDisplay.Enable(isEnabled);
        }

        private void changeSSImage()
        {
            foreach (Rectangle r in Finder.FindVisualChildren<Rectangle>(gameGrid))
            {
                if (r.Tag?.ToString() == "SSImage")
                {
                    r.OpacityMask = new VisualBrush() { Visual = (Visual)ResourceDictionaryManager.GetResources()["appbar_door_enter"] };
                    break;
                }
            }
        }
    }
}