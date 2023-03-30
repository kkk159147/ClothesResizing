using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModelLoader : MonoBehaviour
{
    // 나중에 UI에 붙일 때 PointerHandler 붙일 것
    private float speed = 3f;

    private float currentScale, temp;

    private float scaleRate = 2f;
    private float minScale = 1f;
    private float maxScale = 6f;

    /// <summary>
    /// 모델 로드하는 기능 Awake에 추후 추가.
    /// 일단 하위로 3D 오브젝트 붙혀서 구현
    /// 드래깅으로 회전, 스와이프로 확대
    /// 구조 변경 가능성 O
    /// </summary>
    private void Awake()
    {

    }

    private void Start()
    {
        currentScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {


            float dis = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

            if (temp > dis)
            {
                if (currentScale < minScale)
                    return;

                currentScale -= Time.deltaTime * scaleRate;

                transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            }
            else if (temp < dis)
            {
                if (currentScale > maxScale)
                    return;

                currentScale += Time.deltaTime * scaleRate;

                transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            }

            temp = dis;
        }
        else if (Input.touchCount == 1)
        {
            Touch screenTouch = Input.GetTouch(0);

            if (screenTouch.phase == TouchPhase.Moved)
            {
                transform.Rotate(0f, -screenTouch.deltaPosition.x * speed * Time.deltaTime, 0f);
            }
        }
    }
}
