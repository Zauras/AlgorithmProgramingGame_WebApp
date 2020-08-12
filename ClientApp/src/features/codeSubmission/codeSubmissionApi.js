import { api, getApiRoute } from '../../api';

export const requestCodeTasks = () => {
    const url = getApiRoute.CODE_TASK.GET_ALL();
    return api.get(url);
};

export const requestPostCodeSubmission = (formState) => {
    const url = getApiRoute.SOLUTIONS.SUBMIT();
    return api.post(url, formState);
};
