using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class EntityBase : MonoBehaviour
{
    public delegate void OnDieDelegate();
    public OnDieDelegate OnDie;

    protected bool canDash = true;
    protected TrailRenderer trail;

    private Vector3 initialPosition = Vector3.zero;



    public bool isDead { private set; get; }

    #region Variables
    public enum States
    {
        Init = 0,
        Appearing,
        Normal,
        Hurting,
        Dying,
        Dead,
        Dashing,
        MaxValues
    }
    public States currentState { private set; get; }

    private Animator animator;
    new protected Rigidbody2D rigidbody;
    new protected Collider2D collider;
    new private SpriteRenderer renderer;

    [Header("Data"), SerializeField] protected int id;

    protected int health;
    public float invulnerabilityTime = 1f;

    [SerializeField] protected int healthMax = 3;

    private bool isInvulnerable;
    private const uint invulnerabilityFrames = 5u;

    // Tweens
    [Header("Tweens")]
    public TweenScale tweenAppear;
    public TweenScale tweenDisappear;
    private Vector3 scaleInit = Vector3.zero;
    private Vector3 scaleEnd = Vector3.one;
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
            collider = GetComponent<CircleCollider2D>();
        }
        if (collider == null)
        {
            collider = GetComponentInChildren<CircleCollider2D>();
        }
        if (collider == null)
        {
            //Debug.LogWarning("Collider wasn't setted in " + this.gameObject.name);
        }

        trail = transform.GetComponent<TrailRenderer>();
        trail.sortingLayerName = "Entities";
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
                initialPosition = transform.localPosition;
                health = healthMax;
                Director.Instance.managerUI.SetHealth(id, health);
                isDead = false;
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

        if (isInvulnerable == true)
        {
            StartCoroutine(InvulnerabilityTimed());
        }
    }

    private IEnumerator InvulnerabilityTimed()
    {
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);
        collider.enabled = false;
        yield return new WaitForSeconds(invulnerabilityTime);
        collider.enabled = true;
        isInvulnerable = false;
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
    }

    public void Hurt(int damage = 10)
    {
        if (!isInvulnerable)
        {
            health -= damage;

            Director.Instance.managerUI.SetHealth(id, health);

            //Debug.Log(name+" was hurt, remaining health: "+ health);

            if (health <= 0)
            {
                ChangeState(States.Dying);
            }
            else
            {
                ChangeState(States.Hurting);
                SetInvulnerability(true);
            }
        }
    }

    private void Die()
    {
        isDead = true;

        if (OnDie != null)
        {
            OnDie();
        }

        //Debug.Log(name + " has died.");

        // TODO: Esto peta? deberia borrarse a si mismo? borrarse desde fuera?
        Director.Instance.managerEntity.RemovePlayer(id);

        //Debug.Log(name + " has died.");
    }

    protected void Reappear()
    {
        ChangeState(States.Appearing);
        //transform.localPosition = Vector2.zero;
        transform.localPosition = initialPosition;
        rigidbody.velocity = Vector2.zero;
        canDash = true;
    }
    #endregion


    #region Animations and events
    private void PlayAnimationAppearing()
    {
        // Play animation, when it is finished change state to Normal
        float scaleInit = 0f;
        float scaleFinish = 1f;
        float time = 0.4f;
        Ease ease = Ease.OutBack;

        transform.localScale.Set(scaleInit, scaleInit, transform.localScale.z);
        transform.DOScale(new Vector3(scaleFinish, scaleFinish, transform.localScale.z), time)
                          .SetEase(ease)
                          .OnComplete(PlayAnimationAppearingHelper);

    }

    private void PlayAnimationAppearingHelper()
    {
        ChangeState(States.Normal);
    }

    private void PlayAnimationHurting()
    {
        // Play animation, when it is finished change state to Normal
        float scaleInit = 1f;
        float scaleFinish = 0f;
        float time = 0.4f;
        Ease ease = Ease.OutBack;

        transform.localScale.Set(scaleInit, scaleInit, transform.localScale.z);
        transform.DOScale(new Vector3(scaleFinish, scaleFinish, transform.localScale.z), time)
                          .SetEase(ease)
                          .OnComplete(Reappear);

    }

    private void PlayAnimationDying()
    {
        // Play animation, when it is finished change state to Dead
        ChangeState(States.Dead);
    }
    #endregion

}
