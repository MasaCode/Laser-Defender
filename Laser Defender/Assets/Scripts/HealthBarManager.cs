using UnityEngine;
using System.Collections;

public class HealthBarManager : MonoBehaviour {


    private SpriteRenderer _renderer;
    private Color _currentColor;
    private float _maxHealth = 0.0f;
    private float _currentHealth = 0.0f;
    private float _currentPercentage = 1.0f;
    private float _currentGreen = 255.0f;


	// Use this for initialization
	void Start () {
        _renderer = this.GetComponent<SpriteRenderer>();
        _currentColor = new Color(0, 255, 0);
        _renderer.color = _currentColor;
	}

    public void setMaxHealth(float health)
    {
        _maxHealth = health;
        _currentHealth = health;
        
    }

    public void takeDamage(float damage)
    {

        _currentHealth = Mathf.Max( _currentHealth - damage,0.0f);
        _currentPercentage = _currentHealth / _maxHealth;
        _currentGreen = 255.0f * _currentPercentage;
        _currentColor = new Color(255.0f - _currentGreen, _currentGreen, 0);
        _renderer.color = _currentColor;
        

        this.transform.localScale = new Vector3(_currentPercentage, 1, 1);

    }
    
}
