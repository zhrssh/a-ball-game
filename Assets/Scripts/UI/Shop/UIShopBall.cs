using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DevZhrssh.SaveSystem;
using UnityEngine.UI;

public class UIShopBall : MonoBehaviour
{
    [SerializeField] private UIShopNavigation shopBallNavigation;
    [SerializeField] private Transform navigationBar;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    [SerializeField] private TextMeshProUGUI abilityText;

    [SerializeField] private UIShopBuyOrEquip shopBallBuyAndEquip;

    private GameSystemSaveHandler playerSave;
    private SaveData data;

    public enum Ability
    {
        NONE,
        ROCKETS,
        DOUBLESCORE,
        DOUBLECOINS,
        INVULNERABLE,
        RANDO
    }

    private bool isSelected;

    [SerializeField] private Image _image;
    public Image image { 
        get {
            return _image; 
        } 
    }

    [SerializeField] private bool isDefault;
    [SerializeField] private int id;
    [SerializeField] private int price;
    [SerializeField] private Ability ability;

    private bool isBought;

    private void Start()
    {
        playerSave = GameObject.FindObjectOfType<GameSystemSaveHandler>();
        if (playerSave != null)
            data = playerSave.Load();

        if (shopBallNavigation != null)
            shopBallNavigation.OnBallSelectedCallback += Select;

        if (shopBallBuyAndEquip != null)
            shopBallBuyAndEquip.OnButtonPressedCallback += Buy;

        if (isDefault == true)
        {
            isSelected = true;
            isBought = true;
        }
    }

    private void Update()
    {
        if (id == 1)
            isBought = data.ball2;
        if (id == 2)
            isBought = data.ball3;
        if (id == 3)
            isBought = data.ball4;
        if (id == 4)
            isBought = data.ball5;

        if (isSelected)
        {
            // Move bar into position
            Vector3 pos = navigationBar.transform.position;
            Vector3 targetPos = transform.position + offset;

            Vector3 newPos = Vector3.MoveTowards(pos, targetPos, speed * Time.deltaTime);
            navigationBar.transform.position = newPos;

            // Change text
            switch (ability)
            {
                case Ability.NONE:
                    abilityText.text = "Ability: None";
                    break;
                case Ability.DOUBLESCORE:
                    abilityText.text = "Ability: Double Score";
                    break;
                case Ability.DOUBLECOINS:
                    abilityText.text = "Ability: Double Coins";
                    break;
                case Ability.ROCKETS:
                    abilityText.text = "Ability: Spawn Rockets";
                    break;
                case Ability.INVULNERABLE:
                    abilityText.text = "Ability: Invulnerable for 10s";
                    break;
                case Ability.RANDO:
                    abilityText.text = "Ability: Random Buffs";
                    break;
            }     
            
            if (shopBallBuyAndEquip != null)
            {
                shopBallBuyAndEquip.SetCurrentBall(gameObject.GetComponent<UIShopBall>());
            }
        }
    }

    public bool GetSelect()
    {
        return isSelected;
    }

    public int GetPrice()
    {
        return price;
    }

    public int GetBallID()
    {
        return id;
    }

    public bool IsDefault()
    {
        return isDefault;
    }

    public bool IsBought()
    {
        return isBought;
    }

    public Ability GetAbility()
    {
        return ability;
    }

    public void Select(int id)
    {
        if (this.id == id)
        {
            isSelected = true;
        }
        else
        {
            isSelected = false;
        }
    }

    public void Buy(int id)
    {
        if (this.id == id)
        {
            isBought = true;
        }
    }
}
