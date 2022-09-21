using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopBallSelect : MonoBehaviour
{
    [SerializeField] private ShopBallNavigation shopBallNavigation;
    [SerializeField] private Transform navigationBar;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    [SerializeField] private TextMeshProUGUI abilityText;

    [SerializeField] private ShopBallBuyAndEquip shopBallBuyAndEquip;

    public enum Ability
    {
        NONE,
        ROCKETS,
        DOUBLESCORE,
        DOUBLECOINS,
        EXTRALIFE
    }

    private bool isSelected;

    [SerializeField] private bool isDefault;
    [SerializeField] private int id;
    [SerializeField] private int price;
    [SerializeField] private Ability ability;

    private bool isBought;

    private void Start()
    {
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
                case Ability.EXTRALIFE:
                    abilityText.text = "Ability: Extra Life";
                    break;
            }     
            
            if (shopBallBuyAndEquip != null)
            {
                shopBallBuyAndEquip.SetCurrentBall(gameObject.GetComponent<ShopBallSelect>());
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
