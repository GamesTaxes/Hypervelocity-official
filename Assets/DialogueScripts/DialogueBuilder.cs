using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;


namespace Dialogue
{
    /// <summary>
    /// The DialogueBuilder-class is used to create a dialogue tree and update lines to the slides therein.
    /// </summary>
    class DialogueBuilder
    {
        private StreamReader dialogues = new StreamReader(@"Assets\Resources\\Dialogues.txt");
        private string line;
        private List<string> readLines = new List<string>(1000);
        private List<DialogueSlide> slides = new List<DialogueSlide>(100);
        private int dialogueIndex;


        /// <summary>
        /// Constructor reads a text file and saves the lines into a list.
        /// </summary>
        public DialogueBuilder()
        {
            readTextFile(600);
            Debug.Log(readLines.Count);
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
        /// BuildDialogueX methods are used to create dialogue trees as the game progresses.
        /// Dialogues are indexed according to their appearance in game. 
        /// </summary>
        public void BuildDialogue(int dialogueIndex)
        {
            if(dialogueIndex == 0) // builds Prologue
            {
                buildDialogueTree(6, 0);

                slides[0].SetNextSlide(slides[1], null, null, null);
                slides[1].SetNextSlide(slides[2], null, null, null);
                slides[2].SetNextSlide(slides[3], null, null, null);
                slides[3].SetNextSlide(slides[4], null, null, null);
                slides[4].SetNextSlide(slides[5], null, null, null);
                slides[5].SetNextSlide(null, null, null, null);

            }
            else if (dialogueIndex == 1) //builds dialogue with Evil Empire General
            {
                buildDialogueTree(9, 30);

                slides[0].SetNextSlide(slides[1], slides[2], slides[3], slides[4]);
                slides[1].SetNextSlide(slides[5], slides[6], slides[3], slides[6]);
                slides[2].SetNextSlide(slides[2], slides[3], slides[4], null);
                slides[3].SetNextSlide(slides[7], slides[8], slides[8], null);
                slides[4].SetNextSlide(slides[5], slides[6], null, null);
                slides[5].SetNextSlide(null, null, null, null);
                slides[6].SetNextSlide(null, null, null, null);
                slides[7].SetNextSlide(null, null, null, null);
                slides[8].SetNextSlide(null, null, null, null);

            }
            else if (dialogueIndex == 2) //builds dialogue with Chicken Cage of Terror
            {
                buildDialogueTree(9, 75);

                slides[0].SetNextSlide(slides[1], slides[4], slides[2], null);
                slides[1].SetNextSlide(slides[3], slides[4], null, null);
                slides[2].SetNextSlide(slides[5], slides[6], null, null);
                slides[3].SetNextSlide(slides[9], null, null, null);
                slides[4].SetNextSlide(slides[9], slides[8], null, null);
                slides[5].SetNextSlide(slides[4], slides[8], null, null);
            }
            else if (dialogueIndex == 3) //Builds planet dialogue
            {
                buildDialogueTree(26, 125);

                slides[0].SetNextSlide(slides[1], slides[2], slides[3], slides[4]);
                slides[1].SetNextSlide(slides[5], slides[6], slides[0], null);
                slides[2].SetNextSlide(slides[8], slides[0], null, null);
                slides[3].SetNextSlide(slides[11], slides[0], null, null);
                slides[4].SetNextSlide(slides[0], null, null, null);
                slides[5].SetNextSlide(slides[14], slides[15], slides[16], null);
                slides[6].SetNextSlide(slides[15], slides[0], null, null);
                slides[7].SetNextSlide(slides[25], null, null, null);
                slides[8].SetNextSlide(slides[17], slides[18], slides[0], null);
                slides[9].SetNextSlide(slides[0], null, null, null);
                slides[10].SetNextSlide(slides[1], slides[2], slides[3], slides[4]);
                slides[11].SetNextSlide(slides[22], slides[0], null, null);
                slides[12].SetNextSlide(slides[23], slides[20], slides[19], null);
                slides[13].SetNextSlide(slides[21], slides[23], null, null);
                slides[14].SetNextSlide(slides[0], null, null, null);
                slides[15].SetNextSlide(slides[24], null, null, null);
                slides[16].SetNextSlide(slides[14], slides[15], null, null);
                slides[17].SetNextSlide(slides[0], null, null, null);
                slides[18].SetNextSlide(slides[17], null, null, null);
                slides[19].SetNextSlide(slides[0], null, null, null);
                slides[20].SetNextSlide(slides[0], null, null, null);
                slides[21].SetNextSlide(slides[0], null, null, null);
            }
            else if (dialogueIndex == 4) //builds moon dialogue
            {
                buildDialogueTree(14,255);

                slides[0].SetNextSlide(slides[1], null, null, null);
                slides[1].SetNextSlide(slides[3], slides[0], null, null);
                slides[2].SetNextSlide(slides[5], slides[6], null, null);
                slides[3].SetNextSlide(slides[7], slides[8], null, null);
                slides[4].SetNextSlide(slides[0], null, null, null);
                slides[5].SetNextSlide(slides[11], slides[10], slides[9], null);
                slides[6].SetNextSlide(slides[5], slides[0], null, null);
                slides[7].SetNextSlide(slides[0], null, null, null);
                slides[8].SetNextSlide(slides[0], null, null, null);
                slides[9].SetNextSlide(slides[0], null, null, null);
                slides[10].SetNextSlide(null, null, null, null);
                slides[11].SetNextSlide(null, null, null, null);
                slides[12].SetNextSlide(null, null, null, null);
                slides[13].SetNextSlide(null, null, null, null);
            }
            else if(dialogueIndex == 5 ||dialogueIndex == 9) // builds Outro
            {
                buildDialogueTree(3, 325);
                slides[0].SetNextSlide(slides[1], null, null, null);
                slides[1].SetNextSlide(slides[2], null, null, null);
                slides[2].SetNextSlide(null, null, null, null);
            }
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
                if (numberOfSlides > 7 && (numberOfSlides - counter) < 5)
                {
                    slides[counter].SetDialogueOptions(readLines[lineStart], readLines[lineStart + 1], readLines[lineStart + 2], readLines[lineStart + 3], readLines[lineStart + 4], (numberOfSlides - counter) + 1);
                }
                else if (numberOfSlides - counter == 1)
                {
                    slides[counter].SetDialogueOptions(readLines[lineStart], readLines[lineStart + 1], readLines[lineStart + 2], readLines[lineStart + 3], readLines[lineStart + 4], (numberOfSlides - counter));
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
                line = dialogues.ReadLine();

                if (!(counter == 0 || counter % 6 == 0))
                {
                    readLines.Add(line);
                }
            }
            dialogues.Close();
        }

        public int GetSlideIndex(DialogueSlide currentSlide)
        {
            return slides.IndexOf(currentSlide);
        }
        
        public void AddSlideConnectionDB(int fromSlide, int toSlide, int optionRank)
        {
            if (optionRank == 1)
            {
                slides[fromSlide].AddSlideConnection(slides[toSlide], optionRank);
            }
            else if (optionRank == 2)
            {
                slides[fromSlide].AddSlideConnection(slides[toSlide], optionRank);
            }
            else if (optionRank == 3)
            {
                slides[fromSlide].AddSlideConnection(slides[toSlide], optionRank);
            }
            else
            {
                slides[fromSlide].AddSlideConnection(slides[toSlide], optionRank);
            }
        }
        public void SeverSlideConnection(int fromSlide, int optionRank)
        {
            if (optionRank == 1)
            {
                slides[fromSlide].RemoveSlideConnection(optionRank);
            }
            else if (optionRank == 2)
            {
                slides[fromSlide].RemoveSlideConnection(optionRank);
            }
            else if (optionRank == 3)
            {
                slides[fromSlide].RemoveSlideConnection(optionRank);
            }
            else
            {
                slides[fromSlide].RemoveSlideConnection(optionRank);
            }
        }
    }
}
