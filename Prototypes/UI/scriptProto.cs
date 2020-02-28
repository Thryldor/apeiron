using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scriptProto : MonoBehaviour
{
    public Slider lifeBar;
    public Slider soulBar;
    public CanvasGroup lifeBarCanvas;
    public CanvasGroup soulBarCanvas;
    public CanvasGroup orb;
    public CanvasGroup breakGauge;
    private float life;
    private float soul;
    private bool soulBreak;
    // Start is called before the first frame update
    void Start()
    {
        life = 100;
        soul = 50;
        soulBreak = false;
        Screen.SetResolution(1080,720,false);
    }

    // Update is called once per frame
    void Update()
    {
        if (soul < 50f && soulBreak == false)
        {
            soul = soul + 5f * Time.deltaTime;
            if (soul >= 50f)
            {
                soul = 50f;
                orb.alpha = 1f;
                soulBarCanvas.alpha = 0f;
            }
        }
        else if (soul > 0f && soulBreak == true)
        {
            soul = soul - 5f * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (soulBreak)
            {
                life = 20f;
                soulBreak = false;
                lifeBarCanvas.alpha = 1f;
                breakGauge.alpha = 1f;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && soulBreak == false)
        {
            life = life + 20f;
            if (life > 100f)
            {
                life = 100f;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && soulBreak == false)
        {
            life = life - 20f;
            if (life < 0f)
            {
                life = 0f;
            }
            if (life == 0f)
            {
                soulBreak = true;
                breakGauge.alpha = 0f;
                lifeBarCanvas.alpha = 0f;
                soulBarCanvas.alpha = 1f;
                orb.alpha = 0f;
            }
        }

        lifeBar.value = life;
        soulBar.value = soul;
    }
}
