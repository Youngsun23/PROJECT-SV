using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantedCropsManager : SingletonBase<PlantedCropsManager>
{
    public PlantedCropsContainer Container => container;
    [SerializeField] private PlantedCropsContainer container;
}
