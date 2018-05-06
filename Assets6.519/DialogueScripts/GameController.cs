using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dialogue;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System;

namespace Dialogue
{


    /// <summary>
    /// GameController links the code segments to unity UI, and manages any actions necessary in relation to dialogue.
    /// </summary>

    public class GameController : MonoBehaviour
    {

        private Button buttonDialogueOptionTop;
        private Button buttonDialogueOptionHigh;
        private Button buttonDialogueOptionLow;
        private Button buttonDialogueOptionBottom;

        private Button buttonPrologue;

        private Button buttonPlanetOptionTop;
        private Button buttonPlanetOptionHigh;
        private Button buttonPlanetOptionLow;
        private Button buttonPlanetOptionBottom;

        private Text textButtonDialogueOptionTop;
        private Text textButtonDialogueOptionHigh;
        private Text textButtonDialogueOptionLow;
        private Text textButtonDialogueOptionBottom;

        private Text textButtonPlanetOptionTop;
        private Text textButtonPlanetOptionHigh;
        private Text textButtonPlanetOptionLow;
        private Text textButtonPlanetOptionBottom;
        private Text textButtonPrologue;

        private Text textDialogueText;
        private Text textPrologueText;
        private Text textPlanetText;

        private DialogueController dialogueStorage;

        private int dialogueOutcome;
        private int savedProgressInt;
        private int progressCheck1;
        private int progressCheck2;
        private int progressCheck3;
        private string savedProgressString;

        private GameObject canvasDialogue;
        private GameObject canvasPrologue;
        private GameObject panelDialoguePanel;
        private GameObject panelPlanetPanel;

        private RawImage rawImageDialogueInteraction;
        private RawImage rawImageDialoguePlanetInteraction;
        private RawImage rawImagePrologueInteraction;

