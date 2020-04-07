using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{

	SpriteRenderer sprite;
	public int _r;
	public int _g;
	public int _b;
	public int _a;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sprite.color = new Color(_r, _g, _b, _a);
    }

    public void myNewFunctionS(int r, int g, int b, int a){
    	_r = r;
    	_g = g;
    	_b = b;
    	_a = a;
    }
}
