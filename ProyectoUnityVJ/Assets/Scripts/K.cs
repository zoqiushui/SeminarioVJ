using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class K
{
    // ===== INPUT =====
    public const string INPUT_HORIZONTAL = "Horizontal";
    public const string INPUT_VERTICAL = "Vertical";
    public const string INPUT_HANDBRAKE = "Handbrake";
    public const string INPUT_NITRO = "Nitro";

    // ===== MESSAGES =====
    public const string OBS_MESSAGE_DESTROYED = "Destroyed";
    public const string OBS_MESSAGE_FINISHED = "Finished";
    public const string OBS_MESSAGE_SPEED = "Speed";
    public const string OBS_MESSAGE_LAPCOUNT = "Laps";

    // ===== LAYERS =====
    public const int LAYER_GROUND = 8;
    public const int LAYER_MNINIMAP_PLAYER_CAR = 9;
    public const int LAYER_PLAYER = 10;
    public const int LAYER_NODE = 11;
    public const int LAYER_IA = 12;
    public const int LAYER_ENEMY = 13;
    public const int LAYER_MISSILE = 14;
    public const int LAYER_CHECKPOINT = 15;
    public const int LAYER_OBSTACLE = 16;
    public const int LAYER_RAMP = 17;
    public const int LAYER_SIDEGROUND = 18;
    public const int LAYER_DESTRUCTIBLE= 19;

    // ===== TAG =====
    public const string TAG_PLAYER = "Player";
    public const string TAG_MANAGERS = "Managers";
    public const string TAG_VEHICLES = "Vehicles";
    public const string TAG_CHECKPOINTS = "Checkpoints";

    // ===== GROUND CHECK =====
    public const float IS_GROUNDED_RAYCAST_DISTANCE = 2;

    // ===== JEEP CONFIG =====
    public const float JEEP_MAX_SPEED = 100;
    public const float JEEP_MIN_SPEED = 0;
    public const float JEEP_ACCELERATION_RATE = 10;
    public const float JEEP_DECELERATION_RATE = 50;
    public const float JEEP_MAX_STEERING_ANGLE = 30;
    public const float JEEP_STEER_SPEED = 0.8f;
    public const float JEEP_BRAKE = 1.5f;
    public const float JEEP_ROTATION_FORCE = 10000;

    // ===== UI =====
    public const float SPEEDOMETER_MAX_ANGLE = -150;
    public const float SPEEDOMETER_MIN_ANGLE = -20;
    public const float SPEEDOMETER_MAX_SPEED = 200;

    // ===== IA =====
    public const float IA_MAX_HP = 100;
    public const float IA_MAX_SPEED = JEEP_MAX_SPEED - 30;
    public const float IA_TURN_SPEED = 5;
    public const float IA_FALLFORCE = 30000f;

    // ===== MINIMAP =====
    public const float MINIMAP_HEIGHT = 50;

    // ===== GAME PRESETS =====
    public const int MAX_LAPS = 3;

    // ===== SCENE CONTAINERS =====
    public const string CONTAINER_VEHICLES_NAME = "VEHICLES";
    public const string CONTAINER_CHECKPOINTS_NAME = "CHECKPOINTS";

    public const float GRAVITY = 9.8f;

    // ====== SOUNDS IDS =======
    public const int SOUND_MACHINE_GUN = 0;
    public const int SOUND_MINE_EXPLOSION = 1;
    public const int SOUND_CAR_DESTROY = 2;
    public const int SOUND_MISSILE_HEAVY = 3;
    public const int SOUND_MISSILE = 4;
    public const int SOUND_MOLOTOV_LAUNCH = 5;
    public const int SOUND_MISIL_LAUNCH = 6;

    // ======  PORTRAIT =======
    public static Color[] arrayColorHair = new Color[] { new Color(0.10980392156f, 0.07843137254f, 0.04705882352f), new Color(1, 0.90196078431f, 0.4862745098f), new Color(0.30196078431f, 0.26666666666f, 0.20784313725f) };
    public static Color[] arrayColorSkin = new Color[] { new Color(0.94901960784f, 0.90980392156f, 0.85098039215f), new Color(0.86666666666f, 0.71764705882f, 0.49019607843f), new Color(0.43529411764f, 0.26274509803f, 0.0431372549f) };
    //Sprites
    public static Sprite[] spritesFace=Resources.LoadAll<Sprite>("Sprites/Face");
    public static Sprite[] spritesHair = Resources.LoadAll<Sprite>("Sprites/Hair");
    public static Sprite[] spritesAccesory = Resources.LoadAll<Sprite>("Sprites/Accesory");
    public static Sprite[] spritesFacialHair = Resources.LoadAll<Sprite>("Sprites/FacialHair");
    public static Sprite[] spritesDamage = Resources.LoadAll<Sprite>("Sprites/Damage");
    public static Sprite[] spritesFlag = Resources.LoadAll<Sprite>("Sprites/Flags");
    //Names
    public static List<string> names = new List<string>();

    public const float KPH_TO_MPS_MULTIPLIER = 3.6f; // Kilometros por hora a metros por segundo
    public const float TRAIL_WHEEL_START_SPEED = 50f; // Velocidad de inicio de trail
    public const float MIN_FORCE_MULTIPLIER = .155f;

    // ======  PILOT =======
    public static bool pilotIsAlive=true;
}

