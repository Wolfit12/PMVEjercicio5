using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlShoot : MonoBehaviour
{
    public float speed = 1f;

    public ControlPlayer controlPlayer;
    
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spriteRenderer.isVisible)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime));    
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
      if (col.gameObject.TryGetComponent<ControlEnemyA>(out var component))
        {
            component.HitByShoot();
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        controlPlayer.CanFire();
    }
}
