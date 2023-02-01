using UnityEngine;

public class CameraCtrl : MonoBehaviour
{

    Vector3 Position = new Vector3(0,15,-15);

    void Awake()
    {
        Camera.main.transform.position = transform.position + Position;
        Camera.main.transform.LookAt(transform.position);
    }

    void LateUpdate()
    {
        Camera.main.transform.position = transform.position + Position;
        Camera.main.transform.LookAt(transform.position);
    }
}
