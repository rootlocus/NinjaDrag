using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public static event Action PlayerFoundFriend;
    private AudioSource source;
    private SpriteRenderer sprite;
    private BoxCollider2D myCollider;

    private void Awake() {
        source = GetComponent<AudioSource>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            source.Play();
            PlayerFoundFriend?.Invoke();
            sprite.enabled = false;
            myCollider.enabled = false;
        }
    }
}
