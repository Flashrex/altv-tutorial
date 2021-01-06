using AltV.Net;
using AltV.Net.Elements.Entities;
using altvtutorial.MyEntitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace altvtutorial.Factories {
    class PlayerFactory : IEntityFactory<IPlayer> {
        public IPlayer Create(IntPtr entityPointer, ushort id) {
            return new MyPlayer(entityPointer, id);
        }
    }
}
