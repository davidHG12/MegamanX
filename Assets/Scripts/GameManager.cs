using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] AudioClip sfx_win;
    bool finish;
    Megaman mega;

    void Awake()
    {
        this.gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(false);
    }
    void Start()
    {
        mega = FindObjectOfType<Megaman>();
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int size = enemies.Length;
        this.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text = "0" + size.ToString();
        if(size <= 0 && !finish)
        {
            finish = true;
            AudioSource.PlayClipAtPoint(sfx_win, Camera.main.transform.position);
            this.gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
            Destroy(mega.gameObject.GetComponent<Megaman>());
        }
        if(mega.pause)
        {
            this.gameObject.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("MegamanX");
    }
}
