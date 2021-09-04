import { createAction } from '@ngrx/store';

export const generalGenerals = createAction(
    '[General] General Generals'
);

export const increment = createAction('[General] Increment');
export const decrement = createAction('[General] Decrement');
export const reset = createAction('[General] Reset');
