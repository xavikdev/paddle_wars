using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 200f;
	[Export]
	public float MinY = 100f;
	[Export]
	public float MaxY = 500f;
	private Vector2 _velocity = Vector2.Zero;


	private void GetInput(double delta)
	{
		_velocity = Vector2.Zero;

		if (Input.IsActionPressed("move_left") || Input.IsActionPressed("move_up"))
		{
			_velocity.Y -= 1;
		}

		if (Input.IsActionPressed("move_right") || Input.IsActionPressed("move_down"))
		{
			_velocity.Y += 1;
		}

		_velocity = _velocity.Normalized() * Speed;

		Position += _velocity * (float)delta;
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput(delta);
		MoveAndSlide();

		Position = new Vector2(Position.X, Mathf.Clamp(Position.Y, MinY, MaxY));
	}
}
