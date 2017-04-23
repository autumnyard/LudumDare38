using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityPlayer : EntityBase
{
    public delegate void OnExitDelegate();
    public OnExitDelegate OnExit;

    public delegate void OnCollisionDelegate(Vector2 position);
    public OnCollisionDelegate OnCollision;

    public SpriteRenderer sprite;

    private float dashTime = 0.5f;
    private float dashAgainTime = 0.5f;
    private float impactForce = 6f;

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

    Coroutine dashTimerCoroutine;
    public void Dash()
    {
        if (canDash == false) { return; }

        canDash = false;
        ChangeState(States.Dashing);
        dashTimerCoroutine = StartCoroutine(DashTimer());
        trail.time = 0.5f;

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

    public void Set(int idNew, Sprite to )
    {
        id = idNew;
        sprite.sprite = to;
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
            if (dashTimerCoroutine != null)
            {
                StopCoroutine(dashTimerCoroutine);
            }
            Hurt(1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<EntityBase>().currentState == States.Dashing)
        {
            //rigidbody.sharedMaterial.bounciness = 12;
            //collider.enabled = false;
            //collider.enabled = true;

            //Vector2 impactVelocity = collision.rigidbody.velocity.normalized;
            //rigidbody.AddForce(impactVelocity * 20f, ForceMode2D.Impulse);

            rigidbody.AddForce(rigidbody.velocity.normalized * impactForce, ForceMode2D.Impulse);

            if (OnCollision != null)
            {
                OnCollision(transform.localPosition);
            }

        }
    }

    IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(dashTime);
        if (currentState == States.Dashing)
        {
            ChangeState(States.Normal);
            trail.time = 0.1f;
        }

        yield return new WaitForSeconds(dashAgainTime);
        canDash = true;
    }
}
