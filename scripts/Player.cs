using Godot;
using System;

public partial class Player : Area2D, IHasScore
{
	[Export]
	private int moveSpeed = 200;
	[Export]
	public Label ScoreDisplay { get; set; }
	public AudioStreamPlayer ScoreSound { get; set; }
	public int Score { get; set; }

	public override void _Ready()
	{
		ScoreSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	}

    public override void _PhysicsProcess(double delta)
    {
		var input = 0f;
		if (Input.IsActionPressed("move_down") || Input.IsActionPressed("move_right"))
		{
			input = 1f;
		}

		if (Input.IsActionPressed("move_up") || Input.IsActionPressed("move_left"))
		{
			input = -1f;
		}

		var position = Position;
		position += new Vector2(0, (float)(input * moveSpeed * delta));
		position.Y = Mathf.Clamp(position.Y, 16, GetViewportRect().Size.Y - 16);
		Position = position;
    }

	private void OnAreaEntered(Area2D area)
	{
		if (area is Ball ball)
		{
			var direction = new Vector2(Vector2.Right.X, (float)Random.Shared.NextDouble() * 2 - 1).Normalized();
			ball.Bounce(direction);
		}
	}
}
