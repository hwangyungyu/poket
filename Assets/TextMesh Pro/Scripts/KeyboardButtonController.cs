using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardButtonController : MonoBehaviour
{
    public GameObject hideSkillButtons;
    public Button targetButton;  // 유니티 에디터에서 설정할 버튼 참조
    public KeyCode activationKey = KeyCode.Space;  // 기본 키는 스페이스바

    void Update()
    {
        // hideSkillButtons가 활성화 상태가 아닌 경우에만 키 입력을 처리
        if (!hideSkillButtons.activeSelf && Input.GetKeyDown(activationKey))
        {
            // 버튼의 onClick 이벤트 호출
            targetButton.onClick.Invoke();
        }
    }
}
