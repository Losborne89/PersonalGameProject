using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    private bool playerInRange;

    public DialogueManager dialogueManager;

    private void OnEnable()
    {
        //register the event with + 
        InputManager.OnOpenDialogue += HandleOpenDialogue;
    }

    private void OnDisable()
    {
        //register the event with -
        InputManager.OnOpenDialogue -= HandleOpenDialogue;
    }

    private void HandleOpenDialogue()
    {
        // If player is in range and dialogue is not playing 
        if (playerInRange && !dialogueManager.dialogueIsPlaying)
        {
            dialogueManager.StartDialogue();
        }
    }

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            visualCue.SetActive(false);
        }
    }
}