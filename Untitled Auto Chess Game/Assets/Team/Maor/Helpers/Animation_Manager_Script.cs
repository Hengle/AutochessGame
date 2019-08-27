using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Manager_Script : MonoBehaviour
{

    private Animator animator;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        animator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Hurt anim management

public void PlayHurtAnim()
    {
        animator.SetBool("Hurt", true);

        Invoke("turnOffHurtBool", 0.1f);
    }

    public void turnOffHurtBool()
    {
        animator.SetBool("Hurt", false);
    }


    //Walk anim management

    public void PlayWalkAnim()
    {
        animator.SetBool("Walk", true);

        Invoke("turnOffWalkBool", 0.1f);
    }

    public void turnOffWalkBool()
    {
        animator.SetBool("Walk", false);
    }


    //Attack anim management

    public void PlayAttackAnim()
    {
        animator.SetBool("Spin_Attack", true);

        Invoke("turnOffAttackBool", 0.1f);
    }

    public void turnOffAttackBool()
    {
        animator.SetBool("Spin_Attack", false);
    }

}

