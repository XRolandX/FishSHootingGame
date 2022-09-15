using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IMGUIMainScene : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private HeroMove _hero;
    [SerializeField] private Texture2D heartIconFull;
    [SerializeField] private Texture2D heartIconHalf;
    [SerializeField] private Texture2D heartIconEmpty;
    [SerializeField] private Texture2D _bernelli;
    [SerializeField] private Texture2D _nexus;
    [SerializeField] private Texture2D _grenade;

    private GameObject _camera;

    private void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 45, 100, 30), "Restart Enemy"))
            _enemy.Restart();

        if (GUI.Button(new Rect(20, 75, 100, 30), "Restart"))
            SceneManager.LoadScene(0);

        switch (_hero.GetHP())
        {
            case 6:
                GUI.DrawTexture(new Rect(20, 10, 30, 30), heartIconFull);
                GUI.DrawTexture(new Rect(50, 10, 30, 30), heartIconFull);
                GUI.DrawTexture(new Rect(80, 10, 30, 30), heartIconFull);
                break;
            case 5:
                GUI.DrawTexture(new Rect(20, 10, 30, 30), heartIconFull);
                GUI.DrawTexture(new Rect(50, 10, 30, 30), heartIconFull);
                GUI.DrawTexture(new Rect(80, 10, 30, 30), heartIconHalf);
                break;
            case 4:
                GUI.DrawTexture(new Rect(20, 10, 30, 30), heartIconFull);
                GUI.DrawTexture(new Rect(50, 10, 30, 30), heartIconFull);
                GUI.DrawTexture(new Rect(80, 10, 30, 30), heartIconEmpty);
                break;
            case 3:
                GUI.DrawTexture(new Rect(20, 10, 30, 30), heartIconFull);
                GUI.DrawTexture(new Rect(50, 10, 30, 30), heartIconHalf);
                GUI.DrawTexture(new Rect(80, 10, 30, 30), heartIconEmpty);
                break;
            case 2:
                GUI.DrawTexture(new Rect(20, 10, 30, 30), heartIconFull);
                GUI.DrawTexture(new Rect(50, 10, 30, 30), heartIconEmpty);
                GUI.DrawTexture(new Rect(80, 10, 30, 30), heartIconEmpty);
                break;
            case 1:
                GUI.DrawTexture(new Rect(20, 10, 30, 30), heartIconHalf);
                GUI.DrawTexture(new Rect(50, 10, 30, 30), heartIconEmpty);
                GUI.DrawTexture(new Rect(80, 10, 30, 30), heartIconEmpty);
                break;
            case 0:
                _camera.GetComponent<AudioSource>().Stop();
                GUI.DrawTexture(new Rect(20, 10, 30, 30), heartIconEmpty);
                GUI.DrawTexture(new Rect(50, 10, 30, 30), heartIconEmpty);
                GUI.DrawTexture(new Rect(80, 10, 30, 30), heartIconEmpty);
                break;
            default:
                GUI.DrawTexture(new Rect(20, 10, 30, 30), heartIconEmpty);
                GUI.DrawTexture(new Rect(50, 10, 30, 30), heartIconEmpty);
                GUI.DrawTexture(new Rect(80, 10, 30, 30), heartIconEmpty);
                break;

        }
        
        

        GUI.Label(new Rect(120, 10, 100, 30), new GUIContent(_bernelli));
        if (_hero.GetGrenadeCount() == 2)
        {
            GUI.DrawTexture(new Rect(130, 30, 30, 30), _grenade);
            GUI.DrawTexture(new Rect(150, 30, 30, 30), _grenade);
        }
        else if (_hero.GetGrenadeCount() == 1)
        {
            GUI.DrawTexture(new Rect(130, 30, 30, 30), _grenade);
        }
        else if (_hero.GetGrenadeCount() == 0)
        {

        }


    }

}