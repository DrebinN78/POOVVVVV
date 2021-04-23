using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IEntity
{
    GameObject gameObject { get; }

    public void InitEntity(int p1, int p2, int p3, int p4, int p5, int p6);
}