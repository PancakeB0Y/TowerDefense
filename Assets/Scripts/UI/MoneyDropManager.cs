using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDropManager : MonoBehaviour
{
    public static MoneyDropManager instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] GameObject moneyDropPrefab;
    Canvas canvas;
    RectTransform canvasRect;

    void Awake()
    {
        //Ensure singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvasRect = GetComponent<RectTransform>();
    }

    public void ShowMoney(EnemyController enemy)
    {
        if(canvas == null || canvasRect == null)
        {
            return;
        }

        //Get the position of the money on the canvas
        Vector2 canvasPosition = WorldToCanvasPosition(canvas, canvasRect, Camera.main, enemy.gameObject.transform.position);
        canvasPosition = new Vector2(canvasPosition.x - 15, canvasPosition.y + 15);

        GameObject moneyDrop = Instantiate(moneyDropPrefab, transform);
        moneyDrop.transform.position = canvasPosition;

        TextMeshProUGUI textMesh = moneyDrop.GetComponent<TextMeshProUGUI>();

        if (textMesh == null)
        {
            return;
        }
        //Set the correct money value
        textMesh.text = "+$" + enemy.money.ToString();
        

        EnemyMoneyDrop enemyMoneyDrop = moneyDrop.GetComponent<EnemyMoneyDrop>();
        if (enemyMoneyDrop == null)
        {
            return;
        }

        //Show money on screen
        enemyMoneyDrop.ShowMoneyDrop();
    }

    Vector2 WorldToCanvasPosition(Canvas canvas, RectTransform canvasRect, Camera camera, Vector3 position)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(camera, position);
        Vector2 result;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : camera, out result);

        return canvas.transform.TransformPoint(result);
    }
}
