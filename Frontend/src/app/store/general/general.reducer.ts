/* eslint-disable ngrx/on-function-explicit-return-type */
import { createReducer, on } from '@ngrx/store';
import { GeneralState } from './general';
import { decrement, increment, reset } from './general.actions';

export const initialState: GeneralState = {
    number: 0
};

export const reducer = createReducer(
    initialState,
    on(increment, (state) => ({ ...state, number: state.number + 1 })),
    on(decrement, (state) => ({ ...state, number: state.number - 1 })),
    on(reset, (state) => ({ ...state, number: 0 }))
);

export function generalReducer(state: any, action: any) {
    return reducer(state, action);
}
