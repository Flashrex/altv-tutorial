using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Enums;
using altvtutorial.Database;
using altvtutorial.MyEntitys;
using System.Text.RegularExpressions;

namespace altvtutorial {
    class PlayerEvents : IScript {

        //[ScriptEvent(ScriptEventType.EventName)] -> ServerEvent
        //Alt.Emit() -> Server to Server

        //Alt.EmitAllClient() -> Server to all Clients
        //player.Emit() -> Server to Client
        //[ClientEvent("eventname")] -> Client to Server 

        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public void OnPlayerConnect(MyPlayer player, string reason) {

            player.Model = (uint)PedModel.Azteca01GMY;
            player.Spawn(new Position(0, 0, 75), 0);
            //Alt.Emit("eventname");

            /*if (PlayerDatabase.DoesPlayerNameExists(player.Name)) {
                player.LoadPlayer(player.Name);
            } else {
                player.CreatePlayer(player.Name, "1234");
            }

            player.SendNotification($"Cash: ~b~{player.Cash}$");*/
        }

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public void OnPlayerDisconnect(MyPlayer player, string reason) {
            if (player.IsLoggedIn) player.Save();
        }

        [ScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(MyVehicle vehicle, MyPlayer player, byte seat) {
            vehicle.SecondaryColorRgb = new Rgba(255, 0, 0, 255);
            player.SendNotification("Fahrzeug betreten!");
            vehicle.RadioStation = (uint)RadioStation.FlyloFm;
        }

        [ScriptEvent(ScriptEventType.PlayerLeaveVehicle)]
        public void OnPlayerLeaveVehicle(MyVehicle vehicle, MyPlayer player, byte seat) {
            vehicle.SecondaryColorRgb = new Rgba(255, 255, 255, 255);
            player.SendNotification("Fahrzeug verlassen!");
        }

        //[ServerEvent("eventname")]

        [ClientEvent("alttutorial:loginAttempt")]
        public void OnLoginAttempt(MyPlayer player, string username, string password) {
            if (player.IsLoggedIn || username.Length < 4 || password.Length < 4) return;

            //Vorname_Nachname
            Regex regex = new Regex(@"([a-zA-Z]+)_([a-zA-Z]+)");

            if(!regex.IsMatch(username)) {
                player.Emit("alttutorial:loginError", 1, "Der Name muss dem Format: Vorname_Nachname entsprechen.");
                return;
            }

            if(!PlayerDatabase.DoesPlayerNameExists(username)) {
                player.Emit("alttutorial:loginError", 1, "Der Name ist nicht vergeben!");
                return;
            }

            if(PlayerDatabase.CheckLoginDetails(username, password)) {
                //Passwort ist korrekt
                player.LoadPlayer(username);
                player.Spawn(new Position(0, 0, 72), 0);
                player.Emit("alttutorial:loginSuccess");
                player.SendNotification("Erfolgreich eingeloggt!");
            
            } else {
                //Passwort ist nicht korrekt
                player.Emit("alttutorial:loginError", 1, "Login Daten stimmen nicht überein!");

                int attempts = 1;
                if(player.HasData("alttutorial:loginattempts")) {
                    player.GetData("alttutorial:loginattempts", out attempts);

                    if (attempts == 2) player.Kick("Zu viele Loginversuche.");
                    else attempts++;

                }
                player.SetData("alttutorial:loginattempts", attempts);
            }


        }

        [ClientEvent("alttutorial:registerAttempt")]
        public void OnRegisterAttempt(MyPlayer player, string username, string password) {
            if (player.IsLoggedIn || username.Length < 4 || password.Length < 4) return;

            //Vorname_Nachname
            Regex regex = new Regex(@"([a-zA-Z]+)_([a-zA-Z]+)");

            if (!regex.IsMatch(username)) {
                player.Emit("alttutorial:loginError", 1, "Der Name muss dem Format: Vorname_Nachname entsprechen.");
                return;
            }

            if(PlayerDatabase.DoesPlayerNameExists(username)) {
                player.Emit("alttutorial:loginError", 2, "Name ist bereits vergeben!");
            
            } else {
                player.CreatePlayer(username, password);
                player.Spawn(new Position(0, 0, 72), 0);
                player.Emit("alttutorial:loginSuccess");
            }
        }
    }
}
