using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintMenu : MonoBehaviour
{
    public Transform backMenu;

    public Button okay;

    // Start is called before the first frame update
    void Awake()
    {
        okay.onClick.AddListener(Okay);
    }

    void Okay()
    {
        gameObject.SetActive(false);
        backMenu.gameObject.SetActive(true);
    }
}
