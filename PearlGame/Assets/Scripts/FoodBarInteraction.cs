using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBarInteraction : MonoBehaviour
{
    public Slider progressBar;
    public float playerProximityDistance;
    public KeyCode addPointsKey = KeyCode.F;
    public int maxPoints = 10;

    private int currentPoints = 0;
    private int numberOfPearls = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial value of slider to 0
        progressBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is close to the slider bar
        if (Vector3.Distance(transform.position, progressBar.transform.position) < playerProximityDistance)
        {
            // Check if player presses key
            if (Input.GetKeyDown(addPointsKey))
            {
                // Check if there are enough food pearls in inventory
                if (numberOfPearls > 0)
                {
                    //Add points and update slider bar
                    AddPoints(1);

                    // Decrease food count
                    numberOfPearls--;
                }
            }
        }
       
    }

    void AddPoints(int pointsToAdd)
    {
        currentPoints += pointsToAdd;

        // Points not to exceed maxium amount
        currentPoints = Mathf.Clamp(currentPoints, 0, maxPoints);

        // Update slider bar value based on current points
        progressBar.value = currentPoints;
    }

    // Method called to increase pearl count
    public void PearlsCollected()
    {
        numberOfPearls++;
    }
}
