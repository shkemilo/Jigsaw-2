﻿using Jigsaw_2.Abstracts;
using Jigsaw_2.Games.LetterOnLetter.Commands;
using Jigsaw_2.Helpers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Jigsaw_2.Games.LetterOnLetter
{
    public enum LoLState { Stop, Submit };

    /// <summary>
    /// Interaction logic for LetterOnLetter.xaml
    /// </summary>
    public partial class LetterOnLetter : GamePage
    {
        private readonly ILoLGameBehavior lolGameBehavior;

        private readonly CommandManager commandManager;
        private readonly ICommand feedbackCommand;
        private readonly ICommand uncoverCommand;
        private readonly ICommand startCommand;
        private readonly ICommand confirmCommand;

        private readonly int numberOfFields;

        private LoLState state;

        public LetterOnLetter()
        {
            InitializeComponent();

            numberOfFields = 12;

            List<IAnimatableGUI> tempList = new List<IAnimatableGUI>();
            foreach (Button b in Finder.FindVisualChildrenWithTag<Button>(LetterOnLetterGrid, "CharacterDisplayButton"))
            {
                b.Click += selectHandler;
                tempList.Add(new AnimatableDisplay(b));
            }

            lolGameBehavior = new LoLGameControler(new LoLGUI(tempList), new LoLEngine(numberOfFields), LetterOnLetterGrid);

            SetGame(lolGameBehavior as Game);

            commandManager = new CommandManager();
            feedbackCommand = new FeedbackCommand(lolGameBehavior);
            uncoverCommand = new UncoverCommand(lolGameBehavior);
            startCommand = new StartCommand(lolGameBehavior);
            confirmCommand = new ConfirmCommand(lolGameBehavior);

            state = LoLState.Stop;
        }

        private void selectHandler(object sender, RoutedEventArgs e)
        {
            commandManager.ExecuteCommand(new SelectCommand(lolGameBehavior, sender as Button));
        }

        private void feedbackHandler(object sender, RoutedEventArgs e)
        {
            commandManager.ExecuteCommand(feedbackCommand);
        }

        private void startStopSubmitHandler(object sender, RoutedEventArgs e)
        {
            if (state == LoLState.Stop)
            {
                uncover();
            }
            else if (state == LoLState.Submit)
            {
                confirm();
            }
        }

        private void confirmHandler(object sender, RoutedEventArgs e)
        {
            commandManager.ExecuteCommand(confirmCommand);
        }

        private void undoHandler(object sender, RoutedEventArgs e)
        {
            commandManager.Undo();
        }

        private void uncover()
        {
            commandManager.ExecuteCommand(uncoverCommand);

            if (UncoverCommand.Count == numberOfFields)
            {
                start();
            }
        }

        private void start()
        {
            commandManager.ExecuteCommand(startCommand);

            state = LoLState.Submit;
        }

        private void confirm()
        {
            commandManager.ExecuteCommand(confirmCommand);
        }
    }
}