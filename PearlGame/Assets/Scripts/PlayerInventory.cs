using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] public int numberOfPearls {  get; private set; }

    [SerializeField] public UnityEvent<PlayerInventory> OnPearlCollected;

    [SerializeField] private AudioSource audioSource1;

    public FoodBarInteraction foodBarInteraction;

    public InventoryUI inventoryUI;

    void Start()
    {
        // Insure reference is set
        if (foodBarInteraction == null)
        {
            Debug.LogError("FoodBarInteraction reference not set in PlayerInventory script ");
        }

        // Insure reference is set 
        if (inventoryUI == null)
        {
            Debug.LogError("InventoryUI reference not set in PlayerInventory script");
        }

        
        
    }

    public void PearlsCollected()
    {
        // Number of pearls collected
        numberOfPearls++;
        OnPearlCollected.Invoke(this);
 
        if (foodBarInteraction != null)
        {
            foodBarInteraction.PearlsCollected();
            // UI updated when pearls collected
            inventoryUI.UpdatePearlText(this);

        }
    }

    public void UsePearls(int count)
    {
        // Decreases when pearls used
        numberOfPearls -= count;
        // Pearl count doesnt go below zero
        numberOfPearls = Mathf.Max(0, numberOfPearls);
        // UI updated when pearls used 
        inventoryUI.DecreasePearlCount();
    }
}
