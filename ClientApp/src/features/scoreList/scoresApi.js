import { api, getApiRoute } from '../../api';

export const requestTopScores = async (countOfTopScores) => {
    const url = getApiRoute.SCORES.GET_TOP(countOfTopScores);
    return await api.get(url);
};
