using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour {

    [SerializeField]
    private bool isEnemyLaser = true;    

    [SerializeField]
    private float _damage = 10.0f;
    

    public float GetDamage()
    {
        return _damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

   public bool IsEnemyLaser()
    {
        return isEnemyLaser;
    }

}
