using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Jigsaw_2.Games.MyNumber
{
    /// <summary>
    /// Interaction logic for MyNumber.xaml
    /// </summary>
    public partial class MyNumber : GamePage
    {
        private IMyNumberBehavior myNumberBehavior;

        private CommandManager commandManager;
        private ICommand startCommand;
        private ICommand uncoverCommand;
        private ICommand submitCommand;

        private bool isLastNum;

        private readonly int numberOfFields;

        private Stack<bool> isLastNumHistory;

        public MyNumber()
        {
            InitializeComponent();

            myNumberBehavior = new MyNumberControlerFactory(grid).GetControler();

            commandManager = new CommandManager();

            startCommand = new StartCommand(myNumberBehavior);
            uncoverCommand = new UncoverCommand(myNumberBehavior);
            submitCommand = new SubmitCommand(myNumberBehavior);

            SetGame(myNumberBehavior as Game);

            isLastNumHistory = new Stack<bool>();

            isLastNum = false;

            isLastNumHistory.Push(isLastNum);

            numberOfFields = 9;
        }

        private void start()
        {
            commandManager.ExecuteCommand(startCommand);

            addHandlers();
        }

        private void uncoverHandler(object sender, RoutedEventArgs e)
        {
            commandManager.ExecuteCommand(uncoverCommand);

            if (UncoverCommand.Count == numberOfFields)
            {
                (sender as Button).Click -= uncoverHandler;

                start();

                (sender as Button).Click += submitHandler;
            }
        }

        private void submitHandler(object sender, RoutedEventArgs e)
        {
            commandManager.ExecuteCommand(submitCommand);
        }

        private void addHandler(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();

            if ((tag.Contains("Number") && !isLastNum) || ((tag == "Operation") && isLastNum))
            {
                isLastNum = !isLastNum;
                isLastNumHistory.Push(isLastNum);

                commandManager.ExecuteCommand(new AddCommand(myNumberBehavior, sender as Button));
            }
        }

        private void bracketAddHandler(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();

            if (((content == "(") && !isLastNum) || ((content == ")" && isLastNum)))
            {
                isLastNumHistory.Push(isLastNum);

                commandManager.ExecuteCommand(new AddCommand(myNumberBehavior, sender as Button));
            }
        }

        private void undoHandler(object sender, RoutedEventArgs e)
        {
            if (isLastNumHistory.Count != 1)
            {
                isLastNumHistory.Pop();

                isLastNum = isLastNumHistory.Peek();

                commandManager.Undo();
            }
        }

        private void addHandlers()
        {
            foreach (Button element in Finder.FindVisualChildren<Button>(grid))
            {
                string tag = element.Tag?.ToString();

                if (tag == "SmallNumber" || tag == "MediumNumber" || tag == "BigNumber" || tag == "Operation")
                {
                    element.Click += addHandler;
                }
                else if (tag == "Bracket")
                {
                    element.Click += bracketAddHandler;
                }
            }

            undo.Click += undoHandler;
        }
    }
}