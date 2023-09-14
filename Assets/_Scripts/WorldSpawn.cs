using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpawn : MonoBehaviour
{
    public GameObject Spawn;
    public GameObject Player;


    void Start()
    {
        Player.transform.position = Spawn.transform.position;
    }
}
