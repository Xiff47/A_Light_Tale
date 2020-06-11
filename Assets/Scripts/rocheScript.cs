using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocheScript : MonoBehaviour
{
	[SerializeField] bool normal = true;
	[SerializeField] bool givre = false;
	[SerializeField] bool neige = false;
	
	[SerializeField] Sprite normalSkin;
	[SerializeField] Sprite givreSkin;
	[SerializeField] Sprite neigeSkin;
	
	Sprite newSprite;
	
    // Start is called before the first frame update
    void Start()
    {
        if(neige && !normal && !givre && neigeSkin != null){
			newSprite = neigeSkin;
		}else if(givre && !neige && !normal && givreSkin != null){
			newSprite = givreSkin;
		}else{
			newSprite = normalSkin;
		}
		if(newSprite != null){
			GetComponent<SpriteRenderer>().sprite = newSprite;
		}
    }
}
