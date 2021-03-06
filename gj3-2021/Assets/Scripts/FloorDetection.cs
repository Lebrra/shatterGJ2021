using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetection : MonoBehaviour
{
    CharacterMovement myChar;

    List<Collider2D> floors;

    void Awake()
    {
        myChar = transform.parent.GetComponent<CharacterMovement>();
        myChar.fd = this;
        floors = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Walkable"))
        {
            floors.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Walkable"))
        {
            floors.Remove(collision);
        }
    }

    public bool OnGround()
    {
        return floors.Count > 0;
    }
}
