using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player; 
    public float smooth;
    public GameObject minBound;
    public GameObject maxBound;
    private Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        cameraOffset = new Vector3(camera.orthographicSize * camera.aspect, camera.orthographicSize, 0f);
    }

    public void RepositionClamp(Vector3 position)
    {
        Vector3 minPos = minBound.transform.position + cameraOffset;
        Vector3 maxPos = maxBound.transform.position - cameraOffset;
        
        transform.position = new Vector3(
            Mathf.Clamp(position.x, minPos.x, maxPos.x), 
            Mathf.Clamp(position.y, minPos.y, maxPos.y), 
            transform.position.z
        );
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != player.transform.position)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 minPos = minBound.transform.position + cameraOffset;
            Vector3 maxPos = maxBound.transform.position - cameraOffset;

            transform.position = Vector3.Lerp(transform.position, new Vector3(
                Mathf.Clamp(playerPosition.x, minPos.x, maxPos.x), 
                Mathf.Clamp(playerPosition.y, minPos.y, maxPos.y), 
                transform.position.z
            ), smooth);
        }
    }
}
