import httpClient from './httpClient';

// Routed by Ocelot: /projects/** -> Portfolio.Projects.API (:5003)
const projectsService = {
  getAll: async () => {
    const { data } = await httpClient.get('/projects');
    return data;
  },

  getById: async (id) => {
    const { data } = await httpClient.get(`/projects/${id}`);
    return data;
  },

  create: async (payload) => {
    const { data } = await httpClient.post('/projects', payload);
    return data;
  },

  update: async (id, payload) => {
    const { data } = await httpClient.put(`/projects${id}`, payload);
    return data;
  },

  remove: async (id) => {
    await httpClient.delete(`/projects/${id}`);
  },
};

export default projectsService;
