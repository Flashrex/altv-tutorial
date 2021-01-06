using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using System;
using static altvtutorial.Enums;

namespace altvtutorial.MyEntitys {
    class MyVehicle : Vehicle {

        public static float MAX_FUEL = 60.0f;

        public FuelTypes FuelType { get; set; }
        public float Fuel { get; set; }

        public MyVehicle(IntPtr nativePointer, ushort id) : base(nativePointer, id) {

        }

        public MyVehicle(uint model, Position position, Rotation rotation, FuelTypes fueltype = FuelTypes.None) : base(model, position, rotation) {
            FuelType = fueltype;
            Fuel = 0;
            ManualEngineControl = true;
        }

        /*public void Repair() {
            if(NetworkOwner != null) {
                Fuel = MAX_FUEL;
                NetworkOwner.Emit("alttutorial:fixveh");
                PlayerEvents.SendNotification(NetworkOwner, "Fahrzeug repariert!");
            }
        }*/

        public void ToggleEngine() {
            if(!EngineOn && FuelType != FuelTypes.None && Fuel == 0) {
                MyPlayer player = (MyPlayer)NetworkOwner;
                player.SendNotification("Tank leer!");
                return;
            }
            EngineOn = !EngineOn;
        }
    }
}