using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{


    public GameObject dialogBox;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DisplayDialog()
    {
        dialogBox.SetActive(true);
    }
}
