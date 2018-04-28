using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dirt : MonoBehaviour {
    public float MaxHealth = 100;
    public float Health { get { return _health; } }
    float _health;
    private void Start()
    {
        DirtManager.AddDirt(this);
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
            DirtManager.ClearDirt(this);
            DirtManager.Status s = DirtManager.GetTotalDirt();
            if(s.Progress == 0)
            {
                SceneManager.LoadScene("world_map");
            }
        }
    }
}
