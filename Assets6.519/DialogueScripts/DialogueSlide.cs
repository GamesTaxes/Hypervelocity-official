using System;

namespace Dialogue
{
    /// <summary>
    /// The DialogueSlide-class creates blank slides that are used in construction of a dialogue tree.
    /// This class also has the methods required to Get and Set the information each slide has, as well as
    /// the methods for setting paths between the slides, and saving the outcome of the dialogue.
    /// </summary>
    class DialogueSlide
    {
        private string slideName;
        private string dialogueLine;
        private string dialogueOptionTop;
        private string dialogueOptionHigh;
        private string dialogueOptionLow;
        private string dialogueOptionBottom;
        private int dialogueOutcome;

        private DialogueSlide optionTop;
        private DialogueSlide optionHigh;
        private DialogueSlide optionLow;
        private DialogueSlide optionBottom;
        
        /// <summary>
        /// The DialogueSlide constructor takes the slide name as a parameter.
        /// </summary>
        /// <param name="slideName"></param>
        public DialogueSlide (string slideName)
        {
            this.slideName = slideName;
        }

        /// <summary>
        /// The SetDialogueOptions method sets the dialogue options for the player and stores the current progress of the dialogue to the outcome variable.
        /// </summary>
        /// <param name="dialogueLine"></param>
        /// <param name="dialogueOptionTop"></param>
        /// <param name="dialogueOptionHigh"></param>
        /// <param name="dialogueOptionLow"></param>
        /// <param name="dialogueOptionBottom"></param>
        /// <param name="dialogueOutcome"></param>
        public void SetDialogueOptions(string dialogueLine, string dialogueOptionTop, string dialogueOptionHigh, string dialogueOptionLow, string dialogueOptionBottom, int dialogueOutcome)
        {
            this.dialogueLine = dialogueLine;
            this.dialogueOptionTop = dialogueOptionTop;
            this.dialogueOptionHigh = dialogueOptionHigh;
            this.dialogueOptionLow = dialogueOptionLow;
            this.dialogueOptionBottom = dialogueOptionBottom;
            this.dialogueOutcome = dialogueOutcome;
        }
        /// <summary>
        /// The SetNextSlide method sets the connections (usable via the Buttons in UI) between slides, which is essential in creation of a dialogue tree.
        /// </summary>
        /// <param name="optionTop"></param>
        /// <param name="optionHigh"></param>
        /// <param name="optionLow"></param>
        /// <param name="optionBottom"></param>

        public void SetNextSlide(DialogueSlide optionTop, DialogueSlide optionHigh, DialogueSlide optionLow, DialogueSlide optionBottom)
        {
            this.optionTop = optionTop;
            this.optionHigh = optionHigh;
            this.optionLow = optionLow;
            this.optionBottom = optionBottom;
        }
        
        /// <summary>
        /// The GetNextSlide method retrieves the chosen slide following the current slide.
        /// </summary>
        /// <param name="playerChoice"></param>
        /// <returns></returns>
        public DialogueSlide GetNextSlide(string playerChoice)
        {
            if (playerChoice == "optionTop")
            {
                return optionTop;
            }
            else if (playerChoice == "optionHigh")
            {
                return optionHigh;
            }
            else if (playerChoice == "optionLow")
            {
                return optionLow;
            }
            else if (playerChoice == "optionBottom")
            {
                return optionBottom;
            }
            else
            {
                return null;
            }
        }

        // retrieves the dialogue options for the current slide.
        /// <summary>
        /// The GetDialogueOpt method retrieves the dialogue options for the current slide, Using location variable to identify the right elements. 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public string GetDialogueOpt(string location)
        {
            if (location == "line")
            {
                return this.dialogueLine;
            }
            else if (location == "top")
            {
                return this.dialogueOptionTop;
            }
            else if (location == "high")
            {
                return this.dialogueOptionHigh;
            }
            else if (location == "low")
            {
                return this.dialogueOptionLow;
            }
            else if (location == "bottom")
            {
                return this.dialogueOptionBottom;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// The GetDialogueOutcome method retrieves the outcome variable of the currently active dialogue.
        /// </summary>
        /// <returns></returns>
        public int GetDialogueOutcome()
        {
            return dialogueOutcome;
        }
        /// <summary>
        /// The AddSlideConnection method is used to create new connections between current slide and an existing, in currently active dialogue tree, slide(variable newConnection).
        /// optionRank variable defines through which UI button the new connection is accessible.
        /// </summary>
        /// <param name="newConnection"></param>
        /// <param name="optionRank"></param>
        public void AddSlideConnection(DialogueSlide newConnection, int optionRank)
        {
            if (optionRank == 1)
            {
                this.optionTop = newConnection;
            }
            else if (optionRank == 2)
            {
                this.optionHigh = newConnection;
            }
            else if (optionRank == 3)
            {
                this.optionLow = newConnection;
            }
            else
            {
                this.optionBottom = newConnection;
            }
        }

        /// <summary>
        /// The RemoveSlideConnection method is used to sever an existing connection between current slide and another slide.
        /// The optionRank variable tells from which UI button the connection is cut.
        /// </summary>
        /// <param name="optionRank"></param>
        public void RemoveSlideConnection(int optionRank)
        {
            if (optionRank == 1)
            {
                this.optionTop = null;
            }
            else if (optionRank == 2)
            {
                this.optionHigh = null;
            }
            else if (optionRank == 3)
            {
                this.optionLow = null;
            }
            else
            {
                this.optionBottom = null;
            }
        }

    }
}
