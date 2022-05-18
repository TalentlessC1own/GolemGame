using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class refere : MonoBehaviour
{
    [SerializeField]
    private GameObject player1;

    [SerializeField]
    private GameObject player2;


    [SerializeField]
    private GameObject Cong1;

    [SerializeField]
    private GameObject Cong2;


    public void Winner(GameObject player)
    {
        StartCoroutine(Delay(player));
    }

    private IEnumerator Delay(GameObject player)
    {
        yield return new WaitForSeconds(2f);
        if (player == player1)
            Cong2.SetActive(true);
        if (player == player2)
            Cong1.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