        // Use this for initialization
        /// <summary>
        /// Start() method serves as the link between Unity's UI and the code sections
        /// by making UI elements interactable.
        /// </summary>
        void Start()
        {
            //Connects the code with UI elements
            canvasDialogue = GameObject.Find("CanvasDialogue");
            canvasPrologue = GameObject.Find("CanvasPrologue");
            panelDialoguePanel = GameObject.Find("PanelDialoguePanel");
            panelPlanetPanel = GameObject.Find("PanelPlanetPanel");

            buttonDialogueOptionTop = GameObject.Find("ButtonDialogueOptionTop").GetComponent<Button>();
            buttonDialogueOptionHigh = GameObject.Find("ButtonDialogueOptionHigh").GetComponent<Button>();
            buttonDialogueOptionLow = GameObject.Find("ButtonDialogueOptionLow").GetComponent<Button>();
            buttonDialogueOptionBottom = GameObject.Find("ButtonDialogueOptionBottom").GetComponent<Button>();
            buttonPrologue = GameObject.Find("ButtonPrologueButton").GetComponent<Button>();

            buttonPlanetOptionTop = GameObject.Find("ButtonPlanetOptionTop").GetComponent<Button>();
            buttonPlanetOptionHigh = GameObject.Find("ButtonPlanetOptionHigh").GetComponent<Button>();
            buttonPlanetOptionLow = GameObject.Find("ButtonPlanetOptionLow").GetComponent<Button>();
            buttonPlanetOptionBottom = GameObject.Find("ButtonPlanetOptionBottom").GetComponent<Button>();

            textButtonDialogueOptionTop = GameObject.Find("TextButtonDialogueOptionTop").GetComponent<Text>();
            textButtonDialogueOptionHigh = GameObject.Find("TextButtonDialogueOptionHigh").GetComponent<Text>();
            textButtonDialogueOptionLow = GameObject.Find("TextButtonDialogueOptionLow").GetComponent<Text>();
            textButtonDialogueOptionBottom = GameObject.Find("TextButtonDialogueOptionBottom").GetComponent<Text>();
            textButtonPrologue = GameObject.Find("TextPrologueButtonText").GetComponent<Text>();

            textButtonPlanetOptionTop = GameObject.Find("TextButtonPlanetOptionTop").GetComponent<Text>();
            textButtonPlanetOptionHigh = GameObject.Find("TextButtonPlanetOptionHigh").GetComponent<Text>();
            textButtonPlanetOptionLow = GameObject.Find("TextButtonPlanetOptionLow").GetComponent<Text>();
            textButtonPlanetOptionBottom = GameObject.Find("TextButtonPlanetOptionBottom").GetComponent<Text>();

            textDialogueText = GameObject.Find("TextDialogueText").GetComponent<Text>();
            textPrologueText = GameObject.Find("TextPrologueText").GetComponent<Text>();
            textPlanetText = GameObject.Find("TextPlanetText").GetComponent<Text>();

            rawImageDialogueInteraction = GameObject.Find("RawImageDialogueInteraction").GetComponent<RawImage>();
            rawImageDialoguePlanetInteraction = GameObject.Find("RawImageDialoguePlanetInteraction").GetComponent<RawImage>();
            rawImagePrologueInteraction = GameObject.Find("RawImagePrologueInteraction").GetComponent<RawImage>();

            // Allows interaction with UI dialogue buttons 
            buttonDialogueOptionTop.onClick.AddListener(() => buttonPressed(buttonDialogueOptionTop));
            buttonDialogueOptionHigh.onClick.AddListener(() => buttonPressed(buttonDialogueOptionHigh));
            buttonDialogueOptionLow.onClick.AddListener(() => buttonPressed(buttonDialogueOptionLow));
            buttonDialogueOptionBottom.onClick.AddListener(() => buttonPressed(buttonDialogueOptionBottom));
            buttonPrologue.onClick.AddListener(() => buttonPressed(buttonPrologue));
            buttonPlanetOptionTop.onClick.AddListener(() => buttonPressed(buttonPlanetOptionTop));
            buttonPlanetOptionHigh.onClick.AddListener(() => buttonPressed(buttonPlanetOptionHigh));
            buttonPlanetOptionLow.onClick.AddListener(() => buttonPressed(buttonPlanetOptionLow));
            buttonPlanetOptionBottom.onClick.AddListener(() => buttonPressed(buttonPlanetOptionBottom));

            loadProgress(); //loads game progress
            selectCanvas(savedProgressInt); //selects the correct canvas by using the loaded progress
            dialogueStorage = new DialogueController(savedProgressInt); //builds a dialogue tree
            selectPanel(); //selects the correct planel
            updateDialogueRawImage(); //updates the rawImage in dialogue window
            updateUISlideInfo(); // Update the information of the initial dialogue slide to the UI


        }
        /// <summary>
        /// The buttonPressed method identifies which UI button is pressed and links the button to corresponding actions.
        /// First off the method checks for a dialogue outcome.
        /// IF-statements advance the dialogue to the correspongding direction,
        /// update the information on the new dialogue slide to UI, select the correct panel.
        /// </summary>
        /// <param name="button"></param>
        private void buttonPressed(Button button)
        {
            dialogueOutcomeManager();
            if (button.CompareTag("DialogueOptionTop"))
            {
                dialogueStorage.DialogueForward("optionTop");
                dialogueStorage.UpdateProgress(savedProgressInt);
                selectPanel();
                updateUISlideInfo();
                Debug.Log(dialogueStorage.GetCurrentSlideIndex);
            }
            else if (button.CompareTag("DialogueOptionHigh"))
            {
                dialogueStorage.DialogueForward("optionHigh");
                dialogueStorage.UpdateProgress(savedProgressInt);
                selectPanel();
                updateUISlideInfo();
                Debug.Log(dialogueStorage.GetCurrentSlideIndex);
            }
            else if (button.CompareTag("DialogueOptionLow"))
            {
                dialogueStorage.DialogueForward("optionLow");
                dialogueStorage.UpdateProgress(savedProgressInt);
                selectPanel();
                updateUISlideInfo();
                Debug.Log(dialogueStorage.GetCurrentSlideIndex);
            }
            else if (button.CompareTag("DialogueOptionBottom"))
            {
                dialogueStorage.DialogueForward("optionBottom");
                dialogueStorage.UpdateProgress(savedProgressInt);
                selectPanel();
                updateUISlideInfo();
                Debug.Log(dialogueStorage.GetCurrentSlideIndex);
            }
        }
        /// <summary>
        /// The updateUISlideInfo method updates current slide information to the UI.
        /// </summary>
        private void updateUISlideInfo()
        {
            dialogueManager(savedProgressInt);

            textDialogueText.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("line");
            textButtonDialogueOptionTop.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("top");
            textButtonDialogueOptionHigh.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("high");
            textButtonDialogueOptionLow.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("low");
            textButtonDialogueOptionBottom.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("bottom");
            textPrologueText.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("line");
            textButtonPrologue.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("top");
            textPlanetText.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("line");
            textButtonPlanetOptionTop.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("top");
            textButtonPlanetOptionHigh.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("high");
            textButtonPlanetOptionLow.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("low");
            textButtonPlanetOptionBottom.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("bottom");
        }

