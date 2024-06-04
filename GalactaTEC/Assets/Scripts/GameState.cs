using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player status
public enum PlayerState
{
    Playing,
    Waiting,
    GameOver
}

// Class that represents the state of the game
public class GameState
{
    private string player;
    private int score;
    private int level;
    private int ship;
    private float lifes;

    public GameState(string player, int score, int level, int ship, float lifes)
    {
        Player = player;
        Score = score;
        Level = level;
        Ship = ship;
        Lifes = lifes;
    }

    public string Player
    {
        get { return player; }
        set { player = value; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public int Ship
    {
        get { return ship; }
        set { ship = value; }
    }

    public float Lifes
    {
        get { return lifes; }
        set { lifes = value; }
    }

    // Method to save game state
    public Memento save()
    {
        return new Memento(Player, Score, Level, Ship, Lifes);
    }

    // Method to restore game state
    public void restore(Memento memento)
    {
        Player = memento.player;
        Score = memento.score;
        Level = memento.level;
        Ship = memento.ship;
        Lifes = memento.lifes;
    }
}

// Class to save game state
public class Memento
{
    public readonly string player;
    public readonly int score;
    public readonly int level;
    public readonly int ship;
    public readonly float lifes;

    public Memento(string player, int score, int level, int ship, float lifes)
    {
        this.player = player;
        this.score = score;
        this.level = level;
        this.ship = ship;
        this.lifes = lifes;
    }
}

// Context that uses player states
public class PlayerContext
{
    private PlayerState state;
    private GameState gameState;
    private Memento memento;
    private bool enable;

    public PlayerContext(PlayerState initialState, string player, int score, int level, int ship, float lifes)
    {
        state = initialState;
        gameState = new GameState(player, score, level, ship, lifes);
        enable = true;
    }

    public string getPlayer()
    {
        return gameState.Player;
    }

    public int getScore()
    {
        return gameState.Score;
    }

    public int getLevel()
    {
        return gameState.Level;
    }

    public float getLifes()
    {
        return gameState.Lifes;
    }

    public void setEnable(bool enable)
    {
        this.enable = enable;
    }

    public bool isEnable()
    {
        return enable;
    }

    public void saveInitPlayerState()
    {
        memento = gameState.save();
    }

    public void savePlayerState(int score, int level, float lifes)
    {
        state = PlayerState.Waiting;
        gameState.Score = score;
        gameState.Level = level;
        gameState.Lifes = lifes;

        // Save game state
        memento = gameState.save();
    }

    public void restorePlayerState()
    {
        state = PlayerState.Playing;
        gameState.restore(memento);
        //Debug.Log("Player: " + gameState.Player);
        //Debug.Log("Score: " + gameState.Score);
        //Debug.Log("Level: " + gameState.Level);
        //Debug.Log("Ship: " + gameState.Ship);
        //Debug.Log("Lifes: " + gameState.Lifes);
    }

    public void gameOver()
    {
        state = PlayerState.GameOver;
        // Logic to end the game
    }
}
