using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipBox : MonoBehaviour
{
    Canvas canvas;
    Animator animator;

    [SerializeField] TextMeshProUGUI tooltipText = null;
    [SerializeField] Button button = null;


    public void End()
    {
        animator.SetTrigger ( "End" );
    }

    public void DisableTooltip()
    {
        gameObject.SetActive ( false );
    }

    public void OnTooltip(string msg, Action action)
    {
        gameObject.SetActive ( true );
        tooltipText.text = msg;
        button.onClick.AddListener ( 
            ( ) => action.Invoke ( ) );
    }

    public void OnTooltip(string msg, float lifeTime)
    {
        gameObject.SetActive ( true );
        tooltipText.text = msg;
        button.interactable = false;
        Invoke ( "End", lifeTime );
    }

    void Awake ( )
    {
        canvas = GetComponentInParent<Canvas> ( );
        animator = GetComponent<Animator> ( );
    }

    void Start ( )
    {
        canvas.worldCamera = GameManager.Instance.MainCamera;
        gameObject.SetActive ( false );
    }

    void OnEnable ( )
    {
        animator.SetTrigger ( "Start" );
    }
}
