using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public float speed = 1f;

    public float maxLeft;
    public float maxRight;

    public GameObject shootPrefab;
    public SpriteRenderer laserSpriteRenderer;

    private bool _canFire = true;

    public GameObject enemyPrefab;
    // Start is called before the first frame update
    
  
    void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position = new Vector3( transform.position.x - (speed * Time.deltaTime), transform.position.y);
            transform.position = new Vector3(Mathf.Max(maxLeft, transform.position.x - (speed * Time.deltaTime)), transform.position.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y);
            transform.position = new Vector3(Mathf.Min(maxRight, transform.position.x + (speed * Time.deltaTime)), transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canFire)
            {
                _canFire = false;
                GameObject go = Instantiate(shootPrefab);
                //go.transform.position = this.transform.position;
                go.transform.position = laserSpriteRenderer.gameObject.transform.position;
                go.GetComponent<ControlShoot>().controlPlayer = this;
                laserSpriteRenderer.enabled = false;
            }
        }
    }

    public void CanFire()
    {
        _canFire = true;
        laserSpriteRenderer.enabled = true;
    }


    //Colision con enemigo > Perder vida
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<ControlEnemyA>(out var component))
        {
            component.HitPlayer();
        }
        // Aqui va animacion? Destroy(gameObject);
    }
}
