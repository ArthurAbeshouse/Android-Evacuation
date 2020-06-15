using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_spawner : MonoBehaviour
{

    [SerializeField]
    GameObject Enemy;

    Camera cam;
   
    Plane[] planes;

    Collider2D objCollider;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        objCollider = Enemy.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);

        if (Enemy != null)
        {
            if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
            {
                Enemy.SetActive(true);
                /*if (Dead)
                {
                    Instantiate(gameObject, transform.position, transform.rotation);
                    Dead = false;
                } */
                // Instantiate(gameObject);
                //Onscreen = true;
               // Debug.Log("object is visible");
            }
            else
            {
                Enemy.SetActive(false);
                /*  Destroy(gameObject);
                  Dead = true; */
               // Debug.Log("object is not visible");
            }
        }
    }
}
