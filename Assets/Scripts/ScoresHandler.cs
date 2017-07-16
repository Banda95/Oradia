using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoresHandler : MonoBehaviour {

    public GameObject ScoreText;
    public GameObject startPosLocal;

    public GameObject panel;

	
	void Start () {
		//Local reading
        for(int i = 0; i<3; i++)
        {
            string s = GameData.ReadScore(i);
            if(s != "NONE")
            {
                Vector3 pos = startPosLocal.transform.position;
                pos.y = pos.y - 25 * i;
                Text t = Instantiate(ScoreText, pos, transform.rotation, panel.transform).GetComponent<Text>();
                t.text = s;
            }
        }
	}

}
