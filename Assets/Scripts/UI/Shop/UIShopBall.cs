using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DevZhrssh.SaveSystem;
using UnityEngine.UI;

public class UIShopBall : MonoBehaviour
{
    // Handles UI bar selection
    [SerializeField] private Transform navigationBar;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    // Displays ball ability
    [SerializeField] private TextMeshProUGUI abilityText;

    // References
    private GameSystemShop shop;
    private UIShopNavigation shopBallNavigation;
    private UIShopBuyOrEquip shopBallBuyAndEquip;

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

    // Ball Properties
    [SerializeField] private bool isDefault;
    [SerializeField] private int id;
    [SerializeField] private int price;
    [SerializeField] private Ability ability;

    private bool isBought;

    private void Awake()
    {
        // References
        shopBallBuyAndEquip = GameObject.FindObjectOfType<UIShopBuyOrEquip>();
        shopBallNavigation = GameObject.FindObjectOfType<UIShopNavigation>();
        shop = GameObject.FindObjectOfType<GameSystemShop>();
    }

    private void Start()
    {
        if (shopBallNavigation != null)
            shopBallNavigation.OnBallSelectedCallback += Select;

        if (isDefault == true)
        {
            isSelected = true;
            isBought = true;
        }

        // Check if the ball is bought from shop
        if (isDefault == false)
            isBought = shop.IsBallBought(id);
    }

    private void Update()
    {
        if (isSelected)
        {
            // Check if the ball is bought
            if (isDefault == false)
                isBought = shop.IsBallBought(id);

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
}
