using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Room : MonoBehaviour
{
    public bool leftDoor;
    public bool rightDoor;
    public bool topDoor;
    public bool bottomDoor;
    
    [SerializeField] private EntryTrigger _triggerLeft;
    [SerializeField] private EntryTrigger _triggerRight;
    [SerializeField] private EntryTrigger _triggerTop;
    [SerializeField] private EntryTrigger _triggerBottom;
    
    public List <EntryTrigger> entryTriggerList = new List<EntryTrigger>();
    public bool heroEntered = false;
    public bool enemiesSpawned = false;
    public int xPosInArray;
    public int yPosInArray;


    public void TriggerActivated(EntryTrigger trigger)
    {
        /*if (!heroEntered)
        {
            HeroEntering(trigger);
        }
        else HeroLeaving(trigger);*/
        HeroLeaving(trigger);
        trigger.gameObject.SetActive(false);
    }

    public void HeroEntering(EntryTrigger trigger)
    { 
        heroEntered = true;
        //Spawning/activating enemies
        Debug.Log("hero Entered Room from " + trigger.name);
        //hero vosiel, spawn
        
    }
    public void HeroLeaving(EntryTrigger trigger)
    {
        Debug.Log("hero leaved Room from " + trigger.name);
        //instantiating next room
        RoomManager.instance.SpawnRoom(this,trigger.direction);
    }

    public void DisableLeftTrigger()
    {
        _triggerLeft.gameObject.SetActive(false);
    }
    public void DisableRightTrigger()
    {
        _triggerRight.gameObject.SetActive(false);
    }
    public void DisableTopTrigger() 
    {
        _triggerTop.gameObject.SetActive(false);
    }
    public void DisableBottomTrigger() 
    {
        _triggerBottom.gameObject.SetActive(false);
    }


    public bool SpawnedNeighborFromLeft()
    {
        _triggerLeft.gameObject.SetActive(false);
        return leftDoor;
    }
    public bool SpawnedNeighborFromRight()
    {
        _triggerRight.gameObject.SetActive(false);
        return rightDoor;
    }
    public bool SpawnedNeighborFromTop()
    {
        _triggerTop.gameObject.SetActive(false);
        return topDoor;
    }
    public bool SpawnedNeighborFromBottom()
    {
        _triggerBottom.gameObject.SetActive(false);
        return bottomDoor;
    }


}
