using System.Collections.Generic;
using UnityEngine;

public interface ISpottable
{
    bool isSpotted { get; }
    List<GameObject> spotters { get; }

    void Spot(GameObject _spotter, bool _spotted);
}