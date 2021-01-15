/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import * as alt from 'alt-client';
import * as native from 'natives';

let loginView;

alt.on('connectionComplete', () => {
    loginView = new alt.WebView("http://resource/client/login/index.html");
    loginView.focus();

    alt.showCursor(true);
    alt.toggleGameControls(false);
    native.doScreenFadeOut(0);

    loginView.on('alttutorial:login', (name, password) => {
        alt.emitServer('alttutorial:loginAttempt', name, password);
    });

    loginView.on('alttutorial:register', (name, password) => {
        alt.emitServer('alttutorial:registerAttempt', name, password);
    });
})

alt.onServer('alttutorial:loginSuccess', () => {
    alt.showCursor(false);
    alt.toggleGameControls(true);
    native.doScreenFadeIn(1000);
    if(loginView) loginView.destroy();
})

alt.onServer('alttutorial:loginError', (type, msg) => {
    if(loginView) loginView.emit('showError', type, msg);
})