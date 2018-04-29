using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dirt : MonoBehaviour {
    Renderer _r;
    public AudioClip CleanSound;
    public AudioClip BrushSound;
    public float MaxHealth = 100;
    public float Health { get { return _health; } }
    public Color ProgressColor = new Color(1, 1, 1);
    float _health;
    float _lastBrushSound;
    float _brushDelay = 0.3f;
    private void Start()
    {
        DirtManager.AddDirt(this);
        _health = MaxHealth;
        _r = GetComponent<Renderer>();

        ProgressColor = _r.material.color;
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Brush")
        {
            Toothbrush brush = other.GetComponent<Toothbrush>();
            ReduceHealth(brush.BrushForce);

            float prog = 1-Health / MaxHealth;
            //ProgressColor.r = prog;
            //ProgressColor.g = prog;
            //ProgressColor.b = prog;

            _r.material.color = Color.Lerp(ProgressColor, Color.white, prog);

            if (Time.time - _lastBrushSound > _brushDelay && brush.BrushForce > 0)
            {
                AudioManager.PlaySound(BrushSound, 0.5f);
                _lastBrushSound = Time.time;
            }
        }
    }
    public void ReduceHealth(float amount)
    {
        _health -= amount;
        if(_health <= 0)
        {
            AudioManager.PlaySound(CleanSound);
            DirtManager.ClearDirt(this);
            DirtManager.Status s = DirtManager.GetTotalDirt();
            if(s.Progress == 0)
            {
                MissionPlayerControl plr = GameObject.FindWithTag("Player").GetComponent<MissionPlayerControl>();
                plr.Win();
            }
        }
    }
}
