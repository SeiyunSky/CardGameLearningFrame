using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;

    [SerializeField] private TMP_Text description;

    [SerializeField] private TMP_Text mana;

    [SerializeField] private SpriteRenderer imageSR;

    [SerializeField] private GameObject wrapper;

    public Card Card { get; private set; }

    [Header("记录当前互动卡牌的起始位置")]
    private Vector3 dragStartPosition;
    private Quaternion dragStartRotation;

    private float shakeIntensity = 0.1f;
    private float shakeSpeed = 0.02f;
    private float maxRotationAngle = 8f;


    public void Setup(Card card)
    {
        Card = card;
        this.title.text = card.Title;
        this.description.text = card.Description;
        this.mana.text = card.Mana.ToString();
        this.imageSR.sprite = card.Image;
    }

    private void OnMouseEnter()
    {
        if(!InterActions.Instance.PlayerCanHover()) return;
        wrapper.SetActive(false);
        Vector3 position = new(transform.position.x , -2 , 0);
        CardViewHoverSystem.Instance.Show(Card, position);

    }

    private void OnMouseExit()
    {
        if (!InterActions.Instance.PlayerCanHover()) return;
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (!InterActions.Instance.PlayerCanInteract()) return;
        InterActions.Instance.PlayerIsDragging = true;
        wrapper.SetActive(true);
        CardViewHoverSystem.Instance.Hide();

        //记录位置
        dragStartPosition = transform.position;
        dragStartRotation = transform.rotation;

        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = MouseUtil.GetMouseWorldPosition( -1 );

    }

    private void OnMouseDrag()
    {
        if (!InterActions.Instance.PlayerCanInteract()) return;
        Vector3 targetPosition = MouseUtil.GetMouseWorldPosition(-1);

        // 平滑随机晃动
        transform.DOMove(targetPosition + GetRandomShakeOffset(), 0.2f)
             .SetEase(Ease.Linear);

        // 使用 DOTween 实现随机旋转
        float randomRot = Random.Range(-maxRotationAngle, maxRotationAngle);
        transform.DORotate(new Vector3(0, 0, randomRot), 0.2f)
                 .SetEase(Ease.Linear);
    }

    private Vector3 GetRandomShakeOffset()
    {
        // 使用 PerlinNoise 生成平滑的随机偏移
        float noiseX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0) * 2 - 1;
        float noiseY = Mathf.PerlinNoise(0, Time.time * shakeSpeed) * 2 - 1;
        return new Vector3(noiseX, noiseY, 0) * shakeIntensity;
    }

    private void OnMouseUp()
    {
        if (!InterActions.Instance.PlayerCanInteract()) return;
        transform.DOKill();
        if (Physics.Raycast(transform.position,Vector3.forward,out RaycastHit hit, 10f)){
            //play the card(需要赋予卡牌层级，然后触发对应的动作)
        }
        else
        {
            transform.position = dragStartPosition;
            transform.rotation = dragStartRotation;
        }
        InterActions.Instance.PlayerIsDragging = false;
    }

}
