import httpClient from './httpClient';

// Routed by Ocelot: /skills/** -> Portfolio.Skills.API (:5004)
const skillsService = {
  getAll: async () => {
    const { data } = await httpClient.get('/skills');
    return data;
  },

  getByCategory: async (category) => {
    const { data } = await httpClient.get('/skills', { params: { category } });
    return data;
  },
};

export default skillsService;
