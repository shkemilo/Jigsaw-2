using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using Jigsaw_2.MainPage;
using Jigsaw_2.Score;
using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Jigsaw_2.Games.WhoKnowsKnows
{
    internal class WKKControler : Game, IWKKBehavior
    {
        private readonly WKKEngine engine;
        private readonly IGUI display;

        private readonly TextBox questionDisplay;

        private string answer;

        private DispatcherTimer timeOutTimer;

        public WKKControler(IGUI display, WKKEngine engine, Grid gameGrid) : base(gameGrid, "whoknowsknows")
        {
            this.engine = engine;
            this.display = display;

            engine.Subscribe(display);

            graphicalElements.Add(display);

            answer = string.Empty;

            timeOutTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(3)
            };

            timeOutTimer.Tick += onTimedEvent;

            questionDisplay = Finder.FindVisualChildWithTag<TextBox>(gameGrid, "QuestionDisplay");
        }

        private void onTimedEvent(object sender, EventArgs e)
        {
            next();

            timeOutTimer.Stop();
        }

        public void Submit(string answer)
        {
            this.answer = answer;

            Grader();

            GameOver();
        }

        public void Start()
        {
            ScoreInterface.Instance.SetTimeControlerInterval(TimeSpan.FromSeconds(0.1));
            next();
        }

        public override void Grader()
        {
            if (answer != string.Empty)
            {
                if (engine.Check(answer))
                {
                    ScoreInterface.Instance.ScoreEngine.ChangePoints(5);
                }
                else
                {
                    ScoreInterface.Instance.ScoreEngine.ChangePoints(-1);
                }
            }
        }

        public override void GameOver()
        {
            ScoreInterface.Instance.StopTimeControler();
            ScoreInterface.Instance.ResetTimeBar();

            engine.Broadcast(engine.GetAnswers());

            disableAllControls();
            timeOutTimer.Start();
        }

        private void endGame()
        {
            display.Enable(false);

            ScoreInterface.Instance.SetTimeControlerInterval(TimeSpan.FromSeconds(1));

            GameManager.Instance.NextGame();
        }

        private void next()
        {
            answer = string.Empty;

            engine.Next();
            if (!engine.Empty)
            {
                nextQuestion();
            }
            else
            {
                endGame();
            }
        }

        private void nextQuestion()
        {
            enableAllControls();

            ScoreInterface.Instance.StartTimeControler();

            engine.Broadcast(engine.Question);

            display.Enable(true);

            questionDisplay.Text = engine.Question.QuestionText;
        }
    }
}