export const getApiRoute = {
    CODE_TASK: {
        GET_ALL: () => `codeTask/all`,
    },
    SOLUTIONS: {
        SUBMIT: () => `solution/submit`,
    },

    SCORES: {
        GET_TOP: (countOfTopScores) => `score/top/${countOfTopScores}`,
    },
};
