using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AI.Detection.Player
{
    public interface IPlayerDetection
    {
        PlayerController Player { get; }

        bool IsPlayerVisible(Vector3 from);
    }
}