using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject prefab;
    void Start()
    {
        prefab = Resources.Load("Material/Bow-and-arrow/Arrow") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject arrow = Instantiate(prefab) as GameObject;
            arrow.transform.position = transform.position + Camera.main.transform.forward * 2;
        }
    }


}