using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using JetBrains.Annotations;

public class StoneClick : MonoBehaviour
{
    public Slider clickCountSlider;    // 클릭 횟수를 표시할 슬라이더
    public TMP_Text completeText;      // 완성 텍스트
    public static int num;

    public GameObject Buttonobject;

    private void Awake()
    {
        InvokeRepeating("InstantiateButton", 1f, 4f);
        GameObject[] stoneAxeObjects = GameObject.FindGameObjectsWithTag("StoneAxeItems");
        Debug.Log(stoneAxeObjects.Length);
        foreach (GameObject item in stoneAxeObjects)
        {
            item.SetActive(true);
        }
    }

    void Update()
    {
       
        // 0: 주먹도끼
        if (num == 0)
        {
            Debug.Log("주먹도끼");
            // 미완성 돌 이미지 활성화
            foreach (GameObject item in UIManager.Instance.incompleteStone[num])
            {
                item.SetActive(true);
            }

            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount - 5)
            {
                ActivateCompleteStone(num);
                // InvokeRepeating 멈추기
                CancelInvoke("InstantiateButton");
            }
        }

        // 1: 돌 도끼
        if (num == 1)
        {
            Debug.Log("돌도끼");
            // 미완성 돌 이미지 활성화
            GameObject[] stoneAxeObjects = GameObject.FindGameObjectsWithTag("StoneAxeItems");
            foreach (GameObject item in stoneAxeObjects)
            //foreach (GameObject item in UIManager.Instance.incompleteStone)
            {
                Debug.Log("before: "+item.name);
                item.SetActive(true);
                Debug.Log("after: " + item.name);
            }

            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount - 2)
            {
                ActivateCompleteStone(num);
                // InvokeRepeating 멈추기
                CancelInvoke("InstantiateButton");
            }
        }

        // 2: 창
        if (num == 2)
        {
            // 미완성 돌 이미지 활성화
            foreach (GameObject item in UIManager.Instance.incompleteStone[num])
            {
                item.SetActive(true);
            }

            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount +2)
            {
                ActivateCompleteStone(num);
                // InvokeRepeating 멈추기
                CancelInvoke("InstantiateButton");
            }
        }

        // 3: 돌 화살
        if (num == 3)
        {
            // 미완성 돌 이미지 활성화
            foreach (GameObject item in UIManager.Instance.incompleteStone[num])
            {
                item.SetActive(true);
            }

            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount + 5)
            {
                ActivateCompleteStone(num);
                // InvokeRepeating 멈추기
                CancelInvoke("InstantiateButton");
            }
        }
    }

    void InstantiateButton()
    {
        Buttonobject.SetActive(false);
        Buttonobject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600, 600), Random.Range(-200, 200));
        Buttonobject.SetActive(true);
    }

    void ActivateCompleteStone(int i)
    {
        // 미완성 돌 이미지 비활성화
        foreach (GameObject item in UIManager.Instance.incompleteStone[i])
        {
            item.SetActive(false);
        }

        // 완성된 돌 이미지 활성화
        UIManager.Instance.completeStone[num].SetActive(true);

        // 완성 텍스트
        completeText.gameObject.SetActive(true);
    }

    // Success 버튼과 onClick으로 연결
    public void Success()
    {
        Buttonobject.GetComponent<Animator>().SetTrigger("isSuccess");
        UIManager.Instance.clickCount += 1;

        // 슬라이더 업데이트
        UpdateSlider();

    }

    // Fail 버튼과 onClick으로 연결
    public void Fail()
    {
        Buttonobject.GetComponent<Animator>().SetTrigger("isFail");
    }

    // 슬라이더 업데이트
    void UpdateSlider()
    {
        float normalizedValue = (float)UIManager.Instance.clickCount / UIManager.Instance.maxCount;
        clickCountSlider.value = normalizedValue;
    }
}
