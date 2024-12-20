using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FoodBarInteraction : MonoBehaviour
{
    public Slider progressBar;
    private ParticleSystem particleSys;
    public AudioSource creatureEatingSound;

    public float maxInteractionDistance;
    
    public int maxPoints = 10;
    public float particlePlayDuration = 1f;

    private int currentPoints = 0;
    private int numberOfPearls = 0;

    public PlayerInventory playerInventory;
    public GameObject player;
    public GameObject creature;

    public void OnEnable()
    {
        //register the event with + 
        InputManager.OnFeed += HandleOnFeed;
    }

    public void OnDisable()
    {
        //register the event with - 
        InputManager.OnFeed -= HandleOnFeed;
    }

    private void HandleOnFeed()
    {
        // Check if the player is close to the creature
        if (Vector3.Distance(player.transform.position, creature.transform.position) < maxInteractionDistance)
        {

            // Check if there are enough food pearls in inventory
            if (numberOfPearls > 0)
            {
                //Add points and update slider bar
                if (AddPoints(1))
                {
                    // Decrease food count
                    numberOfPearls--;

                    // Play creature eating sound - remember to assign in inspector for creature
                    if (creatureEatingSound != null)
                    {
                        creatureEatingSound.Play();
                    }

                    // Play particle system
                    StartCoroutine(PlayParticlesAndStop());

                    // Decreases pearls when used
                    playerInventory.UsePearls(1);
                }

            }

        }
    }


    private void Awake()
    {
        particleSys = GameObject.Find("Food Bar Particles").GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial value of slider to 0
        progressBar.value = 0;

    }

    // Return a bool indicating if points were added
    bool AddPoints(int pointsToAdd)
    {
        // Check if adding points will be greater than the maximum
        if (currentPoints + pointsToAdd <= maxPoints) 
        {

            currentPoints += pointsToAdd;

            // Update slider bar value based on current points
            progressBar.value = currentPoints;

            // Points added
            return true;
        }
        
        // Points not added
        return false;

    }

    // Coroutine play particles and stop again after seconds
    IEnumerator PlayParticlesAndStop()
    {
        particleSys.Play();

        yield return new WaitForSeconds(particlePlayDuration);

        particleSys.Stop();
    }

    // Method called to increase pearl count
    public void PearlsCollected()
    {
        numberOfPearls++;
    }

    public float GetSliderValue()
    {
        return progressBar.value;
    }
}
