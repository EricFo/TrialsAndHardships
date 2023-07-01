using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallMap : MonoBehaviour
{
    public GameObject Car;
    public GameObject Map;
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = Map.GetComponent<RectTransform>().pivot;
    }

    // Update is called once per frame
    void Update()
    {
        Map.GetComponent<RectTransform>().pivot = new Vector2(Car.transform.position.x / 445f, Car.transform.position.z / 445f) + startPos;
        Map.transform.eulerAngles = new Vector3(0, 0, Car.transform.eulerAngles.y);
    }
}
