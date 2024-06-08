using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BuildingCreator : MonoBehaviour
{
    [SerializeField] private GameObject buildingPartPrefab;
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private Text destroyedBuildingCountText;
    
    private Vector3 _buildingSize = new Vector3(5, 10, 5);
    private Vector3 _buildingPosition = new Vector3(-50, 0, 20);
    
    private List<Building> _buildings = new List<Building>();
    private int _destroyedBuildingCount = 0;
    
    // ------------------------------------------------------------
    // これはシングルトンパターンといって、インスタンスが1つしか存在しないことを保証するための設計パターンです。
    // これにより、他のスクリプトからこのスクリプトのインスタンスを取得する際に、
    // このスクリプトがアタッチされているオブジェクトを探す必要がなくなります。
    public static BuildingCreator Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // ------------------------------------------------------------
    

    private void Start()
    {
        destroyedBuildingCountText.text = "破壊されたビル: 0/5";
        
        for (int i = 0; i < 5; i++)
        {
            _buildingPosition.x += Random.Range(10, 20);
            _buildingPosition.z += Random.Range(-10, 10);
            CreateBuilding(_buildingPosition);
        }
    }

    private void CreateBuilding(Vector3 position)
    {
        GameObject buildingObject = Instantiate(buildingPrefab, position, Quaternion.identity);
        Building building = buildingObject.GetComponent<Building>();
        _buildings.Add(building);
        
        float offset = 0.1f;

        for (int x = 0; x < _buildingSize.x; x++)
        {
            for (int y = 0; y < _buildingSize.y; y++)
            {
                for (int z = 0; z < _buildingSize.z; z++)
                {
                    Vector3 spawnPosition = position + new Vector3(x * (1 + offset), y * (1 + offset), z * (1 + offset));
                    GameObject buildingPart = Instantiate(buildingPartPrefab, spawnPosition, Quaternion.identity);
                    buildingPart.transform.parent = buildingObject.transform;
                }
            }
        }
    }
    
    public void CheckBuildings()
    {
        _destroyedBuildingCount = 0;
        foreach (var building in _buildings)
        {
            if (building.IsDestroyed)
            {
                _destroyedBuildingCount++;
            }
        }
        destroyedBuildingCountText.text = $"破壊されたビル: {_destroyedBuildingCount} / {_buildings.Count}";
        
    }
}
