using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPlayer : EntityBase
{
    public delegate void OnExitDelegate();
    public OnExitDelegate OnExit;

    [SerializeField, Range(4f, 20f)] private float speed = 7f;
    [SerializeField, Range(0.1f, 6f)] private float dash = 4f;
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

    public void Dash()
    {
        Vector2 direction = Vector2.zero;

        switch (id)
        {
            case 1:
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        direction.y = 1;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        direction.y = -1;
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        direction.x = -1;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        direction.x = 1;
                    }
                    break;
                }

            case 2:
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        direction.y = 1;
                    }
                    else if (Input.GetKey(KeyCode.DownArrow))
                    {
                        direction.y = -1;
                    }

                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        direction.x = -1;
                    }
                    else if (Input.GetKey(KeyCode.RightArrow))
                    {
                        direction.x = 1;
                    }
                    break;
                }
            default: break;
        }

        //if(Input.)
        //rigidbody.velocity.normalized;

        // rigidbody.velocity = rigidbody.velocity.normalized;
        if (direction == Vector2.zero)
        {
            rigidbody.AddForce(rigidbody.velocity.normalized * dash, ForceMode2D.Impulse);
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(direction.normalized * dash, ForceMode2D.Impulse);
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Map"))
        {
            // Destroy everything that leaves the trigger
            //Destroy(other.gameObject);
            /*
            if (OnExit != null)
            {
                OnExit();
            }
            */
            Hurt(1);
        }
    }
}
