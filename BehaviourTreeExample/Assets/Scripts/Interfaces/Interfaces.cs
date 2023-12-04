using System.Collections.Generic;
using UnityEngine;

public interface ISpottable
{
    public bool isSpotted { get; }
    public List<GameObject> spotters { get; }

    public void Spot(GameObject _spotter, bool _spotted);
}