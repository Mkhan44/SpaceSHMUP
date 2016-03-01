using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float speed = 10f; //The speed in m/s
	public float fireRate = 0.3f; // Seconds/shot (unused)
	public float health = 10;
	public int score = 100; //Points earned for destroying this enemy.

	public int showDamageForFrames = 2; //# Frames to show damage.

	public bool ___________;

	public Color[] originalColors;
	public Material[] materials; //All the materials of this & its children.
	public int remainingDamageFrames = 0; //Damage frames left.


	public Bounds bounds; //The bounds of this and its children.
	public Vector3 boundsCenterOffset; // Dist of bounds.center from position.


	void Awake() {
		materials = Utils.GetAllMaterials (gameObject);
		originalColors = new Color[materials.Length];
		for (int i=0; i<materials.Length; i++) {
			originalColors[i] = materials[i].color;
		}
		InvokeRepeating ("CheckOffscreen", 0f, 2f);
	}

	// Update is called once per frame
	void Update () {
		Move ();
		if (remainingDamageFrames > 0) {
			remainingDamageFrames--;
			if(remainingDamageFrames == 0 ) {
				UnShowDamage();
			}
		}
	}

	public virtual void Move() {
		Vector3 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime;
		pos = tempPos;
	}

	//This is a property: A method that acts like a field.
	public Vector3 pos {
		get{
			return (this.transform.position);
		}
		set {
			this.transform.position = value;
		}
	}

	void CheckOffscreen() {
		//If bounds are still their default value...
		if (bounds.size == Vector3.zero) {
		//Then set them.
			bounds = Utils.CombineBoundsOfChildren(this.gameObject);
			//Also find the diff between bounds.center & transform.position.
			boundsCenterOffset = bounds.center - transform.position;
		}

		//Everytime, update the bounds to the current position.
		bounds.center = transform.position + boundsCenterOffset;
		//Check to see whether the bounds are completely offscreen.
		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen);
		if (off != Vector3.zero) {
		//If this enemy has gone off the bottom edge of the screen.
			if (off.y < 0) {
			//Then destroy it.
				Destroy (this.gameObject);
			}
		}
	}

	void OnCollisionEnter(Collision coll) {
		GameObject other = coll.gameObject;
		switch(other.tag) {
		case "ProjectileHero":
				Projectile p = other.GetComponent<Projectile>();
			//Enemies don't take damage unless they're on screen.
			//This stops the player from shooting them before they are visible.
			bounds.center = transform.position + boundsCenterOffset;
			if(bounds.extents == Vector3.zero || Utils.ScreenBoundsCheck(bounds, BoundsTest.offScreen) != Vector3.zero) {
				Destroy (other);
				break;
			}
			//Hurt this enemy.
			ShowDamage();
			//Get the damage amount from the Projectile.type & Main.W_DEFS
			health -= Main.W_DEFS[p.Types].damageOnHit;
			if(health <=0) {
			//Destroy this enemy.
				Destroy (this.gameObject);
			}
			Destroy (other);
			break;
		}
	}

	void ShowDamage() {
	foreach (Material m in materials) {
			m.color = Color.red;
		}
		remainingDamageFrames = showDamageForFrames;
	}

	void UnShowDamage() {
		for (int i=0; i<materials.Length; i++) {
			materials[i].color = originalColors[i];
		}
	}

}
