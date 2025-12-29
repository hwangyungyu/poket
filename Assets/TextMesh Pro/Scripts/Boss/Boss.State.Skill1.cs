using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public partial class Boss : MonoBehaviour
{
    [System.Serializable]
    public class Skill1Data
    {
        public GameObject skill1Range;
        public Transform dashPoint;
        public bool isSkill1Used;
    }

    public Skill1Data skill1Data;
    private GameObject clone;

    private void Init_State_Skill1()
    {
        var dashPoint = skill1Data.dashPoint.transform.position;
        var skill1Used = skill1Data.isSkill1Used;

        void OnEnter()
        {
            //  ~~~~~~~ 비쥬얼 이펙트 or 사운드 재성
            //AudioManager.instance.PlaySfx(AudioManager.Sfx.BossSkill1);
            //dashPoint 위치에 skill1Range 생성 & dashPoint 위치를 플레이어 위치로 변경
            skill1Used = false;
            dashPoint = target.transform.position;
            skill1RangeShow();
        }

        void OnExecute()
        {
            speed = 0;
            StartCoroutine(skill1Use());

            bool isCollisionToTarget = 1f >= Vector3.Distance(dashPoint, transform.position);

            if (isCollisionToTarget || skill1Used)
            {
                stateMachine.Change((int)States.Idle);
            }
        }

        void OnExit()
        {
            speed = 3;
            Destroy(clone);
        }

        State state = new State((int)States.Skill1);
        state.onEnter = OnEnter;
        state.onExecute = OnExecute;
        state.onExit = OnExit;

        stateMachine.Add(state);

        IEnumerator skill1Use()
        {
            yield return new WaitForSecondsRealtime(1.5f);
            speed = 12;
            Vector2 dashPoint2 = new(dashPoint.x, dashPoint.y);
            Vector2 dirVec = dashPoint2 - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
            yield return new WaitForSecondsRealtime(1.5f);
            
            skill1Used = true;
        }

        void skill1RangeShow()
        {
            clone = Instantiate(skill1Data.skill1Range, dashPoint, Quaternion.identity);
        }
    }
}
