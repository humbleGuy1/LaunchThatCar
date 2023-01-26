using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoad : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropDown;

    private void Awake()
    {
        _dropDown.SetValueWithoutNotify(SceneManager.GetActiveScene().buildIndex);    
    }

    public void Load(int index)
    {
        SceneManager.LoadScene(index);
    }
}
