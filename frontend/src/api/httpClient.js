import axios from 'axios';

// Every request goes through the Ocelot Gateway (Portfolio.Gateway, :5000).
// Ocelot then re-routes internally to Auth / Profile / Projects / Skills / Contact.
// In dev, vite.config.js proxies "/api" to the gateway, so baseURL can be empty.
const baseURL = import.meta.env.VITE_API_BASE_URL || '';

const httpClient = axios.create({
  baseURL,
  headers: { 'Content-Type': 'application/json' },
  timeout: 10000,
});

// Attach the JWT (issued by Portfolio.Auth.API) to every outgoing request.
httpClient.interceptors.request.use((config) => {
  const token = localStorage.getItem('accessToken');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Central error handling + 401 refresh/logout hook.
httpClient.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');
      window.dispatchEvent(new CustomEvent('auth:unauthorized'));
    }
    return Promise.reject(normalizeError(error));
  }
);

function normalizeError(error) {
  const message =
    error.response?.data?.message ||
    error.response?.data?.title ||
    error.message ||
    'Something went wrong. Please try again.';
  return { message, status: error.response?.status, raw: error };
}

export default httpClient;
