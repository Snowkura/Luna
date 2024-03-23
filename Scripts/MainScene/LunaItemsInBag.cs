using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LunaItemsInBag : MonoBehaviour
{
    public static LunaItemsInBag Instance;
    public List<Item> items;
    public List<Item> importantItems;
    public GameObject itemIcon1;
    public GameObject itemIcon2;
    public GameObject itemIcon3;
    public GameObject itemIcon4;
    public GameObject itemIcon5;
    public GameObject itemIcon6;
    public GameObject importantItemIcon1;
    public GameObject importantItemIcon2;
    public GameObject importantItemIcon3;
    public GameObject importantItemIcon4;
    public GameObject importantItemIcon5;
    public GameObject importantItemIcon6;
    public Text itemNum1;
    public Text itemNum2;
    public Text itemNum3;
    public Text itemNum4;
    public Text itemNum5;
    public Text itemNum6;
    public Text importantItemNum1;
    public Text importantItemNum2;
    public Text importantItemNum3;        
    public Text importantItemNum4;
    public Text importantItemNum5;
    public Text importantItemNum6;

    public List<GameObject> itemIcons;
    public List<GameObject> importantItemIcons;
    public List<Text> itemNums;
    public List<Text> importantItemNums;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this; 
        items = new List<Item>();
        importantItems = new List<Item>();
        itemIcons = new List<GameObject>();
        importantItemIcons = new List<GameObject>();
        itemNums = new List<Text>();
        importantItemNums = new List<Text>();
        itemIcons.Add(itemIcon1);
        itemIcons.Add(itemIcon2);
        itemIcons.Add(itemIcon3);
        itemIcons.Add(itemIcon4);
        itemIcons.Add(itemIcon5);
        itemIcons.Add(itemIcon6);
        importantItemIcons.Add(importantItemIcon1);
        importantItemIcons.Add(importantItemIcon2);
        importantItemIcons.Add(importantItemIcon3);
        importantItemIcons.Add(importantItemIcon4);
        importantItemIcons.Add(importantItemIcon5);
        importantItemIcons.Add(importantItemIcon6);
        itemNums.Add(itemNum1);
        itemNums.Add(itemNum2);
        itemNums.Add(itemNum3);
        itemNums.Add(itemNum4);
        itemNums.Add(itemNum5);
        itemNums.Add(itemNum6);
        importantItemNums.Add(importantItemNum1);
        importantItemNums.Add(importantItemNum2);
        importantItemNums.Add(importantItemNum3);
        importantItemNums.Add(importantItemNum4);
        importantItemNums.Add(importantItemNum5);
        importantItemNums.Add(importantItemNum6);
    }    
    public void ShowOnBag(Item item)
    {                
        if (item.Important)
        {
            for (int j = 0; j < 6; j++)
            {
                if (importantItemIcons[j].GetComponent<Image>().sprite == null)
                {
                    importantItemIcons[j].GetComponent<Image>().sprite = item.Icon;
                    importantItemIcons[j].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    importantItemNums[j].text = importantItems[j].Number.ToString();
                    importantItemNums[j].color = new Color(1, 1, 1, 1);
                    importantItems[j].Index = j;
                    importantItemIcons[j].GetComponent<Button>().onClick.AddListener(item.UseItem);
                    break;
                }
                else if (importantItems[j].Name == item.Name)
                {
                    importantItems[j].NumPlusOne();
                    importantItemNums[j].text = importantItems[j].Number.ToString();
                    break;
                }
            }
                
        }
        else
        {
            for (int j = 0; j < 6; j++)
            {
                if (itemIcons[j].GetComponent<Image>().sprite == null)
                {
                    itemIcons[j].GetComponent<Image>().sprite = item.Icon;
                    itemIcons[j].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    items[j].Number = 1;
                    itemNums[j].text = items[j].Number.ToString();
                    itemNums[j].color = new Color(1, 1, 1, 1);
                    items[j].Index = j;
                    itemIcons[j].GetComponent<Button>().onClick.AddListener(item.UseItem);
                    break;
                }
                else if (items[j].Name == item.Name)
                {
                    items[j].NumPlusOne();
                    itemNums[j].text = items[j].Number.ToString();
                    break;
                }
            }
        }
        
    }
    
    public void Test()
    {
        Debug.Log("test");
    }

}
