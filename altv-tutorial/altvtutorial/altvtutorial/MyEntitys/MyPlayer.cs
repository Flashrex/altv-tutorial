using AltV.Net.Elements.Entities;
using altvtutorial.Database;
using System;

namespace altvtutorial.MyEntitys {
    class MyPlayer : Player{

        public bool IsLoggedIn { get; set; }
        public int Db_Id { get; set; }
        public string DisplayName { get; set; }
        public uint Cash { get; set; }

        public MyPlayer(IntPtr nativePointer, ushort id) : base(nativePointer, id) {
            IsLoggedIn = false;
            Cash = 500;
        }

        public void Login() {
            IsLoggedIn = true;
        }

        public void SendNotification(string msg) {
            Emit("alttutorial:notify", msg);
        }

        public void CreatePlayer(string username, string password) {
            DisplayName = username;

            Db_Id = PlayerDatabase.CreatePlayer(DisplayName, password);
            Login();
        }

        public void LoadPlayer(string username) {
            DisplayName = username;
            PlayerDatabase.LoadPlayer(this);
            Login();
        }

        public void Save() {
            PlayerDatabase.UpdatePlayer(this);
        }
    }
}
