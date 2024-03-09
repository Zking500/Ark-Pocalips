using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDetector : MonoBehaviour
{
    public enum TileDetectorType
    {
        Forward,
        Backward,
        Floor
    }

    public TileDetectorType detectorType;
    PlayerController player;
    private Vector3 initialLocalPosition;
    private Vector3 savedLocalPosition;
    private Quaternion initialRotation;

    void Start()
    {
        player = transform.parent.parent.GetComponent<PlayerController>();
        initialLocalPosition = GetInitialLocalPosition();
        savedLocalPosition = initialLocalPosition;
        initialRotation = transform.localRotation; // Guarda la rotación inicial
    }

    void Update()
    {
        RestoreLocalPosition();
    }

    private void SaveLocalPosition()
    {
        savedLocalPosition = transform.localPosition;
    }

    private void RestoreLocalPosition()
    {
        transform.localPosition = savedLocalPosition;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f) * initialRotation;
    }

    private Vector3 GetInitialLocalPosition()
    {
        switch (detectorType)
        {
            case TileDetectorType.Forward:
                return new Vector3(0f, -0.5f, 1f);
            case TileDetectorType.Backward:
                return new Vector3(0f, -0.5f, -1f);
            case TileDetectorType.Floor:
                return new Vector3(0f, -0.5f, 0f);
            default:
                return Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Tile tile = other.GetComponent<Tile>();
        if (tile != null)
        {
            switch (detectorType)
            {
                case TileDetectorType.Forward:
                    player.SetForwardTile(tile);
                    break;
                case TileDetectorType.Backward:
                    player.SetBackwardTile(tile);
                    break;
                case TileDetectorType.Floor:
                    player.SetCurrentTile(tile);
                    break;
            }
        }

        TileRotar tileRotar = other.GetComponent<TileRotar>();
        if (tileRotar != null)
        {
            switch (detectorType)
            {
                case TileDetectorType.Forward:
                    player.SetForwardTileRotar(tileRotar);
                    break;
                case TileDetectorType.Backward:
                    player.SetBackwardTileRotar(tileRotar);
                    break;
                case TileDetectorType.Floor:
                    player.SetCurrentTileRotar(tileRotar);
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Tile tile = other.GetComponent<Tile>();
        if (tile != null)
        {
            switch (detectorType)
            {
                case TileDetectorType.Forward:
                    player.SetForwardTile(null);
                    break;
                case TileDetectorType.Backward:
                    player.SetBackwardTile(null);
                    break;
                case TileDetectorType.Floor:
                    player.SetCurrentTile(null);
                    break;
            }
        }

        TileRotar tileRotar = other.GetComponent<TileRotar>();
        if (tileRotar != null)
        {
            switch (detectorType)
            {
                case TileDetectorType.Forward:
                    player.SetForwardTileRotar(null);
                    break;
                case TileDetectorType.Backward:
                    player.SetBackwardTileRotar(null);
                    break;
                case TileDetectorType.Floor:
                    player.SetCurrentTileRotar(null);
                    break;
            }
        }
    }
}
