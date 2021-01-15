/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import * as alt from 'alt-client';
import * as native from 'natives';

//const player = alt.Player.local;

alt.onServer('alttutorial:notify', (msg) => {
    const textEntry = `TEXT_ENTRY_${(Math.random() * 1000).toFixed(0)}`;
    alt.addGxtText(textEntry, msg);
    native.beginTextCommandThefeedPost('STRING');
    native.addTextComponentSubstringTextLabel(textEntry);
    native.endTextCommandThefeedPostTicker(false, false);

    //alt.emitServer('eventname', params);
});

/*alt.onServer('alttutorial:fixveh', () => {
    if(player.vehicle) {
        native.setVehicleFixed(player.vehicle.scriptID);
    }
})*/
