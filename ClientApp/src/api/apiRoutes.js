export const getApiRoute = {
    SOLUTIONS: {
        SUBMIT: () => `solution/submit`,
    },

    SCORES: {
        GET_TOP: (countOfTopScores) => `score/top/${countOfTopScores}`,
    },
};
