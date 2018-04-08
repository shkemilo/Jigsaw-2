﻿using Jigsaw_2.Abstracts;
using System.Windows.Controls;

namespace Jigsaw_2.MainPage.Commands
{
    internal class StartCurrentGameCommand : ICommand
    {
        private IMainPageBehavior mainPageBehavior;

        private Button startButton;

        private Frame mainFrame;

        public StartCurrentGameCommand(IMainPageBehavior mainPageBehavior, Frame mainFrame, Button startButton)
        {
            this.mainPageBehavior = mainPageBehavior;
            this.startButton = startButton;
            this.mainFrame = mainFrame;
        }

        public void Execute()
        {
            mainPageBehavior.StartCurrentGame(mainFrame, startButton);
        }
    }
}