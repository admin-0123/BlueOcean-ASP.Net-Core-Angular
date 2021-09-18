/* eslint-disable ngrx/on-function-explicit-return-type */
import {
    createReducer,
    on
} from '@ngrx/store';
import { GeneralState } from './general';
import {
    setLoadingScreen,
    toggleLoadingScreen,
    setProductCardLocation
} from './general.actions';

export const initialState: GeneralState = {
    loadingScreen: false,
    location: {
        offsetLeft: 0,
        offsetTop: 0
    }
};

export const reducer = createReducer(
    initialState,
    on(setProductCardLocation, (state, action) => ({ ...state, location: action.location })),
    on(setLoadingScreen, (state, action) => ({ ...state, loadingScreen: action.loadingScreen })),
    on(toggleLoadingScreen, (state) => ({ ...state, loadingScreen: !state.loadingScreen }))
);

export function generalReducer(state: any, action: any) {
    return reducer(state, action);
}
