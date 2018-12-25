using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBox : MonoBehaviour
{
    public Transform logTemplate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteLog(string text)
    {
        Transform log = Instantiate(logTemplate, transform);
        log.gameObject.SetActive(true);
        //log.parent = transform;

        log.GetComponent<LogTemplateScript>().content.text = text;
    }
}
