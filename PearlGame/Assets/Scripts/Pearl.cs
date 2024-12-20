using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pearl : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private GameObject pearlGraphics;
    [SerializeField] private SphereCollider sphereCollider;

    public static Action OnPearlCollected;

    private void OnTriggerEnter(Collider other)
    {
        // Shows collected pearls 
        OnPearlCollected?.Invoke();

        audioSource1.Play();

        // Pearls set to inactive once collected
        pearlGraphics.SetActive(false);

        sphereCollider.enabled = false;
    }
}
