using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ItemUseAction();
public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public Item smallHPPotion;
    public Sprite smallHPPotionImg;
    public Item candle;
    public Sprite candleImg;
    public Item mushroom;
    public Sprite mushroomImg;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        smallHPPotion = new Item("SmallHPPotion", smallHPPotionImg, false, 1, ItemActions.Heal100HP);
        candle = new Item("Candle", candleImg, true, 1, ItemActions.DoNothing);
        mushroom = new Item("Mushroom", mushroomImg, false, 1, ItemActions.Restore50MP);
    }
    
    public void AddItemInBag(Item item)
    {
        if (!LunaItemsInBag.Instance.items.Contains(item))
        {
            LunaItemsInBag.Instance.items.Add(item);
        }
        LunaItemsInBag.Instance.ShowOnBag(item);
    }
    public void AddImportantItemInBag(Item item)
    {
        if (!LunaItemsInBag.Instance.importantItems.Contains(item))
        {
            LunaItemsInBag.Instance.importantItems.Add(item);
        }
        LunaItemsInBag.Instance.ShowOnBag(item);
    }

    
}

public interface ItemInterface
{
    string Name { get; }
    Sprite Icon { get; }
    bool Important { get; }
    int Number { get; }
    int Index { get; }

    public void NumPlusOne() { }
}

public class Item : ItemInterface
{
    public string Name { get; set; }
    public Sprite Icon { get; set; }
    public bool Important { get; set; }
    public int Number { get; set; }
    public int Index { get; set; } 
    private ItemUseAction useAction;

    public Item(string name, Sprite icon, bool important, int number, ItemUseAction action)
    {
        Name = name;
        Icon = icon;
        Important = important;
        Number = number;
        Index = 0;
        useAction = action;
    }
    public void UseItem()
    {
        useAction?.Invoke();
        if (!Important)
        {
            Number--;
            if (Number == 0)
            {
                LunaItemsInBag.Instance.items.RemoveAt(Index);
                LunaItemsInBag.Instance.itemIcons[Index].GetComponent<Image>().sprite = null;
                LunaItemsInBag.Instance.itemIcons[Index].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                LunaItemsInBag.Instance.itemNums[Index].color = new Color(1, 1, 1, 0);
                LunaItemsInBag.Instance.itemIcons[Index].GetComponent<Button>().onClick.RemoveAllListeners();
                for (int i = Index + 1; i < 6; i++)
                {
                    if (LunaItemsInBag.Instance.itemIcons[i].GetComponent<Image>().sprite != null)
                    {
                        Debug.Log(Index);
                        Debug.Log("进入条件");
                        LunaItemsInBag.Instance.itemIcons[i - 1].GetComponent<Image>().sprite = LunaItemsInBag.Instance.items[i - 1].Icon;
                        LunaItemsInBag.Instance.items[i - 1].Index--;
                        LunaItemsInBag.Instance.itemIcons[i - 1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        LunaItemsInBag.Instance.itemNums[i - 1].text = LunaItemsInBag.Instance.items[i - 1].Number.ToString();
                        LunaItemsInBag.Instance.itemNums[i - 1].color = new Color(1, 1, 1, 1);
                        LunaItemsInBag.Instance.itemIcons[i - 1].GetComponent<Button>().onClick.AddListener(LunaItemsInBag.Instance.items[i - 1].UseItem);
                        LunaItemsInBag.Instance.itemIcons[i].GetComponent<Image>().sprite = null;
                        LunaItemsInBag.Instance.itemIcons[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                        LunaItemsInBag.Instance.itemNums[i].color = new Color(1, 1, 1, 0);
                        LunaItemsInBag.Instance.itemIcons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                LunaItemsInBag.Instance.itemNums[Index].text = Number.ToString();
            }
        }
    }
    public void NumPlusOne()
    {
        Number++;
    }
}

public class ItemActions : MonoBehaviour
{   
    public static void DoNothing()
    {

    }
    public static void Heal100HP()
    {
        GameManager.Instance.ChangeHP(100);
        GameManager.Instance.PlaySound(GameManager.Instance.drinkPosion);
        Debug.Log("使用了小型药水，恢复100血");
    }
    public static void Heal200HP()
    {
        GameManager.Instance.ChangeHP(200);
        GameManager.Instance.PlaySound(GameManager.Instance.drinkPosion);
    }
    public static void Restore50MP()
    {
        GameManager.Instance.ChangeMP(50);
        GameManager.Instance.PlaySound(GameManager.Instance.drinkPosion);
    }
}
