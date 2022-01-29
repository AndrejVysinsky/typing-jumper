using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// High score entry from db.
/// </summary>
public class ResultModel : RequestModel
{
    public int Position { get; set; }
}