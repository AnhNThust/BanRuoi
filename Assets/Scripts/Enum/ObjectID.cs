namespace Assets.Scripts.Enum
{
	public class ObjectID
	{

	}

	public class BulletID
	{
		// ===== For Player =====
		public const int SHIP1_BULLET = 1;
		public const int SHIP2_BULLET = 2;
		public const int SHIP3_BULLET = 3;
		public const int SHIP4_BULLET = 4;

		public const int SHIP1_BULLET_SPECIAL = 101;
		public const int SHIP2_BULLET_SPECIAL = 102;
		public const int SHIP3_BULLET_SPECIAL = 103;
		public const int SHIP4_BULLET_SPECIAL = 104;

		public const int SHIP2_BULLET_SUPER = 112;
		public const int SHIP4_BULLET_SUPER = 114;

		public const int SHIP4_BULLET_SUPER_POWER = 124;

		public const int LASER_TEST = 125;

		// ===== For Enemy =====
		public const int ENEMY1_BULLET = 21;
		public const int ENEMY2_BULLET = 22;
		public const int ENEMY3_BULLET = 23;
		public const int ENEMY4_BULLET = 24;

		public const int ROCKET_MINI = 25;
		public const int ROCKET = 26;
		public const int FIRE_BALL = 27;
		public const int SHORT_LASER = 28;
		public const int LIGHTNING_1 = 29;
		public const int ENEMY_WAVE_BULLET = 30;
		public const int FIRE_RAY = 200;
		public const int ENEMY_SPECIAL_BULLET_1 = 401;
	}
	public class ItemID
	{
		public const int ITEM_UPGRADE = 10;
		public const int ITEM_POW = 11;
		public const int ITEM_CHANGE_SHIP_1 = 12;
		public const int ITEM_CHANGE_SHIP_2 = 13;
		public const int ITEM_CHANGE_SHIP_3 = 14;
		public const int ITEM_CHANGE_SHIP_4 = 15;
		public const int ITEM_ADD_A_LIFE = 16;
	}

	public class EnemyID
	{
		public const int ENEMY_1 = 31;
		public const int ENEMY_2 = 32;
		public const int ENEMY_3 = 33;
		public const int ENEMY_4 = 34;

		public const int ENEMY_8 = 138;
		public const int ENEMY_9A = 139;
		public const int ENEMY_9B = 140;
		public const int ENEMY_10A = 141;
		public const int ENEMY_10B = 142;
		public const int ENEMY_11A = 143;
		public const int ENEMY_11B = 144;
		public const int ENEMY_12A = 145;
		public const int ENEMY_12B = 146;
	}

	public class EnemyTeamID
	{
		public const int ENEMY_TEAM_1 = 41;
	}

	public class EffectID
	{
		// ======= Explosion =======
		public const int EXPLOSION_1 = 51;
		public const int EXPLOSION_2 = 52;
		public const int EXPLOSION_3 = 53;

		// ====== Explosion Mini =======
		public const int EXPLOSION_3_MINI = 63;

		// ====== Hit ==========
		public const int HIT_01 = 71;

		// ====== Targeting =========
		public const int TARGETING = 80;

		// ====== Open Dimension ======
		public const int LIGHTING_22 = 90;

		// ====== Dimension Gate ======
		public const int GREEN_HOLE = 201;

		// ====== Notification ======
		public const int ALERT_ATTACK = 210;
		public const int ALERT_ATTACK_MINI = 211;
	}
}
