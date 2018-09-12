using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	//This means that the score will be shared along all classes. Effectively won't reset
    public static int score;


    Text text;


    void Awake ()
    {
		//At initialization, we set our score and text variables
        text = GetComponent <Text> ();
        score = 0;
    }


    void Update ()
    {
		//We update the text of our ScoreText with the score
        text.text = "Score: " + score;
    }
}
