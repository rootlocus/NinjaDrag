using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static event Action PlayerEnterCheckpoint;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            PlayerEnterCheckpoint?.Invoke();
        }
    }
}
