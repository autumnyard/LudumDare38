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

    [Header("Movement physics")]
    public float dashTime = 0.5f;
    public float dashAgainTime = 0.5f;
    public float impactForce = 6f;
    public float velocityLimit = 6f;
    public float moonBounce = 2f;

    [SerializeField, Range(4f, 20f)] private float runSpeed = 4f;
    [SerializeField, Range(0.1f, 10f)] private float dashSpeed = 6f;

    [Header("Sound effects")]
    public AudioSource sfxChoque;
    public AudioSource sfxDash;


    public void MoveUp()
    {
        rigidbody.AddForce(Vector2.up * runSpeed, ForceMode2D.Force);
    }
    public void MoveDown()
    {
        rigidbody.AddForce(Vector2.down * runSpeed, ForceMode2D.Force);
    }
    public void MoveLeft()
    {
        rigidbody.AddForce(Vector2.left * runSpeed, ForceMode2D.Force);
    }
    public void MoveRight()
    {
        rigidbody.AddForce(Vector2.right * runSpeed, ForceMode2D.Force);
    }

    Coroutine dashTimerCoroutine;

    public void Dash()
    {
        // Espera a que pueda hacer dash
        if (canDash == false) { return; }

        canDash = false;
        ChangeState(States.Dashing);
        dashTimerCoroutine = StartCoroutine(DashTimer());

        // TRAIL
        trail.time = 0.5f;

        /*
        // GHOST
        Debug.Log(" + Start dash");
        GetComponent<GhostSprites>().enabled = true;
        */
        if (sfxDash != null)
        {
            sfxDash.Play();
        }

        Vector2 direction = Vector2.zero;

        switch (id)
        {
            case 2:
                {
                    if (Input.GetKey(KeyCode.W) || (Input.GetAxis("JoyY1") < 0))
                    {
                        direction.y = 1;
                    }
                    else if (Input.GetKey(KeyCode.S) || (Input.GetAxis("JoyY1") > 0))
                    {
                        direction.y = -1;
                    }

                    if (Input.GetKey(KeyCode.A) || (Input.GetAxis("JoyX1") < 0))
                    {
                        direction.x = -1;
                    }
                    else if (Input.GetKey(KeyCode.D) || (Input.GetAxis("JoyX1") > 0))
                    {
                        direction.x = 1;
                    }
                    break;
                }

            case 1:
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
            case 3:
                {
                    if (Input.GetKey(KeyCode.U) || (Input.GetAxis("JoyY2") < 0))
                    {
                        direction.y = 1;
                    }
                    else if (Input.GetKey(KeyCode.J) || (Input.GetAxis("JoyY2") > 0))
                    {
                        direction.y = -1;
                    }

                    if (Input.GetKey(KeyCode.H) || (Input.GetAxis("JoyX2") < 0))
                    {
                        direction.x = -1;
                    }
                    else if (Input.GetKey(KeyCode.K) || (Input.GetAxis("JoyX2") > 0))
                    {
                        direction.x = 1;
                    }
                    break;
                }
            default: break;
        }

        // If we are not pressing anything, dash on the current direction
        if (direction == Vector2.zero)
        {
            rigidbody.AddForce(rigidbody.velocity.normalized * dashSpeed, ForceMode2D.Impulse);
        }
        else // If we are pressing on a certain direction, dash over there
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(direction.normalized * dashSpeed, ForceMode2D.Impulse);
        }

    }

    public void Set(int idNew, Sprite to, int life)
    {
        id = idNew;
        sprite.sprite = to;

        healthMax = life;
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
            Camera.main.GetComponent<TweenShake>().Play();
            
            //rigidbody.sharedMaterial.bounciness = 12;
            //collider.enabled = false;
            //collider.enabled = true;

            //Vector2 impactVelocity = collision.rigidbody.velocity.normalized;
            //rigidbody.AddForce(impactVelocity * 20f, ForceMode2D.Impulse);

            rigidbody.AddForce(rigidbody.velocity.normalized * impactForce, ForceMode2D.Impulse);
            if (rigidbody.velocity.magnitude > velocityLimit)
            {
                rigidbody.velocity = rigidbody.velocity.normalized * velocityLimit;
            }
            Vector3 holePosition = new Vector3((collision.transform.position.x + transform.position.x) / 2,
                                               (collision.transform.position.y + transform.position.y) / 2, transform.position.z);

            if (OnCollision != null)
            {
                OnCollision(holePosition);
            }

            //Detect collision with earth piece
            Collider2D collider = Physics2D.OverlapCircle(holePosition, 0.1f, 1 << LayerMask.NameToLayer("MapPieces"));
            if (collider != null)
            {
                collider.gameObject.GetComponent<TweenScale>().enabled = true;
                collider.gameObject.GetComponent<TweenShake>().enabled = true;
                collider.gameObject.GetComponent<TweenAlpha>().enabled = true;
                collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }

        }
        else if (collision.gameObject.CompareTag("Moon"))
        {
            rigidbody.AddForce(rigidbody.velocity.normalized * moonBounce, ForceMode2D.Impulse);
        }
        else
        {

            if (sfxChoque != null)
            {
                sfxChoque.Play();
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
            /*
            Debug.Log(" - Finish dash");
            GetComponent<GhostSprites>().ClearTrail();
            GetComponent<GhostSprites>().enabled = false;
            */
            //GetComponent<GhostSprites>().TrailSize = 0;
        }

        yield return new WaitForSeconds(dashAgainTime);
        canDash = true;
    }
}
