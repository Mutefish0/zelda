using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    public GameObject targetEntry;
    private GameObject room;
    private RoomChanger roomChanger;
    // Start is called before the first frame update
    void Start()
    {
        room = gameObject.transform.parent.gameObject;
        roomChanger = GameObject.Find("RoomChanger").GetComponent<RoomChanger>(); 
    }

    public GameObject GetRoom()
    {
        return room;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        roomChanger.FadeToNextRoom(targetEntry);
    }
}
