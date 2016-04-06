using UnityEngine;
using System.Collections;

public class K
{
    // ===== LAYERS =====
    public const int LAYER_GROUND = 8;
    public const int LAYER_MNINIMAP_PLAYER_CAR = 9;
    public const int LAYER_PLAYER = 10;

    // ===== Ground Check =====
    public const float IS_GROUNDED_RAYCAST_DISTANCE = 2;

    // ===== JEEP CONFIG =====
    public const float JEEP_MAX_SPEED = 100;
    public const float JEEP_MIN_SPEED = 0;
    public const float JEEP_ACCELERATION_RATE = 10;
    public const float JEEP_DECELERATION_RATE = 50;
    public const float JEEP_MAX_STEERING_ANGLE = 30;
    public const float JEEP_STEER_SPEED = 0.8f;
    public const float JEEP_BRAKE = 1.5f;

    public const float GRAVITY = 9.8f;
}
