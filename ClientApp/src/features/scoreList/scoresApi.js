import { api, getApiRoute } from '../../api';

export const requestTopScores = (countOfTopScores) => {
    const url = getApiRoute.SCORES.GET_TOP(countOfTopScores);
    return api.get(url);
};
