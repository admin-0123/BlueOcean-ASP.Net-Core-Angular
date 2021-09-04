import { GeneralState } from './general/general';
import { generalReducer } from './general/general.reducer';

export interface AppStore {
    general: GeneralState;
}

export const Reducers = {
    general: generalReducer
};
