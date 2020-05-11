using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    Virus,
    WhiteBloodCell,
    RedBloodCell,
}
public class NPC : MonoBehaviour
{

    public GameObject _virus;
    public GameObject _redBloodCell;
    public GameObject _whiteBloodCell;
    public NPC Target { get; private set; }

    public CellType _cellType;
    //public StateMachine StateMachine => GetComponent<StateMachine>();

    private void Awake()
    {
        InitializeStateMachine();
        SetCellType(_cellType);
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>
        {
            {typeof(PatrolState), new PatrolState(this) },
            {typeof(ChaseState), new ChaseState(this) },
        };
        GetComponent<StateMachine>().SetStates(states);
    }

    public void SetTarget(NPC target)
    {
        if(target._cellType != _cellType) Target = target;
    }

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") 
        { 
            CellType cellType = collision.gameObject.GetComponent<NPC>()._cellType;
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
