using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using System.Collections.Generic;

public class DialogSeletor : MonoBehaviour {

    public Dropdown.DropdownEvent callback;
    [SerializeField]
    private Text prefab;
    private Queue<Text> hideItems = new Queue<Text>();
    private List<Text> showing = new List<Text>();
    internal void ChargeSelecton(List<string> infomation)
    {
        HideItems();
        ShowItems(infomation);
    }
    private void HideItems()
    {
        foreach (var item in showing)
        {
            item.gameObject.SetActive(false);
            hideItems.Enqueue(item);
        }
        showing.Clear();
    }
    private void ShowItems(List<string> infomation)
    {
        for (int i = 0; i < infomation.Count; i++)
        {
            var item = infomation[i];
            Text instence = null;
            if(hideItems.Count > 0)
            {
                instence = hideItems.Dequeue();
            }
            else
            {
                var index = i;
                instence = CreateNewItem(index);
            }
            instence.text = item;
            instence.gameObject.SetActive(true);
            showing.Add(instence);
        }
    }
    private Text CreateNewItem(int index)
    {
        var textItem = Instantiate<Text>(prefab);
        textItem.transform.SetParent(transform, false);
        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((x)=>callback.Invoke(index));
        textItem.GetComponent<EventTrigger>().triggers.Add(entry);
        return textItem;
    }

}
