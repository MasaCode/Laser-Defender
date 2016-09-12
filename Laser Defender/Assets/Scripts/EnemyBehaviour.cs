using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public GameObject laser;
    public AudioClip laserSound;
    public AudioClip dieSound;
    public float beamSpeed = 10f;
    public float hitPoint = 100f;
    public float shotsPerSecond = 0.5f;
    public int scorePoint = 150;
    

    protected ScoreKeeper _scoreKeeper;
    protected HealthBarManager _health;
    protected bool _isBoss = false;
    protected Vector2 _speed;
    protected float _xMin, _xMax;
    protected float _yMin, _yMax;

    private bool _isOverTheScore = false;
    

    // Use this for initialization
    protected void Start () {

        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        this.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
        _health = this.transform.GetChild(0).GetChild(0).GetComponent<HealthBarManager>();
        _health.setMaxHealth(hitPoint);

        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
        _xMin = leftMost.x + 0.5f;
        _xMax = rightMost.x - 0.5f;
        _yMin = leftMost.y + 0.5f;
        _yMax = rightMost.y - 0.5f;

        if (!_isBoss)
        {
            if (this.transform.parent.parent.gameObject.GetComponent<EnemySpawner>().IsTheFormationOver())
            {
                _isOverTheScore = true;
                _speed = new Vector2(Random.Range(8, 10), Random.Range(8, 10));
                _speed.x = Random.Range(0, 1) == 0 ? _speed.x : -_speed.x;
                _yMin = 0.0f;
            }
        }

    }
	
	// Update is called once per frame
	protected void Update () {

        {// for fire!
            float probability = shotsPerSecond * Time.deltaTime;
            if (Random.value < probability)
            {
                Fire();
            }
        }


        if (_isOverTheScore)
        {
            if(this.transform.position == this.transform.parent.gameObject.transform.position)
            {
                this.GetComponent<Animator>().Stop();
               
            }

            Movement();
        }

	}


    protected void OnTriggerEnter2D(Collider2D collider)
    {
        LaserBeam beam = collider.gameObject.GetComponent<LaserBeam>();
        if (beam != null)
        {
            if (!beam.IsEnemyLaser())
            {
                float damage = beam.GetDamage();
                hitPoint -= damage;
                beam.Hit();
                if (hitPoint <= 0.0f)
                {
                    Die();
                }
                _health.takeDamage(damage);
            }
        }
        
    }

    protected void Die()
    {
        AudioSource.PlayClipAtPoint(dieSound, this.transform.position);

        _scoreKeeper.Score(scorePoint);
        Destroy(gameObject);
    }

    protected void Fire()
    {
        Vector3 pos = this.transform.position + new Vector3(0, -0.5f, 0);

        GameObject beam = Instantiate(laser, pos, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -beamSpeed);

        AudioSource.PlayClipAtPoint(laserSound, pos);
    }


    protected void Movement()
    {
        Vector3 pos = this.transform.position;
        pos.x = Mathf.Clamp(pos.x + _speed.x * Time.deltaTime, _xMin, _xMax);
        pos.y = Mathf.Clamp(pos.y + _speed.y * Time.deltaTime, _yMin, _yMax);

        if (pos.x == _xMin || pos.x == _xMax)
        {
            _speed.x *= -1;
            _speed.y += Random.Range(-3f, 3f);
        }
        if (pos.y == _yMin || pos.y == _yMax)
        {
            _speed.y *= -1;
            _speed.x += Random.Range(-3f, 3f);

        }

        this.transform.position = pos;
    }

}
