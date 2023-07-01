using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public GameObject start;
    public GameObject set;
    public AudioSource bg;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bg.volume = slider.value;
    }

    public void Set()
    {
        set.SetActive(true);
        start.SetActive(false);
    }
    public void Return()
    {
        start.SetActive(true);
        set.SetActive(false);
    }
    public void Load()
    {
        SceneManager.LoadScene("Load");
    }
}
