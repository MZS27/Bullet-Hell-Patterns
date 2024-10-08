using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowScript : MonoBehaviour
{
    public GameObject player;

    private void FixedUpdate()
    {
        transform.position = player.transform.position;
    }
}
