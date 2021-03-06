using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBumper : MonoBehaviour
{
    public bool overlapped = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Walkable")) overlapped = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Walkable")) overlapped = false;
    }
}
