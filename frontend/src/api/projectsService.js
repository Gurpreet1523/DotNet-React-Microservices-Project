import httpClient from './httpClient';

// Routed by Ocelot: /api/projects/** -> Portfolio.Projects.API (:5003)
const projectsService = {
  getAll: async () => {
    const { data } = await httpClient.get('/api/projects');
    return data;
  },

  getById: async (id) => {
    const { data } = await httpClient.get(`/api/projects/${id}`);
    return data;
  },

  create: async (payload) => {
    const { data } = await httpClient.post('/api/projects', payload);
    return data;
  },

  update: async (id, payload) => {
    const { data } = await httpClient.put(`/api/projects/${id}`, payload);
    return data;
  },

  remove: async (id) => {
    await httpClient.delete(`/api/projects/${id}`);
  },
};

export default projectsService;
