using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        CrearEnemigos();
    }

    void CrearEnemigos()
    {
        float x = -2.1f, y = 1.3f, incx = 0.6f, incy = 0.5f;
        int maxRows = 3;
        int maxCols = 9;
        Vector3 position =  new Vector3(x, y);

        for (int j = 0; j < maxRows; j++)
        {
            for (int i = 0; i < maxCols; i++)
            {
                position.x += incx;
                GameObject enemyGO = Instantiate(enemyPrefab, position, Quaternion.identity);
                enemyGO.transform.parent=gameObject.transform; 
            }
            position = new Vector3(x, position.y + incy);
        }
    }
}
