using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class spawner : MonoBehaviour
{
    public GameObject spawnee;
    public int TotalSpawnee;
    public float TimeToSpawn;
    private GameObject[] SpawneeList;
    private bool PositionSet;
    // Start is called before the first frame update
    void Start()
    {
        //  StartCoroutine(ChangePosition());
        StartCoroutine(SpawnLoop());
        SpawneeList = new GameObject[TotalSpawnee];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool SetPosition()
    {
        Transform cam = Camera.main.transform;
        transform.position = cam.forward * 10;
        return true;
    }

    private IEnumerator ChangePosition()
    {
        yield return new WaitForSeconds(0.2f);
            if (!PositionSet)
        {
            if (VuforiaBehaviour.Instance.enabled)
            {
                SetPosition();
            }
        }
        
    }
    private IEnumerator SpawnLoop()
    {
        StartCoroutine(ChangePosition());
        yield return new WaitForSeconds(1f);
        int i = 0;
        while (i <= (TotalSpawnee - 1))
        {
            SpawneeList[i] = SpawnElement();
            i++;
            yield return new WaitForSeconds(Random.Range(TimeToSpawn, TimeToSpawn * 3));
        }
       
    }
    private GameObject SpawnElement()
    {
        GameObject spawnobject = Instantiate(spawnee, (Random.insideUnitSphere * 4) + transform.position, transform.rotation) as GameObject;
        float scale = Random.Range(0.5f, 2f);
        spawnobject.transform.localScale = new Vector3(scale, scale, scale);
        return spawnobject;
    }

}
