using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //end level
            Debug.Log("Level Complete!");
            collision.GetComponent<CharacterMovement>().stopMovement();
            LevelManager.inst.EndLevel(true);
        }
    }
}
