using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public int _playerNumber;
    public int _life;
    public int _speed;
    public int _damage;
    public int _fireRate;

    [Space]

    [Header("Movements")]
    Vector3 movement;
    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    public float m_Speed = 12f;
    private Rigidbody m_Rigidbody;
    private float m_deadzone = 0.2f;


    public void Movement()
    {
        Vector3 verticalMovement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        Vector3 horizontalMovement = transform.right * m_MovementInputValue * m_Speed * Time.deltaTime;

        movement.Set(m_TurnInputValue, 0f, m_MovementInputValue);
        movement = movement.normalized * m_Speed * Time.deltaTime;

        m_Rigidbody.MovePosition(transform.position + movement);

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        // Store the value of both input axes.

        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        if (Mathf.Abs(m_MovementInputValue) < m_deadzone)
        {
            m_MovementInputValue = 0;
        }

        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
        if (Mathf.Abs(m_TurnInputValue) < m_deadzone)
        {
            m_TurnInputValue = 0;
        }

    }
}
