/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import * as alt from 'alt-client';
import * as native from 'natives';

const player = alt.Player.local;
const atm_hashes = ["prop_atm_01", "prop_atm_02", "prop_atm_03", "prop_fleeca_atm"];

alt.on('keydown', (key) => {
    if(key === 69) { //e-Key
        for(let i = 0; i < atm_hashes.length; i++) {
            var object = native.getClosestObjectOfType(player.pos.x, player.pos.y, player.pos.z, 2.0, alt.hash(atm_hashes[i]), false, false, false);
            if(object) {
                alt.log('Spieler ist am ATM');
            }
        }
    }
})