using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowReleaseScript : MonoBehaviour
{
    public GameObject arrowprefab;
    public GameObject arrow;
    public Rigidbody a_Rigidbody;
    public float f_stretch = 0;
    public float stiffness = 0;
    public float w_in;
    public float w_out;
    public float v_launch = 0f;
    public float v_rot = 600f;
    public float v_bow = 0.5f;
    public float rot_bow = 0f;
    public float m_arrow = 1f;
    public float m_bow = 1f;
    public Vector3 pos_original;
    public float pos_1;
    public float pos_2 = 0f;
    public float shot = 0f;
    public float check = 0f;
    // Start is called before the first frame update
    public void Start()
    {
        arrowprefab = Resources.Load("Material/Bow-and-arrow/Arrow 1") as GameObject;

        arrow = GameObject.Find("Arrow 1");

        pos_original = arrow.transform.position;

        pos_1 = arrow.transform.position.z;
    }

    // Update is called once per frame
    public void Update()
    {

        if (shot == 0)
        {

            pos_original = arrow.transform.position;

            if (Input.GetKey(KeyCode.UpArrow))
            {

                rot_bow = -v_rot * Time.deltaTime;

                transform.Rotate(-v_rot * Time.deltaTime, 0, 0, Space.Self);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {

                rot_bow = v_rot * Time.deltaTime;

                transform.Rotate(v_rot * Time.deltaTime, 0, 0, Space.Self);
            }

        }

        if (Input.GetKey(KeyCode.Space) && shot < 2)
        {
            shot = 1;
            stiffness += Time.deltaTime;
            arrow.transform.position += transform.forward * -1 * Time.deltaTime * Mathf.Pow(Mathf.Abs(arrow.transform.position.z), stiffness);
            f_stretch += Mathf.Pow(10f, stiffness + 1) * Time.deltaTime;
            pos_2 = arrow.transform.position.z;
        }

        if (!Input.GetKey(KeyCode.Space) && shot == 1)
        {
            shot = 2;
            if (f_stretch > 2500)
            {
                f_stretch = 2500;
            }
            w_in = (pos_1 - pos_2) * f_stretch;
            w_out = w_in * 0.9f;
        }

        if (shot == 2)
        {
            v_launch = Mathf.Sqrt((2 * w_out) / (m_arrow + (m_bow * v_bow * v_bow)));
            arrow.GetComponent<ArrowMovement>().enabled = true;
            shot = 3;
        }

        if (arrow.transform.position.z > 10f)
        {
            shot = 0;
            f_stretch = 0;
            stiffness = 0;
            Destroy(arrow);
            arrow = Instantiate(arrowprefab);
            arrow.transform.position = pos_original;
            arrow.transform.eulerAngles = new Vector3(-90 + transform.localEulerAngles.x, 0, 0);
            pos_2 = 0f;
            arrow.transform.parent = GameObject.FindGameObjectWithTag("bow").transform;
            arrow.transform.position = pos_original;
            arrow.AddComponent<ArrowMovement>();
        }

        if (arrow.transform.position.y < -10f)
        {
            shot = 0;
            f_stretch = 0;
            stiffness = 0;
            Destroy(arrow);
            arrow = Instantiate(arrowprefab);
            arrow.transform.position = pos_original;
            arrow.transform.eulerAngles = new Vector3(-90 + transform.localEulerAngles.x, 0, 0);
            pos_2 = 0f;
            arrow.transform.parent = GameObject.FindGameObjectWithTag("bow").transform;
            arrow.transform.position = pos_original;
            arrow.AddComponent<ArrowMovement>();
        }
    }
}
