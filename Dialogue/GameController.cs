using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dialogue;
using UnityEngine.SceneManagement;

/// <summary>
/// GameController links the code segments to unity UI.
/// </summary>
public class GameController : MonoBehaviour {
    private Button buttonDialogueOptionTop;
    private Button buttonDialogueOptionHigh;
    private Button buttonDialogueOptionLow;
    private Button buttonDialogueOptionBottom;
    private Text textButtonDialogueOptionTop;
    private Text textButtonDialogueOptionHigh;
    private Text textButtonDialogueOptionLow;
    private Text textButtonDialogueOptionBottom;
    private Text textDialogueText;
    private DialogueController dialogueStorage = new DialogueController();
    private int dialogueOutcome;
    private int savedProgress = 0;
    //System.IO.StreamWriter Progress = new System.IO.StreamWriter(@"C:\Users\Torsti\Documents\Test\HyperVelocity\Assets\Resources\Progress.txt");



    // Use this for initialization
    /// <summary>
    /// Start() method serves as the link between Unity's UI and the code sections
    /// by making UI elements interactable.
    /// </summary>
    void Start () {
        //Connects the code with UI Button elements
        buttonDialogueOptionTop = GameObject.Find("ButtonDialogueOptionTop").GetComponent<Button>();
        buttonDialogueOptionHigh = GameObject.Find("ButtonDialogueOptionHigh").GetComponent<Button>();
        buttonDialogueOptionLow = GameObject.Find("ButtonDialogueOptionLow").GetComponent<Button>();
        buttonDialogueOptionBottom = GameObject.Find("ButtonDialogueOptionBottom").GetComponent<Button>();
        
        //Connects the code with UI TextButton elements
        textButtonDialogueOptionTop = GameObject.Find("TextButtonDialogueOptionTop").GetComponent<Text>();
        textButtonDialogueOptionHigh = GameObject.Find("TextButtonDialogueOptionHigh").GetComponent<Text>();
        textButtonDialogueOptionLow = GameObject.Find("TextButtonDialogueOptionLow").GetComponent<Text>();
        textButtonDialogueOptionBottom = GameObject.Find("TextButtonDialogueOptionBottom").GetComponent<Text>();
        
        //Connects the code with UI TextDialogueText elements
        textDialogueText = GameObject.Find("TextDialogueText").GetComponent<Text>();
        
        // Allows interaction with UI dialogue buttons 
        buttonDialogueOptionTop.onClick.AddListener(() => buttonPressed(buttonDialogueOptionTop));
        buttonDialogueOptionHigh.onClick.AddListener(() => buttonPressed(buttonDialogueOptionHigh));
        buttonDialogueOptionLow.onClick.AddListener(() => buttonPressed(buttonDialogueOptionLow));
        buttonDialogueOptionBottom.onClick.AddListener(() => buttonPressed(buttonDialogueOptionBottom));

        // Update the information of the initial dialogue slide to the UI
        updateUISlideInfo();

        System.IO.StreamWriter Progress = new System.IO.StreamWriter(@"C:\Users\Torsti\Documents\Test\HyperVelocity\Assets\Resources\Progress.txt");
    }
    /// <summary>
    /// Identifies which UI button is pressed and links the button to corresponding
    /// actions.
    /// IF-statements advance the dialogue to the correspongding direction, and
    /// Update the information on the new dialogue slide to UI.
    /// The final IF-statement stores the out come to of the Dialogue to a variable
    /// </summary>
    /// <param name="button"></param>
    private void buttonPressed(Button button)
    {
        if(button == buttonDialogueOptionTop)
        {
            dialogueStorage.DialogueForward("optionTop");
            updateUISlideInfo();
        }
        else if(button == buttonDialogueOptionHigh)
        {
            dialogueStorage.DialogueForward("optionHigh");
            updateUISlideInfo();
        }
        else if(button == buttonDialogueOptionLow)
        {
            dialogueStorage.DialogueForward("optionLow");
            updateUISlideInfo();
        }
        else if(button == buttonDialogueOptionBottom)
        {
            dialogueStorage.DialogueForward("optionBottom");
            updateUISlideInfo();
        }

        if (dialogueStorage.GetCurrentSlide().GetDialogueOutcome() != 0)
        {
            dialogueOutcome = dialogueStorage.GetCurrentSlide().GetDialogueOutcome();
            Debug.Log(dialogueOutcome);
            if(button == buttonDialogueOptionTop)
            {
                //savedProgress++;
                //Progress.WriteLine(savedProgress);
                SceneManager.LoadScene("DemoSceneCombat");
            }
        }

    }
    /// <summary>
    /// Updates current slide information to the UI.
    /// </summary>
    private void updateUISlideInfo()
    {
        textDialogueText.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("line");
        textButtonDialogueOptionTop.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("top");
        textButtonDialogueOptionHigh.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("high");
        textButtonDialogueOptionLow.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("low");
        textButtonDialogueOptionBottom.text = dialogueStorage.GetCurrentSlide().GetDialogueOpt("bottom");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
