using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
public class EventMaintenance
{
    public List<Event> events = new List<Event>();
    public List<LongEvent> long_events = new List<LongEvent>();
    public List<Event> environmental_events = new List<Event>();
    public List<LongEvent> environmental_long_events = new List<LongEvent>();
    public List<Event> relation_events = new List<Event>();
    public List<LongEvent> relation_long_events = new List<LongEvent>();
    public int pick = -1;
    // Use this for initialization
    public EventMaintenance()
    {
        using (var reader = new StreamReader(@"Assets/Scripts/testLongEvents.tsv"))
        {
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split('\t');
                int EventID = int.Parse(values[0]);
                int noofoptions = int.Parse(values[1]);
                int factor = int.Parse(values[2]);
                string desc = values[3];
                string type = values[4];
                LongEvent Event = new LongEvent(EventID, desc, noofoptions, factor);
                long_events.Add(Event);
 
            }
        }
        using (var reader = new StreamReader(@"Assets/Scripts/testLongEventsoptions.tsv"))
        {
            reader.ReadLine();
            int n = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split('\t');
                int eventID = int.Parse(values[0]);
                int prEffect = int.Parse(values[1]);
                int legalEffect = int.Parse(values[2]);
                int researchEffect = int.Parse(values[3]);
                double moneyPercentChange = double.Parse(values[4]);
                int prReq = int.Parse(values[5]);
                int legalReq = int.Parse(values[6]);
                int researchReq = int.Parse(values[7]);
                int doomCounter = int.Parse(values[8]);
                double chance = double.Parse(values[9]);
                int marketEffect = int.Parse(values[10]);
                int regulation = int.Parse(values[11]);
                int gasreq = int.Parse(values[12]);
                string optionDescription = values[13];
                int EN = int.Parse(values[14]);
                int RN = int.Parse(values[15]);
                if (n != 1)
                {
                    long_events[eventID - 1].options[n] = new Option(optionDescription, prEffect, legalEffect, researchEffect, moneyPercentChange, prReq, legalReq, researchReq, doomCounter, chance, marketEffect, regulation, optionDescription, EN, RN);
                    n++;
                }
            }

        }
        // Read in event csv
        using (var reader = new StreamReader(@"Assets/Scripts/testOptions.tsv"))
        {
            reader.ReadLine();
            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                var values = line.Split('\t');
                int EventID = int.Parse(values[0]);
                string type = values[1];
                string description = values[2];
                //Debug.Log(description);
                Event newEvent = new Event(EventID, type, description,3);
                events.Add(newEvent);
            }
        }

        using (var reader = new StreamReader(@"Assets/Scripts/testEvents.tsv"))
        {
            int count = 0;
            reader.ReadLine();
            int lineNum = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split('\t');
     
                int eventID = int.Parse(values[0]);
                int prEffect = int.Parse(values[1]);
                int legalEffect = int.Parse(values[2]);
                int researchEffect = int.Parse(values[3]);
                double moneyPercentChange = double.Parse(values[4]);
                int prReq = int.Parse(values[5]);
                int legalReq = int.Parse(values[6]);
                int researchReq = int.Parse(values[7]);
                int doomCounter = int.Parse(values[8]);
                double chance = double.Parse(values[9]);
                int marketEffect = int.Parse(values[10]);
                int regulation = int.Parse(values[11]);
                string optionDescription = values[12];
                int EN = int.Parse(values[13]);
                int RN = int.Parse(values[14]);

                events[eventID - 1].options[count] = new Option(optionDescription, prEffect, legalEffect, researchEffect,
                                                                            moneyPercentChange, prReq, legalReq, researchReq, doomCounter,
                                                                            chance, marketEffect, regulation, optionDescription,EN,RN);
                lineNum = lineNum + 1;
                if (count == events[eventID - 1].Optionsnumber)
                {
                    count = 0;
                }
            }
        }
        foreach (var Event in events)
        {
            if (Event.type == "Environment")
            {
                environmental_events.Add(Event);
            }
            if (Event.type == "Relations")
            {
                relation_events.Add(Event);
            }
        }
        foreach (var LongEvent in long_events)
        {
            if (LongEvent.type == "Environment")
            {
                environmental_long_events.Add(LongEvent);
            }
            if (LongEvent.type == "Relations")
            {
                relation_long_events.Add(LongEvent);
            }
        }

    }
    public Event getEvent()
    {
        return GetLongEvent("Environment");
        Player curPlayer = MasterControl.control.currGame.players1[MasterControl.control.currGame.currentPlayer].GetComponent<Player>();
        foreach (var Event in events){
            if (Random.value < Event.probability){
                return Event;
            }
        }
        if (curPlayer.Environmental_neglect >= 50){
            foreach (var Event in environmental_events)
            {
                if (Random.value < Event.probability)
                {
                    return Event;
                }
            }
        }
        if (curPlayer.Public_neglect >= 50)
        {
            foreach (var Event in relation_events)
            {
                if (Random.value < Event.probability)
                {
                    return Event;
                }
            }
        }
        if (curPlayer.Environmental_neglect >= 100)
        {
            return GetLongEvent("Environment");
        }
        if (curPlayer.Public_neglect >= 100)
        {
            return GetLongEvent("Relations");
        }

        Debug.Log("Event Not Shown This Turn");
        return null;



    }
    public LongEvent GetLongEvent(string type ){
        if(type == "Environment"){
            foreach (var LongEvent in environmental_long_events)
            {
                if (Random.value < LongEvent.probability)
                {
                    return LongEvent;
                }
            }
        }
        if(type == "Relations"){
            foreach (var LongEvent in relation_long_events)
            {
                if (Random.value < LongEvent.probability)
                {
                    Debug.Log("LongEvent Not Shown This Turn");
                    return LongEvent;
                }
            }
        }
        Debug.Log("Event Not Shown This Turn");
        return null;
    }

    //public Event getRandomEvent(List<Event> events)
        //{
        //    //Debug.Log(Random.Range(0, events.Count));
        //    int selectedEvent = Random.Range(0, events.Count * 2);
        //    //Debug.Log(selectedEvent);

        //    if (selectedEvent >= events.Count)
        //    {
        //        Debug.Log("Event Not Shown This Turn");
        //        return null;
        //    }

        //return events[selectedEvent];
        //}
}


