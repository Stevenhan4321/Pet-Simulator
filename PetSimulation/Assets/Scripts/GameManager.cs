using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject nameText;
    public GameObject namePanel;
    public GameObject nameInput;

    public GameObject happinessText;
    public GameObject hungerText;

    public GameObject moralityText;
    public GameObject coinsText;

    public GameObject colorText;

    public GameObject slime;

    public GameObject homePanel;
    public Sprite[] homeTileSprites;
    public GameObject[] homeTiles;


    public GameObject background;
    public Sprite[] backgroundOptions;


    public GameObject foodPanel;
    public Sprite[] foodIcons;

    public GameObject jobPanel;



    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey ("tiles")){
            PlayerPrefs.SetInt("tiles", 0);
        }else{
            changeTiles(PlayerPrefs.GetInt("tiles"));
        }

        if(!PlayerPrefs.HasKey ("background")){
            PlayerPrefs.SetInt("background", 0);
        }else{
            changeTiles(PlayerPrefs.GetInt("background"));
        }

        if(!PlayerPrefs.HasKey ("food")){
            PlayerPrefs.SetInt("food", 0);
        }else{
            changeFood(PlayerPrefs.GetInt("food"));
        }


    }

    // Update is called once per frame
    void Update()
    {
        happinessText.GetComponent<Text> ().text = "" + slime.GetComponent<Slime> ().happiness;
        hungerText.GetComponent<Text> ().text = "" + slime.GetComponent<Slime> ().hunger;
        moralityText.GetComponent<Text> ().text = "" + slime.GetComponent<Slime> ().morality;
        coinsText.GetComponent<Text> ().text = "" + slime.GetComponent<Slime> ().coins;
        colorText.GetComponent<Text> ().text = "" + slime.GetComponent<Slime> ().color;
        nameText.GetComponent<Text> ().text = "" + slime.GetComponent<Slime> ().name;

    }



    public void triggerNamePanel(bool b){
        namePanel.SetActive(!namePanel.activeInHierarchy);
        if(b){
            slime.GetComponent<Slime>().name = nameInput.GetComponent<InputField> ().text;
            PlayerPrefs.SetString("name", slime.GetComponent<Slime> ().name);
        }
    }


    public void buttonBehavior(int i){
        switch(i){
        
        case(0):
            break;
        case(1):
            homePanel.SetActive (!homePanel.activeInHierarchy);
            break;
        case(2):
            foodPanel.SetActive(!foodPanel.activeInHierarchy);

            break;
        case(3):
            jobPanel.SetActive(!jobPanel.activeInHierarchy);

            break;
        case(4):
            slime.GetComponent<Slime> ().saveSlime();
            Application.Quit();
            break;


        }
    }





    public void changeTiles(int t){
        for(int i = 0; i<homeTiles.Length; i++){
            homeTiles [i].GetComponent<SpriteRenderer> ().sprite = homeTileSprites [t];
        }
        toggle(homePanel);
        PlayerPrefs.SetInt("tiles", t);
   }

   public void changeBackground(int i){
       background.GetComponent<SpriteRenderer> ().sprite = backgroundOptions [i];
       toggle(homePanel);
       PlayerPrefs.SetInt("background", i);
   }

    public void changeFood(int t){
        toggle(foodPanel);
   }


   public void toggle(GameObject g){
       if (g.activeInHierarchy){
           g.SetActive(false);
       }
   }
}
