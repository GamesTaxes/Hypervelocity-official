using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Dialogue
{
    /// <summary>
    /// The DialogueBuilder-class is used to create a dialogue tree and update lines to the slides therein.
    /// </summary>
    class DialogueBuilder
    {
        System.IO.StreamReader Dialogues = new System.IO.StreamReader(@"C:\Users\Torsti\Documents\Test\HyperVelocity\Assets\Resources\Dialogues.txt");
        private string line;
        private List<string> readLines = new List<string>(1000);
        private List<DialogueSlide> slides = new List<DialogueSlide>(100);

        /// <summary>
        /// Constructor reads a text file and saves the lines into a list.
        /// </summary>
        public DialogueBuilder()
        {
            readTextFile(61);
        }

        /// <summary>
        /// GetSlideName returns the name of the slide in corresponding index (parameter).
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetSlideName(int i)
        {
            return slides[i].GetName();
        }

        /// <summary>
        /// DialogueSlide returns the slide the player is currently in. 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public DialogueSlide CurrentSlide(int currentSlide)
        {
            return slides[currentSlide];
        }
        /// <summary>
        /// BuildPrologue builds dialogue for the game's prologue.
        /// </summary>
        public void BuildPrologue()
        {
            buildDialogueTree(9, 0);

            slides[0].SetNextSlide(slides[1], null, null, null);
            slides[1].SetNextSlide(slides[2], null, null, null);
            slides[2].SetNextSlide(slides[3], null, null, null);
            slides[3].SetNextSlide(slides[4], null, null, null);
            slides[4].SetNextSlide(slides[5], null, null, null);
            slides[5].SetNextSlide(slides[6], null, null, null);
            slides[6].SetNextSlide(slides[7], null, null, null);
            slides[7].SetNextSlide(slides[8], null, null, null);
            slides[8].SetNextSlide(slides[9], null, null, null);
            slides[9].SetNextSlide(slides[10], null, null, null);
        }
        /// <summary>
        /// BuildDialogueX methods are used to create dialogue trees as the game progresses.
        /// Dialogues are indexed according to their appearance in game. 
        /// </summary>
        public void BuildDialogue0()
        {
            buildDialogueTree(9, 0);

            slides[0].SetNextSlide(slides[1], slides[4], slides[2], null);
            slides[1].SetNextSlide(slides[3], slides[4], null, null);
            slides[2].SetNextSlide(slides[5], slides[6], null, null);
            slides[3].SetNextSlide(slides[7], slides[8], null, null);
            slides[4].SetNextSlide(slides[7], slides[8], slides[9], null);
            slides[5].SetNextSlide(slides[4], slides[9], null, null);
            
        }

        public void BuildDialogue1()
        {
            buildDialogueTree(9, 0);

            slides[0].SetNextSlide(slides[1], slides[4], slides[2], null);
            slides[1].SetNextSlide(slides[3], slides[4], null, null);
            slides[2].SetNextSlide(slides[5], slides[6], null, null);
            slides[3].SetNextSlide(slides[7], slides[8], null, null);
            slides[4].SetNextSlide(slides[7], slides[8], slides[9], null);
            slides[5].SetNextSlide(slides[4], slides[9], null, null);
        }

        public void BuildDialogue2()
        {
            buildDialogueTree(9, 0);

            slides[0].SetNextSlide(slides[1], slides[4], slides[2], null);
            slides[1].SetNextSlide(slides[3], slides[4], null, null);
            slides[2].SetNextSlide(slides[5], slides[6], null, null);
            slides[3].SetNextSlide(slides[7], slides[8], null, null);
            slides[4].SetNextSlide(slides[7], slides[8], slides[9], null);
            slides[5].SetNextSlide(slides[4], slides[9], null, null);
        }

        public void BuildDialogue3()
        {
            buildDialogueTree(1, 0);
        }

        /// <summary>
        /// buildDialogueTree creates a given amount of slides for a dialogue (numberOfSlides),
        /// and sets dialogue lines to each slide. the lines are located in readLines-list. 
        /// </summary>
        /// <param name="numberOfSlides"></param>
        /// <param name="lineStart"></param>
        private void buildDialogueTree(int numberOfSlides, int lineStart)
        {
            slides.Clear();
            for(int counter = 0; counter <= numberOfSlides; counter++)
            {
                slides.Add(new DialogueSlide("Slide" + counter));
            }

            for (int counter = 0; counter <= numberOfSlides; counter++)
            {
                if (counter - numberOfSlides > -4)
                {
                    slides[counter].SetDialogueOptions(readLines[lineStart], readLines[lineStart + 1], readLines[lineStart + 2], readLines[lineStart + 3], readLines[lineStart + 4], (numberOfSlides-counter+1));
                }
                else
                {
                    slides[counter].SetDialogueOptions(readLines[lineStart], readLines[lineStart + 1], readLines[lineStart + 2], readLines[lineStart + 3], readLines[lineStart + 4], 0);
                }
                lineStart = lineStart + 5;
            }

        }
        /// <summary>
        /// readTextFile reads dialogue lines from a text file and saves them into readLines-list.
        /// The method takes the final line-to-read as a parameter.
        /// After reaching this line the text file is closed.
        /// </summary>
        /// <param name="finalLine"></param>
        private void readTextFile(int finalLine)
        {
            for (int counter = 0; counter <= finalLine; counter++)
            {
                line = Dialogues.ReadLine();

                if (!(counter == 0 || counter % 6 == 0))
                {
                    readLines.Add(line);
                }
            }
            Dialogues.Close();
        }
    }
}
