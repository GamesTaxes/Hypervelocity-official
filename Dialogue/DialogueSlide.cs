using System;

namespace Dialogue
{
    /// <summary>
    /// The DialogueSlide-class creates blank slides that are used in construction of a dialogue tree.
    /// This class also has the methods required to Get and Set the information each slide has, as well as
    /// the methods for setting paths between the slides, and saving the out come of the dialogue.
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
        //Constructor which takes the name of the slide as a parameter
        public DialogueSlide (string slideName)
        {
            this.slideName = slideName;
        }
        //Sets the dialogue options for the player and stores the current progress of the dialogue to the outcome variable.
        public void SetDialogueOptions(string dialogueLine, string dialogueOptionTop, string dialogueOptionHigh, string dialogueOptionLow, string dialogueOptionBottom, int dialogueOutcome)
        {
            this.dialogueLine = dialogueLine;
            this.dialogueOptionTop = dialogueOptionTop;
            this.dialogueOptionHigh = dialogueOptionHigh;
            this.dialogueOptionLow = dialogueOptionLow;
            this.dialogueOptionBottom = dialogueOptionBottom;
            this.dialogueOutcome = dialogueOutcome;
        }
        //This method sets the connections between slides, which is essential in creation of a dialogue tree.
        public void SetNextSlide(DialogueSlide optionTop, DialogueSlide optionHigh, DialogueSlide optionLow, DialogueSlide optionBottom)
        {
            this.optionTop = optionTop;
            this.optionHigh = optionHigh;
            this.optionLow = optionLow;
            this.optionBottom = optionBottom;
        }
        //retrieves the slide following the current slide.
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
        // retrieves the name of the current slide.
        public string GetName()
        {
            return this.slideName;
        }
        // retrieves the dialogue options for the current slide.
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
        //retrieves the outcome variable of the current dialogue.
        public int GetDialogueOutcome()
        {
            return dialogueOutcome;
        }

    }
}
