using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    #region Parameters

    private GameObject canvas;
    private SpriteRenderer spriteRenderer;

    #endregion

    private void Start()
    {
        canvas = transform.GetChild(0).gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvas.SetActive(false);
        spriteRenderer.enabled = false;
    }

    private void SetState(bool state)
    {
        canvas.SetActive(state);
        spriteRenderer.enabled = state;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Action") && canvas.activeSelf)
        {
            PlayerStatus.status.SaveGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerStatus>() != null) SetState(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerStatus>() != null)
        {
            SetState(false);
            Container.container.CloseContainer();
        }
    }
}
