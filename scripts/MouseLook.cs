using Godot;
using System;

public partial class MouseLook : Node3D
{
    [Export] private float _sensitivity = 30f;

    private Vector2 _motion;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion e)
        {
            _motion += e.Relative;
        }
    }

    public override void _Process(double delta)
    {
        if (Input.MouseMode != Input.MouseModeEnum.Captured) return;
        RotateY(-Mathf.DegToRad(_motion.X) * (float)delta * _sensitivity);
        RotateObjectLocal(-Vector3.Left, -Mathf.DegToRad(_motion.Y) * (float)delta * _sensitivity);
        _motion = Vector2.Zero;
        // LookAt(target.GlobalPosition, Vector3.Up);
    }
}