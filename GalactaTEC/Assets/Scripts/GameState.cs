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
    public Memento save()
    {
        return new Memento(Player, Score, Level, Ship, Lifes);
    }

    // Metodo para restaurar el estado del juego
    public void restore(Memento memento)
    {
        Player = memento.player;
        Score = memento.score;
        Level = memento.level;
        Ship = memento.ship;
        Lifes = memento.lifes;
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

    public PlayerContext(PlayerState initialState, string player, int score, int level, int ship, float lifes)
    {
        state = initialState;
        gameState = new GameState(player, score, level, ship, lifes);
    }

    public string getPlayer()
    {
        return gameState.Player;
    }

    public int getScore()
    {
        return gameState.Score;
    }

    public void saveInitPlayerState()
    {
        memento = gameState.save();
    }

    public void savePlayerState(int score, int level)
    {
        state = PlayerState.Waiting;
        gameState.Lifes -= 1;
        gameState.Score = score;
        gameState.Level = level;

        // Guardar el estado del juego
        memento = gameState.save();
        // Logica para pasar el turno al otro jugador
    }

    public void restorePlayerState()
    {
        state = PlayerState.Playing;
        // Restaurar el estado del juego
        gameState.restore(memento);
        //Debug.Log("Player: " + gameState.Player);
        //Debug.Log("Score: " + gameState.Score);
        //Debug.Log("Level: " + gameState.Level);
        //Debug.Log("Ship: " + gameState.Ship);
        //Debug.Log("Lifes: " + gameState.Lifes);
        // Logica para iniciar el turno del otro jugador
    }

    public void gameOver()
    {
        state = PlayerState.GameOver;
        // Logica para finalizar el juego
    }
}
