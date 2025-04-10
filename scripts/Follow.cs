using Godot;

namespace CameraJitter;

public partial class Follow : Node3D
{
    [Export] private Node3D _target;

    public override void _Ready()
    {
        SetPhysicsInterpolationMode(PhysicsInterpolationModeEnum.Off);
    }

    public override void _Process(double delta)
    {
        GlobalPosition = _target.GetGlobalTransformInterpolated().Origin;
        GlobalRotation = _target.GlobalRotation;
    }
}