using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour
{
    public delegate void OnDieDelegate();
    public OnDieDelegate OnDie;

    #region Variables
    protected enum States
    {
        Init = 0,
        Appearing,
        Normal,
        Hurting,
        Dying,
        Dead,
        MaxValues
    }
    protected States currentState { private set; get; }

    private Animator animator;
    new private Rigidbody2D rigidbody;
    new private Collider2D collider;
    new private SpriteRenderer renderer;

    protected int health;
    protected const int healthMax = 20;

    private bool isInvulnerable;
    private const uint invulnerabilityFrames = 5u;
    #endregion



    #region Monobehaviour
    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (animator == null)
        {
            //Debug.LogWarning("Animator wasn't setted in " + this.gameObject.name);
        }

        if (rigidbody == null)
        {
            rigidbody = transform.GetComponent<Rigidbody2D>();
        }
        if (rigidbody == null)
        {
            rigidbody = transform.GetComponentInChildren<Rigidbody2D>();
        }
        if (rigidbody == null)
        {
            //Debug.LogWarning("Rigidbody wasn't setted in " + this.gameObject.name);
        }

        if (renderer == null)
        {
            renderer = GetComponent<SpriteRenderer>();
        }
        if (renderer == null)
        {
            renderer = GetComponentInChildren<SpriteRenderer>();
        }
        if (renderer == null)
        {
            //Debug.LogWarning("Renderer wasn't setted in " + this.gameObject.name);
        }

        if (collider == null)
        {
            collider = GetComponent<Collider2D>();
        }
        if (collider == null)
        {
            collider = GetComponentInChildren<Collider2D>();
        }
        if (collider == null)
        {
            //Debug.LogWarning("Collider wasn't setted in " + this.gameObject.name);
        }

    }

    private void Start()
    {
        ChangeState(States.Init);
    }

    #endregion


    #region Entity management
    protected void ChangeState(States to)
    {
        currentState = to;
        //Debug.Log("Change the entity " + name + " state to: " + currentState);
        RunState();
    }

    private void RunState()
    {
        switch (currentState)
        {
            case States.Init:
                SetInvulnerability(true);
                health = healthMax;
                Director.Instance.managerUI.SetHealth(health);
                ChangeState(States.Appearing); // Automatically change to appearing
                break;

            case States.Appearing:
                PlayAnimationAppearing(); // Play appearing animation
                break;

            case States.Normal:
                SetInvulnerability(false);
                break;

            case States.Hurting:
                SetInvulnerability(true);
                PlayAnimationHurting(); // Play hurting animation
                break;

            case States.Dying:
                SetInvulnerability(true);
                PlayAnimationDying(); // Play dying animation
                break;

            case States.Dead:
                Die();
                break;
        }
    }

    private void SetInvulnerability(bool to)
    {
        isInvulnerable = to;
    }

    //private IEnumerator InvulnerabilityTimed()
    //{
    //    isInvulnerable = true;
    //    for (uint i = 0; i < invulnerabilityFrames; i++)
    //    {
    //        yield return new WaitForEndOfFrame();
    //    }
    //    isInvulnerable = false;
    //}

    public void Hurt( int damage = 10)
    {
        if (!isInvulnerable)
        {
            health -= damage;
            Director.Instance.managerUI.SetHealth(health);

            //Debug.Log(name+" was hurt, remaining health: "+ health);

            if (health <= 0)
            {
                ChangeState(States.Dying);
            }
            else
            {
                ChangeState(States.Hurting);
            }
        }
    }

    private void Die()
    {
        // TODO: This is a placeholder, this should be better integrated with entitymanager

        if (OnDie != null)
        {
            OnDie();
        }

        //Debug.Log(name + " has died.");
    }
    #endregion


    #region Animations and events
    private void PlayAnimationAppearing()
    {
        // Play animation, when it is finished change state to Normal
        ChangeState(States.Normal);
    }

    private void PlayAnimationHurting()
    {
        // Play animation, when it is finished change state to Normal
        ChangeState(States.Normal);
    }

    private void PlayAnimationDying()
    {
        // Play animation, when it is finished change state to Dead
        ChangeState(States.Dead);
    }

    #endregion


    #region Physics

    //void OnTriggerExit(Collider col)
    //{
    //    if (col.gameObject.CompareTag("Boundary"))
    //    {
    //        Die();
    //    }
    //}
    #endregion
}
