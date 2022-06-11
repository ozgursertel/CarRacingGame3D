using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _BodyShopManager : MonoBehaviour
{

   [SerializeField] private List<CarBody> bodies;
   public GameObject initBody;
    float y = 800;

    private void Start()
    {   
        CreateBodyMarket();
        PlayerPrefs.SetInt("Bodies0Buy", 1);
    }

    private void CreateBodyMarket()
    {
        for (int i = 0; i<bodies.Count;i++)
        {
            int id = i;
            GameObject body = Instantiate(initBody, transform);
            if(PlayerPrefs.GetInt("Bodies"+id+"Buy",0) == 0)
            {
                body.transform.Find("BodyPhoto").GetComponent<Image>().sprite = bodies[i].Image;
                body.transform.Find("Body Buy Button").GetComponent<Button>().onClick.AddListener(() => Buy(id, body));

            }
            if (PlayerPrefs.GetInt("Bodies" + id + "Buy", 0) == 1)
            {
                body.transform.Find("BodyPhoto").GetComponent<Image>().sprite = bodies[id].BuyedImage;
                body.transform.Find("Body Buy Button").GetChild(0).GetComponent<Text>().text = "Owned";
                body.transform.Find("Body Buy Button").GetComponent<Button>().onClick.AddListener(() => Use(id));
                bodies[id].isBuyed = true;
            }

            body.transform.Find("BodyText").GetComponent<Text>().text = bodies[i].Name;
            body.transform.Find("Body Coin Text").GetComponent<Text>().text = bodies[i].Coin + "";
            
            body.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,y);
            y = y - 800;
        }
    }

    private void Buy(int id,GameObject body)
    {
        if(GameManager.TotalCoin >= bodies[id].Coin)
        {
            AudioManager.instance.Play("Click");
            PlayerPrefs.SetInt("Bodies" + id + "Buy", 1);
            GameManager.TotalCoin -= bodies[id].Coin;
            body.transform.Find("BodyPhoto").GetComponent<Image>().sprite = bodies[id].BuyedImage;
            bodies[id].isBuyed = true;
            body.transform.Find("Body Buy Button").GetChild(0).GetComponent<Text>().text = "Owned";
            body.transform.Find("Body Buy Button").GetComponent<Button>().onClick.AddListener(() => Use(id));
        }
        else
        {
            Debug.Log("Not Enough Coin");
        }
    }

    private void Use(int id)
    {
        AudioManager.instance.Play("Click");
        PlayerPrefs.SetInt("Selected_Car", id);
        _CarManager.Instance.SelectCar();
    }
}

[System.Serializable]
public class CarBody
{
    public string Name;
    public int Coin;
    public Sprite Image;
    public Sprite BuyedImage;
    public bool isBuyed;

    public CarBody(string name, int coin, Sprite Image,bool buyed,Sprite buyedImage)
    {
        Name = name;
        Coin = coin;
        this.Image = Image;
        isBuyed = buyed;
        BuyedImage = buyedImage;
    }
}


