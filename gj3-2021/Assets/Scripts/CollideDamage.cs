using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideDamage : MonoBehaviour
{
    public enum type
    {
        pitFall,
        spike,
        shock
    }

    public type myType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            switch (myType)
            {
                case type.pitFall:
                    PlayerSound.inst.PlayFall();
                    break;
                case type.spike:
                    PlayerSound.inst.PlayHit();
                    break;
                case type.shock:
                    PlayerSound.inst.PlayShock();
                    break;
                default:
                    break;
            }
            LevelManager.inst.EndLevel(false);
            collision.gameObject.GetComponent<CharacterMovement>().stopMovement();
        }
    }
}