        /// <summary>
        /// The selectCanvas method selects which canvas to use depending on the dialogueIndex taken as a parameter.
        /// </summary>
        /// <param name="dialogueIndex"></param>
        private void selectCanvas(int dialogueIndex)
        {
            if (dialogueIndex == 0 || dialogueIndex == 10)
            {

                this.canvasDialogue.SetActive(false);
                this.canvasPrologue.SetActive(true);
            }
            else if (dialogueIndex > 0 || dialogueIndex < 10)
            {
                this.canvasPrologue.SetActive(false);
                this.canvasDialogue.SetActive(true);
            }
            else
            {
                this.canvasPrologue.SetActive(false);
                this.canvasDialogue.SetActive(true);
            }

        }

        /// <summary>
        ///The loadProgress method is used to read game progress from the Progress.txt and turn it into an int variable that can be used as a parameter in various methods. 
        /// </summary>
        private void loadProgress()
        {
            savedProgressString = File.ReadAllText("Assets\\Resources\\Progress.txt");
            if (Int32.TryParse(savedProgressString, out savedProgressInt) != false)
            {
                savedProgressInt = Convert.ToInt32(savedProgressString);
            }
            else
            {
                savedProgressInt = 0;
            }
        }

        /// <summary>
        /// The selectPanel method selects the correct panel to be used based on savedProgressInt variable and current slide's index number in slides list of DialogueBuilder class.
        /// </summary>
        private void selectPanel()
        {
            if (savedProgressInt == 3 && dialogueStorage.GetCurrentSlideIndex == 0 || savedProgressInt == 4 && dialogueStorage.GetCurrentSlideIndex == 0)
            {
                panelDialoguePanel.SetActive(false);
                panelPlanetPanel.SetActive(true);
            }
            else
            {
                panelPlanetPanel.SetActive(false);
                panelDialoguePanel.SetActive(true);
            }
        }

