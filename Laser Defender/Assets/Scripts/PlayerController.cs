using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject laser;
    public AudioClip laserSound;
    public float hitPoint = 250f;
    public float speed = 15.0f;
    public float beamSpeed = 0f;
    public float firingRate = 0.2f;

    private HealthBarManager _health;
    private float padding = 0.5f;
    private float _xMin;
    private float _xMax;
    private float _yMin;
    private float _yMax;
    
    

	// Use this for initialization
	void Start () {

        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));

        _xMin = leftMost.x + padding;
        _xMax = rightMost.x - padding;
        _yMin = leftMost.y + padding;
        _yMax = rightMost.y - padding;

        _health = this.transform.GetChild(0).GetChild(0).GetComponent<HealthBarManager>();
        _health.setMaxHealth(hitPoint);

	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = this.transform.position;

        // For moving the space ship.
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position = new Vector3(Mathf.Clamp(pos.x - speed * Time.deltaTime, _xMin, _xMax), pos.y, pos.z);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position = new Vector3(Mathf.Clamp(pos.x + speed * Time.deltaTime, _xMin, _xMax), pos.y, pos.z);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position = new Vector3(pos.x, Mathf.Clamp(pos.y + (speed * Time.deltaTime) / 2.0f, _yMin, 0), pos.z);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position = new Vector3(pos.x, Mathf.Clamp(pos.y - (speed * Time.deltaTime) / 2.0f, _yMin, 0), pos.z);
        }

        //For shooting the enemy.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke();
        }


	}
    
    private void Fire()
    {
        GameObject beam = Instantiate(laser, this.transform.position, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, beamSpeed);
        AudioSource.PlayClipAtPoint(laserSound, this.transform.position);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        { // For Laser beam
            LaserBeam beam = collider.gameObject.GetComponent<LaserBeam>();
            if (beam != null)
            {
                if (beam.IsEnemyLaser())
                {
                    TakeDamage(beam.GetDamage());
                    beam.Hit();

                }
            }
        }


        { // For Item

            Item item = collider.gameObject.GetComponent<Item>();
            if(item != null)
            {
                float effect = item.GetEffect();
                ItemKind kind = item.GetItemKind();
                
                if(kind == ItemKind.HEAL)
                {
                    effect = -effect;
                }

                TakeDamage(effect);
                item.Hit();

            }


        }


    }

    void TakeDamage(float damage)
    {
        hitPoint -= damage;
        if (hitPoint <= 0.0f)
        {
            Die();
        }
        _health.takeDamage(damage);
    }


    void Die()
    {
        Destroy(gameObject);
        FindObjectOfType<LevelManager>().LoadLevel("End Screen");
       
    }


}
