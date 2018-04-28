using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour {
    public float MaxHealth = 100;
    float _health;

    private void Start()
    {
        _health = MaxHealth;
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Brush")
        {
            Toothbrush brush = other.GetComponent<Toothbrush>();
            ReduceHealth(brush.BrushForce);
        }
    }
    public void ReduceHealth(float amount)
    {
        Debug.Log(_health);
        _health -= amount;
        if(_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
