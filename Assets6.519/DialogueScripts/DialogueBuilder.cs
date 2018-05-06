using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;


namespace Dialogue
{
    /// <summary>
    /// The DialogueBuilder class is used to create a dialogue tree and update lines to the slides therein. And to perform various tasks associated with slide manipulation such as slide connection.
    /// </summary>
    class DialogueBuilder
    {
        private StreamReader dialogues = new StreamReader(@"Assets\Resources\\Dialogues.txt"); //used to read the textfile
        private string line;
        private List<string> readLines = new List<string>(1000);
        private List<DialogueSlide> slides = new List<DialogueSlide>(100);
        private int dialogueIndex;


        /// <summary>
        /// The DialogueBuilder constructor initiates the reading of the Dialogues.txt file to create a list of dialogue lines.
        /// </summary>
        public DialogueBuilder()
        {
            readTextFile(600);
        }

        /// <summary>
        /// The DialogueSlide method returns the slide the player is currently in.
        /// currentSlide parameter is the index number of the current slide.
        /// </summary>
        /// <param name="currentSlide"></param>
        /// <returns></returns>
        public DialogueSlide CurrentSlide(int currentSlide)
        {
            return slides[currentSlide];
        }



        /// <summary>
        /// The BuildDialogue method is used to create a dialogue tree of index corresponding the parameter dialogueIndex.
        /// The dialogueIndex is based the game's progress.txt file, which is read in the GameController. 
        /// </summary>
        /// <param name="dialogueIndex"></param>
        public void BuildDialogue(int dialogueIndex)
        {
            if(dialogueIndex == 0) //prologue
            {
                buildDialogueTree(6, 0);

                slides[0].SetNextSlide(slides[1], null, null, null);
                slides[1].SetNextSlide(slides[2], null, null, null);
                slides[2].SetNextSlide(slides[3], null, null, null);
                slides[3].SetNextSlide(slides[4], null, null, null);
                slides[4].SetNextSlide(slides[5], null, null, null);
                slides[5].SetNextSlide(null, null, null, null);

            }
            else if (dialogueIndex == 1) //Evil Empire General
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
            else if (dialogueIndex == 2) //Chicken Cage of Terror
            {
                buildDialogueTree(9, 75);

                slides[0].SetNextSlide(slides[1], slides[4], slides[2], null);
                slides[1].SetNextSlide(slides[3], slides[4], null, null);
                slides[2].SetNextSlide(slides[5], slides[6], null, null);
                slides[3].SetNextSlide(slides[9], null, null, null);
                slides[4].SetNextSlide(slides[9], slides[8], null, null);
                slides[5].SetNextSlide(slides[4], slides[8], null, null);
            }
            else if (dialogueIndex == 3) //Planet dialogue
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
            else if (dialogueIndex == 4) //Moon dialogue
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
            else if(dialogueIndex == 5 ||dialogueIndex == 9) // Epilogue
            {
                buildDialogueTree(3, 325);
                slides[0].SetNextSlide(slides[1], null, null, null);
                slides[1].SetNextSlide(slides[2], null, null, null);
                slides[2].SetNextSlide(null, null, null, null);
            }
        }


        /// <summary>
        /// The buildDialogueTree method creates an amount of slides defined by the parameter numberOfSlides.
        /// Then it reads and allocates dialogue lines (from readLines list) to each slide starting from an index corresponding lineStart parameter. 
        /// It also assings dialogueOutcome variables to 4 last slides (or just the last slide in case of small numbers of slides) of the slides list
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
        /// The readTextFile method reads dialogue lines from Dialogues.txt file and saves them into readLines list.
        /// The method takes the final line-to-read from the .txt file as a parameter.
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

        /// <summary>
        /// The GetSlideIndex method takes currentSlide as a parameter and returns the index number of that slide in slides list.
        /// </summary>
        /// <param name="currentSlide"></param>
        /// <returns></returns>
        public int GetSlideIndex(DialogueSlide currentSlide)
        {
                return slides.IndexOf(currentSlide);
        }

        /// <summary>
        /// The AddSlideConnectionDB is used to create new slide connections to an existing dialogue tree by using the AddSlideConnection() of DialogueSlide-class.
        /// It takes, as variables, the index number of the slide the connection originates from, the index number of target slide, and the rank of the button to which the connection is added in the slide of origin.
        /// </summary>
        /// <param name="fromSlide"></param>
        /// <param name="toSlide"></param>
        /// <param name="optionRank"></param>
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

        /// <summary>
        /// The SeverSlideConnection method is used to terminate an excisting connection between 2 slides.
        /// It takes, as variables, the index number of the slide from which the connection originates, and rank of the button to which the connection is established. 
        /// </summary>
        /// <param name="fromSlide"></param>
        /// <param name="optionRank"></param>
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
