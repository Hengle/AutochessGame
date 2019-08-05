using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RobotAnimation : MonoBehaviour
{
    Animator _anim;
    string[] anims = { "Walk", "Attack", "SpinAttack", "Hurt" };


    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    public void MoveAnim()
    {
        Activate("Walk");
    }

    public void HurtAnim()
    {
        Activate("Hurt");
    }
    public void SpinAttackAnim()
    {
        Activate("SpinAttack");
    }
    public void AttackAnim()
    {
        Activate("Attack");
    }
    public void IdleAnim()
    {
        DeactivateAll();
    }

    private void Activate(string anim)
    {
        foreach (var str in anims)
        {
            if (str == anim)
            {
                _anim.SetBool(anim, true);
                if (anim == "Hurt" || anim == "SpinSttack")
                {
                    Invoke("DeactivateAll", 0.0001f);
                }
                continue;
            }

            _anim.SetBool(str, false);
        }
    }
    private void DeactivateAll()
    {
        foreach (var str in anims)
        {
            _anim.SetBool(str, false);
        }
    }
}