        /// <summary>
        /// The dialogueManager method takes savedProgressInt as a parameter, and is used to manage adding, and removing slide connections, and in hiding text on buttons, which have had their connection severed.
        /// It uses savedProgressInt, currentSlideIndex, and DialogueController's CheckProgress method to determine which actions to take.
        /// </summary>
        /// <param name="savedProgressInt"></param>
        private void dialogueManager(int savedProgressInt)
        {
            if (savedProgressInt == 3)
            {
                textButtonDialogueOptionTop.gameObject.SetActive(true);
                textButtonDialogueOptionHigh.gameObject.SetActive(true);
                textButtonDialogueOptionLow.gameObject.SetActive(true);
                textButtonDialogueOptionBottom.gameObject.SetActive(true);

                if (dialogueStorage.GetCurrentSlideIndex == 1)
                {
                    progressCheck1 = dialogueStorage.CheckProgress("quest1Completed");
                    progressCheck2 = dialogueStorage.CheckProgress("quest2Completed");
                    progressCheck3 = dialogueStorage.CheckProgress("quest1");

                    if (progressCheck3 == 1)
                    {
                        dialogueStorage.SeverSlideConnectionDC(1);
                        textButtonDialogueOptionTop.gameObject.SetActive(false);
                    }

                    if (progressCheck1 == 1 && progressCheck2 == 1)
                    {
                        dialogueStorage.AddSlideConnectionDC(7, 4);
                    }
                    else
                    {
                        textButtonDialogueOptionBottom.gameObject.SetActive(false);
                    }
                }
                else if (dialogueStorage.GetCurrentSlideIndex == 2)
                {
                    progressCheck1 = dialogueStorage.CheckProgress("quest2Objective");
                    progressCheck2 = dialogueStorage.CheckProgress("quest2Completed");
                    progressCheck3 = dialogueStorage.CheckProgress("quest2");

                    if (progressCheck3 == 1)
                    {
                        dialogueStorage.SeverSlideConnectionDC(1);
                        textButtonDialogueOptionTop.gameObject.SetActive(false);
                    }

                    if (progressCheck1 == 1 && progressCheck2 == 0)
                    {
                        dialogueStorage.AddSlideConnectionDC(9, 3);
                    }
                    else if (progressCheck1 == 1 && progressCheck2 == 1)
                    {
                        dialogueStorage.SeverSlideConnectionDC(3);
                        textButtonDialogueOptionLow.gameObject.SetActive(false);
                    }
                    else
                    {
                        textButtonDialogueOptionLow.gameObject.SetActive(false);
                    }
                }
                else if (dialogueStorage.GetCurrentSlideIndex == 3)
                {
                    progressCheck1 = dialogueStorage.CheckProgress("quest1");
                    progressCheck2 = dialogueStorage.CheckProgress("quest1Completed");

                    if (progressCheck1 == 1 && progressCheck2 == 0)
                    {
                        dialogueStorage.AddSlideConnectionDC(12, 3);
                    }
                    else if (progressCheck1 == 1 && progressCheck2 == 1)
                    {
                        dialogueStorage.SeverSlideConnectionDC(3);
                        textButtonDialogueOptionLow.gameObject.SetActive(false);
                    }
                    else
                    {
                        textButtonDialogueOptionLow.gameObject.SetActive(false);
                    }
                }
                else if (dialogueStorage.GetCurrentSlideIndex == 4)
                {
                    progressCheck1 = dialogueStorage.CheckProgress("quest2");
                    progressCheck2 = dialogueStorage.CheckProgress("quest2Objective");

                    if (progressCheck1 == 1 && progressCheck2 == 0)
                    {
                        dialogueStorage.AddSlideConnectionDC(13, 2);
                    }
                    else if (progressCheck1 == 1 && progressCheck2 == 1)
                    {
                        dialogueStorage.SeverSlideConnectionDC(2);
                        textButtonDialogueOptionHigh.gameObject.SetActive(false);
                    }
                    else
                    {
                        textButtonDialogueOptionHigh.gameObject.SetActive(false);
                    }
                }
            }
            else if (savedProgressInt == 4)
            {
                textButtonDialogueOptionTop.gameObject.SetActive(true);
                textButtonDialogueOptionHigh.gameObject.SetActive(true);
                textButtonDialogueOptionLow.gameObject.SetActive(true);
                textButtonDialogueOptionBottom.gameObject.SetActive(true);
                textButtonPlanetOptionTop.gameObject.SetActive(true);
                textButtonPlanetOptionHigh.gameObject.SetActive(true);
                textButtonPlanetOptionLow.gameObject.SetActive(true);
                textButtonPlanetOptionBottom.gameObject.SetActive(true);

                if (dialogueStorage.GetCurrentSlideIndex == 0)
                {
                    progressCheck1 = dialogueStorage.CheckProgress("quest1Completed");
                    progressCheck2 = dialogueStorage.CheckProgress("quest2Completed");
                    progressCheck3 = dialogueStorage.CheckProgress("quest1");

                    if (progressCheck1 == 1 && progressCheck3 == 1)
                    {
                        dialogueStorage.AddSlideConnectionDC(13, 4);
                        textButtonPlanetOptionLow.gameObject.SetActive(false);
                        dialogueStorage.SeverSlideConnectionDC(2);
                        textButtonPlanetOptionHigh.gameObject.SetActive(false);
                    }
                    else if (progressCheck3 == 1)
                    {
                        dialogueStorage.AddSlideConnectionDC(2, 2);
                        textButtonPlanetOptionBottom.gameObject.SetActive(false);
                    }
                    else
                    {
                        textButtonPlanetOptionHigh.gameObject.SetActive(false);
                        textButtonPlanetOptionBottom.gameObject.SetActive(false);

                    }

                    if (progressCheck2 == 1 && progressCheck1 == 0)
                    {
                        dialogueStorage.AddSlideConnectionDC(12, 3);
                    }
                    else
                    {
                        textButtonPlanetOptionLow.gameObject.SetActive(false);
                    }

                }
                else if (dialogueStorage.GetCurrentSlideIndex == 1)
                {
                    progressCheck1 = dialogueStorage.CheckProgress("quest1");
                    progressCheck2 = dialogueStorage.CheckProgress("quest1Objective");

                    if (progressCheck1 == 1)
                    {
                        dialogueStorage.SeverSlideConnectionDC(1);
                        textButtonDialogueOptionTop.gameObject.SetActive(false);
                    }

                    if (progressCheck1 == 1 && progressCheck2 == 1)
                    {
                        dialogueStorage.AddSlideConnectionDC(4, 3);
                    }
                    else
                    {
                        textButtonDialogueOptionLow.gameObject.SetActive(false);
                    }
                }
            }

        }

