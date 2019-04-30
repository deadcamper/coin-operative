using UnityEngine;

public class EscToToggle : MonoBehaviour
{
    public Transform objectToToggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            objectToToggle.gameObject.SetActive(!objectToToggle.gameObject.activeSelf);
        }

        if (objectToToggle.gameObject.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
