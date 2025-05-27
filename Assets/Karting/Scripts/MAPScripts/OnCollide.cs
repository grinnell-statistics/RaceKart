using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollide : MonoBehaviour
{
    public float offsetTime = 10f;
    private float timer = 0f;

    public GameObject WarningText;
    // Start is called before the first frame update
    void Start()
    {
        WarningText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (WarningText.activeInHierarchy)
        {
            timer += Time.deltaTime;
            if(timer > offsetTime)
            {
                timer = 0f;
                WarningText.SetActive(false);
            }
        }
    }

    void OnTriggerEnter (Collider Col)
    {
        WarningText.SetActive(true);
        Debug.Log ("collison!");
    }

}
