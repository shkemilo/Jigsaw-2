using Jigsaw_2.Abstracts;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Jigsaw_2.Games.Jumper
{
    /// <summary>
    /// GUIElement used for representing the whole Jumper Game display.
    /// </summary>
    public class JumperDisplay : GUIElement
    {
        #region Private Fields

        private List<JumperDisplayComposite> displays;
        private List<JumperCheckerComposite> checkers;

        private int numberOfRows;
        private int currentRow;

        #endregion Private Fields

        #region Constuctors

        public JumperDisplay(List<JumperDisplayComposite> displays, List<JumperCheckerComposite> checkers, int numberOfRows = 6)
        {
            this.displays = displays;
            this.checkers = checkers;

            this.numberOfRows = numberOfRows;

            currentRow = 0;
        }

        #endregion Constuctors

        #region Public Properties

        public int CurrentRow { get => currentRow; }

        #endregion Public Properties

        #region Public Methods

        public void NextRow()
        {
            if (currentRow < numberOfRows - 1)
                currentRow++;
        }

        public void SetActiveRowVisual()
        {
            checkers[currentRow].SetActive();
        }

        /// <summary> Returns the current element to be set. </summary>
        public JumperDisplayLeaf GetActiveElement()
        {
            return displays[currentRow].GetActiveElement();
        }

        /// <summary> Returns the current row. </summary>
        public JumperDisplayComposite GetCurrentRow()
        {
            return displays[currentRow];
        }

        #endregion Public Methods

        #region Public Override Methods

        /// <summary> Shows the displays and checkers. </summary>
        public override void Show()
        {
            displays[currentRow].Show();
            checkers[currentRow].Show();
        }

        /// <summary> Sets the displays or checker depending on the type of the message. </summary>
        public override void Update<T>(T message)
        {
            if (message is BitmapImage || message == null)
                displays[currentRow].Update(message);
            else if (message is string[])
                checkers[currentRow].Update(message);
            else
                throw new Exception("This function only accepts Images of Color arrays");
        }

        #endregion Public Override Methods
    }
}