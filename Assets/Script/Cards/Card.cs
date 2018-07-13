using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public CardsData Data;
    [HideInInspector]
    public CardType Type;
    [HideInInspector]
    public int Attack;
    int life;
    public int Life
    {
        get
        { return life; }
        set
        {
            life = value;
            lifeText.text = life.ToString();
            if (life <= 0)
            {
                Debug.Log(Data.Name + " è morto");
            }
        }
    }

    int purificationOrDarkness;
    public int PurificationOrDarkness
    {
        get { return purificationOrDarkness; }
        set
        {
            purificationOrDarkness = value;
        }
    }


    [SerializeField]
    Image imageCard, imageCover;
    [SerializeField]
    Text nameText, attackText, lifeText, purificationOrDarknessText, descriptionText;
    
    private void Start()
    {
        Type = Data.Type;
        Attack = Data.Attack;
        Life = Data.Life;
        PurificationOrDarkness = Data.PurificationOrDarkness;
        nameText.text = Data.Name;
        imageCard.sprite = Data.SpriteImage;
        attackText.text = Data.Attack.ToString();
        lifeText.text = Data.Life.ToString();
        purificationOrDarknessText.text = Data.PurificationOrDarkness.ToString();
        descriptionText.text = Data.Description;
        if (Type == CardType.Guardian)
        {
            nameText.color = Color.black;
            descriptionText.color = Color.black;
            attackText.color = Color.black;
            lifeText.color = Color.black;
            purificationOrDarknessText.color = Color.black;
            imageCover.color = Color.white;
        }
        else if (Type == CardType.Nightmare)
        {
            nameText.color = Color.white;
            descriptionText.color = Color.white;
            attackText.color = Color.white;
            lifeText.color = Color.white;
            purificationOrDarknessText.color = Color.white;
            imageCover.color = Color.black;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Life--;
        }
    }
}
