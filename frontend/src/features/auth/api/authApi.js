import apiClient from '../../../api/axios';

const unwrap = (response) => response.data?.data ?? response.data;

export const authFeatureApi = {
  loginUser: async (payload) => unwrap(await apiClient.post('/auth/login', payload)),
  registerUser: async (payload) => unwrap(await apiClient.post('/auth/register', payload)),
  getCurrentUser: async () => unwrap(await apiClient.get('/auth/me')),
  logoutUser: async () => unwrap(await apiClient.post('/auth/logout')),
  validateSession: async () => unwrap(await apiClient.get('/auth/me')),
};

export const loginUser = authFeatureApi.loginUser;
export const registerUser = authFeatureApi.registerUser;
export const getCurrentUser = authFeatureApi.getCurrentUser;
export const logoutUser = authFeatureApi.logoutUser;
export const validateSession = authFeatureApi.validateSession;
