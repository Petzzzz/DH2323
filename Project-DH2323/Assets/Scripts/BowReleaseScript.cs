using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowReleaseScript : MonoBehaviour
{
    public GameObject arrowprefab;
    public GameObject target;
    public List<GameObject> hit_arrows;
    public GameObject hit_arrow;
    public Vector3 hit_rot;
    public GameObject arrow;
    public Rigidbody a_Rigidbody;
    public LineRenderer stringUp; 
    public LineRenderer stringDown;
    private float rot_dir = 1;
    private float string_y = 0;
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

        stringUp = GameObject.Find("stringUp").GetComponent<LineRenderer>();

        stringDown = GameObject.Find("stringDown").GetComponent<LineRenderer>();

        pos_original = arrow.transform.position;

        pos_1 = arrow.transform.position.z;
    }

    // Update is called once per frame
    public void Update()
    {
        print(arrow.transform.forward * -1f);

        stringUp.SetPosition(0, GameObject.Find("top").transform.position);

        stringDown.SetPosition(0, GameObject.Find("bot").transform.position);
        
        if (shot == 0)
        {
            stringUp.SetPosition(1, GameObject.Find("midstill").transform.position);

            stringDown.SetPosition(1, GameObject.Find("midstill").transform.position);

            pos_original = arrow.transform.position;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                rot_bow += v_rot * Time.deltaTime;

                transform.Rotate(-v_rot * Time.deltaTime, 0, 0, Space.Self);

                GameObject.Find("midstill").transform.RotateAround(arrow.transform.position, new Vector3(1, 0, 0), -v_rot * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                rot_bow -= v_rot * Time.deltaTime;

                transform.Rotate(v_rot * Time.deltaTime, 0, 0, Space.Self);

                GameObject.Find("midstill").transform.RotateAround(arrow.transform.position, new Vector3(1, 0, 0), v_rot * Time.deltaTime);
            }

        }

        if (Input.GetKey(KeyCode.Space) && shot < 2)
        {
            shot = 1;
            stiffness += Time.deltaTime;
            if((arrow.transform.position + transform.forward * -0.5f * Time.deltaTime * Mathf.Pow(Mathf.Abs(arrow.transform.position.z), stiffness)).z > 0.5f){
            arrow.transform.position += transform.forward * -0.5f * Time.deltaTime * Mathf.Pow(Mathf.Abs(arrow.transform.position.z), stiffness);
            stringUp.SetPosition(1, new Vector3(arrow.transform.position.x, GameObject.Find("midstill").transform.position.y, arrow.transform.position.z-0.75f));
            stringDown.SetPosition(1, new Vector3(arrow.transform.position.x, GameObject.Find("midstill").transform.position.y, arrow.transform.position.z-0.75f));
            } else {
                arrow.transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, 0.48f);
            }
            f_stretch += Mathf.Pow(12f, stiffness + 1) * Time.deltaTime;
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
            stringUp.SetPosition(1, new Vector3(arrow.transform.position.x, arrow.transform.position.y * arrow.transform.forward.y * -1f, arrow.transform.position.z-0.75f));
            stringDown.SetPosition(1, new Vector3(arrow.transform.position.x, arrow.transform.position.y * arrow.transform.forward.y * -1f, arrow.transform.position.z-0.75f));
            shot = 3;
        }

        if(shot==3){
            stringUp.SetPosition(1, GameObject.Find("midstill").transform.position);
            stringDown.SetPosition(1, GameObject.Find("midstill").transform.position);
        }

        if(arrow.GetComponent<ArrowMovement>().hit == true){
            Vector3 hit_pos = arrow.transform.position;
            hit_rot = new Vector3(arrow.transform.localEulerAngles.x, 0, 0);
            shot = 0;
            f_stretch = 0;
            stiffness = 0;
            Destroy(arrow);

            hit_arrow = Instantiate(arrowprefab) as GameObject;
            hit_arrow.transform.position = hit_pos;
            hit_arrow.transform.eulerAngles = hit_rot;
            hit_arrows.Add(hit_arrow);

            arrow = Instantiate(arrowprefab) as GameObject;
            arrow.transform.position = pos_original;
            arrow.transform.eulerAngles = new Vector3(-90+transform.localEulerAngles.x, 0, 0);
            pos_2 = 0f;
            arrow.transform.parent = GameObject.FindGameObjectWithTag("bow").transform;
            arrow.transform.position = pos_original;
            arrow.AddComponent<ArrowMovement>();
            arrow.GetComponent<ArrowMovement>().target = gameObject.GetComponent<BowReleaseScript>().target;
            arrow.GetComponent<ArrowMovement>().enabled = false;
        }

        if (arrow.transform.position.z > 20f)
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
            arrow.GetComponent<ArrowMovement>().target = gameObject.GetComponent<BowReleaseScript>().target;
            arrow.GetComponent<ArrowMovement>().enabled = false;
        }

        if (arrow.transform.position.y < -20f)
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
            arrow.GetComponent<ArrowMovement>().target = gameObject.GetComponent<BowReleaseScript>().target;
            arrow.GetComponent<ArrowMovement>().enabled = false;
        }
    }
}
