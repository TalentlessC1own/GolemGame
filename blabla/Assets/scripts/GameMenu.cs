using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private LevelChanger levelChanger;
    public void Restart()
    {
        levelChanger.FadeToLevel(SceneManager.GetActiveScene().buildIndex); ;
        DataHolder.player_1_lives = 3;
        DataHolder.player_2_lives = 3;
        DataHolder.was_fight = false;
    }
    public void GoTOMainMenu()
    {
        levelChanger.FadeToLevel(0);
    }
}
