using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
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
    public float m_PressValue;
    public float m_Speed = 12f;
    public Rigidbody m_Rigidbody;
    private float m_deadzone = 0.2f;

    [Space]

    [Header("Aiming")]
    public float m_RotationSpeed;
    private float m_TankTurretUpValue;
    private float m_TankTurretRightValue;
    Vector3 finalDir;

    
    public int playerNumber;

    public void Movement()
    {
        Vector3 verticalMovement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        Vector3 horizontalMovement = transform.right * m_MovementInputValue * m_Speed * Time.deltaTime;

        movement.Set(m_TurnInputValue, 0f, m_MovementInputValue);
        movement = movement.normalized * m_Speed * Time.deltaTime;

        //m_Rigidbody.MovePosition(transform.position + movement);
        transform.Translate(movement, Space.World);

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {

        //Movement deadzone
        m_MovementInputValue = Input.GetAxis("Horizontal");
        if (Mathf.Abs(m_MovementInputValue) < m_deadzone)
        {
            m_MovementInputValue = 0;
        }

        m_TurnInputValue = Input.GetAxis("Vertical");
        if (Mathf.Abs(m_TurnInputValue) < m_deadzone)
        {
            m_TurnInputValue = 0;
        }


        //Turret deadzone
        m_TankTurretRightValue = Input.GetAxis("HorizontalTurret");
        if (Mathf.Abs(m_TankTurretRightValue) < m_deadzone)
        {
            m_TankTurretRightValue = 0;
        }


        m_TankTurretUpValue = Input.GetAxis("VerticalTurret");
        if (Mathf.Abs(m_TankTurretUpValue) < m_deadzone)
        {
            m_TankTurretUpValue = 0;
        }

        Turn();

        m_PressValue = Input.GetAxis("Attack");
        if (m_PressValue == 1)
        {
            Attack();
        }

        m_PressValue = Input.GetAxis("Skill");
        if (m_PressValue == 1)
        {
            Skill();
        }


    }


    private void Turn()
    {

        // transform.Rotate(Vector3.up, TurretTurnInputValue * TurretTurnSpeed * Time.deltaTime );        //* TurretTurnSpeed * Time.deltaTime
        //Debug.Log(TurretTurnInputValue);


        Vector3 TurretVector = new Vector3(m_TankTurretRightValue, 0, m_TankTurretUpValue);
        float VectorLenght = Vector3.Magnitude(TurretVector);
        float step = m_RotationSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, TurretVector, step, 0.0f);


        if (VectorLenght > m_deadzone)
        {
            finalDir = Vector3.Lerp(transform.forward, newDir, 1);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(finalDir), Time.deltaTime * m_RotationSpeed);
        }



    }

    public virtual void Attack()
    {      
    }

    public virtual void Skill()
    {
    }

    public virtual void takeDamage(int damage)
    {
        _life -= damage;
        if (playerNumber == 1)
            Score.Instance.AddScore(Score.Instance.scorePerHit, 2);

        if (playerNumber == 2)
        Score.Instance.AddScore(Score.Instance.scorePerHit, 1);
    }
}