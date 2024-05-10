using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Estado del jugador
public enum PlayerState
{
    Playing,
    Waiting,
    GameOver
}

// Clase que representa el estado del juego
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

    // Metodo para guardar el estado del juego
    public Memento Save()
    {
        return new Memento(Player, Score, Level, Ship, Lifes);
    }

    // Metodo para restaurar el estado del juego
    public void Restore(Memento memento)
    {
        Player = memento.Player;
        Score = memento.Score;
        Level = memento.Level;
        Ship = memento.Ship;
        Lifes = memento.Lifes;
    }
}

// Memento para guardar el estado del juego
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

// Contexto que utiliza los estados del jugador
public class PlayerContext
{
    private PlayerState state;
    private GameState gameState;
    private Memento memento;
    private static bool start = false;

    public PlayerContext(PlayerState initialState, string player, int score, int level, int ship, float lifes)
    {
        state = initialState;
        gameState = new GameState(player, score, level, ship, lifes);
    }

    public void SavePlayerState(int score, int level)
    {
        state = PlayerState.Waiting;
        gameState.Lifes -= 1;
        gameState.Score = score;
        gameState.Level = level;

        // Guardar el estado del juego
        memento = gameState.Save();
        // Logica para pasar el turno al otro jugador
    }

    public void RestorePlayerState()
    {
        state = PlayerState.Playing;
        // Restaurar el estado del juego
        gameState.Restore(memento);
        Debug.Log(gameState.Player);
        Debug.Log(gameState.Score);
        Debug.Log(gameState.Level);
        Debug.Log(gameState.Ship);
        Debug.Log(gameState.Lifes);
        // Logica para iniciar el turno del otro jugador
    }

    public void GameOver()
    {
        state = PlayerState.GameOver;
        // Logica para finalizar el juego
    }
}
