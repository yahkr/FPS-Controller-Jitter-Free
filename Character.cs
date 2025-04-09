using Godot;
using System;
using System.Threading.Tasks;

public partial class Character : CharacterBody3D
{
    [Export]
    private Node3D _head;
    [Export]
    private float _speed = 200f;
    
    private Vector3 _input;
    private Vector2 _velocity = Vector2.Zero;
    
    public override void _Process(double delta)
    {
        _input = Vector3.Zero;
        if (Input.IsActionPressed("player_forward"))
        {
            _input -= Basis.Z;
        }

        if (Input.IsActionPressed("player_backward"))
        {
            _input += Basis.Z;
        }

        if (Input.IsActionPressed("player_left"))
        {
            _input -= Basis.X;
        }
        
        if (Input.IsActionPressed("player_right"))
        {
            _input += Basis.X;
        }
        
        if (Input.IsActionPressed("quit"))
        {
            GetTree().Quit();
        }        
        _input = _input.Normalized().Rotated(Vector3.Up,_head.Rotation.Y);
    }
    
    public override void _PhysicsProcess(double delta)
    {
        Velocity = _input * _speed * (float)delta;
        MoveAndSlide();
        _input = Vector3.Zero;
    }
}
