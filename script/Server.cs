using Godot;
using System;

public partial class Server : Node
{
    
    public override void _Ready()
    {
        with_multiplayer_api();
    }

    public void with_multiplayer_api()
    {
        var server = new ENetConnection();
        var err = server.CreateHost(2);
        if (err != Error.Ok)
        {
            GD.Print("Unable to start server");
            SetProcess(false);
            return;
        }
        Callable playerConnectedMethod = new Callable(this, "_player_connected");
        Callable playerDisconnectedMethod = new Callable(this, "_player_disconnected");

        // Utilisation de Callable pour connecter les méthodes
        GetTree().Connect("network_peer_connected", playerConnectedMethod);
        GetTree().Connect("network_peer_disconnected", playerDisconnectedMethod);
        GD.Print("Server created");
    }

    public void _player_connected(int id)
    {
        GD.Print("Player connected: ", id);
    }

    public void _player_disconnected(int id)
    {
        GD.Print("Player disconnected: ", id);
    }
}
