using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pearl : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private GameObject pearlGraphics;
    [SerializeField] private SphereCollider sphereCollider;

    //Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       PlayerInventory playerInventory = other.GetComponentInChildren<PlayerInventory>(); 

       if (playerInventory != null)
       {
            // Shows collected pearls 
            playerInventory.PearlsCollected();

            audioSource1.Play();

            // Pearls set to inactive once collected
            pearlGraphics.SetActive(false);

            sphereCollider.enabled = false;

        }

     
    }
}