/*public class LongEventMaintainence
{
    public List<LongEvent> long_events = new List<LongEvent>();
    public LongEventMaintainence()
    {
        using (var reader = new StreamReader(@"Assets/Scripts/testLongEvents.tsv"))
        {
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split('\t');
                int EventID = int.Parse(values[0]);
                int noofoptions = int.Parse(values[1]);
                int factor = int.Parse(values[2]);
                string desc = values[3];
                LongEvent Event = new LongEvent(EventID,desc, noofoptions, factor);
                long_events.Add(Event);
            }
        }
        using (var reader = new StreamReader(@"Assets/Scripts/testLongEventsoptions.tsv"))
        {
            reader.ReadLine();
            int n = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split('\t');
                int eventID = int.Parse(values[0]);
                int prEffect = int.Parse(values[1]);
                int legalEffect = int.Parse(values[2]);
                int researchEffect = int.Parse(values[3]);
                double moneyPercentChange = double.Parse(values[4]);
                int prReq = int.Parse(values[5]);
                int legalReq = int.Parse(values[6]);
                int researchReq = int.Parse(values[7]);
                int doomCounter = int.Parse(values[8]);
                double chance = double.Parse(values[9]);
                int marketEffect = int.Parse(values[10]);
                int regulation = int.Parse(values[11]);
                int gasreq = int.Parse(values[13]);
                string optionDescription = values[12];

                long_events[eventID - 1].options[n] = new Option(optionDescription, prEffect, legalEffect, researchEffect,
                                                                            moneyPercentChange, prReq, legalReq, researchReq, doomCounter,
                                                                            chance, marketEffect, regulation, optionDescription);
                if (n == long_events[eventID - 1].Optionsnumber){
                    n = 0;
                }
                n++;
            }
        }
              

    }
    

}*/