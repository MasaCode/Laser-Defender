using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;

    public float width = 10f;
    public float height = 5f;
    public float speed = 1.0f;
    public float spawnDelay = 0.5f;
    public int groupScore = 2000;

    private float _xMin;
    private float _xMax;
    private bool _isOverTheScore = false;

	// Use this for initialization
	void Start () {

        SpawnUntilFull();

        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        _xMin = leftMost.x + width / 2.0f;
        _xMax = rightMost.x - width / 2.0f;

    }
	

	// Update is called once per frame
	void Update () {

        if(!_isOverTheScore){// for movement.
            Vector3 pos = this.transform.position;
            pos.x = Mathf.Clamp(pos.x + speed * Time.deltaTime, _xMin, _xMax);

            if (pos.x == _xMin || pos.x == _xMax)
            {
                speed *= -1;
            }

            this.transform.position = pos;
        }

        if (AllMembersDead())
        {
            speed *= 1.2f;
            SpawnUntilFull();

            if (ScoreKeeper.GetScore() >= groupScore)
            {
                _isOverTheScore = true;
                this.transform.position = new Vector3(0, 0, 0);
                spawnDelay *= 1.5f;
            }

        }

	}

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

  
    private void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if(freePosition != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
      
        if(NextFreePosition() != null)
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }

    }

    private Transform NextFreePosition()
    {
        foreach(Transform child in this.transform)
        {
            if (child.childCount == 0)
                return child;
        }

        return null;
    }

    private bool AllMembersDead()
    {

        foreach(Transform child in this.transform)
        {
            if (child.childCount > 0)
                return false;
        }

        return true;
    }

    public bool IsTheFormationOver()
    {
        return _isOverTheScore;
    }

}
