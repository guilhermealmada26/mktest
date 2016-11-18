using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour {

    public int roundTime = 100;
    private float lastTimeUpdate = 0;
    public bool canFight;

    [SerializeField]
    BannerController bannnerCtrl;
    [SerializeField]
    Fighter fighter1, fighter2;
    [SerializeField]
    AudioSource mainSource;
    [SerializeField]
    GameObject pauseMenu, handHeldMenu;
    bool paused;
    float timeScale;

    void Awake()
    {
        if (SystemInfo.deviceType != DeviceType.Handheld)
            handHeldMenu.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        timeScale = Time.timeScale;
        pauseMenu.SetActive(false);
        paused = false;
        bannnerCtrl.ShowROF();
        mainSource.Play();
	}

    // Update is called once per frame
    void Update()
    {
        if(!canFight && !bannnerCtrl.isAnimating())
        {
            canFight = true;
            fighter1.enable = true;
            fighter2.enable = true;
        }

        if (roundTime > 0 && Time.time - lastTimeUpdate > 1)
        {
            roundTime--;
            lastTimeUpdate = Time.time;
        }

        if(canFight)
        CheckEnd();

        if (Input.GetKeyDown(KeyCode.Escape) || (SystemInfo.deviceType != DeviceType.Handheld && Input.touchCount == 3))
        {
            if (!paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
        }

    void Pause()
    {
        paused = true;
        canFight = false;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    void CheckEnd()
    {
        if (fighter1.health == 0)
        {
            canFight = false;
            fighter1.enable = false;
            fighter2.enable = false;
            bannnerCtrl.ShowYL();
            mainSource.Stop();
        }

        if (fighter2.health == 0)
        {
            canFight = false;
            fighter1.enable = false;
            fighter2.enable = false;
            bannnerCtrl.ShowYW();
            mainSource.Stop();
        }
    }

    public void ToggleMenu()
    {
        if (paused)
            Resume();
        else
            Pause();

    }
	
	public void LoadScene(int index){
		Time.timeScale = timeScale;
		SceneManager.LoadScene(index);
	}

    public void Resume()
    {
        paused = false;
        canFight = true;
        Time.timeScale = timeScale;
        pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = timeScale;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
