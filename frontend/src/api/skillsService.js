import httpClient from './httpClient';

// Routed by Ocelot: /api/skills/** -> Portfolio.Skills.API (:5004)
const skillsService = {
  getAll: async () => {
    const { data } = await httpClient.get('/api/skills');
    return data;
  },

  getByCategory: async (category) => {
    const { data } = await httpClient.get('/api/skills', { params: { category } });
    return data;
  },
};

export default skillsService;
