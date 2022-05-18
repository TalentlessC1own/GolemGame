using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGolem : MonoBehaviour
{
    private golem StoneGolem;
    private golem WoodGolem;
    private golem EarthGolem;

    GameObject spawner;
    Vector3 position;

    private golem selected_golem;
    private void Awake()
    {
        StoneGolem = Resources.Load<golem>("StoneGolem");
        WoodGolem = Resources.Load<golem>("WoodGolem");
        EarthGolem = Resources.Load<golem>("EarthGolem");
    }
    private void Start()
    {
        spawner = gameObject;
        position = transform.position;
    }
    
    private void  SelectGolem(Golems golem_type)
    {
        switch (golem_type)
        {
            case Golems.StoneGolem:
                selected_golem = StoneGolem;
                break;
            case Golems.WoodGolem:
                selected_golem = WoodGolem;
                break;
            case Golems.EarthGolem:
                selected_golem=EarthGolem;
                break;
        }
    }
    public golem CreateGolem(Golems golem_type)
   {
        SelectGolem(golem_type);
        
        golem newGolem = Instantiate(selected_golem, position,Quaternion.identity) as golem;

        if (spawner.name == "SpawnGolemPlayer2")
            newGolem.Sprite = true;
        return newGolem;
    }
}
