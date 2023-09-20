using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private TextMeshProUGUI waveText; 
    [SerializeField] private int waveNum = 0;
    public float timer = 10f;
    private int increase;

    [Header("Spawner Setting")]
    [SerializeField] private GameObject entity;  
    [SerializeField] private Transform spawnerPlat;
    [SerializeField] private bool spawn;  
    public GameObject[] mobs;  
    [SerializeField] private int amount;
    [SerializeField] private int amountLimit = 20;  // Set initial limit
    [SerializeField] private float timePerSpawn = 2f;

    [Header("Nav Mesh Settings")]
    public NavMeshSurface Surface;
    public GameObject stage;
    public float stageDble = 0f;
    public Transform stageSpawn;
    public Transform spawner;
    

    private void Update()
    {
        UpdateMobsArray();
        Spawner();
        WaveOn();
    }

    private void UpdateMobsArray()
    {
        mobs = GameObject.FindGameObjectsWithTag("Mob");
    }

    private void Spawner()
    {
        if (spawn && amountLimit > 0)
        {
            if (timePerSpawn <= 0f)
            {
                Vector3 randomPosition = RandomPositionWithinSpawner();
                Instantiate(entity, randomPosition, spawnerPlat.rotation);
                amount = 1;
                amountLimit -= amount;
                timePerSpawn = 2f;
            }

            timePerSpawn -= Time.deltaTime;
        }
    }

    private Vector3 RandomPositionWithinSpawner()
    {
        Renderer rend = spawnerPlat.GetComponent<Renderer>();
        Vector3 minBounds = rend.bounds.min;
        Vector3 maxBounds = rend.bounds.max;

        float x = Random.Range(minBounds.x, maxBounds.x);
        float y = spawnerPlat.position.y; // Keep the y position constant
        float z = Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(x, y, z);
    }

    private void WaveOn()
    {
        if(amountLimit == 0)
        {
            spawn = false;
        }

        if (mobs.Length == 0 && !spawn)
        {
            waveNum += 1;
            increase += 2;
            amountLimit = 20 * increase;
            waveText.text = "Wave: " + waveNum;
            stageDble += 1;
            Vector3 Section = new Vector3(stageSpawn.position.x, stageSpawn.position.y, stageSpawn.position.z + (23.05f * stageDble));
            spawner.transform.localScale += new Vector3(23f, 0,0 );
            spawner.transform.position += new Vector3(0, 0, 12f);
            
            Instantiate(stage, Section, stageSpawn.rotation);
            Surface.BuildNavMesh();
            spawn = true;
        }
    }

}
