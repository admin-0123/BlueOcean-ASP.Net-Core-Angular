import { createSelector } from '@ngrx/store';
import { AppStore } from '../app.store';

export const selectNumber = createSelector(
    (state: AppStore) => state.general.number,
    (number: number) => number
);

export const selectLocation = createSelector(
    (state: AppStore) => state.general.location,
    (location: { x: number, y: number}) => location
);

