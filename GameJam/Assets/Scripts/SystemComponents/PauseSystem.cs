using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    public static PauseSystem Instance { get; private set; }
    [SerializeField] private GameObject pauseObj;
    public bool paused { get; private set; } = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
           
        else
            Destroy(gameObject);
    }

    // void Start()
    // {
    //     pauseObj.SetActive(false);
    // }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        // pauseObj.SetActive(true);
        MenuManager.Instance.OpenRoot();
        MenuManager.Instance.OpenPausePage();
        paused = true;

        // yes pause is just freeze time what of it
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        // pauseObj.SetActive(false);
        MenuManager.Instance.CloseRoot();
        paused = false;

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