        /// <summary>
        /// The updateDialogueRawImage method is used to update the image displayed in the currently active slide. The choise is based on currentProgressInt variable.
        /// </summary>
        private void updateDialogueRawImage()
        {
            if (savedProgressInt == 0)
            {
                rawImagePrologueInteraction.texture = Resources.Load<Texture>("DialoguePrologue");
            }
            if (savedProgressInt == 1)
            {
                rawImageDialogueInteraction.texture = Resources.Load<Texture>("DialogueAdministrator");
            }
            if (savedProgressInt == 2)
            {
                rawImageDialogueInteraction.texture = Resources.Load<Texture>("DialogueCCoT");
            }
            if (savedProgressInt == 3)
            {
                rawImageDialoguePlanetInteraction.texture = Resources.Load<Texture>("DialoguePlanetFertile");
                rawImageDialogueInteraction.texture = Resources.Load<Texture>("DialogueSpaceGondola");
            }
            if (savedProgressInt == 4)
            {
                rawImageDialoguePlanetInteraction.texture = Resources.Load<Texture>("DialogueMoonBlue");
                rawImageDialogueInteraction.texture = Resources.Load<Texture>("DialogueMoonBlue");
            }
            if (savedProgressInt == 5)
            {
                rawImagePrologueInteraction.texture = Resources.Load<Texture>("DialogueOutro");
            }
        }
        /// <summary>
        /// dialogueOutComeManager-method determines which scene is loaded after the dialogue, and whether there is additional progress( such as optional quests) to carry over.
        /// To determine the end result of the current dialogue, the method uses two int-variables, the outcome of the current dialogue("dialogueOutcome"), and stage of the game ("savedProgressInt"). 
        /// </summary>
        private void dialogueOutcomeManager()
        {

            if (dialogueStorage.GetCurrentSlide().GetDialogueOutcome() != 0)
            {
                dialogueOutcome = dialogueStorage.GetCurrentSlide().GetDialogueOutcome();
                Debug.Log("dialogueoutcome" + dialogueOutcome);
                if (dialogueOutcome == 1)
                {
                    savedProgressInt++;
                    savedProgressString = savedProgressInt.ToString();
                    File.WriteAllText("Assets\\Resources\\Progress.txt", savedProgressString);
                    //File.WriteAllText(Application.persistentDataPath + "\\Progress.txt", savedProgressString);
                    SceneManager.LoadScene("DialogueHyperVelocity");
                }
                else if (dialogueOutcome == 2)
                {
                    if (savedProgressInt == 4)
                    {
                        savedProgressInt++;
                        savedProgressString = (savedProgressInt + dialogueOutcome).ToString();
                        File.WriteAllText("Assets\\Resources\\Progress.txt", savedProgressString);
                        //File.WriteAllText(Application.persistentDataPath + "\\Progress.txt", savedProgressString);
                        SceneManager.LoadScene("StarMapHyperVelocity");
                    }
                    else if (savedProgressInt == 5)
                    {
                        savedProgressInt = 0;
                        savedProgressString = savedProgressInt.ToString();
                        File.WriteAllText("Assets\\Resources\\Progress.txt", savedProgressString);
                        SceneManager.LoadScene("MainMenuHyperVelocity");
                    }
                    else
                    {
                        savedProgressInt++;
                        savedProgressString = savedProgressInt.ToString();
                        File.WriteAllText("Assets\\Resources\\Progress.txt", savedProgressString);
                        //File.WriteAllText(Application.persistentDataPath + "\\Progress.txt", savedProgressString);
                        SceneManager.LoadScene("StarMapHyperVelocity");
                    }
                }
                else if (dialogueOutcome == 3)
                {
                    if (savedProgressInt == 1)
                    {
                        savedProgressInt++;
                        savedProgressString = savedProgressInt.ToString();
                        File.WriteAllText("Assets\\Resources\\Progress.txt", savedProgressString);
                        //File.WriteAllText(Application.persistentDataPath + "\\Progress.txt", savedProgressString);
                        SceneManager.LoadScene("DialogueHyperVelocity");
                    }
                    else if (savedProgressInt == 4)
                    {
                        savedProgressInt++;
                        savedProgressString = savedProgressInt.ToString();
                        File.WriteAllText("Assets\\Resources\\Progress.txt", savedProgressString);
                        //File.WriteAllText(Application.persistentDataPath + "\\Progress.txt", savedProgressString);
                        SceneManager.LoadScene("StarMapHyperVelocity");
                    }
                    else
                    {
                        savedProgressInt = 0;
                        savedProgressString = savedProgressInt.ToString();
                        File.WriteAllText("Assets\\Resources\\Progress.txt", savedProgressString);
                        SceneManager.LoadScene("MainMenuHyperVelocity");
                    }

                }
                else
                {
                    savedProgressInt = 0;
                    savedProgressString = savedProgressInt.ToString();
                    File.WriteAllText("Assets\\Resources\\Progress.txt", savedProgressString);
                    SceneManager.LoadScene("MainMenuHyperVelocity");
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
