import { createSelector } from '@ngrx/store';
import { AppStore } from '../app.store';

export const selectNumber = createSelector(
    (state: AppStore) => state.general.number,
    (number: number) => number,
);
