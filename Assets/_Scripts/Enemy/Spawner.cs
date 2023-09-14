using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject _Zombie;
    public Transform spawnPos;
    public bool Spawn;

    void Start()
    {
        Spawn = false;
    }

    void Update()
    {
        if(Spawn)
        {
            GameObject zombie = Instantiate(_Zombie, spawnPos.position, spawnPos.rotation);
        }
    }
}
