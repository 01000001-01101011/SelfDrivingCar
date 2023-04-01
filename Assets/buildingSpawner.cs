using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class buildingSpawner : MonoBehaviour
{
    public PathCreator[] paths  = new PathCreator[2];
    public Transform holder;
    public GameObject target;
    public int count = 100;
    public float scaleDiff = 0.3f;
    public float radius = 100;
    public float margin = 1000;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i<count; i++)
        {
            SpawnBuilding();
        }
    }

    public void SpawnBuilding()
    {

        Vector2 spawnPo = Vector2.zero;
        Vector3 pathPos;
        spawnPo = Random.insideUnitCircle * radius + new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
        float dist = margin * 2;
        foreach (PathCreator path in paths)
        {
            pathPos = path.path.GetClosestPointOnPath(new Vector3(spawnPo.x, 0, spawnPo.y));
            dist = Mathf.Min(dist, (new Vector2(pathPos.x, pathPos.z) - spawnPo).sqrMagnitude);
        }
        if (dist < margin) return;

        GameObject building = Instantiate(target, holder);
        Vector3 spawnPos = new Vector3(spawnPo.x, gameObject.transform.position.y, spawnPo.y);
        building.transform.position = spawnPos;
        building.transform.eulerAngles = new Vector3(building.transform.eulerAngles.x, Random.Range(0, 360), building.transform.eulerAngles.z);
        building.transform.localScale = new Vector3(building.transform.localScale.x, building.transform.localScale.y * Random.RandomRange(scaleDiff, 1), building.transform.localScale.z);
    }
}
