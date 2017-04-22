using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPlayer : EntityBase
{

    [SerializeField, Range(4f, 20f)] private float speed = 7f;
    [SerializeField, Range(1f, 20f)] private float drag = 3f;

    public void MoveUp()
    {
        rigidbody.AddForce(Vector2.up * speed, ForceMode2D.Force);
    }
    public void MoveDown()
    {
        rigidbody.AddForce(Vector2.down * speed, ForceMode2D.Force);
    }
    public void MoveLeft()
    {
        rigidbody.AddForce(Vector2.left * speed, ForceMode2D.Force);
    }
    public void MoveRight()
    {
        rigidbody.AddForce(Vector2.right * speed, ForceMode2D.Force);
    }
}
