using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] private Vector3 offset = new Vector3 (0f, 2f, -10f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Camera position to player
        transform.position = player.transform.position + offset;
    }
}
