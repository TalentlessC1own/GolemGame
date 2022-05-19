using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arena : MonoBehaviour
{
    

    [SerializeField]
    private SpawnGolem spawn1;

    [SerializeField]
    private SpawnGolem spawn2;

    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;

    private Golems player1_Choice;

    private Golems player2_Choice;

    golem player1_golem;
    golem player2_golem;

    [SerializeField]
    private GameObject timer;
   
        
    

    private void Update()
    {
      
        if (player1.GetComponent<player>().player_ready == true && player2.GetComponent<player>().player_ready == true)
        {
            StartCoroutine(FightDealay());
        }

       
    }
    
    private IEnumerator FightDealay()
    {
        player1.GetComponent<player>().player_ready = false;
        player2.GetComponent<player>().player_ready = false;

        player1.GetComponent<player>().Fight();
        player2.GetComponent<player>().Fight();

        timer.GetComponent<Animator>().SetInteger("state", 1);
        yield return new WaitForSeconds(4);
        timer.GetComponent<Animator>().SetInteger("state", 0);

        player1.GetComponent<player>().Cast();
        player2.GetComponent<player>().Cast();
        yield return new WaitForSeconds(1);

        CreateGolems();
        yield return new WaitForSeconds(1);

        player1_golem.Attack();
        player2_golem.Attack();

        yield return new WaitForSeconds(0.8f);
        DefineWinner();

    }

    private IEnumerator GolemActionDelay(int exod )
    {
        switch (exod)
        {
            case 1:
                player1_golem.Die();
                yield return new WaitForSeconds(0.8f);
                player2_golem.RangeAttack();
                yield return new WaitForSeconds(1f);
                player2_golem.Die();
                break;
            case 2:
                player2_golem.Die();
                yield return new WaitForSeconds(1f);
                player1_golem.RangeAttack();
                yield return new WaitForSeconds(0.8f);
                player1_golem.Die();
                break;
        }
        
    }

        public void SetGolemType(Golems golem,players player)
    {
        switch(player)
        {
            case players.player1:
                player1_Choice = golem; 
                break;
            case players.player2:
                player2_Choice = golem;
                break;
        }
    }
    private void CreateGolems()
    {
       player1_golem = spawn1.CreateGolem(player1_Choice);
       player2_golem = spawn2.CreateGolem(player2_Choice);
    }

    private void DefineWinner()
    {
        int result = (player1_Choice - player2_Choice) % 3;
        switch (result < 0 ? result + 3 : result)
        {

            case 0:
                player1_golem.Die();
                player2_golem.Die();
                break;

            case 1:
                StartCoroutine(GolemActionDelay(1));
                break;

            case 2:
                StartCoroutine(GolemActionDelay(2));
                break;

        }

    }

}

public enum Golems
{
    StoneGolem = 1,
    EarthGolem,
    WoodGolem
}