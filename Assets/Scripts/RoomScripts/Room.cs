using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Room : MonoBehaviour
{
    
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
        if (!heroEntered)
        {
            HeroEntering(trigger);
        }
        else HeroLeaving(trigger);
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
}
