using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dialogue
{
    /// <summary>
    /// DialogueController is used to build dialogue trees,
    /// and keep track of the slide that is currently displayed in the game.
    /// </summary>
    class DialogueController
    {
        private DialogueBuilder dialogueTree;
        private DialogueSlide currentSlide;
        public List<string> progress = new List<string>();
        private int dialogueIndex;
        private int currentSlideIndex;

        /// <summary>
        /// Constructor is used to build the dialoguetree,
        /// and set the current location to the 1st slide.
        /// Not-yet-implemented: Takes the index number of the dialogue to build as a parameter.
        /// </summary>
        public DialogueController(int dialogueIndex)
        {
            this.dialogueTree = new DialogueBuilder();
            dialogueTree.BuildDialogue(dialogueIndex);
            this.currentSlide = dialogueTree.CurrentSlide(0);
        }
        /// <summary>
        /// Moves to the next slide in the dialogue tree
        /// depending on the player's choice (parameter).
        /// </summary>
        /// <param name="playerChoice"></param>
        public void DialogueForward(string playerChoice)
        {
            if(currentSlide.GetNextSlide(playerChoice) != null)
            {
                this.currentSlide = this.currentSlide.GetNextSlide(playerChoice);
                currentSlideIndex = dialogueTree.GetSlideIndex(this.currentSlide);
            }
        }
        /// <summary>
        /// DialogueSlide returns the location of the slide the player is currently in.
        /// </summary>
        /// <returns></returns>
        public DialogueSlide GetCurrentSlide()
        {
            return this.currentSlide;
        }
        public int GetCurrentSlideIndex()
        {
            return currentSlideIndex;
        }
        public void UpdateProgress(int savedProgressInt)
        {
            if (savedProgressInt == 3)
            {
                if (currentSlideIndex == 14 && progress.Contains("quest1") == false)
                {
                    progress.Add("quest1");
                }
                else if (currentSlideIndex == 19 && progress.Contains("quest1Completed") == false || currentSlideIndex == 20 && progress.Contains("quest1Completed") == false)
                {
                    progress.Add("quest1Completed");
                }
                else if (currentSlideIndex == 21 && progress.Contains("quest2Objective") == false)
                {
                    progress.Add("quest2Objective");
                }
                else if (currentSlideIndex == 9 && progress.Contains("quest2Completed") == false)
                {
                    progress.Add("quest2Completed");
                }
                else if (currentSlideIndex == 17 && progress.Contains("quest2") == false)
                {
                    progress.Add("quest2");
                }
            }
            else if (savedProgressInt == 4)
            {
                if(currentSlideIndex == 1 && progress.Contains("quest2Completed") == false)
                {
                    progress.Add("quest2Completed");
                }
                else if (currentSlideIndex == 7 && progress.Contains("quest1") == false)
                {
                    progress.Add("quest1");
                }
                else if(currentSlideIndex == 9 && progress.Contains("quest1Objective") == false)
                {
                    progress.Add("quest1Objective");
                }
                else if (currentSlideIndex == 4 && progress.Contains("quest1Completed") == false)
                {
                    progress.Add("quest1Completed");
                }
            }
        }
        public int CheckProgress(string check)
        {
            if (progress.Contains(check) == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public void AddSlideConnectionDC(int toSlide, int optionRank)
        {
            dialogueTree.AddSlideConnectionDB(currentSlideIndex, toSlide, optionRank);
        }
        public void SeverSlideConnectionDC(int optionRank)
        {
            dialogueTree.SeverSlideConnection(currentSlideIndex, optionRank);
        }
    }
}
