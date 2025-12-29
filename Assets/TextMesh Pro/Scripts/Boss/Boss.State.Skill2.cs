using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;
//using static UnityEditor.PlayerSettings;

public partial class Boss : MonoBehaviour
{
    [System.Serializable]
    public class Skill2Data
    {
        public float delayTimer;
        public float delayTime;

        public GameObject bullet;
        public Transform firePoint;
    }

    public Skill2Data skill2Data;

    private void Init_State_Skill2()
    {
        var timer = skill2Data.delayTimer;
        var time = skill2Data.delayTime;

        void OnEnter()
        {

            //  ~~~~~~~ 비쥬얼 이펙트 or 사운드 재성
            //AudioManager.instance.PlaySfx(AudioManager.Sfx.BossSkill2);
            var bullet = skill2Data.bullet;
            var pos = transform.position;// skill2Data.firePoint.position;
            for (int i = 0; i < 360; i += 30)
            {
                transform.rotation = Quaternion.Euler(0, 0, i);
                Instantiate(bullet, pos, transform.rotation);
            }
            transform.rotation = Quaternion.identity;
        }
            
        void OnExecute()
        {

            timer += 1f * Time.deltaTime;
            bool isDone = timer >= time;
            if (isDone)
            {
                timer = 0;
                stateMachine.Change((int)States.Idle);
            }
        }

        void OnExit()
        {
        }

        State state = new State((int)States.Skill2);
        state.onEnter = OnEnter;
        state.onExecute = OnExecute;
        state.onExit = OnExit;

        stateMachine.Add(state);
    }
}
