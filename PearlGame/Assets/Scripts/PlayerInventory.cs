using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] public int numberOfPearls {  get; private set; }

    [SerializeField] public UnityEvent<PlayerInventory> OnPearlCollected;


    public void PearlsCollected()
    {
        // Increment number of pearls collected
        numberOfPearls++;
        OnPearlCollected.Invoke(this);
    }
}
