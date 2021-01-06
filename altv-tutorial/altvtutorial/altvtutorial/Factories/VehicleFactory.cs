using AltV.Net;
using AltV.Net.Elements.Entities;
using altvtutorial.MyEntitys;
using System;

namespace altvtutorial.Factories {
    class VehicleFactory : IEntityFactory<IVehicle> {
        public IVehicle Create(IntPtr entityPointer, ushort id) {
            return new MyVehicle(entityPointer, id);
        }
    }
}
