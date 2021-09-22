using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    string currentWorldName = "Forest1";
    RoomInfo currentLoadRoomData;
    Room currentRoom;
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRooms = new List<Room>();
    bool isLoadingRoom = false;
    public bool spawnedEndRoom = false;
    public bool updatedRooms = false;
    public bool populatedRooms = false;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;   
    }

    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if(isLoadingRoom)
            return;
        if(loadRoomQueue.Count == 0)
        {
            if(!updatedRooms && !spawnedEndRoom)
            {
                foreach(Room room in loadedRooms)
                {
                    room.RemoveConnectedDoors();
                    string roomName = FindReplacementRoomName(room);
                    StartCoroutine(ChangeRoom(room, roomName));
                }
                updatedRooms = true;
            }
            else if(updatedRooms && !spawnedEndRoom)
            {
                StartCoroutine(SpawnEndRoom());
            }   
            return;
        } 
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    string FindReplacementRoomName(Room room)
    {
        bool hasTop = false, hasBottom = false, hasLeft = false, hasRight = false;
        foreach(Door door in room.doors)
        {
            switch(door.doorType)
            {
                case Door.DoorType.top:
                    if(door.gameObject.activeSelf)
                        hasTop = true;
                    break;
                case Door.DoorType.bottom:
                    if(door.gameObject.activeSelf)
                        hasBottom = true;
                    break;
                case Door.DoorType.left:
                    if(door.gameObject.activeSelf)
                        hasLeft = true;
                    break;
                case Door.DoorType.right:
                    if(door.gameObject.activeSelf)
                        hasRight = true;
                    break;
            }
        }
        return DetermineRoomName(hasTop, hasBottom, hasLeft, hasRight);
    }
    string DetermineRoomName(bool hasTop, bool hasBottom, bool hasLeft, bool hasRight)
    {
        string roomName = "Empty";
        if(!hasTop && !hasBottom && !hasLeft && !hasRight)
        {
            roomName +="Cross";
            if(Random.Range(0,100) < 50){
                roomName +="2";
            }
        }
        else if(hasTop && !hasBottom && !hasLeft && !hasRight)
        {
            roomName += "TTop";
        }
        else if(!hasTop && hasBottom && !hasLeft && !hasRight)
        {
            roomName += "TBottom";
        }
        else if(!hasTop && !hasBottom && hasLeft && !hasRight)
        {
            roomName += "TLeft";
        }
        else if(!hasTop && !hasBottom && !hasLeft && hasRight)
        {
            roomName += "TRight";
        }
        else if(!hasTop && !hasBottom && hasLeft && hasRight)
        {
            roomName += "TopToBottom";
        }
        else if(hasTop && hasBottom && !hasLeft && !hasRight)
        {
            roomName += "LeftToRight";
        }
        else if(!hasTop && hasBottom && !hasLeft && hasRight)
        {
            roomName += "TopToLeft";
        }
        else if(!hasTop && hasBottom && hasLeft && !hasRight)
        {
            roomName += "TopToRight";
        }
        else if(hasTop && !hasBottom && !hasLeft && hasRight)
        {
            roomName += "BottomToLeft";
        }
        else if(hasTop && !hasBottom && hasLeft && !hasRight)
        {
            roomName += "BottomToRight";
        }
        else if(hasTop && hasBottom && !hasLeft && hasRight)
        {
            roomName += "DeadEndLeft";
        }
        else if(hasTop && hasBottom && hasLeft && !hasRight)
        {
            roomName += "DeadEndRight";
        }
        else if(!hasTop && hasBottom && hasLeft && hasRight)
        {
            roomName += "DeadEndTop";
        }
        else if(hasTop && !hasBottom && hasLeft && hasRight)
        {
            roomName += "DeadEndBottom";
        }
        return roomName;
    }

    IEnumerator ChangeRoom(Room room, string roomName)
    {
        yield return new WaitForSeconds(0.1f);
        Room tempRoom = new Room(room.X, room.Y);
        Destroy(room.gameObject);
        var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
        loadedRooms.Remove(roomToRemove);
        LoadRoom(roomName, tempRoom.X, tempRoom.Y);
    }

    IEnumerator SpawnEndRoom()
    {
        spawnedEndRoom = true;
        yield return new WaitForSeconds(3f);
        PopulateRooms();
        yield return new WaitUntil(()=> populatedRooms);
        Room endRoom = FindLongDistanceRoom();
        StartCoroutine(ChangeRoom(endRoom, "End"));
    }

    Room FindLongDistanceRoom()
    {
        int maxDistance = 0, index = 0;
        for(int i = 0; i < loadedRooms.Count-1; i++)
        {
            int distance = Mathf.Abs(loadedRooms.ElementAt(i).X) + Mathf.Abs(loadedRooms.ElementAt(i).Y);
            if(distance > maxDistance)
            {
                maxDistance = distance;
                index = i;
            }
        }
        return loadedRooms.ElementAt(index);
    }

    void PopulateRooms()
    {
        foreach(Room room in loadedRooms)
        {
            GameObject spawnables = room.gameObject.transform.Find("Spawnables").gameObject;
            if(spawnables == null){
                Debug.Log("No spawnables object in this room " + room.name);
            }
            else if(spawnables.transform.childCount < 1)
            {
                Debug.Log("No spawnable children in this room " + room.name);
            }
            else
            {
                if(!(room.name.Contains("End") || (room.X == 0 && room.Y == 0)))
                {
                    for (int i = 0; i < spawnables.transform.childCount; i++)
                    {
                        int randomInt = Random.Range(0,100);
                        GameObject child = spawnables.transform.GetChild(i).gameObject;
                        if(randomInt < child.GetComponent<Spawnable>().spawnChance)
                            child.SetActive(true);
                    }
                    
                    
                }
            }
        }
        populatedRooms = true;
    }

    public void LoadRoom(string name, int x, int y)
    {
        if(DoesRoomExist(x, y))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if(!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(
            currentLoadRoomData.X * room.Width,
            currentLoadRoomData.Y * room.Height,
            0
            );

            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if(loadedRooms.Count == 0)
                CameraController.instance.currentRoom = room;
            
            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
     }

     public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
     }

     public void OnPlayerEnterRoom(Room room)
     {
         CameraController.instance.currentRoom = room;
         currentRoom = room;
     }
}
