using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemPowerUpDisplay : MonoBehaviour
{
    [SerializeField] private GameSystemPowerUpData[] powerups;
    [SerializeField] private GameObject template;
    [SerializeField] private Transform parent;

    private Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();

    private void Start()
    {
        foreach(GameSystemPowerUpData powerup in powerups)
        {
            GameObject obj = Instantiate(template, parent);
            if (obj.GetComponent<Image>() != null)
            {
                Image img = obj.GetComponent<Image>();
                img.sprite = powerup.sprite;
                img.color = Color.white;
            }
            
            if (!gameObjects.ContainsKey(powerup.name))
                gameObjects.Add(powerup.name, obj);

            obj.SetActive(false);
        }
    }

    public void DisplayPowerup(string name)
    {
        if (gameObjects.ContainsKey(name))
        {
            GameObject obj = gameObjects[name];
            obj.SetActive(true);
        }
    }

    public void HidePowerup(string name)
    {
        if (gameObjects.ContainsKey(name))
        {
            GameObject obj = gameObjects[name];
            obj.SetActive(false);
        }
    }
}
