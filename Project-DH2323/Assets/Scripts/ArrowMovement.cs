using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float m_mass;

    private float m_start_speed; //Need to get to calculate start speed based on energy
    
    //Coefficients for calculating the drag force
    private float m_dragCoeff;
    private float m_density;
    private float m_crossSecArea;
    private float m_radius;
    private float m_dragParameters;
    //Needed for calculating the speed and position of the arrow
    private Vector3 m_position;
    private Vector3 m_velocity;
    private Vector3 m_gravityForce;
    private Vector3 m_dragForce;
    private Vector3 m_netForce;
    private Vector3 m_normVelocity; 
    // Start is called before the first frame update
    void Start()
    {
        m_position = transform.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_normVelocity = m_velocity / m_velocity.magnitude;
        m_dragForce = m_dragParameters * Mathf.Pow(m_velocity.magnitude, 2) * m_velocity;
        m_netForce = m_gravityForce - m_dragForce;
        m_velocity = m_velocity + m_netForce / m_mass * Time.fixedDeltaTime;
        m_position = m_position + m_normVelocity * Time.fixedDeltaTime;
        transform.position = m_position;
    }
}
