using UnityEngine;
using System.Collections;
using System;

public class Fighter : MonoBehaviour {

    //global health value definition for all the fighters and game
    public const float MAX_HEALTH = 100;

    public float health;
    public Rigidbody RigidBody { private set; get; }
    [SerializeField]
    string fighterName;
    [SerializeField]
    Fighter enemy;
    [SerializeField]
    bool isPlayer;
    public Animator animator;
    AudioSource audioSource;
    public FighterState state;

    public bool enable;

    //for ai randomness
    float random;
    float randomSetTime;

    //for android button handling
    float btClickBegin;
    float btClickEnd = 1f;

	void Start () {
        health = MAX_HEALTH;
        animator = GetComponent<Animator>();
        RigidBody = GetComponent <Rigidbody > ();
        state = FighterState.IDLE;
        audioSource = GetComponent<AudioSource>();
	}

    public string GetName()
    {
        return fighterName;
    }

    public float GetHealthPercent()
    {
        return health / MAX_HEALTH;
    }

    public void PlaySound(AudioClip clip)
    {
        GameAuxiliary.PlaySound(audioSource, clip);
    }

    public void TakeDamage(float amount)
    {
        if (state == FighterState.DEFEND)
            amount *= 0.1f;

        if (amount < health)
            health -= amount;
        else
            health = 0;

        if (health > 0)
            if (state == FighterState.DEFEND)
                animator.SetTrigger("DefendHit");
            else
                animator.SetTrigger("Hit");
        else
            animator.SetTrigger("Dead");
    }

    public bool IsAttacking()
    {
        return state == FighterState.ATTACKING || state == FighterState.OTHER;
    }
	
    public bool IsInvulnerable()
    {
        return (state == FighterState.DEAD) || IsAttacking() || state == FighterState.HIT || state == FighterState.DEFENDHIT;
    }

    public bool IsDefending()
    {
        return state == FighterState.DEFEND;
    }

	// Update is called once per frame
	void Update () {
        //PLAYER INPUT CODE
        if (enable)
        {
            if (isPlayer)
            {
                UpdatePlayerInput();
            }
            else
            {
                UpdateAI();
            }
        }
	}

    private void UpdatePlayerInput()
    {
        //move forward
        if(Input.GetAxis("Horizontal") > 0.1)
        {
            MoveRight();
        }
        //move backward
        if (Input.GetAxis("Horizontal") < -0.1)
        {
            MoveLeft();
        }
        //jump
        if (Input.GetAxis("Vertical") > 0.1)
        {
            MoveUp();
        }
        //crouch
        if (Input.GetAxis("Vertical") < -0.1)
        {
            MoveDown();
        }
        //defend
        if (Input.GetKey(KeyCode.E))
        {
            Defend();
        }
        //punch
        if (Input.GetKeyDown(KeyCode.P))
        {
            Punch();
        }
        //kick
        if (Input.GetKeyDown(KeyCode.K))
        {
            Kick();
        }
        //getoverhere
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpAttack();
        }
    }

    public void MoveUp()
    {
        if(state != FighterState.JUMP)
        {
            animator.SetTrigger("Jump");
            return;
        }
    }

    public void MoveDown()
    {
        animator.SetTrigger("Crouch");
        return;
    }

    public void MoveLeft()
    {
        animator.SetTrigger("WalkBack");
        return;
    }

    public void MoveRight()
    {
        animator.SetTrigger("Walk");
        return;
    }

    public void Punch()
    {
        if (state == FighterState.CROUCH)
            animator.SetTrigger("RoundKick");
        else
            animator.SetTrigger("Punch");
        return;
    }

    public void Kick()
    {
        if (state == FighterState.JUMP)
            animator.SetTrigger("JumpKick");
        else if (state == FighterState.CROUCH)
            animator.SetTrigger("CrouchKick");
        else
            animator.SetTrigger("Kick");
        return;
    }

    public void SpAttack()
    {
        animator.SetTrigger("SpAtk1");
    }

    public void Defend()
    {
        animator.SetTrigger("Defend");
        return;
    }

    void UpdateAI()
    {
        animator.SetBool("enable", enable);
        animator.SetBool("invulnerable", IsInvulnerable());
        animator.SetBool("defending", IsDefending());

        animator.SetBool("opponent_attacking", enemy.IsAttacking());
        animator.SetFloat("opponent_distance", DistanceBetweenFighters());

        //random set saves the last time and Time.time will keep updating and if the difference between then is greaten then n, then it has passes n seconds
        if(Time.time - randomSetTime > 1)
        {
            random = UnityEngine.Random.value;
            randomSetTime = Time.time;
        }

        animator.SetFloat("random", random);
    }

    float DistanceBetweenFighters()
    {
        return Math.Abs(transform.position.z - enemy.transform.position.z);
    }
}
