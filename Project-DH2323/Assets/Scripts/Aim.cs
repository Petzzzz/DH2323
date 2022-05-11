using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    Rigidbody rigidBd;
    [SerializeField]
    bool invertAim = false;
    [SerializeField]
    Camera cam;
    
    // Start is called before the first frame updat
    void Start()
    {
        rigidBd = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;    
    }

    // Update is called once per frame
    void Update()
    {
        AimLogic();
    }


    void AimLogic()
    {
        int aimModifier = -1;
        if (invertAim)
            aimModifier = 1;
        float horizontalValue = Input.GetAxisRaw("Mouse X");
        float verticalValue = aimModifier * Input.GetAxisRaw("Mouse Y");
        Vector3 rotationX = new Vector3(verticalValue, 0, 0);
        Vector3 rotationY = new Vector3(0, horizontalValue, 0);
        rigidBd.MoveRotation(transform.rotation * Quaternion.Euler(1.5f * rotationY));
        cam.transform.Rotate(rotationX/2);

    }
}
