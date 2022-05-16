using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
   /* // Start is called before the first frame update
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject arrow;
    private bool arrowSlotted = false;
    private float pullAmount = 0;
    void Start()
    {
        SpanArrow();
    }

    // Update is called once per frame
    void Update()
    {
        ShootLogic();
    }


    void SpanArrow()
    {
        arrowSlotted = true;
        arrow = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
        arrow.transform.parent = transform;
        Debug.Log(transform.position);
    }

    void ShootLogic()
    {
        if (pullAmount > 100)
            pullAmount = 100;
        
        ArrowMovement arrowProjectile = arrow.transform.GetComponent<ArrowMovement>();
        if (Input.GetMouseButton(0))
        {
            pullAmount += Time.deltaTime * pullAmount;
        }
        if (Input.GetMouseButtonUp(0))
        {
            arrowSlotted = false;
            arrowProjectile.flight = true;
            arrow.transform.parent = null;
            arrowProjectile.shootForce = arrowProjectile.shootForce * ((pullAmount/100) + 0.05f);
            pullAmount = 0;
            arrowProjectile.enabled = true;
        }

        if(Input.GetMouseButtonDown(0) && arrowSlotted == false)
        {
            SpanArrow();
        }
    }
*/
}

