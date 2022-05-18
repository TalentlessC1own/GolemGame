using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private bool taking_damage = false;
    private bool die = false;
    private bool casting = false;

    [SerializeField]
    refere referee;

    [SerializeField]
    private IconScript IconPanel;

    [SerializeField]
    players player_number;

    [SerializeField]
    private arena _arena;

    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private HealthBar healthBar;

    private Animator animator;

    public bool player_ready;

    private bool fighting = false;
    
    Golems user_choice;
    private PlayerState state
    {
        get { return (PlayerState)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }
    private void Start()
    {
        player_ready = false;
        healthBar.SetMaxHealth(lives);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!taking_damage && !die && !casting)
            state = PlayerState.idle;
       
        if(lives == 0)
            Die();
        if (player_number == players.player1 && !player_ready && !die && !fighting)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
               _arena.SetGolemType(Golems.StoneGolem,player_number);
                player_ready = true;
                IconPanel.IconOff();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _arena.SetGolemType(Golems.WoodGolem, player_number);
                player_ready = true;
                IconPanel.IconOff();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                _arena.SetGolemType(Golems.EarthGolem, player_number);
                player_ready = true;
                IconPanel.IconOff();
            }
        }

        if (player_number == players.player2 && !player_ready && !die && !fighting)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                _arena.SetGolemType(Golems.StoneGolem, player_number);
                player_ready = true;
                IconPanel.IconOff();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                _arena.SetGolemType(Golems.WoodGolem, player_number);
                player_ready = true;
                IconPanel.IconOff();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                _arena.SetGolemType(Golems.EarthGolem, player_number);
                player_ready = true;
                IconPanel.IconOff();
            }
        }

        if (Input.GetKeyDown(KeyCode.K) )
        {
            player_ready = true;
        }
    }

    private IEnumerator TakeDamageAnim()
    {
        taking_damage = true;
        lives--;
        healthBar.SetHealth(lives);
        state = PlayerState.takedamage;
        yield return new WaitForSeconds(1f);
        taking_damage = false;
    }
    public void Fight()
    {
        StartCoroutine(FightingTime());
    }
    private IEnumerator FightingTime()
    {
        fighting = true;
        yield return new WaitForSeconds(10f);
        fighting = false;
        IconPanel.IconOn();
    }
        private IEnumerator CastAnim()
    {
        casting = true;
        state = PlayerState.cast;
        yield return new WaitForSeconds(1.2f);
        casting = false;
    }

    public void Cast()
    {
        StartCoroutine(CastAnim());
    }

    private void Die()
    {
        die = true;
        state = PlayerState.die;
        referee.Winner(gameObject);
        Destroy(gameObject,1);
    }

    public void TakeDamage()
    {
        StartCoroutine(TakeDamageAnim());
       
    }

    
}

public enum players { player1, player2 };
public enum PlayerState
{
    idle,
    cast,
    takedamage,
    die
}