using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    [SerializeField] private float followSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 lastMousePosition;
    private Vector2 offset;
    private bool isDragging = false;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseUp() {
        isDragging = false;
        rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (isDragging) {
            rb.velocity = (GetWorldPosition() - transform.position) * followSpeed; // distance * speed = velocity
        }
    }

    private Vector3 GetWorldPosition()
    {
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return lastMousePosition - offset;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = lastMousePosition - (Vector2) transform.localPosition;
    }

}
