export const getApiRoute = {
    SOLUTIONS: {
        SUBMIT: () => `/submit-solution`,
    },

    SCORES: {
        GET_TOP: (countOfTopScores) => `scores/top/${countOfTopScores}`,
    },
};
