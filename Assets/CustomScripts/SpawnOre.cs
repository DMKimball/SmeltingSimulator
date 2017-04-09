using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOre : MonoBehaviour {

    public int oreCount = 0;
    public int maxOreCount = 10;
    public float spawnCooldown = 5.0f;

    public GameObject orePrefab;
	
	// Update is called once per frame
	void Awake () {
        StartCoroutine("Spawn");
	}

    public void DecrementPopulation()
    {
        oreCount--;
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            if (oreCount < maxOreCount)
            {
                Instantiate(orePrefab, transform.position, Quaternion.identity).GetComponent<OreRefineScript>().spawner = this;
                oreCount++;
            }
            yield return new WaitForSeconds(spawnCooldown);
        }
    }
}
