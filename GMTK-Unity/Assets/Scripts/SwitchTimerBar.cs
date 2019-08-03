using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTimerBar : MonoBehaviour
{
    public GameObject GameManager;
    public GameObject FillObject;
    public float MaxScale;

    // Start is called before the first frame update
    void Start()
    {
        FillObject.transform.localScale = new Vector2(FillObject.transform.localScale.x, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ActiveEnemyManager enemyManager = GameManager.GetComponent<ActiveEnemyManager>();
        FillObject.transform.localScale = new Vector2(FillObject.transform.localScale.x, Mathf.Lerp(0, MaxScale, enemyManager.TimerCount / enemyManager.ActiveEnemySwitchTime));
    }
}
