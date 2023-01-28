using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player player;
    public float offsetZ = -9;
    public float maxTopPos = -1.3f;
    public float maxBottomPos = -15.30086f;


    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + offsetZ);

        // 최대 위 지점보다 위로 갔을때 최대 위 지점으로 한다.
        if (transform.position.z > maxTopPos)
            transform.position = new Vector3(transform.position.x, transform.position.y, maxTopPos);

        // 최대 아래 지점보다 아래로 갔을때 최대 아래 지점으로 한다.
        if (transform.position.z < maxBottomPos)
            transform.position = new Vector3(transform.position.x, transform.position.y, maxBottomPos);


    }
}
