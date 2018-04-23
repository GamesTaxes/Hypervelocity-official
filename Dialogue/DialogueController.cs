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
        /// <summary>
        /// Constructor is used to build the dialoguetree,
        /// and set the current location to the 1st slide.
        /// Not-yet-implemented: Takes the index number of the dialogue to build as a parameter.
        /// </summary>
        public DialogueController()
        {
            this.dialogueTree = new DialogueBuilder();
            dialogueTree.BuildDialogue2();
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

        
    }
}
