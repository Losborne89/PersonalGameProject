using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] public int numberOfPearls {  get; private set; }

    [SerializeField] public UnityEvent<PlayerInventory> OnPearlCollected;

    public FoodBarInteraction foodBarInteraction;

    void Start()
    {
        // Insure reference is set
        if (foodBarInteraction == null)
        {
            Debug.LogError("FoodBarInteraction reference not set in PlayerInventory script ");
        }
    }

    public void PearlsCollected()
    {
        // Increment number of pearls collected
        numberOfPearls++;
        OnPearlCollected.Invoke(this);

        if (foodBarInteraction != null)
        {
            foodBarInteraction.PearlsCollected();
        }
    }

    public void UsePearls(int count)
    {
        // Decreases when pearls used
        numberOfPearls -= count;
        // Pearl count doesnt go below zero
        numberOfPearls = Mathf.Max(0, numberOfPearls);
    }
}
