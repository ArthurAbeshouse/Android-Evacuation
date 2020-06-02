using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float timeOffset;

    [SerializeField]
    Vector2 posOffset;

    [SerializeField]
    float leftLimit;

    [SerializeField]
    float rightLimit;

    [SerializeField]
    float bottomLimit;

    [SerializeField]
    float topLimit;

    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        //cameras current position
        Vector3 startPos = transform.position;

        //Players current position
        Vector3 endPos = player.transform.position;

        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        //Lerp Camera
        //transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);

        //SmoothDamp Camera
        transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, timeOffset);

        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit), 
            transform.position.z
        );
    }
   /* private void onDrawGizmos()
	{

	} */
}
