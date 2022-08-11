using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
	[SerializeField]
    private GameObject[] availableRooms;
    [SerializeField]	
	private List<GameObject> currentRooms;
	
	[SerializeField]
	private GameObject[] availableObjects;
	[SerializeField]
	private List<GameObject> objects;
	
	[SerializeField]
	private float objectsMinDistance = 5.0f;
	[SerializeField]
	private float objectsMaxDistance = 10.0f;

	[SerializeField]
	private float objectsMinY = -1.4f;
	[SerializeField]
	private float objectsMaxY = 1.4f;

	[SerializeField]
	private float objectsMinRotation = -45.0f;
	[SerializeField]
	private float objectsMaxRotation = 45.0f;
	
	private int lazerCount;
	private int coinCount;
	private float screenWidthInPoints;
    // Start is called before the first frame update
    private void Start()
    {
	    float height = 2.0f * Camera.main.orthographicSize;
	    screenWidthInPoints = height * Camera.main.aspect;
	    lazerCount = 0;
	    coinCount = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
	    GenerateRoomIfRequired();
	    GenerateObjectsIfRequired();
    }
    
    private void AddRoom(float farhtestRoomEndX){
	    int randomRoomIndex = Random.Range(0, availableRooms.Length);
	    GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);
	    float roomWidth = room.transform.Find("floor").localScale.x;
	    float roomCenter = farhtestRoomEndX + roomWidth * 0.5f;
	    room.transform.position = new Vector3(roomCenter, 0, 0);
	    currentRooms.Add(room);
    }

    private void GenerateRoomIfRequired(){
	    List<GameObject> roomsToRemove = new List<GameObject>();
	    var addRooms = true;
	    var playerX = transform.position.x;
	    var removeRoomX = playerX - screenWidthInPoints;
	    var addRoomX = playerX + screenWidthInPoints;
	    float farthestRoomEndX = 0;
	    foreach(var room in currentRooms){
		    float roomWidth = room.transform.Find("floor").localScale.x;
		    float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
		    float roomEndX = roomStartX + roomWidth;

		    if(roomStartX > addRoomX){
			    addRooms = false;
		    }
		    if(roomEndX < removeRoomX){
			    roomsToRemove.Add(room);
		    }
		    farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
	    }
	    foreach(var room in roomsToRemove){
		    currentRooms.Remove(room);
		    Destroy(room);
	    }
	    if(addRooms){
		    AddRoom(farthestRoomEndX);
	    }
    }
    
    private void AddObject(float lastObjectX)
    {
	    int randomIndex;

	    if (lazerCount == 3)
	    {
		    randomIndex = 0;
			
	    }
	    else if (coinCount == 3)
	    {
		    randomIndex = 1;
			
	    }
	    else
	    {
		    randomIndex = Random.Range(0, availableObjects.Length);
	    }
	    if (randomIndex == 0)
	    {
		    coinCount++;
		    lazerCount = 0;
	    }
	    else { lazerCount++;
		    coinCount = 0;
	    }
	    GameObject obj = (GameObject)Instantiate(availableObjects[randomIndex]);
	    float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
	    float randomY = Random.Range(objectsMinY, objectsMaxY);
	    obj.transform.position = new Vector3(objectPositionX,randomY,0); 
	    float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);
	    obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
	    objects.Add(obj);            
    }

    private void GenerateObjectsIfRequired()
    {
	    float playerX = transform.position.x;
	    float removeObjectsX = playerX - screenWidthInPoints;
	    float addObjectX = playerX + screenWidthInPoints;
	    float farthestObjectX = 0;
	    List<GameObject> objectsToRemove = new List<GameObject>();
	    foreach (var obj in objects)
	    {

		    float objX = obj.transform.position.x;
		    farthestObjectX = Mathf.Max(farthestObjectX, objX);
		    if (objX < removeObjectsX) 
		    {           
			    objectsToRemove.Add(obj);
		    }
	    }
	    foreach (var obj in objectsToRemove)
	    {
		    objects.Remove(obj);
		    Destroy(obj);
	    }
	    if (farthestObjectX < addObjectX)
	    {
		    AddObject(farthestObjectX);
	    }
    }
}
