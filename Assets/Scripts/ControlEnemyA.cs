using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemyA : MonoBehaviour
{
    public float speed = 3f;
    public float timeMove = 3f;
    public float periodAttack = 3f;
    
    private Animator _animator;

    private bool moveLeft = true;
    private float timeMoving;
    //private float timeMovingDown;
    private bool attacking = false;
    private float timeToAttack;

    private Vector3 originalPosition;
    private readonly float deltaY = 0.1f;

    

    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        timeMoving = timeMove;
        //timeMovingDown = 0f;
        timeToAttack = periodAttack;
        originalPosition = transform.position;
        
    }

   
    // Update is called once per frame
    void Update()
    {
        // desplazamiento lateral constante
        timeMoving -= Time.deltaTime;
        if (timeMoving < 0)
        {
            moveLeft = !moveLeft;
            timeMoving = timeMove;
        }
        if (moveLeft)
        {
            transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y);
            originalPosition = new Vector3(transform.position.x, originalPosition.y);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y);
            originalPosition = new Vector3(transform.position.x, originalPosition.y);
        }
        
        // ataque
        if (!attacking)
        {   
            timeToAttack -= Time.deltaTime;
        }
        if (timeToAttack < 0)
        {
            timeToAttack = periodAttack;
            if (Random.Range(0f, 1f) > 0.8f)
            {
                attacking = true;
            }
        }

        if (attacking)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (speed * Time.deltaTime));
            // reentrada por la parte superior de la pantalla
            if (transform.position.y < -5.2f)
            {
                transform.position = new Vector3(transform.position.x, 5.2f);
            }

            // vuelta a la posicion original
            if ((transform.position.y >= originalPosition.y) &&
                (transform.position.y <= (originalPosition.y + deltaY)))
            {
                attacking = false;
                transform.position = new Vector3(originalPosition.x, originalPosition.y);
            }
        }
    }
    

    public void HitByShoot()
    {
        _animator.Play("AnimationExplotion");
        Destroy(gameObject, 0.5f);
        GameManager.Instance.AddEnemy();
        

    }


    //Colision con jugador > Perder vida
    public void HitPlayer()
    {
        _animator.Play("AnimationExplotion");
        Destroy(gameObject, 0.5f);
        GameManager.Instance.LoseLife();

    }

}
