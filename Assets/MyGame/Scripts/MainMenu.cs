using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _GUI;

    [SerializeField] private RectTransform _mainMenuPanel;
    [SerializeField] private RectTransform _infoPanel;
    [SerializeField] private RectTransform _exitScreen;
    [SerializeField] private RectTransform _controlPanel;

    [SerializeField] private AudioSource _mainTheme;
    [SerializeField] private AudioSource _easyTheme;
    [SerializeField] private AudioSource _mediumTheme;

    [SerializeField] private Button _continue;
    [SerializeField] private Button _options;
    [SerializeField] private Button _exit;
    [SerializeField] private Button _back;

    [SerializeField] private Text _healthPointText = null;
    [SerializeField] private Text _weaponText = null;
    [SerializeField] private Text _countGrenadeText = null;
    
    [SerializeField] private HeroMove _hero;

    private float _heroHP;
    private int _grenadeCount;

    private bool _inMenu = false;
    private bool _inInfo = false;
    private bool _exitCheck = false;

    
    private void Awake()
    {
        
        _controlPanel.gameObject.SetActive(false);
        _mainMenuPanel.gameObject.SetActive(false);
        _infoPanel.gameObject.SetActive(false);
        _exitScreen.gameObject.SetActive(false);

        _exit.onClick.AddListener(Exit);
        _back.onClick.AddListener(Back);
    }

    public void ClickEscape()
    {
        _controlPanel.gameObject.SetActive(false);
        _inMenu = true;
        _mainMenuPanel.gameObject.SetActive(true);
        gameObject.GetComponent<AudioSource>().Play();

        _mainTheme.mute = true;
        //_easyTheme.mute = true;
        //_mediumTheme.mute = true;

        Time.timeScale = 0;

        _GUI.SetActive(false);
    }
    public void Continue()
    {
        //_controlPanel.gameObject.SetActive(true);
        _mainMenuPanel.gameObject.SetActive(false);
        _mainTheme.mute = false;
        //_easyTheme.mute = false;
        //_mediumTheme.mute = false;
        _inMenu = false;
        gameObject.GetComponent<AudioSource>().Stop();

        Time.timeScale = 1;


        _GUI.SetActive(true);
    }
    private void Update()
    {
        _heroHP = _hero.GetHP();
        _grenadeCount = _hero.GetGrenadeCount();
        if(_exitCheck == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _inMenu == false)
            {
                ClickEscape();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _inMenu == true && _inInfo == false)
            {
                Continue();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _inInfo == true)
            {
                _inInfo = false;
                _mainMenuPanel.gameObject.SetActive(true);
                _infoPanel.gameObject.SetActive(false);
            }
        }
        
    }

    
    public void InfoPanel()
    {
        _inInfo = true;
        _mainMenuPanel.gameObject.SetActive(false);
        _infoPanel.gameObject.SetActive(true);

        _healthPointText.text = "Health: " + _heroHP;
        _weaponText.text = "Weapon: Benelli MC10";
        _countGrenadeText.text = "Count of grenade: " + _grenadeCount;

    }

    private void Back()
    {
        _inInfo = false;
        _mainMenuPanel.gameObject.SetActive(true);
        _infoPanel.gameObject.SetActive(false);
    }
    private void Exit()
    {
        _inInfo = false;
        _mainMenuPanel.gameObject.SetActive(false);
        _infoPanel.gameObject.SetActive(false);

        _exitScreen.gameObject.SetActive(true);
        _exitCheck = true;

        StartCoroutine(nameof(ExitDelay));
    }
    IEnumerator ExitDelay()
    {
        yield return new WaitForSeconds(.1f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;    // somehow don`t work
#endif
        Application.Quit();
    }

    
}
