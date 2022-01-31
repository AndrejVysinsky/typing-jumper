using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// High score entry from db.
/// </summary>
[Serializable]
public class ResultModel: RequestModel
{
    [SerializeField] public int id;
}