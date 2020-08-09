import { api, getApiRoute } from '../../api';

export const requestPostCodeSubmission = (formState) => {
    const url = getApiRoute.SOLUTIONS.SUBMIT();
    return api.get(url, formState);
};
