using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "CSV Data/Enemy")]

public class CSVEnemy : CSVReader
{
    [SerializeField]
    private string _id;

    [SerializeField]
    private string _enemyName;

    [SerializeField]
    private int _enemyHealth;

    [SerializeField]
    private int _enemyAttack;

    [SerializeField]
    private int _enemySpeed;

    [SerializeField]
    private int _enemyGold;

    [SerializeField]
    private int _enemyExp;

    [SerializeField]
    private string _enemyColour;

    public string ID { get => _id; set => _id = value; }
    public string enemyName { get => _enemyName; set => _enemyName = value; }
    public int enemyHealth { get => _enemyHealth; set => _enemyHealth = value; }
    public int enemyAttack { get => _enemyAttack; set => _enemyAttack = value; }
    public int enemySpeed { get => _enemySpeed; set => _enemySpeed = value; }

    public int enemyGold { get => _enemyGold; set => _enemyGold = value; }
    public int enemyExp { get => _enemyExp; set => _enemyExp = value; }

    public string enemyColour { get => _enemyColour; set => _enemyColour = value; }

    protected override async Task Synchronise()
    {
        var data = await SeekRow(ID);
        enemyName = (data["enemyName"]);
        enemySpeed = int.Parse(data["enemySpeed"]);
        enemyHealth = int.Parse(data["enemyHealth"]);
        enemyAttack = int.Parse(data["enemyAttack"]);
        enemyGold = int.Parse(data["enemyGold"]);
        enemyExp = int.Parse(data["enemyExp"]);
        enemyColour = (data["enemyColour"]);
    }
}
