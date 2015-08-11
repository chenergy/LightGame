using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public DataManager data;
	public InputHandler input;
	

	private static GameManager instance = null;
	public static GameManager Instance {
		get { return GameManager.instance; }
	}

	void Awake(){
		if (instance != null){
			GameObject.Destroy(this.gameObject);
		} else {
			DontDestroyOnLoad(this.gameObject);
			instance = this;
		}
	}
}

