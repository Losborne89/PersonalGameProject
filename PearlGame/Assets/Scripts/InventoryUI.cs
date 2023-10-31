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

    // Text updated as pearls collected
    public void UpdatePearlText(PlayerInventory playerInventory)
    {
        pearlText.text = playerInventory.numberOfPearls.ToString();
    }
    
}
