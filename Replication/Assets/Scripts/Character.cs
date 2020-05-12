using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    Virus,
    WhiteBloodCell,
    RedBloodCell,
}
public class Character : MonoBehaviour
{
    public GameObject _virus;
    public GameObject _redBloodCell;
    public GameObject _whiteBloodCell;
    public CellType _cellType;
    public void SetCellType(CellType cellType)
    {
        _cellType = cellType;
        switch (cellType)
        {
            case CellType.Virus:
                _virus.SetActive(true);
                _redBloodCell.SetActive(false);
                _whiteBloodCell.SetActive(false);
                break;
            case CellType.WhiteBloodCell:
                _virus.SetActive(false);
                _redBloodCell.SetActive(false);
                _whiteBloodCell.SetActive(true);
                break;
            case CellType.RedBloodCell:
                _virus.SetActive(false);
                _redBloodCell.SetActive(true);
                _whiteBloodCell.SetActive(false);
                break;
            default:
                break;
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CellType cellType = collision.gameObject.GetComponent<Character>()._cellType;
            {
                switch (_cellType)
                {
                    case CellType.Virus:
                        if (cellType == CellType.WhiteBloodCell)
                        {
                            DeActivateYourSelf();
                        }
                        break;
                    case CellType.WhiteBloodCell:
                        break;
                    case CellType.RedBloodCell:
                        if (cellType == CellType.Virus) SetCellType(cellType);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void DeActivateYourSelf()
    {
        gameObject.SetActive(false);
    }
}