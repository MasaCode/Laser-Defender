using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

    public GameObject[] items;
    public int spawnFreq = 0;

    private float _xMin, _xMax;
    private float _yMin, _yMax;
    

    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));

        _xMin = leftMost.x + 0.5f;
        _xMax = rightMost.x + 0.5f;
        _yMax = rightMost.y;
    }

	// Update is called once per frame
	void Update () {

        if((ScoreKeeper.GetScore() >= spawnFreq))
        {
            GameObject item = Instantiate(items[Random.Range(0, items.Length)], new Vector3(Random.RandomRange(_xMin, _xMax), _yMax + 1, 0), Quaternion.identity) as GameObject;
            if (item)
            {
                item.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(-8, -14));
            }else
            {
                Debug.Log("NO RIGID2D");
            }
            spawnFreq += spawnFreq;
        }
        	
	}
}
