using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    private Animator animator;
    private GameObject nextEntry;

    private CameraMovement cameraScript;
    void Start()
    {
        animator = GetComponent<Animator>();
        cameraScript = cam.GetComponent<CameraMovement>();
    }

    public void FadeToNextRoom(GameObject entry)
    {
        animator.SetTrigger("FadeOut");
        nextEntry = entry;
    }

    public void OnFadeOutComplete()
    {
        Entry entryScript = nextEntry.GetComponent<Entry>();
        GameObject room = entryScript.GetRoom();
        Vector3 position = nextEntry.transform.Find("Spawn").position;

        // move character
        player.transform.position = position;
        // move camera
        cameraScript.minBound = room.transform.Find("MinBound").gameObject;
        cameraScript.maxBound = room.transform.Find("MaxBound").gameObject;
        cameraScript.RepositionClamp(position);
    }
}
