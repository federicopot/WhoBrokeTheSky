using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Awake(){
        //......controllo se esistono salvataggi
    }
    public void BtnManager(string nameScene){
        if(nameScene.ToLower() != "quit"){
            UnityEngine.SceneManagement.SceneManager.LoadScene(nameScene);
        }else{
            Application.Quit();
            Debug.Log("quit!");
        }
    }
}