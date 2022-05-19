using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ArrowMovement : MonoBehaviour
{
    public bool hit = false;
    public GameObject target;
    public float hit_depth = 0;
    public Vector3 wind;
    public bool flight = false;
    public float m_mass = 0.018f; //Mass of arrow
    public float m_dim = (5.12f * Mathf.Pow(10, -3)); //Diameter of arrow shaft
    // public float shootForce = 2; 
    public float m_dragCoeff = 1.56f;
    private float airDensity = 1.22f;
    private float m_dragParameters;
    //Needed for calculating the speed and position of the arrow
    private Vector3 m_position;
    public Vector3 m_velocity;
    private Vector3 m_gravityForce;
    private Vector3 m_dragForce;
    private Vector3 m_liftForce;
    private Vector3 m_netForce;
    private Vector3 m_normVelocity;
    private Vector3 m_perpNormVelocity;
    // Start is called before the first frame update
    void start()
    {
        gameObject.GetComponent<ArrowMovement>().enabled = false;
    }
    void OnEnable()
    {
        m_position = transform.position;
        m_dragParameters = Mathf.PI / 8 * m_dragCoeff * airDensity * Mathf.Pow(m_dim, 2);
        m_gravityForce = new Vector3(0, -9.81f * m_mass, 0);
        m_velocity = transform.parent.gameObject.GetComponent<BowReleaseScript>().v_launch * transform.parent.forward;
        hit_depth = GameObject.Find("max").GetComponent<Collider>().bounds.size.z-m_velocity.z/1000f-0.1f;
    }
    void FixedUpdate()
    {

        if (transform.parent.gameObject.GetComponent<BowReleaseScript>().shot == 3)
        {
            flight = true;
            projectile();
            rotateArrow();
        }
    }
    void rotateArrow()
    {
        float yVelocity = m_velocity.y;
        float xVelocity = m_velocity.x;
        float zVelocity = m_velocity.z;
        float theta = (-1) * Mathf.Atan2(yVelocity, Mathf.Sqrt(Mathf.Pow(zVelocity, 2) + Mathf.Pow(xVelocity, 2))) * 180 / Mathf.PI;
        if (transform.parent.gameObject.GetComponent<BowReleaseScript>().transform.localEulerAngles.x > 300)
        {
            transform.eulerAngles = new Vector3(theta-90, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(theta-90, 0, 0);
        }
    }
    void projectile()
    {
        if (flight)
        {
            m_normVelocity = m_velocity / m_velocity.magnitude;
            m_dragForce = m_dragParameters * Mathf.Pow(m_velocity.magnitude, 2) * (-m_normVelocity);
            m_netForce = m_gravityForce + m_dragForce;
            m_velocity += m_netForce / m_mass * Time.fixedDeltaTime; 
            m_position += m_velocity * Time.fixedDeltaTime;
            transform.position = m_position;
             if(transform.position.z >= target.transform.GetChild(0).transform.position.z-hit_depth){
                if(transform.position.y >= target.transform.GetChild(2).transform.position.y-2f && transform.position.y <= target.transform.GetChild(2).transform.position.y+2f){
                hit=true;
                }
            }
        }
    }
}