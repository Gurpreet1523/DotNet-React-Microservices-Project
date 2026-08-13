import httpClient from './httpClient';

// Routed by Ocelot: /auth/** -> Portfolio.Auth.API (:5001)
const authService = {
  login: async (credentials) => {
    const { data } = await httpClient.post('/auth/login', credentials);
    localStorage.setItem('accessToken', data.accessToken);
    localStorage.setItem('refreshToken', data.refreshToken);
    return data;
  },

  register: async (payload) => {
    const { data } = await httpClient.post('/auth/register', payload);
    return data;
  },

  refresh: async () => {
    const refreshToken = localStorage.getItem('refreshToken');
    const { data } = await httpClient.post('/auth/refresh', { refreshToken });
    localStorage.setItem('accessToken', data.accessToken);
    return data;
  },

  logout: () => {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  },

  me: async () => {
    const { data } = await httpClient.get('/auth/me');
    return data;
  },
};

export default authService;
