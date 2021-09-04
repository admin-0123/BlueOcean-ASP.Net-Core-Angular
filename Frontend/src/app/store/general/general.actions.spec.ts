import * as fromGeneral from './general.actions';

describe('generalGenerals', () => {
    it('should return an action', () => {
        expect(fromGeneral.generalGenerals().type).toBe('[General] General Generals');
    });
});
