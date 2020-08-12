import axios from 'axios';
import { isEmpty } from 'lodash';

import urlConfig from './urlConfig';

const getCommonHeaders = () => {
    const authToken = window.sessionStorage.getItem('outer-aut');
    return {
        Authorization: Boolean(authToken) ? `Bearer ${authToken}` : '',
        'Content-Type': 'application/json',
    };
};

const axiosInstance = axios.create({
    baseURL: `${urlConfig[window.location.host]}`,
    headers: getCommonHeaders(),
    // transformResponse: [(data) => JSON.parse(data)],
});

axiosInstance.interceptors.request.use((request) => {
    const auth = request.headers.Authorization;
    if (!Boolean(auth) || isEmpty(auth)) {
        request.headers = getCommonHeaders();
    }
    return request;
});

axiosInstance.interceptors.response.use((response) => response.data);

const errorHandler = async (request) => {
    try {
        return await request();
    } catch (error) {
        if (error.response) {
            throw error.response;
        } else if (error.request) {
            throw error.request;
        } else {
            throw error;
        }
    }
};

export const api = {
    get: (...params) => errorHandler(() => axiosInstance.get(...params)),
    post: (...params) => errorHandler(() => axiosInstance.post(...params)),
    put: (...params) => errorHandler(() => axiosInstance.put(...params)),
    patch: (...params) => errorHandler(() => axiosInstance.patch(...params)),
    delete: (...params) => errorHandler(() => axiosInstance.delete(...params)),
};
