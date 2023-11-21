using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI pearlText;

    // Start is called before the first frame update
    void Start()
    {
       pearlText = GetComponent<TextMeshProUGUI>(); 
    }

    
    public void UpdatePearlText(PlayerInventory playerInventory)
    {
        // Text updated as pearls collected
        pearlText.text = playerInventory.numberOfPearls.ToString();
    }
    
}
