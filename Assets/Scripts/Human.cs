using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Human : MonoBehaviour
{
    public int _startingLife;
    public int _life;
    public int _speed;
    public int _damage;
    public int _fireRate;

    [Space]

    [Header("Movements")]
    Vector3 movement;

    private float movementRightValue;
    private float movementUpValue;
    public bool keyDown;
    public float m_Speed = 12f;
    public Rigidbody m_Rigidbody;
    private float m_deadzone = 0.2f;

    [Space]

    [Header("Aiming")]
    public float m_RotationSpeed;
    private float aimUpValue;
    private float aimRightValue;
    Vector3 finalDir;

    private string aimUp;
    private string aimRight;
    private string moveUp;
    private string moveRight;
    private KeyCode attackKey;
    private KeyCode skillKey;
    public int playerNumber;


    private string attackButton;
    private string skillButton;
    private float attackAxisValue;
    private float skilAxisValue;


    protected virtual void OnEnable()
    {
        // When the tank is enabled, reset the tank's health and whether or not it's dead.
        _life = _startingLife; //réinitialiser la vie des joueurs
        GameObject.Find("Barre2VieScript").GetComponent<Barre2Vie>().UpdateBarre2Vie(playerNumber, _life * 100 / _startingLife);
    }

    protected virtual void Start()
    {
        moveUp = "moveUp" + playerNumber;
        moveRight = "moveRight" + playerNumber;

        aimUp = "aimUp" + playerNumber;
        aimRight = "aimRight" + playerNumber;

        attackButton = "attack" + playerNumber;
        skillButton = "skill" + playerNumber;

        switch (playerNumber)
        {
            case 1:
                attackKey = KeyCode.Joystick1Button2;
                skillKey = KeyCode.Joystick1Button0;
                break;
            case 2:
                attackKey = KeyCode.Joystick2Button2;
                skillKey = KeyCode.Joystick2Button0;
                break;
            case 3:
                attackKey = KeyCode.Joystick3Button2;
                skillKey = KeyCode.Joystick3Button0;
                break;
            case 4:
                attackKey = KeyCode.Joystick4Button2;
                skillKey = KeyCode.Joystick4Button0;
                break;
        }
    }

    public void Movement()
    {
        Vector3 verticalMovement = transform.forward * movementRightValue * m_Speed * Time.deltaTime;
        Vector3 horizontalMovement = transform.right * movementRightValue * m_Speed * Time.deltaTime;

        movement.Set(movementRightValue, 0f, movementUpValue);
        movement = movement.normalized * m_Speed * Time.deltaTime;

        //m_Rigidbody.MovePosition(transform.position + movement);
        transform.Translate(movement, Space.World);

    }

    
    private void FixedUpdate()
    {
        //if (isLocalPlayer == false)
        //    return;
        Movement();
    }

    protected virtual void Update()
    {
        //if (isLocalPlayer == false)
        //    return;
        //Movement deadzone
        movementRightValue = Input.GetAxis(moveRight);
        if (Mathf.Abs(movementRightValue) < m_deadzone)
        {
            movementRightValue = 0;
        }

        movementUpValue = Input.GetAxis(moveUp);
        if (Mathf.Abs(movementUpValue) < m_deadzone)
        {
            movementUpValue = 0;
        }


        //Turret deadzone
        aimRightValue = Input.GetAxis(aimRight);
        if (Mathf.Abs(aimRightValue) < m_deadzone)
        {
            aimRightValue = 0;
        }


        aimUpValue = Input.GetAxis(aimUp);
        if (Mathf.Abs(aimUpValue) < m_deadzone)
        {
            aimUpValue = 0;
        }

        Turn();

        if(Input.GetAxis(attackButton) > 0.2f)
        {
            Debug.Log("press");
            CmdAttack();
        }

        if (Input.GetAxis(skillButton) > 0.2f)
        {
            
            Skill();
        }

        Death();

        //update barre 2 vie
        GameObject.Find("Barre2VieScript").GetComponent<Barre2Vie>().UpdateBarre2Vie(playerNumber, _life * 100 / _startingLife);

        Debug.Log(Input.GetAxis(attackButton));
    }


    private void Turn()
    {

        //transform.Rotate(Vector3.up, TurretTurnInputValue * TurretTurnSpeed * Time.deltaTime);        //* TurretTurnSpeed * Time.deltaTime
        //Debug.Log(TurretTurnInputValue);


        Vector3 TurretVector = new Vector3(aimRightValue, 0, aimUpValue);
        float VectorLenght = Vector3.Magnitude(TurretVector);
        float step = m_RotationSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, TurretVector, step, 0.0f);


        if (VectorLenght > m_deadzone)
        {
            finalDir = Vector3.Lerp(transform.forward, newDir, 1);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(finalDir), Time.deltaTime * m_RotationSpeed);
        }



    }
    public virtual void CmdAttack()
    {
    }

    public virtual void Skill()
    {
    }

    public virtual void takeDamage(int damage)
    {
        //degat
        _life -= damage;
        //update score
        if (playerNumber == 1)
            Score.Instance.AddScore(Score.Instance.scorePerHit, 2);
        if (playerNumber == 2)
        Score.Instance.AddScore(Score.Instance.scorePerHit, 1);
    }

    public virtual void takeDamageNecromancien(int damage)
    {
        //hotfix car la necromancien ajouter du score à chaque tick de son skill
        _life -= damage;
    }


    public void Death()
    {
        if(_life <= 0)
        {
            Score.Instance.AddOnDeath(playerNumber);
            gameObject.SetActive(false);
        }
    }


}