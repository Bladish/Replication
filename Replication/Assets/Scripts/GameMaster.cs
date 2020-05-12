using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    GameObject text;
    Character[] cells;
    private void Start()
    {
        cells = GetComponentsInChildren<Character>();
    }
    public void CheckCells()
    {
        foreach (Character cell in cells)
        {
            if (cell._cellType == CellType.RedBloodCell)
            {
                return;
            }
        }
        text.SetActive(true);
        Time.timeScale = 0;
    }
}