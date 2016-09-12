using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour {

    public GameObject bossPrefav;
    public int bossCount = 0;
    public int spawnScore = 1000;
    public float spawnDelay = 0.5f;

    private bool _isFinishedSpawing = false;
    private int _currentCount = 0;
	
	// Update is called once per frame
	void Update () {
        
            if (ScoreKeeper.GetScore() >= spawnScore)
            {
                Spawn();
                spawnScore += spawnScore;
                
            }


	}


    void Spawn()
    {
        Instantiate(bossPrefav, new Vector3(0, 0, 0), Quaternion.identity);
        _currentCount++;

        if (_currentCount < bossCount)
        {
            Invoke("Spawn", spawnDelay);
        }
    }
}
