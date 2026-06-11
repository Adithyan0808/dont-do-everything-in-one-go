import axios from 'axios';
import toast from 'react-hot-toast';
import { getAccessToken } from '../features/auth/services/sessionService';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || '/api/v1';

function createCorrelationId() {
  if (globalThis.crypto?.randomUUID) {
    return globalThis.crypto.randomUUID();
  }

  return `${Date.now()}-${Math.random().toString(16).slice(2)}`;
}

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

apiClient.interceptors.request.use((config) => {
  const token = getAccessToken();

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  config.headers['X-Correlation-Id'] = createCorrelationId();
  config.headers['X-Client-Name'] = 'maverick-certification-hub-web';
  config.metadata = { startedAt: Date.now() };

  return config;
});

apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    const status = error?.response?.status;

    if (status === 401) {
      window.dispatchEvent(new CustomEvent('auth:session-expired'));
      toast.error('Session expired. Please sign in again.');
      window.location.assign('/login');
    } else if (status === 403) {
      window.location.assign('/unauthorized');
    } else if (status === 404) {
      toast.error('The requested resource was not found.');
    } else if (status >= 500 || !status) {
      toast.error('Service temporarily unavailable.');
    }

    return Promise.reject(error);
  },
);

export default apiClient;
