using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MemoryLibrary;

public class Program : MonoBehaviour, IPlayable
{
    LogicMemory logic;

	// Use this for initialization
	void Start ()
    {
        logic = new LogicMemory(this);
        logic.CreateNewGame();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnClick()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        int nr = int.Parse(go.name.Replace("Button", " "));
        Debug.Log(nr);
        logic.ClickPicture(nr);
    }

    public void ShowCard(int nr, int card)
    {
        GameObject button = GameObject.Find("Button" + nr);
        GameObject picture = GameObject.Find("" + card);
        button.GetComponent<Image>().sprite =
            picture.GetComponent<SpriteRenderer>().sprite;
    }

    public void HideCard(int nr)
    {
        ShowCard(nr, 0);
    }

    public void ShowWinner()
    {
        Debug.Log("Winner");
        logic.CreateNewGame();    
    }
}
