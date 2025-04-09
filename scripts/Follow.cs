using Godot;

namespace CameraJitter;

public partial class Follow : Node3D
{
    [Export] private Node3D _target;

    public override void _Process(double delta)
    {
        GlobalPosition = _target.GlobalPosition;
        GlobalRotation = _target.GlobalRotation;
    }
}