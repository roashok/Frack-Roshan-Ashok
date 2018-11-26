using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class Event
{
    public Option[] options;
    public string Event_description;
    public int Optionsnumber = 3;
    public string type;
    public int ID;
    public float probability;
    public Event(){}
    public Event(int eventID,string type_name,string description, int num)
    {
        this.ID = eventID;
        this.type = type_name;
        this.Event_description = description;
        this.Optionsnumber = num;
        options = new Option[num];
    }
    

}
public class LongEvent : Event{
    public int level;
    public  int factor;
    public LongEvent(int eventID,string description, int num,int factor){
        this.ID = eventID;
        this.Event_description = description;
        this.Optionsnumber = num;
        options = new Option[num];
        this.factor = factor;
    }
 
    public void  Escalation(){
        level++;
        for (int i = 0; i <= Optionsnumber; i++){
            options[i].moneyPercentChange += factor;
            options[i].legalEffect += factor;
            options[i].prEffect += factor;
            options[i].gaseffect += factor;
            options[i].prEffect += factor;
            options[i].marketEffect += factor;
        }
    }
    public void Descalation(){
        level--;
        for (int i = 0; i <= Optionsnumber; i++)
        {
            options[i].moneyPercentChange -= factor;
            options[i].legalEffect -= factor;
            options[i].prEffect -= factor;
            options[i].gaseffect -= factor;
            options[i].prEffect -= factor;
            options[i].marketEffect -= factor;
        }
    }

}

public class Option
{
    public int gaseffect;
    public string optionDescription;
    public int prEffect;
    public int legalEffect;
    public int researchEffect;
    public double moneyPercentChange;
    public int prReq;
    public int legalReq;
    public int researchReq;
    public int doomCounter;
    public double chance;
    public int marketEffect;
    public int regulation;
    public string link;
    public int Environmental_Harm;
    public int Relation_Harm;

    public Option(string od, int pre, int le, int re, double mpc,
                   int prr, int lr, int rr, int dc, double c, int me, int r, string l,int neg1 ,int neg2 )
    {
        optionDescription = od;
        prEffect = pre;
        legalEffect = le;
        researchEffect = re;
        moneyPercentChange = mpc;
        prReq = prr;
        legalReq = lr;
        researchReq = rr;
        doomCounter = dc;
        chance = c;
        marketEffect = me;
        regulation = r;
        link = l;
        Environmental_Harm = neg1;
        Relation_Harm = neg2;
    }
}

