using UnityEngine;
using System.Collections;

public enum ItemKind {
    ATTACK = 0,
    HEAL,
    NONE
};


public class Item : MonoBehaviour {

    public float effect = 0;
    public int moveCount = 10;
    public ItemKind kind = ItemKind.NONE;

    private float _xPos;
    private float _moveRnagePerOneTime = 0.1f;
    private int _count = 0;
    private bool _isRight = false;

    void Start()
    {
        _xPos = this.transform.position.x;
        Debug.Log(_moveRnagePerOneTime);
    }

    void Update()
    {
        _count++;
        if(_count >= moveCount*2)
        {
            _count = 0;
            _isRight = !_isRight;
        }

        float addition = _isRight ? _moveRnagePerOneTime : -_moveRnagePerOneTime;
        this.transform.position += new Vector3(addition, 0, 0);

    }

    public float GetEffect()
    {
        return effect;
    }

    public ItemKind GetItemKind()
    {
        return kind;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
	
}
