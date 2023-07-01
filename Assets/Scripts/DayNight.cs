using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DayNight : MonoBehaviour
{
    public float dayLength = 30f;
    //public Gradient dayLightColor;
    Light dayLight;

    // Start is called before the first frame update
    void Start()
    {
        dayLight = GetComponent<Light>();
        Sequence dayLightColor = DOTween.Sequence();
        this.transform.DOLocalRotate(new Vector3(360, 0, 0), dayLength).SetRelative(true).SetLoops(-1).SetEase(Ease.Linear);
        dayLightColor.AppendInterval(dayLength * 0.05f);
        dayLightColor.Append(dayLight.DOColor(new Color(255/255f, 170/255f, 0 / 255f), dayLength * 0.1f));
        dayLightColor.Append(dayLight.DOColor(new Color(0 / 255f, 0 / 255f, 0 / 255f), dayLength * 0.1f));
        dayLightColor.AppendInterval(dayLength * 0.5f);
        dayLightColor.Append(dayLight.DOColor(new Color(255 / 255f, 140 / 255f, 0 / 255f), dayLength * 0.1f));
        dayLightColor.Append(dayLight.DOColor(new Color(255 / 255f, 255 / 255f, 255 / 255f), dayLength * 0.1f));
        dayLightColor.AppendInterval(dayLength*0.05f);
        dayLightColor.SetEase(Ease.Linear).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
