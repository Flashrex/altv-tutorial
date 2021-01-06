using AltV.Net;
using AltV.Net.Elements.Entities;
using altvtutorial.Database;
using altvtutorial.Factories;

namespace altvtutorial {
    class ServerHandler : Resource {
        public override void OnStart() {
            Alt.Log(">> Server gestartet <<");
            new MyDatabase();
        }

        public override void OnStop() {
        }

        public override IEntityFactory<IVehicle> GetVehicleFactory() {
            return new VehicleFactory();
        }

        public override IEntityFactory<IPlayer> GetPlayerFactory() {
            return new PlayerFactory();
        }
    }
}
