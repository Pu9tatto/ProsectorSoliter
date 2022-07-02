using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Set Dynamiclly")]
    public string suit; //масть карыт
    public int rank; //достоинство карты 
    public Color color = Color.black; //цвет значков
    public string colS = "Black";
    //Списко хранящий все игровые обънекты Decorator
    public List<GameObject> decoGOs = new List<GameObject>();
    //Списко хранящий все игровые обънекты pip
    public List<GameObject> pipGOs = new List<GameObject>();

    public GameObject back; //игровой объект рубашки карты

    public CardDefinition def; //извлекатеся из DeckXML.xml
    //список компонентов spriterenderer этого и вложенных в него игровых объектов 
    public SpriteRenderer[] spriteRenderers;

    void Start()
    {
        SetSortOrder(0);   
    }

    public void PopulateSpriteRenderers()
    {
        if(spriteRenderers == null || spriteRenderers.Length == 0)
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }
    }

    public void SetSortingLayerName(string tSLN)
    {
        PopulateSpriteRenderers();
        foreach (SpriteRenderer tSR in spriteRenderers)
        {
            tSR.sortingLayerName = tSLN;
        }
    }

    public void SetSortOrder (int sOrd)
    {
        PopulateSpriteRenderers();

        foreach (SpriteRenderer tSR in spriteRenderers)
        {
            if (tSR.gameObject == this.gameObject)
            {
                tSR.sortingOrder = sOrd;
                continue;
            }
            switch (tSR.gameObject.name)
            {
                case "back":
                    tSR.sortingOrder = sOrd + 2;
                    break;
                case "face":
                default:
                    tSR.sortingOrder = sOrd + 1;
                    break;

            }
        }
    }

    public bool faceUp
    {
        get
        {
            return (!back.activeSelf);
        }
        set
        {
            back.SetActive(!value);
        }
    }

    virtual public void OnMouseUpAsButton()
    {
        print(name);
    }
}

[System.Serializable]
public class Decorator {
// Этот класс хранит информацию из DeckXML о каждом значке на карте
public string type; // Значок, определяющий достоинство карты, имеет
// type = "pip"
public Vector3 loc; // Местоположение спрайта на карте
public bool flip = false; // Признак переворота спрайта по вертикали
public float scale = 1f; // Масштаб спрайта
}
[System.Serializable]
public class CardDefinition {
// Этот класс хранит информацию о достоинстве карты
public string face; // Спрайт, изображающий лицевую сторону карты
public int rank; // Достоинство карты (1-13)
public List<Decorator> pips = new List<Decorator>(); // Значки // a
}

