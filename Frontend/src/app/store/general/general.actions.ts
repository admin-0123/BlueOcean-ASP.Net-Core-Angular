import {
    createAction,
    props
} from '@ngrx/store';

export const generalGenerals = createAction(
    '[General] General Generals'
);

export const setProductCardLocation = createAction(
    '[General] setProductCardLocation',
    props<{ location: { offsetLeft: number; offsetTop: number} }>()
);

export const setLoadingScreen = createAction(
    '[General] setLoadingScreen',
    props<{ loadingScreen: boolean }>()
);

export const toggleLoadingScreen = createAction('[General] toggleLoadingScreen');
