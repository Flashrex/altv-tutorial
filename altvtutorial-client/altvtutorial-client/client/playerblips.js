/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import * as alt from 'alt-client';
import * as native from 'natives';

alt.on('gameEntityCreate', (entity) => {
    if(entity.hasStreamSyncedMeta("altvtutorial:teamcolor")) {
        let color = entity.getStreamSyncedMeta("altvtutorial:teamcolor");
        let blip = native.getBlipFromEntity(entity.scriptID);
        if(blip === 0) blip = native.addBlipForEntity(entity.scriptID);
        native.setBlipColour(blip, isNaN(color) ? 0 : color);
        native.setBlipCategory(blip, 7);
        native.showHeadingIndicatorOnBlip(blip, true);
    }
})

alt.on('gameEntityDestroy', (entity) => {
    if(entity.hasStreamSyncedMeta("altvtutorial:teamcolor")) {
        let blip = native.getBlipFromEntity(entity.scriptID);
        if(blip !== 0) native.removeBlip(blip);
    }
})

alt.on('streamSyncedMetaChange', (entity, key, value, oldvalue) => {
    if(key === 'altvtutorial:teamcolor') {
        let color = entity.getStreamSyncedMeta("altvtutorial:teamcolor");
        let blip = native.getBlipFromEntity(entity.scriptID);
        if(blip === 0) blip = native.addBlipForEntity(entity.scriptID);
        native.setBlipColour(blip, isNaN(color) ? 0 : color);
        native.setBlipCategory(blip, 7);
        native.showHeadingIndicatorOnBlip(blip, true);
    }
})