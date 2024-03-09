using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerAvatar;
    public GameObject Dectetor;
    //entrada de datos de los dectetores
    public Tile currentTile;
    public Tile forwardTile;
    public Tile backwardTile;
    public TileRotar forwardTileRotar;
    public TileRotar backwardTileRotar;
    public TileRotar currentTileRotar;
    //variables
    public float speed;
    public float rotationSpeed;
    public bool moving;
    public bool rotating;
    public Vector3 movingDirection;
    public Vector3 targetPosition;
    public float distanceToTarget;
    public bool turno = true;
    
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        //ReadInput();
        
        //actualizar estado
        if (moving){
            PerformMovement();
            
        }
        if (rotating){
            RotateTowardsDirection();
        }


    }
    public void MovingbyNum(int u)
    {
        StartCoroutine(MoveMultipleTiles(u));
    }

    private IEnumerator MoveMultipleTiles(int u)
    {
        for (int i = 0; i < u; i++)
        {
            Debug.Log("El personaje avanzo: "+ (i+1));
            if (forwardTile != null && !moving)
            {
                MoveToPosition(forwardTile.GetTilePosition());
            }
            else if (forwardTileRotar != null && !moving)
            {
                MoveToPosition(forwardTileRotar.GetTilePosition());
            }

            yield return new WaitUntil(() => !moving); // Esperar hasta que el movimiento actual se complete
        }
    }

    void ReadInput(){
        //ver si se preciona la tecla w
            if (Input.GetKeyDown(KeyCode.W)){
                //comprobar si existe el achirvo
                if (forwardTile != null){
                    //obtener la ubicacion del objecto
                    MoveToPosition(forwardTile.GetTilePosition());
                }
                else if (forwardTileRotar != null)
                {
                    MoveToPosition(forwardTileRotar.GetTilePosition());
                }
            }
        
            if (Input.GetKeyDown(KeyCode.S)){//Ver si se preciona la tecla S
                if (backwardTile != null){
                    MoveToPosition(backwardTile.GetTilePosition());
                }
                else if (backwardTileRotar != null){
                    MoveToPosition(backwardTileRotar.GetTilePosition());
                }
            }
    }

    void PerformMovement(){
        Vector3 newPosition = transform.position + movingDirection * speed * Time.deltaTime; // cordanadas para el movimiento
        Vector3 targetXZ = new Vector3(targetPosition.x, newPosition.y, targetPosition.z); // Mantener la altura del jugador constante

        float currentDistance = (targetXZ - newPosition).magnitude;//distancia
        
        if (distanceToTarget < currentDistance)
        {
            moving = false;
            transform.position = new Vector3(targetXZ.x, transform.position.y, targetXZ.z); // Ajustar solo las coordenadas (x) y (z)
        }
        else
        {
            distanceToTarget = currentDistance;
            transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z); // Ajustar solo las coordenadas (x) y (z)
        
        }
    }

    void RotateTowardsDirection()
    {
        Vector3 targetDirection = new Vector3(movingDirection.x, 0f, movingDirection.z).normalized;
        Quaternion targetRotation = Quaternion.identity; // Definir la variable fuera del condicional

        if (targetDirection != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(targetDirection);
            playerAvatar.transform.rotation = Quaternion.RotateTowards(playerAvatar.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            Dectetor.transform.rotation = playerAvatar.transform.rotation;
        }

        if (Quaternion.Angle(playerAvatar.transform.rotation, targetRotation) < 0.01f)
        {
            rotating = false;
        }
    }

    public void MoveToPosition(Vector3 position)    {
        targetPosition = position;
        movingDirection = (position - transform.position);
        distanceToTarget = movingDirection.magnitude;
        movingDirection = movingDirection.normalized;
        moving = true;
        rotating = true;
    }

    public void SetForwardTile(Tile tile)    {
        forwardTile = tile;
    }
    public void SetBackwardTile(Tile tile)    {
        backwardTile = tile;
    }
    public void SetCurrentTile(Tile tile)    {
        currentTile = tile;
    }

    public void SetForwardTileRotar(TileRotar tileRotar)    {
        forwardTileRotar = tileRotar;
    }
    public void SetBackwardTileRotar(TileRotar tileRotar)    {
        backwardTileRotar = tileRotar;
    }
    public void SetCurrentTileRotar(TileRotar tileRotar)    {
        currentTileRotar = tileRotar;
    }
}