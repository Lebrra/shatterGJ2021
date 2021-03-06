using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed;
    public bool facingRight = true;

    [HideInInspector]
    public FloorDetection fd;
    public bool onGround = false;

    public CharacterBumper leftBumper;
    public CharacterBumper rightBumper;

    void Start()
    {
        facingRight = true;
    }

    void Update()
    {
        onGround = fd.OnGround();
        if (onGround)
        {
            if (facingRight) transform.position = Vector2.MoveTowards(transform.position, new Vector2(100, transform.position.y), moveSpeed * Time.deltaTime);
            else transform.position = Vector2.MoveTowards(transform.position, new Vector2(-100, transform.position.y), moveSpeed * Time.deltaTime);

            if (leftBumper.overlapped && !facingRight) facingRight = true;
            else if (rightBumper.overlapped && facingRight) facingRight = false;
        }
    }
}
