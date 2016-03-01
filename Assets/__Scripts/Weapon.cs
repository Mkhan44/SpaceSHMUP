using UnityEngine;
using System.Collections;

//This is an enum of the various possibl eweapon types.
//It also includes a "shield" type to allow a shield power-up.
//Items marked [NI] below are Not Implemented in this book
public enum WeaponType {
	none, //The default/ no weapon.
	blaster, //A simple blaster
	spread, //Two shots simultanesously.
	phaser, //Shots that move in waves [NI]
	missle, //Homing missles [NI]
	laser, //Damage ower time [NI]
	shield //Raise shieldLevel
}

//The WeaponDefinition class allows you to se the properties of a sepcific weapon in the Inspector.
//Main has an array of WeaponDefinitions that makes this possible.
// [System.Serializable] tells Unity to try to view WeaponDefinition
//In the inspector pane. It doesn't work for everything, but it will work for simple classes like this.

[System.Serializable]
public class WeaponDefinition {
	public WeaponType type = WeaponType.none;
	public string letter; //The letter to show on the power-up.
	public Color color = Color.white; //Color of collar & power-up.
	public GameObject projectilePrefab; //Prefab for projectiles.
	public Color projectileColor = Color.white;
	public float damageOnHit = 0; //Amount of damage caused.
	public float continuousDamage = 0; //Damage per second (Laser).
	public float delayBetweenShots = 0;
	public float velocity = 20; //SPeed of projectiles.
}

//Note: weapon prefabs, colors, and so on are set in the class Main.

public class Weapon : MonoBehaviour {
	//The weapon class will be filled in later.

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
