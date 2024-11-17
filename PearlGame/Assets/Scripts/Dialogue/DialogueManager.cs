using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices; // Choice buttons
    private TextMeshProUGUI[] choicesText; 

    [Header("Continue Icon")]
    [SerializeField] private GameObject continueIcon; 

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; } // Read-only

    private int currentChoiceIndex = 0; // Track currently selected choice

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false); // Initially hide dialogue panel
        continueIcon.SetActive(false); // Initially hide continue icon

        //sets up an array of text choices for each choice in the inkly file 
        choicesText = new TextMeshProUGUI[choices.Length];
        for (int index = 0; index < choices.Length; index++)
        {
            choicesText[index] = choices[index].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        if (dialogueIsPlaying == false)
        {
            //exit early if the dialogue is not open dont do any more of this update loop
            return;
        }

        // Check if choices available to navigate
        if (currentStory.currentChoices.Count > 0)
        {
            // Navigate choices with arrow keys
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // short hand adding 1, currentChoiceIndex = currentChoiceIndex + 1;
                currentChoiceIndex--;
                currentChoiceIndex = CheckIndex(currentChoiceIndex);
                HighlightChoice(currentChoiceIndex);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // short hand subtracting 1, currentChoiceIndex = currentChoiceIndex - 1;
                currentChoiceIndex++;
                currentChoiceIndex = CheckIndex(currentChoiceIndex);
                HighlightChoice(currentChoiceIndex);
            }

            // Confirm choice with space key
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ConfirmChoice();
            }
        }
        else
        {
            // Cycle through dialogue with Space key
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ContinueStory();
            }
        }
    }

    private int CheckIndex(int newIndex)
    {  
        //if the new index is greater than the last one in list wrap back to start
        if(newIndex > currentStory.currentChoices.Count - 1)
        {
           return newIndex = currentStory.currentChoices.Count - 1;
        }
        //if the new index is less than the first element set it to the last element
        if(newIndex < 0)
        {
            return newIndex = 0;
        }
        return newIndex;
    }


    public void EnterDialogueMode(TextAsset inkJSON)
    {
        //sets the story to the one in the json
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
       
        dialoguePanel.SetActive(true);  
        continueIcon.SetActive(false);

        // Reset choice index when entering dialogue
        currentChoiceIndex = 0;
        // Highlight choice
        HighlightChoice(currentChoiceIndex);

        ContinueStory();
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
            // Show the continue icon with dialogue
            continueIcon.SetActive(true);
        }
        else
        {
            dialogueIsPlaying = false;
            dialoguePanel.SetActive(false); 
            continueIcon.SetActive(false);
            dialogueText.text = "";
            //delays the end to stop accidental jumping
            Invoke(nameof(EndDialogue), 0.2F);
        }
    }

    public void StartDialogue()
    {
        // Start dialogue and disable player movement
        EnterDialogueMode(inkJSON);
        GameManager.GetInstance().DisablePlayerMovement();
    }

    public void EndDialogue()
    {
        // Called by DialogueManager when dialogue ends
        GameManager.GetInstance().EnablePlayerMovement();
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        //loops through the choices
        for (int i = 0; i < choices.Length; i++)
        {
            if (i < currentChoices.Count)
            {
                //sets choice text active and the text to currentchoice text
                choices[i].SetActive(true);
                choicesText[i].text = currentChoices[i].text;
            }
            else
            {
                //if there is no inkly choice hide the choice gameobject
                choices[i].SetActive(false);
            }
        }

        // Reset current choice index
        currentChoiceIndex = 0;
        // Highlight choice
        HighlightChoice(currentChoiceIndex);
    }


    private void HighlightChoice(int choiceIndex)
    {
        //loops through the choices and sets the highlight colour
        for (int i = 0; i < choices.Length; i++)
        {
            if (i == choiceIndex)
            {
                // when selected
                choices[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                // when unselected
                choices[i].GetComponent<Image>().color = Color.clear;
            }
        }
    }

    private void ConfirmChoice()
    {
        if (currentStory.currentChoices.Count > currentChoiceIndex)
        {
            // Confirm choice
            currentStory.ChooseChoiceIndex(currentChoiceIndex);
            // Continue story after confirming choice
            ContinueStory();
        }
    }
    
}