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
        private List<string> progress = new List<string>();
        private int dialogueIndex;
        private int currentSlideIndex;

        /// <summary>
        /// The DialogueController constructor is used to build a dialoguetree of an index corresponding the parameter dialogueIndex,
        /// and set the current location to the 1st slide.
        /// </summary>
        /// <param name="dialogueIndex"></param>
        public DialogueController(int dialogueIndex)
        {
            this.dialogueTree = new DialogueBuilder();
            dialogueTree.BuildDialogue(dialogueIndex);
            this.currentSlide = dialogueTree.CurrentSlide(0);
        }
        /// <summary>
        /// Moves to the next slide in the dialogue tree depending on the player's chosen path, playerChoise parameter.
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
        /// The DialogueSlide method returns the location of the slide the player is currently in.
        /// </summary>
        /// <returns></returns>
        public DialogueSlide GetCurrentSlide()
        {
            return this.currentSlide;
        }


        /// <summary>
        /// The GetCurrentSlideIndex method returns the index number of current slide.
        /// </summary>
        public int GetCurrentSlideIndex
        {
            get
            {       
                return currentSlideIndex;
            }
        }

        /// <summary>
        /// The UpdateProgress method is used in tracking quests and quest progress. It takes as parameter the index of currently active dialogue tree and then tracks the slides the player moves into updating quest progress at the same time.
        /// </summary>
        /// <param name="savedProgressInt"></param>
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

        /// <summary>
        /// The CheckProgress method is used to check the players quest progress at various states of the game to help determine which slide connections to create and which ones to terminate.
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
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

        /// <summary>
        /// The AddSlideConnectionDC is used to add new slide connections by calling AddSlideConnectionDB method of DialogueBuilder class.
        /// It takes, as parameters, the index of the slide of origin, and rank of the button to which the connection is bound.
        /// </summary>
        /// <param name="toSlide"></param>
        /// <param name="optionRank"></param>
        public void AddSlideConnectionDC(int toSlide, int optionRank)
        {
            dialogueTree.AddSlideConnectionDB(currentSlideIndex, toSlide, optionRank);
        }

        /// <summary>
        /// The SeverSlideConnection uses DialogueTree-class' SeverSlideConnection method to sever a slide connection.
        /// It takes, as parameter, the rank of the button to which the connection is bound.
        /// </summary>
        /// <param name="optionRank"></param>
        public void SeverSlideConnectionDC(int optionRank)
        {
            dialogueTree.SeverSlideConnection(currentSlideIndex, optionRank);
        }
    }
}
