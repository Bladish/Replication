using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public override void DeActivateYourSelf()
    {
        SceneManager.LoadScene("Menu");
        gameObject.SetActive(false);
    }
}