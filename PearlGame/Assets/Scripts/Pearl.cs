using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Pearl : MonoBehaviour
{
   

    //Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       PlayerInventory playerInventory = other.GetComponent<PlayerInventory>(); 

       if (playerInventory != null)
       {
            // Shows Collects pearls 
            playerInventory.PearlsCollected();
            // Pearls set to inactive once collected
            gameObject.SetActive(false);
       }

     
    }
}
