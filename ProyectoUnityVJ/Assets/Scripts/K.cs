using UnityEngine;
using System.Collections;

public class K
{
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

    // ===== TAG =====
    public const string TAG_PLAYER = "Player";

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
    public const float SPEEDOMETER_MAX_ANGLE = -270;
    public const float SPEEDOMETER_MIN_ANGLE = 45;
    public const float SPEEDOMETER_MAX_SPEED = 260;

    // ===== IA =====
    public const float IA_MAX_HP = 100;
    public const float IA_MAX_SPEED = JEEP_MAX_SPEED - 30;
    public const float IA_TURN_SPEED = 5;
    public const float IA_FALLFORCE = 30000f;

    // ===== MINIMAP =====
    public const string MNINIMAP_VEHICLES_CONTAINER_NAME = "VEHICLES";
    public const float MINIMAP_HEIGHT = 50;

    // ===== GAME PRESETS =====
    public const int MAX_LAPS = 3;


    public const float GRAVITY = 9.8f;
}
