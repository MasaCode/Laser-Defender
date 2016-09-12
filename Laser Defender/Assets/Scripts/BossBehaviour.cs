using UnityEngine;
using System.Collections;


public class BossBehaviour : EnemyBehaviour{

    public float playerRadius = 0.5f;
    public float enemyRadius = 0.5f;

    private Transform _position;
    private bool _isAnimationFinished = false;
    

    void Start()
    {

        _speed = new Vector2(Random.Range(6, 8), Random.Range(2, 4));
        _isBoss = true;

        base.Start();

        _yMin = -2f;

    }

    void Update()
    {
        base.Update();

        if(this.transform.position == new Vector3(0, 0, 0))
        {
            this.GetComponent<Animator>().Stop();
            _isAnimationFinished = true;
        }

        if(_isAnimationFinished){
            Movement();
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        if (collider.gameObject.tag == "Player")
        {
            if(_speed.y < 0.0f && _speed.x < 0.0f)
            {
                this.transform.position += new Vector3(playerRadius, playerRadius, 0);
            }
            else if(_speed.y >= 0.0f && _speed.x >= 0.0f)
            {
                this.transform.position -= new Vector3(playerRadius, playerRadius, 0);
            }else if(_speed.y < 0.0f && _speed.x >= 0.0f)
            {
                this.transform.position += new Vector3(-playerRadius, playerRadius, 0);
            }
            else
            {
                this.transform.position += new Vector3(playerRadius, -playerRadius, 0);
            }


            _speed.y *= -1;
        }

    }


    

}

