using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerTree : MonoBehaviour
{
    [SerializeField]
    GameObject mine;
    [SerializeField]
    GameObject deposit;
    NSelector minerSelector;
    NSecuence toTheMineSecuence;
    NSecuence miningSecuence;
    NSecuence toDepositSecuence;
    NSecuence depositSecuence;
    NSecuence lastTripSecuence;
    NDecorator notMine;
    NDecorator notFull;
    NDecorator notOnMine;
    NDecorator notOnDeposit;
    ActionNode toMine;
    ActionNode toDeposit;
    ActionNode mining;
    ActionNode deposited;
    ActionNode onMine;
    ActionNode onDeposit;
    ActionNode isMine;
    ActionNode full;
    ActionNode hasMinerals;
    List<BNode> selectorList;
    List<BNode> toMineList;
    List<BNode> toDepositList;
    List<BNode> miningList;
    List<BNode> depositList;
    List<BNode> lastTripList;
    int minedOre;
    const int FULLMINEDORE = 10;
    void Start()
    {
        toMine = new ActionNode(ToMine);
        toDeposit = new ActionNode(ToDeposit);
        mining = new ActionNode(Mining);
        deposited = new ActionNode(Deposit);
        onMine = new ActionNode(OnMine);
        onDeposit = new ActionNode(OnDeposit);
        isMine = new ActionNode(IsMine);
        full = new ActionNode(Full);
        hasMinerals = new ActionNode(HasMinerals);
        notMine = new NDecorator(isMine);
        notFull = new NDecorator(full);
        notOnMine = new NDecorator(onMine);
        notOnDeposit = new NDecorator(onDeposit);
        toMineList = new List<BNode>();
        toMineList.Add(isMine);
        toMineList.Add(notOnMine);
        toMineList.Add(notFull);
        toMineList.Add(toMine);
        toDepositList = new List<BNode>();
        toDepositList.Add(full);
        toDepositList.Add(notOnDeposit);
        toDepositList.Add(toDeposit);
        miningList = new List<BNode>();
        miningList.Add(isMine);
        miningList.Add(onMine);
        miningList.Add(notFull);
        miningList.Add(mining);
        depositList = new List<BNode>();
        depositList.Add(hasMinerals);
        depositList.Add(onDeposit);
        depositList.Add(deposited);
        lastTripList = new List<BNode>();
        lastTripList.Add(notMine);
        lastTripList.Add(hasMinerals);
        lastTripList.Add(notOnDeposit);
        lastTripList.Add(toDeposit);
        toTheMineSecuence = new NSecuence(toMineList);
        miningSecuence = new NSecuence(miningList);
        toDepositSecuence = new NSecuence(toDepositList);
        depositSecuence = new NSecuence(depositList);
        lastTripSecuence = new NSecuence(lastTripList);
        selectorList = new List<BNode>();
        selectorList.Add(toTheMineSecuence);
        selectorList.Add(miningSecuence);
        selectorList.Add(toDepositSecuence);
        selectorList.Add(depositSecuence);
        selectorList.Add(lastTripSecuence);
        minerSelector = new NSelector(selectorList);

    }

    void Update()
    {
        minerSelector.Evaluate();
    }

    public int OnMine()
    {
        if (this.transform.position == mine.gameObject.transform.position)
        {
            return 1;
        }
        return 0;
    }

    public int OnDeposit()
    {
        if (this.transform.position == deposit.gameObject.transform.position)
        {
            return 1;
        }
        return 0;
    }

    public int Full()
    {
        if (minedOre >= FULLMINEDORE)
        {
            return 1;
        }
        return 0;
    }

    public int HasMinerals()
    {
        if (minedOre > 0)
        {
            return 1;
        }
        return 0;
    }

    public int IsMine()
    {
        if (mine.gameObject.GetComponent<MeshRenderer>().enabled)
        {
            return 1;
        }
        return 0;
    }

    public int ToMine()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, mine.gameObject.transform.position, 0.05f);
        return 1;
    }

    public int ToDeposit()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, deposit.gameObject.transform.position, 0.05f);
        return 1;
    }

    public int Mining()
    {
        minedOre++;
        mine.GetComponent<MineMaquinita>().SetMinerals(mine.GetComponent<MineMaquinita>().GetMinerals() - 1);
        return 1;
    }

    public int Deposit()
    {
        minedOre = 0;
        return 1;
    }
}
