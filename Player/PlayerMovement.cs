using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public VariableJoystick variableJoystick;
    public static PlayerMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerMovement>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerMovement");
                    instance = instanceContainer.AddComponent<PlayerMovement>();
                }
            }
            return instance;
        }
    }

    private static PlayerMovement instance;

    Rigidbody rb;
    public Animator Anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);

        if (variableJoystick.Horizontal != 0 || variableJoystick.Vertical != 0)
        {
            rb.velocity = new Vector3(variableJoystick.Horizontal * moveSpeed, rb.velocity.y, variableJoystick.Vertical * moveSpeed);
            rb.rotation = Quaternion.LookRotation(new Vector3(variableJoystick.Horizontal, 0f, variableJoystick.Vertical));
            Anim.SetBool("Walk", true);
        }
        else
        {
            Anim.SetBool("Walk", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("NextRoom"))
        {
            Debug.Log(" Get Next Room ");
            StageMgr.Instance.NextStage();
        }

        if (other.transform.CompareTag("HpBooster"))
        {
            PlayerHpBar.Instance.GetHpBoost();
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("MeleeAtk"))
        {
            other.transform.parent.GetComponent<EnemyDuck>().meleeAtkArea.SetActive(false);
            PlayerHpBar.Instance.currentHp -= other.transform.parent.GetComponent<EnemyDuck>().damage * 2f;

            if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Dmg"))
            {
                Anim.SetTrigger("Dmg");
                Instantiate(EffectSet.Instance.PlayerDmgEffect, PlayerTargeting.Instance.AttackPoint.position, Quaternion.Euler(90, 0, 0));
            }

        }
    }
}
