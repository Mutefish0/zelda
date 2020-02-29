using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public string dialog;
    private GameObject UIDialog;
    private Text dialogText;
    private bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        UIDialog = GameObject.Find("GlobalDialog").transform.Find("Dialog").gameObject;
        dialogText = UIDialog.transform.Find("Dialog Box").Find("Text").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (UIDialog.activeInHierarchy)
            {
                UIDialog.SetActive(false);
            }
            else
            {
                dialogText.text = dialog;
                UIDialog.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        playerInRange = false;
        UIDialog.SetActive(false);
    }
}
