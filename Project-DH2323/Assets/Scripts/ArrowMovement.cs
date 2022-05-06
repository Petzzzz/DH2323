using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowMovement : MonoBehaviour
{
    public float m_mass = 0.018f; //Mass of arrow
    public float m_dim = (5.12f * Mathf.Pow(10, -3)); //Diameter of arrow shaft

    private float m_start_speed; //Need to get to calculate start speed based on energy

    //Coefficients for calculating the drag force
    public float m_dragCoeff = 1.56f;
    private float airDensity = 1.22f;
    private float m_dragParameters;
    //Needed for calculating the speed and position of the arrow
    private Vector3 m_position;
    private Vector3 m_velocity;
    private Vector3 m_gravityForce;
    private Vector3 m_dragForce;
    private Vector3 m_liftForce;
    private Vector3 m_netForce;
    private Vector3 m_normVelocity;
    private Vector3 m_perpNormVelocity;
    // Start is called before the first frame update
    void Start()
    {
        m_position = transform.position;
        m_dragParameters = Mathf.PI/8 * m_dragCoeff * airDensity * Mathf.Pow(m_dim, 2);
        m_gravityForce = new Vector3(0, -9.81f * m_mass, 0);
        m_velocity = Camera.main.transform.forward * 20;
        //transform.rotation = Quaternion.LookRotation(m_velocity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_normVelocity = m_velocity / m_velocity.magnitude;
        m_dragForce = m_dragParameters * Mathf.Pow(m_velocity.magnitude, 2) * (-m_normVelocity);
        m_netForce = m_gravityForce + m_dragForce;
        m_velocity += m_netForce / m_mass * Time.fixedDeltaTime;
        //transform.forward = Vector3.Slerp(transform.forward, m_velocity.normalized, Time.deltaTime); 
        m_position += m_velocity * Time.fixedDeltaTime;
        rotateArrow();
        transform.position = m_position;
        
    }
    void rotateArrow()
    {
        float yVelocity = m_velocity.y;
        float xVelocity = m_velocity.x;
        float zVelocity = m_velocity.z;
        float theta = Mathf.Atan2(yVelocity, Mathf.Sqrt(Mathf.Pow(xVelocity, 2) + Mathf.Pow(zVelocity, 2))) * 180/Mathf.PI;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, theta, transform.eulerAngles.z); 
    }
}




/*public class ArrowMovement : MonoBehaviour
{

    public float m_mass = 0.018f; //Mass of arrow
    public float m_diameter = 5.12f * Mathf.Pow(10, -3); //Diameter of arrow shaft

    private float m_start_speed; //Need to get to calculate start speed based on energy
    
    //Coefficients for calculating the drag force
    public float m_dragCoeff = 1.56f;
    private float airDensity = 1.22f;
    private float m_dragParameters;
    //Needed for calculating the speed and position of the arrow
    private Vector3 m_position;
    private Vector3 m_velocity;
    private Vector3 m_gravityForce;
    private Vector3 m_dragForce;
    private Vector3 m_liftForce;
    private Vector3 m_netForce;
    private Vector3 m_normVelocity;
    private Vector3 m_perpNormVelocity;
    // Start is called before the first frame update
    void Start()
    {
        m_position = transform.position;
        m_dragParameters = Mathf.PI/8 * m_dragCoeff * airDensity * Mathf.Pow(m_diameter, 2);
        m_gravityForce = new Vector3(0, -9.81f * m_mass, 0);
        m_velocity = Camera.main.transform.forward * 20f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_normVelocity = m_velocity / m_velocity.magnitude;
        m_dragForce = m_dragParameters * Mathf.Pow(m_velocity.magnitude, 2) * m_normVelocity;
        m_netForce = m_gravityForce - m_dragForce;
        m_velocity += m_netForce/m_mass * Time.fixedDeltaTime;
        m_position +=  m_velocity * Time.fixedDeltaTime;
        transform.position = m_position;
    }
}
*/