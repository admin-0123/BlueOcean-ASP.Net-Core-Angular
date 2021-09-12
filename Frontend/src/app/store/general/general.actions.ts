import {
    createAction,
    props
} from '@ngrx/store';

export const generalGenerals = createAction(
    '[General] General Generals'
);

export const setProductCardLocation = createAction('[General] setProductCardLocation', props<{ location: { offsetLeft: number; offsetTop: number} }>());
export const decrement = createAction('[General] Decrement');
export const reset = createAction('[General] Reset');
