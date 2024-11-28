using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    private bool playerInRange;

    public DialogueManager dialogueManager;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        // If player is in range and dialogue is not playing 
        if (playerInRange && !dialogueManager.dialogueIsPlaying)
        {
            visualCue.SetActive(true);

            // Check if interact key is pressed
            if (Input.GetKeyDown(KeyCode.I))
            {
                dialogueManager.StartDialogue();
            }
        }
        //not in range
        else
        {
            // Hide visual cue when out of range or dialogue is playing
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}