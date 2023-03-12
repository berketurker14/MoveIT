using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Witch : MonoBehaviour
{
    public GameObject panel;
    private bool isPanelOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //if (collision.gameObject.tag == "Player")
        //{
            OpenClosePanel(!isPanelOpen);
        //}


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //{
            OpenClosePanel(isPanelOpen);
        //}
    }
    void OpenClosePanel(bool test)
    {
        panel.SetActive(test);
    }
}
