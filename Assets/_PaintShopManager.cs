using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _PaintShopManager : MonoBehaviour
{
    [SerializeField] private List<Paint> paints;
    public GameObject initBody;
    float y = 800;

    private void Start()
    {
        CreateBodyMarket();
        PlayerPrefs.SetInt("Paints0Buy",1);
    }

    private void CreateBodyMarket()
    {
        for (int i = 0; i < paints.Count; i++)
        {
            int id = i;
            GameObject body = Instantiate(initBody, transform);
            if (PlayerPrefs.GetInt("Paints" + id + "Buy", 0) == 0)
            {
                body.transform.Find("Body Buy Button").GetComponent<Button>().onClick.AddListener(() => Buy(id, body));
            }
            if (PlayerPrefs.GetInt("Paints" + id + "Buy", 0) == 1)
            {
                
                body.transform.Find("Body Buy Button").GetChild(0).GetComponent<Text>().text = "Owned";
                body.transform.Find("Body Buy Button").GetComponent<Button>().onClick.AddListener(() => Use(id));
                paints[id].isBuyed = true;
            }
            body.transform.Find("BodyPhoto").GetComponent<Image>().sprite = paints[id].BuyedImage;
            body.transform.Find("BodyText").GetComponent<Text>().text = paints[i].Name;
            body.transform.Find("Body Coin Text").GetComponent<Text>().text = paints[i].Coin + "";

            body.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, y);
            y = y - 800;
        }
    }

    private void Buy(int id, GameObject body)
    {
        if (GameManager.TotalCoin >= paints[id].Coin || paints[id].isBuyed)
        {
            AudioManager.instance.Play("Click");
            PlayerPrefs.SetInt("Paints" + id + "Buy", 1);
            if (!paints[id].isBuyed)
            {
                GameManager.TotalCoin -= paints[id].Coin;
            }
            body.transform.Find("BodyPhoto").GetComponent<Image>().sprite = paints[id].BuyedImage;
            paints[id].isBuyed = true;
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
        PlayerPrefs.SetInt("Mesh_Select", id);
        _CarManager.Instance.SelectCar();
    }
}

[System.Serializable]
public class Paint
{
    public string Name;
    public int Coin;
    public Sprite BuyedImage;
    public bool isBuyed;

    public Paint(string name, int coin,bool buyed, Sprite buyedImage)
    {
        Name = name;
        Coin = coin;
        isBuyed = buyed;
        BuyedImage = buyedImage;
    }
}
