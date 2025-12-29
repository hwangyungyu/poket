using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public partial class Boss : MonoBehaviour
{

    private void Init_State_Idle()
    {

        void OnEnter()
        {
            skillTime = 0f;
        }

        void OnExecute()
        {
            skillTime += 1f * Time.deltaTime;

            speed = 3;
            Vector2 dirVec = target.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;


            bool canUseSkill = skillTime > coolTime;
            if (canUseSkill == false)
            {
                return;
            }

           

            bool isInSkill1Range = 6f >= Vector3.Distance(target.position, transform.position);
            if (isInSkill1Range)
            {
               stateMachine.Change((int)States.Skill1);
            }
            else
            {
                stateMachine.Change((int)States.Skill2);
            }

        }
        void OnExit()
        {
        }

        State state = new State((int)States.Idle);
        state.onEnter = OnEnter;
        state.onExecute = OnExecute;
        state.onExit = OnExit;

        stateMachine.Add(state);
    }
}
