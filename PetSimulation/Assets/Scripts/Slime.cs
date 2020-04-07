using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Slime : MonoBehaviour
{

    public int _hunger;
    public int _happiness;
    // [SerializedField]
    public int _morality;

    public int _coins;

    public string _color;

    public string _name;
    private int _clickCount;
    public int _money;


    public bool _serverTime;
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.SetString("then", "03/31/2020 19:20:12");
        updateStatus();
        if(!PlayerPrefs.HasKey("name")){
            PlayerPrefs.SetString("name", "Slime");

        }
        _name = PlayerPrefs.GetString("name");
    }

    void Update(){
        GetComponent<Animator>().SetBool ("Walk", gameObject.transform.position.y > -2.9f);
        if(Input.GetMouseButtonUp(0)){
            Debug.Log("Clicked");
            Vector2 v = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v), Vector2.zero);
            if(hit){
                Debug.Log(hit.transform.gameObject.name);
                if(hit.transform.gameObject.tag == "Slime"){
                    _clickCount++;
                    if(_clickCount >= 3){
                        _clickCount = 0;
                        updateHappiness(20);
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 50));
                    }
                }
            }
        }
    }

    void updateStatus(){
        if(!PlayerPrefs.HasKey("_hunger")){
            _hunger = 100;
            PlayerPrefs.SetInt("_hunger", _hunger);
        }else{
            hunger = PlayerPrefs.GetInt("_hunger");
        }

        if(!PlayerPrefs.HasKey("_happiness")){
            _happiness = 100;
            PlayerPrefs.SetInt("_happiness", _happiness);
        }else{
            _happiness = PlayerPrefs.GetInt("_happiness");
        }


        if(!PlayerPrefs.HasKey("_morality")){
            _morality = 50;
            PlayerPrefs.SetInt("_morality", _morality);
        }else{
            _morality = PlayerPrefs.GetInt("_morality");
        }

        if(!PlayerPrefs.HasKey("_coins")){
            _coins = 50;
            PlayerPrefs.SetInt("_coins", _coins);
        }else{
            _coins = PlayerPrefs.GetInt("_coins");
        }

        if(!PlayerPrefs.HasKey("_color")){
            _color = "neutral";
            PlayerPrefs.SetString("_color", _color);
        }else{
            _color = PlayerPrefs.GetString("_color");
        }



        if(!PlayerPrefs.HasKey("then")){
            PlayerPrefs.SetString("then", getStringTime());
        }


        TimeSpan ts = GetTimeSpan();


        _hunger -= (int)(ts.TotalMinutes *2);
        if(_hunger <0){
            _hunger = 0;
        }

        _happiness -= (int)((100-_hunger)*(ts.TotalMinutes/5));

        if(_happiness <0){
            _happiness = 0;
        }
        // Debug.Log(GetTimeSpan ().ToString());
        // Debug.Log(GetTimeSpan ().TotalHours);

        if(_serverTime){
            updateServer();
        }else{
            InvokeRepeating("updateDevice", 0f, 30f);
        }
    }


    void updateServer(){
    }

    void updateDevice(){
        PlayerPrefs.SetString("then", getStringTime());
    }

    TimeSpan GetTimeSpan(){
        if(_serverTime){
            return new TimeSpan();
        }else{
            return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("then"));
        }
    }
    string getStringTime(){
        DateTime now = DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second; 
    }


    public int hunger{
        get{ return _hunger;}
        set{ _hunger = value;}
    }

    public int happiness{
        get{ return _happiness;}
        set{ _happiness = value;}
    }

    public int morality{
        get{ return _morality;}
        set{ _morality = value;}
    }
    public int coins{
        get{ return _coins;}
        set{ _coins = value;}
    }
    public string color{
        get{ return _color;}
        set{ _color = value;}
    }

    public string name{
        get{return _name;}
        set{_name = value;}
    }

    public void updateHappiness(int i){
        happiness += i;
        if(happiness>100){
            happiness = 100;
        }
    }

    public void buyElement(string x){
        if(coins < 10 || hunger > 100){
            return;
        }
        if(color == x){
            coins -= 10;
            hunger += Convert.ToInt32(hunger * .10);
        }
        coins -= 20;
        color = x;
        hunger += 1;
    }


    public void buyVegan(){
        if(coins < 35 || hunger > 100){
            return;
        }
        coins -= 35;
        hunger += 1;
        morality += 5;
    }

    public void buySteak(){
        if(coins < 35 || hunger > 100){
            return;
        }
        coins -= 35;
        hunger += 1;
        morality -= 5;
    }

    public void starve(){
        if(hunger < 15){
            return;
        }
        hunger -= 15;
    }

    public void sadMusic(){
        if(happiness < 15){
            return;
        }
        happiness -= 15;
    }


    public void regularJob(){
        if(happiness < 5){
            return;
        }
        coins += 5;
        happiness -= 5;
    }

    public void moralJob(){
        if(happiness < 5 || morality < 80){
            return;
        }
        coins += 30;
        happiness -= 15;
    }

    public void illegalJob(){
        if(happiness < 10 || morality > 20 ){
            return;
        }
        coins += 20;
        happiness -= 10;
    }



    public void saveSlime(){
        if(!_serverTime)
            updateDevice();
        PlayerPrefs.SetInt("_hunger", _hunger);
        PlayerPrefs.SetInt("_happiness", _happiness);
    }



}
